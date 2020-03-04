using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIVALE.BLL.Services
{
    public class HistoryServices : UnitOfWork, IHistoryServices
    {
        private IRepository<History> historyRepository;
        public IRepository<History> HistoryRepository
        {
            get
            {
                if (historyRepository == null)
                {
                    historyRepository = GetRepository<History>();
                }
                return historyRepository;
            }
        }

        public void DeleteHistory(int HistoryID)
        {
            throw new NotImplementedException();
        }

        public HistoryDTO GetHistoryByID(int HistoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HistoryDTO> GetHistorys()
        {
            var objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();
            var target = HistoryRepository.Get();
            var result = objHistoryMapper.MapList(target);
            return result;
        }

        public IEnumerable<HistoryDTO> GetHistoriesByConditions(UserDTO user, UserDTO parentUser,
            string date, string startTime, string endTime, string smartGatewayId, string category)
        {
            if (user == null || String.IsNullOrEmpty(date)
                || String.IsNullOrEmpty(startTime) || String.IsNullOrEmpty(endTime)
                || String.IsNullOrEmpty(smartGatewayId))
            {
                return null; // Bad Request
            }

            var startInt = this.ConvStdTimeToSec(startTime);
            var endInt = this.ConvStdTimeToSec(endTime);

            if (startInt == -1 || endInt == -1)
            {
                return null; // Bad Request
            }


            var userNodePermissions = user.NodePermissions.ToList().FindAll(obj => obj.PERMISSION_OWNER_TYPE == 1);
            // Get parent's permissions
            if (parentUser != null)
            {
                var parentNodePermission = parentUser.NodePermissions.ToList().FindAll(p => p.PERMISSION_OWNER_TYPE == 2);
                userNodePermissions.AddRange(parentNodePermission);
            }

            if (userNodePermissions.Count == 0)
            {
                return null; // User has no permission
            }

            var target = HistoryRepository.Get(obj =>
                obj.USER_ID == user.Id && // User ID
                obj.SMART_GATEWAY_ID.ToString().Equals(smartGatewayId) && // Smart Gateway
                obj.DATE.Equals(date) && // Date 
                (String.IsNullOrEmpty(category) ? true : obj.CATEGORY.ToString().Equals(category))
            );
            var filterTarget = new List<History>();
            foreach (History hist in target)
            {
                // Check Time 
                var timeInt = this.ConvStdTimeToSec(hist.TIME_OF_DATE_TIME);
                if (timeInt > endInt || timeInt < startInt || timeInt == -1)
                {
                    continue;
                }

                // Check Node Paths
                if (this.CheckUserHasPermission(userNodePermissions, hist.PERSON_NODE_PATH)
                    && this.CheckUserHasPermission(userNodePermissions, hist.CREDENTIAL_NODE_PATH)
                    && this.CheckUserHasPermission(userNodePermissions, hist.EQUIPMENT_POINT_NODE_PATH)
                    && this.CheckUserHasPermission(userNodePermissions, hist.USER_OPERATION_NODE_PATH))
                {
                    filterTarget.Add(hist);
                }
            }

            var objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();
            var result = objHistoryMapper.MapList(filterTarget);
            return result;
        }

        public void InsertHistory(HistoryDTO History)
        {
            HistoryMapper obj = DependencyInjector.Retrieve<HistoryMapper>();
            var targetEntity = obj.Map(History);
            var targetDTO = obj.Map(targetEntity);
            HistoryRepository.Insert(targetEntity);
            Save();
        }

        public void UpdateHistory(HistoryDTO History)
        {
            throw new NotImplementedException();
        }

        #region Private Methods

        /// <summary>
        /// Convert standard time hh:mm:ss to seconds
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvStdTimeToSec(string time)
        {
            if (String.IsNullOrEmpty(time))
            {
                return -1;
            }
            var timeParams = time.Split(':');
            if (timeParams.Length != 3)
            {
                return -1;
            }
            return int.Parse(timeParams[0]) * 3600 + int.Parse(timeParams[1]) * 60 + int.Parse(timeParams[2]);
        }

        /// <summary>
        /// Check whether user has a specific permission
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="requiredPermission"></param>
        /// <returns></returns>
        private bool CheckUserHasPermission(ICollection<NodePermissionDTO> userPermissions, string requiredPermission)
        {
            foreach (NodePermissionDTO item in userPermissions)
            {
                if (item.NODE_PATH.StartsWith(requiredPermission))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

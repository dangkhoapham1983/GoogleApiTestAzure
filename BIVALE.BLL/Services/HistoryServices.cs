using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
using System;
using System.Collections.Generic;

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

        public IEnumerable<HistoryDTO> GetHistoryByConditions()
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

        public IEnumerable<HistoryDTO> GetHistoriesByConditions(UserDTO user, ICollection<NodePermissionDTO> userNodePermissions,
            string date, string startTime, string endTime, string smartGatewayId, string category)
        {
            // Bad Request
            if (user == null || userNodePermissions == null || String.IsNullOrEmpty(date)
                || String.IsNullOrEmpty(startTime) || String.IsNullOrEmpty(endTime)
                || String.IsNullOrEmpty(smartGatewayId))
            {
                return null;
            }

            var objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();

            var test = HistoryRepository.Get();
            var target = HistoryRepository.Get(obj =>
                obj.USER_ID == user.Id && // User ID
                obj.SMART_GATEWAY_ID.ToString().Equals(smartGatewayId) && // Smart Gateway
                obj.DATE.Equals(date)  // Date 
                //(String.IsNullOrEmpty(category) ? true : obj.CATEGORY.ToString().Equals(category))
            );
            var filterTarget = new List<History>();
            foreach (History hist in target)
            {
                // Check Time
                var timeInt = this.ConvStdTimeToSec(hist.TIME_OF_DATE_TIME);
                var startInt = this.ConvStdTimeToSec(startTime);
                var endInt = this.ConvStdTimeToSec(endTime);
                if (timeInt > endInt || timeInt < startInt)
                {
                    continue;
                }

                // Check Node Paths
                if (this.CheckNodePermissionInUserPermissions(userNodePermissions, hist.PERSON_NODE_PATH)
                    && this.CheckNodePermissionInUserPermissions(userNodePermissions, hist.CREDENTIAL_NODE_PATH)
                    && this.CheckNodePermissionInUserPermissions(userNodePermissions, hist.EQUIPMENT_POINT_NODE_PATH)
                    && this.CheckNodePermissionInUserPermissions(userNodePermissions, hist.USER_OPERATION_NODE_PATH))
                {
                    filterTarget.Add(hist);
                }
            }

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

        private int ConvStdTimeToSec(string time)
        {
            var chars = time.Split(':');
            return int.Parse(chars[0]) * 3600 + int.Parse(chars[1]) * 60 + int.Parse(chars[2]);
        }

        private bool CheckNodePermissionInUserPermissions(ICollection<NodePermissionDTO> userNodePermissions, string nodePath)
        {
            foreach (NodePermissionDTO item in userNodePermissions)
            {
                if (item.PERMISSION_OWNER_TYPE == 1 && item.NODE_PATH.StartsWith(nodePath))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using BIVALE.BLL.Enum;
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
        private INodePermissionService nodePermissionService;

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
        public INodePermissionService NodePermissionService
        {
            get
            {
                if (nodePermissionService == null)
                {
                    nodePermissionService = DependencyInjector.Retrieve<INodePermissionService>();
                }
                return nodePermissionService;
            }
        }

        public IEnumerable<HistoryDTO> GetHistories()
        {
            var objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();
            var target = HistoryRepository.Get();
            var result = objHistoryMapper.MapList(target);
            return result;
        }

        public HistoryResultDTO GetHistoriesByConditions(UserDTO user, string startDate,
            string endDate, string startTime, string endTime, string smartGatewayId, string category)
        {
            if (user == null || String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)
                || String.IsNullOrEmpty(startTime) || String.IsNullOrEmpty(endTime)
                || String.IsNullOrEmpty(smartGatewayId))
            {
                return null; // Bad Request
            }

            var startDateTime = this.ConvStdDateToDateTime(startDate, startTime);
            var endDateTime = this.ConvStdDateToDateTime(endDate, endTime);

            if (startDateTime == null || endDateTime == null)
            {
                return null; // Bad Request
            }

            List<String> userNodePermissions = user.NodePermissions.ToList().FindAll(p => p.PERMISSION_OWNER_TYPE == (int)PermissionOwnerType.USER).Select(p => p.NODE_PATH).ToList();

            // Get parent's permissions
            if (user.PARENT_ID != null)
            {
                var parentNodePermission = NodePermissionService.GetNodePermissionsByUser(user.PARENT_ID.Value, PermissionOwnerType.USER_GR).Select(p => p.NODE_PATH);
                userNodePermissions.AddRange(parentNodePermission);
                userNodePermissions = userNodePermissions.Distinct().ToList();
            }

            if (userNodePermissions.Count == 0)
            {
                return null; // User has no permission
            }

            IEnumerable<History> target = HistoryRepository.Get(obj =>
                obj.USER_ID == user.Id && // User ID
                obj.SMART_GATEWAY_ID.ToString().Equals(smartGatewayId) && // Smart Gateway
                (String.IsNullOrEmpty(category) ? true : obj.CATEGORY.ToString().Equals(category)) // Category (optional)

                //// Time
                //&& DateTime.Compare(this.ConvStdDateToDateTime(obj.DATE, obj.TIME_OF_DATE_TIME).Value, startDateTime.Value) >= 0
                //&& DateTime.Compare(this.ConvStdDateToDateTime(obj.DATE, obj.TIME_OF_DATE_TIME).Value, endDateTime.Value) <= 0

                //// Node Permissions
                //&& this.CheckUserHasPermission(userNodePermissions, obj.PERSON_NODE_PATH)
                //&& this.CheckUserHasPermission(userNodePermissions, obj.CREDENTIAL_NODE_PATH)
                //&& this.CheckUserHasPermission(userNodePermissions, obj.EQUIPMENT_POINT_NODE_PATH)
                //&& this.CheckUserHasPermission(userNodePermissions, obj.USER_OPERATION_NODE_PATH)

                );

            List<History> filterTarget = new List<History>();
            foreach (History hist in target)
            {
                // Check Time 
                var datetime = this.ConvStdDateToDateTime(hist.DATE, hist.TIME_OF_DATE_TIME);
                if (datetime == null || DateTime.Compare(datetime.Value, startDateTime.Value) < 0 || DateTime.Compare(datetime.Value, endDateTime.Value) > 0)
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

            HistoryMapper objHistoryMapper = DependencyInjector.Retrieve<HistoryMapper>();
            IList<HistoryDTO> historyDTOs = objHistoryMapper.MapList(filterTarget);
            HistoryResultDTO result = new HistoryResultDTO()
            {
                smart_gateway_id = int.Parse(smartGatewayId),
                dates = new List<DateResultDTO>()
            };
            Dictionary<string, ICollection<TimeResultDTO>> dateDict = new Dictionary<string, ICollection<TimeResultDTO>>();

            foreach (var obj in historyDTOs)
            {
                if (!dateDict.ContainsKey(obj.DATE))
                {
                    dateDict.Add(obj.DATE, new List<TimeResultDTO>());
                }
                dateDict[obj.DATE].Add(new TimeResultDTO()
                {
                    card_face_no = obj.CARD_FACE_NO,
                    category = obj.CATEGORY,
                    equipment_name1 = obj.EQUIPMENT_NAME1,
                    equipment_name2 = obj.EQUIPMENT_NAME2,
                    event_message = obj.EVENT_MESSAGE,
                    event_text = obj.EVENT_TEXT,
                    management_code = obj.MANAGEMENT_CODE,
                    person_name = obj.PERSON_NAME,
                    site_name = obj.SITE_NAME,
                    time_of_date_time = obj.TIME_OF_DATE_TIME
                });
            }

            foreach (var obj in dateDict)
            {
                result.dates.Add(new DateResultDTO()
                {
                    date = obj.Key,
                    times = obj.Value
                });
            }

            return result;
        }

        #region Private Methods

        /// <summary>
        /// Convert datetime string to datetime object
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime? ConvStdDateToDateTime(string date, string time)
        {
            if (String.IsNullOrEmpty(date) || String.IsNullOrEmpty(time))
            {
                return null;
            }

            var dateParams = date.Split('/');
            var timeParams = time.Split(':');

            if (dateParams.Length != 3 || timeParams.Length != 3)
            {
                return null;
            }

            // Date
            var yearInt = int.Parse(dateParams[0]);
            var monthInt = int.Parse(dateParams[1]);
            var dayInt = int.Parse(dateParams[2]);

            // Time
            var hourInt = int.Parse(timeParams[0]);
            var minuteInt = int.Parse(timeParams[1]);
            var secondInt = int.Parse(timeParams[2]);

            var dateObj = new DateTime(yearInt, monthInt, dayInt, hourInt, minuteInt, secondInt);
            return dateObj;
        }

        /// <summary>
        /// Check whether user has a specific permission
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="requiredPermission"></param>
        /// <returns></returns>
        private bool CheckUserHasPermission(ICollection<string> userPermissions, string requiredPermission)
        {
            foreach (var item in userPermissions)
            {
                if (item.StartsWith(requiredPermission))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;

namespace BIVALE.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string LOGIN_NAME { get; set; }
        public int USER_TYPE { get; set; }
        public string PASSWORD1 { get; set; }
        public string PASSWORD2 { get; set; }
        public string AUTHENTICATION_CODE { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public string NAME { get; set; }
        public string POSITION { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> EDIT_FORBIDDEN { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public Nullable<int> PASSWORD_EXPIRATION_TERM { get; set; }
        public Nullable<int> PASSWORD_UPDATE_NOTIFICATION { get; set; }
        public Nullable<System.DateTime> PASSWORD_UPDATE_DATE { get; set; }
        public Nullable<int> HISTORY_DOWNLOAD_NOTIFICATION { get; set; }
        public Nullable<System.DateTime> LAST_DOWNLOAD_DATE { get; set; }
        public Nullable<int> HISTORY_DOWNLOAD_NOTIFI_FLG { get; set; }
        public Nullable<int> ANNOUNCEMENT_MAIL_RECEIVE { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRATION_DATE { get; set; }
        public string COLUMN1 { get; set; }
        public string COLUMN2 { get; set; }
        public string NODE_PATH { get; set; }
        public Nullable<int> NOTIFICATION_INTERVAL { get; set; }
        public Nullable<int> NOTIFICATION_DATE_COUNT { get; set; }
        public Nullable<int> BUILDING_MANAGEMENT_SERVICE { get; set; }
        public Nullable<int> BUILDING_MANAGEMENT_USER_TYPE { get; set; }
        public Nullable<int> ANNOUNCEMENT_NOTIFICATION { get; set; }
        public ICollection<NodePermissionDTO> NodePermissions { get; set; }
    }
}

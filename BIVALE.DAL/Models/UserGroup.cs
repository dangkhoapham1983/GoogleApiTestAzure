//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIVALE.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserGroup
    {
        public int Id { get; set; }
        public string NAME { get; set; }
        public string HOMEPAGE_URL { get; set; }
        public Nullable<int> DEFAULT_PASSWORD_VALID_TERM { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public int HIERARCHY_DEPTH { get; set; }
        public string NODE_PATH { get; set; }
        public string USER_GR_CODE { get; set; }
        public Nullable<int> USERS_GR_TYPE { get; set; }
        public Nullable<int> MAX_USER_COUNT { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIVALE.Simulator.Models
{
	public class UserProfile
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string GoogleID { get; set; }
		public string ProfileURL { get; set; }
		public string AccessToken { get; set; }
		public string Scope { get; set; }
		public string TokenType { get; set; }
		public int ExpiredIn { get; set; }
		public long ExpiredAt { get; set; }
		public DateTime ExpiredTime { get; set; }

	}
}
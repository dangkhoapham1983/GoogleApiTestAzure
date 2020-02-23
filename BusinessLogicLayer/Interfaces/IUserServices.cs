using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
	public interface IUserServices
	{
		IEnumerable<User> GetUsers();
		User GetUserByID(int userId);
		void InsertUser(User user);
		void DeleteUser(int userID);
		void UpdateUser(User user);
		void Save();
	}
}

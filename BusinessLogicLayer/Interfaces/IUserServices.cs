using DataTransferObjectLayer;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
	public interface IUserServices
	{
		IEnumerable<UserDTO> GetUsers();
		UserDTO GetUserByID(int userId);
		void InsertUser(UserDTO user);
		void DeleteUser(int userID);
		void UpdateUser(UserDTO user);
		void Save();
	}
}

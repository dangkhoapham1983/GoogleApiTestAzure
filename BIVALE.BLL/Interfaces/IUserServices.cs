﻿using BIVALE.DTO;
using System.Collections.Generic;

namespace BIVALE.BLL.Interfaces
{
	public interface IUserServices
	{
		IEnumerable<UserDTO> GetUsers();
		UserDTO GetUserByID(int userId);
		UserDTO GetUserByEmailAddress(string email);
		void InsertUser(UserDTO user);
		void DeleteUser(int userID);
		void UpdateUser(UserDTO user);
		void Save();
	}
}

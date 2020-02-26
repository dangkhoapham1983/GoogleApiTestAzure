﻿using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using System;
using System.Collections.Generic;

namespace BIVALE.BLL.Services
{
	public class UserServices : UnitOfWork, IUserServices
	{
		private IRepository<User> userRepository;
		public IRepository<User> UserRepository
		{
			get
			{
				if (userRepository == null)
				{
					userRepository = GetRepository<User>();
				}
				return userRepository;
			}
		}

		public void DeleteUser(int userID)
		{
			throw new NotImplementedException();
		}

		public UserDTO GetUserByID(int userId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<UserDTO> GetUsers()
		{
			var objUserMapper = DependencyInjector.Retrieve<UserMapper>();
			var target = UserRepository.Get();
			var result = objUserMapper.MapList(target);
			return result;
		}

		public void InsertUser(UserDTO user)
		{
			UserMapper obj = DependencyInjector.Retrieve<UserMapper>();
			var targetEntity = obj.Map(user);
			var targetDTO = obj.Map(targetEntity);
			UserRepository.Insert(targetEntity);
			Save();
		}

		public void UpdateUser(UserDTO user)
		{
			throw new NotImplementedException();
		}
	}
}
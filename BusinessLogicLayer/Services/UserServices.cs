using BusinessLogicLayer.Generic;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mapping;
using DataAccessLayer.Models;
using DataTransferObjectLayer;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
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
			UserMapper objUserMapper = DependencyInjector.Retrieve<UserMapper>();
			IEnumerable<User> target = UserRepository.Get();
			List<UserDTO> result = new List<UserDTO>();
			foreach (var item in target)
			{
				result.Add(objUserMapper.Map(item));
			}
			
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

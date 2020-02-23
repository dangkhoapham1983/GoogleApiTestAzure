using BusinessLogicLayer.Generic;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
	public class UserServices : UnitOfWork, IUserServices
	{
		private IRepository<User> userRepository;
		public IRepository<User> UserRepository
		{
			get
			{
				if (this.userRepository == null)
				{
					this.userRepository = GetRepository<User>();
				}
				return userRepository;
			}
		}

		public void DeleteUser(int userID)
		{
			throw new NotImplementedException();
		}

		public User GetUserByID(int userId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<User> GetUsers()
		{
			throw new NotImplementedException();
		}

		public void InsertUser(User user)
		{
			UserRepository.Insert(user);
			Save();
		}

		public void UpdateUser(User user)
		{
			throw new NotImplementedException();
		}
	}
}

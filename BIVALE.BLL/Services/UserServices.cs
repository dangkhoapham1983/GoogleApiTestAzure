using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;
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
        public UserDTO GetUserByEmail(string userEmail)
        {
            var objUserMapper = DependencyInjector.Retrieve<UserMapper>();
            var target = UserRepository.Get(x => x.MAIL_ADDRESS.Equals(userEmail));
            var result = objUserMapper.MapList(target);

            using (var enumerator = result.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }
            return null;
        }

		public UserDTO GetUserByEmailAddress(string email)
		{
			var objUserMapper = DependencyInjector.Retrieve<UserMapper>();
			var targetEntity = UserRepository.FirstOrDefault(p => p.MAIL_ADDRESS == email);
			var targetDTO = objUserMapper.Map(targetEntity.Result);
			return targetDTO;
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

using BIVALE.BLL.Generic;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Mapping;
using BIVALE.DAL.Models;
using BIVALE.DTO;
using BIVALE.Extensions.Services;

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

        public UserDTO GetUserByID(int userId)
        {
            var objUserMapper = DependencyInjector.Retrieve<UserMapper>();
            var targetEntity = UserRepository.FirstOrDefault(p => p.Id == userId);
            var targetDTO = objUserMapper.Map(targetEntity.Result);
            return targetDTO;
        }

        public UserDTO GetUserByEmailAddress(string email)
        {
            var objUserMapper = DependencyInjector.Retrieve<UserMapper>();
            var targetEntity = UserRepository.FirstOrDefault(p => p.MAIL_ADDRESS == email);
            var targetDTO = objUserMapper.Map(targetEntity.Result);
            return targetDTO;
        }
    }
}

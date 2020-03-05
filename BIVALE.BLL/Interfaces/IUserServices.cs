using BIVALE.DTO;

namespace BIVALE.BLL.Interfaces
{
    public interface IUserServices
	{
		UserDTO GetUserByID(int userId);
		UserDTO GetUserByEmailAddress(string email);
	}
}

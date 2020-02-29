using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BIVALE.GoogleClient.Extend.GoogleExtension;

namespace BIVALE.GoogleClient.Interfaces
{
	public interface IGoogleServices
	{
		UserGoogle ValidateByWeb();

		UserGoogle ValidateByProcess();

		Task<UserGoogle> CodeValidate(string code);

		Task<User> GetUserInfo(Token token);
	}
}

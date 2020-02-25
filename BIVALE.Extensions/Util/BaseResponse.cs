using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.Extensions.Util
{
	public class BaseResponse
	{
		public Guid ResponseId { get; private set; }
		public HttpResponseMessage Response { get; set; }
		public string ResponseBody { get; set; }
		public IDictionary<string, string> ResponseHeaders { get; set; }

		public BaseResponse()
		{
			ResponseId = Guid.NewGuid();
		}
	}
}

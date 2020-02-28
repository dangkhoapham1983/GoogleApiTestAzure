﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.GoogleClient
{
    public class UserGoogle
    {
		public string Email { get; set; }
		public string IdToken { get; set; }
		public string AccessToken { get; set; }
		public long? ExpiresInSeconds { get; set; }
		public DateTime Issued { get; set; }
		public DateTime IssuedUtc { get; set; }
		public string RefreshToken { get; set; }
		public string Scope { get; set; }
		public string TokenType { get; set; }
		public string Code { get; set; }

    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner
{
    public class Settings
    {

		public static IConfiguration _config;

		public static string GetOmniDbConfigParameter(string param)
		{
			return _config.GetSection("OmniDb").GetSection(param).Value;
		}

		public static string GetPosDbConfigParameter(string param)
		{
			return _config.GetSection("PosCSDb").GetSection(param).Value;
		}

		public string ClientId { get; set; }
	}
}

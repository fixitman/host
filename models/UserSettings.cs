using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace host
{
    public class UserSettings
    {
        public static string FILENAME = @"fixitman\host\UserSettings.json";

        // public required string UserName;
        // public required string Password;
        public string? token;
        public string? expiration;


    }
}
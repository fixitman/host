using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace host
{
    public class UserSettings
    {
        public static readonly string FILENAME = @"Fixitman\Host\UserSettings.json";
        public string token = "";
        public string expiration = DateTime.Now.ToString();
    }
}
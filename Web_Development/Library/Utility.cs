using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public static partial class Utility
    {
        public static int Get_UTC_NOW_UNIX_TIMESTAMP()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        public static int Get_LOCAL_NOW_UNIX_TIMESTAMP()
        {
            return (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string Get_Directory()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            if (String.IsNullOrEmpty(dir))
            {
                return Directory.GetCurrentDirectory();
            }
            return dir;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService
{
    public static class Consts
    {
        public static readonly int MAX_TRIES = 3;
        public static readonly int MAX_SIZE = 10; 
        public static readonly string CONNECTION_STRING = "Data Source=sql5031.smarterasp.net;Initial Catalog=DB_A11131_shallvatest;User Id=DB_A11131_shallvatest_admin;Password=akO10614@";
        public static readonly string LOGO_PATH = @"C:\Projects\Services\ShallvaMailService\src\ShallvaMailService\Images\logo-shallva.png";
    }
}

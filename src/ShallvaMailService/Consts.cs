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
        public static readonly string CONNECTION_STRING = "Data Source=SQL5029.SmarterASP.NET;Initial Catalog=DB_A1A527_shallva;User Id=DB_A1A527_shallva_admin;Password=lTaO456852#;";
        public static readonly string LOGO_PATH = @"C:\Projects\Services\ShallvaMailService\src\ShallvaMailService\Images\logo-shallva.png";
    }
}

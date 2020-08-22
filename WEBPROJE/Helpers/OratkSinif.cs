using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPROJE.Models;


namespace WEBPROJE.Helpers
{
    public class OratkSinif
    {
        WebDB db = new WebDB();
        public static bool EditIzinYetkiVarmi(int id,Kullanici user)
        {
            if (user.id == id)
            {
                return true;
            }
            else if (user.YetkiId > 2)
            {
                return true;
            }
            return false;
        }

        public static bool DeleteIzinYetkiVarmi(int id, Kullanici user)
        {
            if (user.id == id)
            {
                return true;
            }
            else if (user.YetkiId > 3)
            {
                return true;
            }
            return false;
        }
    }
}
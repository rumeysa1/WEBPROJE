using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPROJE.Models;

namespace WEBPROJE.ViewModels
{
    public class SonAtilanMakaleViewModel
    {
        public Makale Makalem { get; set; }
        public List<Makale> SonMakaleler { get; set; }
    }
}
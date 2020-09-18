using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace kodtest_CGI.Models
{
    public class BusinesscardViewModel
    {
        public int Id { get; set; }
        [DisplayName("Förnamn")]
        public string Name { get; set; }
        [DisplayName("Efternamn")]
        public string SurName { get; set; }
        [DisplayName("Telefon")]
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
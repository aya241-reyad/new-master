using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.Models
{
    public class Messages
    {
        public string Key { get; set; }
        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Display(Name = "رقم الهاتف")]
        public string Tele { get; set; }

        [Display(Name = "الرساله")]
        public string Message { get; set; }


        [Display(Name = "تاريخ الارسال")]
        public DateTime EnterDate { get; set; }
    }
}

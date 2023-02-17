using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.Models
{
    public class RealState
    {
        public string Key { get; set; }

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "العنوان")]
        public string Title { get; set; }


        [Display(Name = "الوصف")]
        public string Discription { get; set; }



        [Display(Name = "الصوره")]
        public string ImgName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.Models
{
    public class Patient
    {
        public string Key { get; set; }

        [Required(ErrorMessage ="ادخل هذا الحقل")]
        [Display(Name ="Full Name")]
        public string Name { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Reg. date")]
        public DateTime ExamDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Report Date")]
        public DateTime ResultDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Result")]
        public string Result { get; set; }

        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Reference Range")]
        public string ReferenceRange { get; set; }




        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }


        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "ادخل هذا الحقل")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        public string QRLocation { get; set; }

        public Byte[] QR { get; set; }


    }
}

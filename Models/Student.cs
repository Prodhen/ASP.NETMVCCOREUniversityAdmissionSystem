using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1262228_Arosh.Models
{
    public class Student
    {
      
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "You Must be Give Minimum 4 and Maximum 20 Chrecter")]
        public string Name { get; set; }
        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mothers's Name")]
        public string MotherName { get; set; }
        public string Gender { get; set; }
   
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime DOB { get; set; }
        [Display(Name = "Birth ID")]
        public string BirthID { get; set; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        public string Unit { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Address")]
    
        public string Email { get; set; }

        public string Mobile { get; set; }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Mobile")]
        [Display(Name = "Confirm Number")]
        public string ConfirmMobile { get; set; }
        public bool? Status { get; set; } = false;
     

        public string Picture { get; set; }

        [NotMapped]
        public IFormFile UploadImage { get; set; }

        public virtual List<AcademicResult> AcademicResults { get; set; } = new List<AcademicResult>();
    }

    public enum Gender
    {
        Male,
        Female



    }
}

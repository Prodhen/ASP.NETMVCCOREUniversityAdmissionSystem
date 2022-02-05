using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1262228_Arosh.Models
{
    public class AcademicResult
    {
        [Key]
        public int AcademicResultID { get; set; }
        public string RollNo { get; set; }
        public string Examination { get; set; }
        public decimal Result { get; set; }

        public string Group { get; set; }
        public string PassingYear { get; set; }
        //[ForeignKey("Student")]
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}

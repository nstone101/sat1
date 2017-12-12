using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SAT1//.Models
{
    public class ScheduledClassMetaData
    {
        [Display(Name = "Course ID")]
        [Required(ErrorMessage= "**** Required ***")]
        public int courseId { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString="{0:d}", ApplyFormatInEditMode=true)]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode=true)]
        public DateTime endDate { get; set; }

        [Display(Name = "Credit Hours")]
        public float creditHours { get; set; }

        [Display(Name = "Instructor")]
        [StringLength(75, ErrorMessage="Too many characters...75 max")]
        public string instructorName { get; set; }

        [Display(Name = "Location")]
        [StringLength(50, ErrorMessage = "Too many characters...50 chars max")]
        public string location { get; set; }

        [Display(Name="Status ID")]
        [Required(ErrorMessage="***Required***")]
        public int statusId { get; set; }


    }
    [MetadataType(typeof(ScheduledClassMetaData))]
    public partial class SATScheduledClass
    {
        public string Display
        {
            get 
            {
                return string.Format("{0:d} {1}", startDate, SATCours.courseName); 
            }
        }
    }

}


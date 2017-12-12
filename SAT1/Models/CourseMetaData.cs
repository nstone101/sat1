using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace SAT1//.Models
{
    public class CourseMetadata
    {
        [Required(ErrorMessage="** Course name is required!")]
        [Display(Name="Course Name:")]
        [StringLength(150,ErrorMessage="**Must be less than 150 characters")]
        public string courseName { get; set; }

        [UIHint("MultilineText")]
        [Display(Name="Description:")]
        public string courseDescription { get; set; }

        [Display(Name = "Department:")]
        public string department { get; set; }

        [Display(Name = "Curriculum:")]
        public string curriculum { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Notes:")]
        public string notes { get; set; }

        [Required(ErrorMessage="Status is required!")]
        [Display(Name = "Status:")]
        public int SATCourseStatus { get; set; }
        
        [Required(ErrorMessage = "Status is required!")]
        [Display(Name = "Status:")]
        public int statusId { get; set; }

}

         [MetadataType(typeof(CourseMetadata))]
         public partial class SATCourse
         { 

         }
}
    
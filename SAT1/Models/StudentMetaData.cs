using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace SAT1//.Models

{
    
    public class StudentMetaData
    
    {
        [Display(Name= "First Name")]
        [Required(ErrorMessage="** Please enter your first name")]
        public string firstName {get;set;}

        [Display(Name = "Last Name")]
        [Required]
        public string lastName { get; set; }


        [Required(ErrorMessage="**Required Field")]
        public int statusId { get; set; }
    
    }

    [MetadataType(typeof(StudentMetaData))]
    public partial class SATStudent
    {
        public string Display
        {
            get
            {
                return string.Format("{0}, {1} ID: {2}", lastName, firstName, studentId); 
            }
        }

    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //metadata


namespace SAT1.Models
{
    public class ContactInfo
    {
        [Required(ErrorMessage = "* Name is required.")]
        [Display(Name = "Your Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Email is required")]
        [RegularExpression("^[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}$",
            ErrorMessage = "Valid Email Address required.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Display(Name = "Comments: ")]
        [Required(ErrorMessage =
            "* Comments are required to send this mail message.")]
        [UIHint("MultilineText")]
        public string Comments { get; set; }

        [Display(Name = "I would please like a response by this time: ")]
        [DisplayFormat(DataFormatString = "{0:d}")]

        //nullable so that it doesnt get "auto" validation
        public DateTime? ResponseRequestedBy { get; set; }

    }
}
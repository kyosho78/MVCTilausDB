using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTilausDB.ViewModels
{
    public class LoginTilausDB
    {
        public int LoginId { get; set; }

        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]

        [Required(ErrorMessage = "Anna käyttäjätunnus")]
        //[StringLength(50, MinimumLength = 3)]
        [Display(Name = "Käyttäjätunnus")]
        public string UserName { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]

        [Display(Name = "Salasana")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana")]
        public string PassWord { get; set; }

        public string LoginErrorMessage { get; set; }

        public Nullable<System.DateTime> RegisterationDate { get; set; }


    }
}
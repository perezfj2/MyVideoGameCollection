using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace MyVideoGameUI.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential LoginInfo { get; set; }

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger= logger;
        }
        public void OnGet()
        {

        }

        public void OnPost() 
        { 

            if(ModelState.IsValid)
            {
                //verify user credentials
                if (LoginInfo.Email == "admin@mysite.com" && LoginInfo.Password == "Password")
                {
                    //credentials have been verified

                    //create security context
                }
            }
            

            //else: invalid username password


        }

        public class Credential
        {
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }


    }
}

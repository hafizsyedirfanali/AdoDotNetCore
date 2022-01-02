using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestApp.Pages
{
    public class UpdateUserModel : PageModel
    {
        [Required][BindProperty]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [BindProperty]
        public string Mobile { get; set; }
        private readonly string constr;
        public UpdateUserModel(IConfiguration config)
        {
            constr = config.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            var command = $"Update PersonalDetails Set Name = '{Name}', Mobile = '{Mobile}' where Id=2";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(command, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

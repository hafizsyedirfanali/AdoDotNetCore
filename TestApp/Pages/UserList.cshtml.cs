using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TestApp.Data;

namespace TestApp.Pages
{
    public class UserListModel : PageModel
    {
        [BindProperty]
        public List<PersonalDetails> PersonalDetails { get; set; }
        private readonly string constr;
        //[BindProperty]
        //public string Name { get; set; }
        //[BindProperty]
        //public string Mobile { get; set; }
        
        public UserListModel(IConfiguration config)
        {
            constr = config.GetConnectionString("DefaultConnection");
            PersonalDetails = new List<PersonalDetails>();
        }
        public void OnGet()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //var command = $"Update PersonalDetails Set Name = '{Name}', Mobile = '{Mobile}' where Id=1";
                var command = $"Select * from PersonalDetails";
                using(SqlCommand cmd = new SqlCommand(command, con))
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonalDetails.Add(new Data.PersonalDetails
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Mobile = reader["Mobile"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
        }
        public void OnPost(int Id)
        {

        }
    }
}

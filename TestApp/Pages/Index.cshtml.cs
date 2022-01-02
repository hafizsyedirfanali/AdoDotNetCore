using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration config;
        [Required]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Mobile { get; set; }
        
        public List<TestClass> TestClasses { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            Name = Mobile = string.Empty;
            TestClasses = new List<TestClass>();
            _logger = logger;
            this.config = config;
        }

        public void OnGet()
        {
            var constr = config.GetConnectionString("DefaultConnection");
            var query = "Select * from __EFMigrationsHistory";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TestClasses.Add(new TestClass
                            {
                                MigrationId = reader["MigrationId"].ToString(),
                                Version = reader["ProductVersion"].ToString()
                            });
                        }
                    }
                }
                conn.Close();
            }
        }
        public void OnPost()
        {
            var constr = config.GetConnectionString("DefaultConnection");
            var query = $"Insert into PersonalDetails (Name,Mobile) values ('{Name}', '{Mobile}')";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //var query = "Insert into PersonalDetails (Name,Mobile) values (@Name, @Mobile)";
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand(query,con))
            //    {
            //        cmd.Parameters.AddWithValue("@Name", Name);
            //        cmd.Parameters.AddWithValue("@Mobile", Mobile);
            //        con.Open();
            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}

            //var name = Request.Form["Name"];
            //var email = Request.Form["Email"];
            ViewData["confirmation"] = $"{Name}, information will be sent to {Mobile}";
        }
    }
}

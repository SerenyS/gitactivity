using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS_Proj_HIT.ViewModels
{
    public class RegisterDetailsViewModel
    {
        // from UserTable
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Instructor { get; set; }
        public string ProgramEnrolledIn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AspNetUserID { get; set; }
                   
        
    }
}

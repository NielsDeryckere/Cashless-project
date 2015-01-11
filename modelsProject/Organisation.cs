using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace models
{
    public class Organisation
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _login;

        [Required]
        [StringLength(50,MinimumLength=6,ErrorMessage="The login name has to be at least 6 characters")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _password;
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password has to be at least 6 characters")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _dbName;
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The database name has to be at least 6 characters")]
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        private string _dbLogin;
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The database loginname has to be at least 6 characters")]
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }

        private string _dbPassword;
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The database password to be at least 6 characters")]
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }

        private string _organisationName;
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The organisation name has to be at least 6 characters")]
        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        private string _address;
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The address has to be at least 6 characters")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;
        [Required]
        [EmailAddress(ErrorMessage="The emailadres has to be  part1@part2.part3")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;
        [Required]
       
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

      
    }
}

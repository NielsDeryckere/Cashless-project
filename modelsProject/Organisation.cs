using System;
using System.Collections.Generic;
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
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _password;
        [Required]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _dbName;
        [Required]
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        private string _dbLogin;
        [Required]
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }

        private string _dbPassword;
        [Required]
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }

        private string _organisationName;
        [Required]
        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        private string _address;
        [Required]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;
        [Required]
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

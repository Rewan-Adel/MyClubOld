using System;
using System.Transactions;
using System.Web;
using MyClubLib.Models;
using MyClubLib.Repository;
using static System.Collections.Specialized.BitVector32;

namespace SecurityLib.Repositoty
{
    public class SecurityRepository 
    {
        private readonly MyClubDBEntities _db;
        private readonly EFClubRepository _repository;

        public SecurityRepository()
        {
            _db = new MyClubDBEntities();
            _repository = new EFClubRepository();
        }
        private void ValidateUser(string user,string userPass, string password)
        {
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, userPass) )
            {
                throw new Exception("Invalid username or password!");
            }
        }


        public bool IsActiveUser(string userName)
        {
            try{
                bool flag = true;
                var user = _repository.FindByName<User_Profile>(userName); 
          
                if (user == null)
                   throw new Exception("User not found");

                if (!user.IsActive)
                    flag = false;

                return flag;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void enableUser(string username, bool isActive)
            {
            try
            {
                var user = _repository.FindByName<User_Profile>(username);
                if (user == null)
                    throw new Exception("User not found");

                user.IsActive = isActive;
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AdminPermissions(string userName)
        {
            try
            {
                bool flag = true;
                var user = _repository.FindByName<User_Profile>(userName);

                if (user == null)
                    throw new Exception("User not found");

                if (!user.IsAdmin)
                    flag = false;

                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Person Register(int? userId, string PersonName, string password, string Gender, string MobileNumber, string HomePhoneNumber,
                                 string Email, string Address, string Nationality)
        {
            try
            {
                Person person = _repository.CreatePerson(userId,
                                                     PersonName,
                                                     password,
                                                     Gender,
                                                     
                                                     MobileNumber,
                                                     HomePhoneNumber,
                                                     Email,
                                                     Address,
                                                     Nationality);

                _repository.CreateProfile(person.PersonName, true);

                _repository.SaveChanges();

                return person;
            }
            catch(Exception ex)
            {
                throw new Exception( ex.ToString());
            }
        }

        public void ChangePassword(string userName, string password)
        {
            var User = _repository.FindByName<Person>(userName);
            if(User == null)
                throw new Exception("Invalid username.");

            User.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _repository.SaveChanges();
        }

        public void Logout()
        {
            //Clear Session
            // Redirect to login page or another page as needed
            HttpContext.Current.Response.Redirect("~/Account/Login");
        }










    }
}
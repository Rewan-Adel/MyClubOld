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
        private void ValidateUser(Person user, string password)
        {
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("Invalid username or password!");
            }
        }
        public void CreateProfile(string userName, bool isActive)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var profile = new User_Profile()
                    {
                        UserName = userName,
                        IsActive = isActive,
                        IsAdmin = false,
                        Enable = true
                    };
                    _repository.Add(profile);

                    _repository.SaveChanges();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
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

        public Person Register(int? userId, string PersonName, string password, string Gender, DateTime BirthDate, string MobileNumber, string HomePhoneNumber,
                                 string Email, string Address, string Nationality)
        {
            Person person = _repository.CreatePerson(userId,
                                                     PersonName,
                                                     password,
                                                     Gender,
                                                     BirthDate,
                                                     MobileNumber,
                                                     HomePhoneNumber,
                                                     Email,
                                                     Address,
                                                     Nationality);
            return person;
        }
        public void Login(string userName, string password)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var User = _repository.FindByName<Person>(userName);
                    ValidateUser(User, password);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
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
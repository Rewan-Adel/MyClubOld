using System;
using System.Transactions;
using MyClub.UI.Models;

namespace SecurityLib.Entities
{
    public class Profile
    {
        public void createPerson(Person person)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var newPerson = new Person() 
                    {
                          PersonName = person.PersonName,
                          Gender = person.Gender,
                          BirthDate = person.BirthDate,
                          MobileNumber = person.MobileNumber,
                          HomePhoneNumber = person.HomePhoneNumber,
                          Email = person.Email,
                          Address = person.Address,
                          Nationality = person.Nationality,

                          RegistrationDate = DateTime.Now
                          
                    };
                                     
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
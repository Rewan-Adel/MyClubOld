using System;
using System.Transactions;
using MyClub.UI.Models;
using context =  MyClubLib.Repository;

namespace SecurityLib.Entities
{

    public class Profile
    {
        //This class likely represents a user profile,
        // including properties related to user information, roles, and other attributes
        
        public void CreateProfile(Person person)
        {
            var profile = new User_Profile()
            {
                UserID   = person.PersonId,
                UserName = person.PersonName,
                IsActive = true,
                IsAdmin  = false,
                Enable   = true
            };

         context.EFClubRepository.
        }
        
    }
}
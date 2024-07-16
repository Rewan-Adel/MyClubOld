using MyClub.UI.Models;
using System;
using System.Data.Entity.Migrations;
using System.Transactions;

namespace MyClubLib.Repository
{
    //All methods deals with db (add, get, edit, delete)
    public class EFClubRepository
    {
        private MyClubDBEntities context = new MyClubDBEntities(); //this class generated in models->context  

        public void SaveChanges() => context.SaveChanges();
        private void Add<T>(T entity) where T : class => context.Set<T>().Add(entity);
        private void Delete<T>(T entity) where T : class => context.Set<T>().Remove(entity);
        private void Edit<T>(T entity) where T : class => context.Set<T>().AddOrUpdate(entity);

        private T Find<T>(long id) where T : class => context.Set<T>().Find(id)  ;
        private void CreateAudit(ActionType actionType, Action action, int? userId, MasterEntity entity, string entityRecord)
        {
            try
            {
                var Audit = new AuditTrail
                {
                    ActionTypeId = (int)actionType,
                    ActionId = (int)action,
                    UserId = userId,
                    EntityId = (int)entity,
                    EntityRecord = entityRecord,
                    TransactionTime = DateTime.Now,
                    //IPAddress =
                };

                Add(Audit);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public void CreateMember(string memberName, Person person)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var Member = new Member
                    {
                        MemberName = memberName,
                        PersonId   = person.PersonId,
                        UserId     = person.UserId,
                       
                        RegistrationDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                       // RegisteredById   =  
                    };
                    string entityRecord = $"Adding {memberName} successfully";
                    Add(Member);
                    CreateAudit(ActionType.Add, Action.Create_Member, Member.UserId, MasterEntity.Member, entityRecord);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }

        }
        public void DeleteMember(int memberId)

        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var member = Find<Member>(memberId);
                    string entityRecord = $"Deleting {member.MemberName} successfully";

                    if (member != null)
                    {
                        Delete(member);
                        CreateAudit(ActionType.Delete, Action.Delete_Member, member.UserId, MasterEntity.Member, entityRecord);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
        public void EditMember(int memberId )
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var member = Find<Member>(memberId);
                    string entityRecord = $"updating {member.MemberName} successfully";

                    if (member != null)
                    {
                        Edit(member);
                        SaveChanges();
                    }
                    CreateAudit(ActionType.Edit, Action.Edit_Member, member.UserId, MasterEntity.Member, entityRecord);

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
       
        public void CreatePerson(Person person)
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
                        //UserId
                    };
                    string entityRecord = $"Creating {person.PersonName} successfully";
                    CreateAudit(ActionType.Add, Action.Create_Person, person.UserId, MasterEntity.Member, entityRecord);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
        public void DeletePesron(int personId)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var person = Find<Person>(personId);
                    string entityRecord = $"Deleting {person.PersonName} successfully";

                    if (person != null)
                    {
                        Delete(person);
                        CreateAudit(ActionType.Delete, Action.Delete_Person, person.UserId, MasterEntity.Member, entityRecord);
                    }

                        scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
        public void EditPerson(int personId)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var person = Find<Person>(personId);

                    if (person != null)
                    {

                    }
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
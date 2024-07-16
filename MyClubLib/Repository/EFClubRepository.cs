using MyClub.UI.Models;
using System;
using System.Transactions;
using Action = MyClub.UI.Models.Action;

namespace MyClubLib.Repository
{
    //All methods deals with db (add, get, edit, delete)
    public class EFClubRepository
    {
        private MyClubDBEntities context = new MyClubDBEntities(); //this class generated in models->context  

        private void SaveChanges() => context.SaveChanges();
        private void Add<T>(T entity) where T : class => context.Set<T>().Add(entity);
        private void Delete<T>(T entity) where T : class => context.Set<T>().Remove(entity);

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
                    var member = Find(memberId);

                    if (member != null)  Delete(member);
                    
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
                    var member = Find(memberId);

                    if (member != null)
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

namespace MyClubLib.Repository
{
    public enum MasterEntity
    {
        Service     = 1,
        Offer       = 2,
        Trainer     = 3,
        Member      = 4,
        MemberOffer = 5,
        //Setting     = 6,
    }

    public enum Service
    {
        Fittnes  = 1,
        Zomba    = 2,
        Sauna    = 3,
        Cardio   = 4,
        Yoga     = 5,
        Swimming = 6,
    }
    public enum Action
    {
        Create_Member      = 1,
        Edit_Member        = 2,
        Delete_Member      = 3,
        
        Create_Trainer     = 4,
        Edit_Trainer       = 5,
        Delete_Trainer     = 6,

        Create_Service     = 7,
        Edit_Service       = 8,
        Delete_Service     = 9,
        Enable_Service     = 10,
        Disable_Service    = 11,

        Create_Offer       = 11,
        Edit_Offer         = 12,
        Delete_Offer       = 13,
        Edit_OfferPrice    = 14,
        Create_OfferDetail = 15,
        Edit_OfferDetail   = 16,
        Delete_OfferDetail = 17,

        Add_MemberAttendance  = 18,
        Edit_MemberAttendance = 19,
       
        create_MemberOffer = 20,
        Delete_MemberOffer = 21,
        Start_MemberOffer  = 22,
        End_MemberOffer    = 23,
        Edit_MemberOffer   = 24,

        Apply_Offer        = 25
    }

    public enum ActionType
    {
        Add    = 1,
        Edit   = 2,
        Delete = 3 
    }
    public class Utilities
    {
      
    }
}
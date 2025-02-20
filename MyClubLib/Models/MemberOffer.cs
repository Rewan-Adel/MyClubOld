//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClubLib.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberOffer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberOffer()
        {
            this.MemberAttendances = new HashSet<MemberAttendance>();
        }
    
        public int MemberOfferId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> OfferPriceId { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> CurrentStatusId { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<decimal> MemberPrice { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> DiscountValue { get; set; }
        public Nullable<int> DiscountById { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Note { get; set; }
        public Nullable<int> TrainerId { get; set; }
    
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberAttendance> MemberAttendances { get; set; }
        public virtual OfferPrice OfferPrice { get; set; }
    }
}

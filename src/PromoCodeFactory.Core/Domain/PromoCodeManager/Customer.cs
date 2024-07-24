using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManager
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }
        
        public IList<Preference> Preferences { get; set; } 
        
        public IList<PromoCode> PromoCodes { get; set; } 
    }
}
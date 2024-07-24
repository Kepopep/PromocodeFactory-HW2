using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManager
{
    public class Preference : BaseEntity
    {
        public string Name { get; set; }
        
        public IList<Customer> Customers { get; set; }
    }
}
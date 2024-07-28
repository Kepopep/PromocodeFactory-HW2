using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models;

public class CustomerPost
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
        
    public IList<string> Preferences { get; set; } 
}
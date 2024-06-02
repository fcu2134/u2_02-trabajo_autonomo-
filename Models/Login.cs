using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace projecto_net.Models
{
    public class Login
    {
        
      
        [DataType(DataType.EmailAddress)]
        public string? Correo { get; set; } = string.Empty;

     
     
        public string? Password { get; set; }
    }
    
}
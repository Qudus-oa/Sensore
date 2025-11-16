using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sensore.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password{ get; set; }
        public UserType Type { get; set; } /* Patient, Clinician, Admin*/
        public DateTime CreatedDate { get; set; }

    }

}
public enum UserType
{
    Admin,
    Clinician,
    patient
}


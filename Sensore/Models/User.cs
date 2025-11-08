namespace Sensore.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; } // Patient, Clinician, Admin
        public DateTime CreatedDate { get; set; }
    }
    public enum UserType
    {
        Patient,
        Clinician,
        Admin
    }

}

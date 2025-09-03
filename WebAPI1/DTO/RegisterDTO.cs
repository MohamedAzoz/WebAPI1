using System.ComponentModel.DataAnnotations;

namespace WebAPI1.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z1-9]+@gmail\.com$")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

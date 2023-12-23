using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelerikAspNetCoreApp1.Model;
public class Admission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    [Index(IsUnique = true)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date Birthday")]
    [CustomValidation(typeof(Admission), "ValidateDateOfBirth")]
    public DateTime DateBirthday { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid Email Format")]
    public string Email { get; set; }

    [StringLength(255)]
    public string Subject { get; set; }

    [StringLength(4096)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Return Date")]
    [CustomValidation(typeof(Admission), "ValidateReturnDate")]
    public DateTime ReturnDate { get; set; }

    public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
    {
        if (dateOfBirth < new DateTime(1900, 1, 1) || dateOfBirth > DateTime.Now)
        {
            return new ValidationResult("Date Birthday must be greater than 01/01/1900 and less than current date.");
        }

        return ValidationResult.Success;
    }

    public static ValidationResult ValidateReturnDate(DateTime returnDate, ValidationContext context)
    {
        if (returnDate <= DateTime.Now.AddDays(1))
        {
            return new ValidationResult("Return Date must be greater than current date plus one day.");
        }

        return ValidationResult.Success;
    }
}
 
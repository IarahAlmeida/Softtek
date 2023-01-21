using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities
{
    public class Account
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public long Number { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Account()
        {
            Id = string.Empty;
            Name = string.Empty;
        }

        public Account(string id, long number, string name, bool isActive)
        {
            Id = id;
            Number = number;
            Name = name;
            IsActive = isActive;
        }
    }
}
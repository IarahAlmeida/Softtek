using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities
{
    public class Movement
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [RegularExpression("C|D", ErrorMessage = "Invalid movement type.")]
        public char Type { get; set; }

        [Required]
        public double Value { get; set; }

        public Movement(string accountId, string date, char type, double value)
        {
            Id = Guid.NewGuid().ToString();
            AccountId = accountId;
            Date = date;
            Type = type;
            Value = value;
        }

        public Movement(string id, string accountId, string date, char type, double value)
        {
            Id = id;
            AccountId = accountId;
            Date = date;
            Type = type;
            Value = value;
        }
    }
}
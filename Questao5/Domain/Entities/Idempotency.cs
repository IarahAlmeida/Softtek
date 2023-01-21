using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities
{
    public class Idempotency
    {
        [Key]
        public string Id { get; set; }

        [StringLength(1000)]
        public string Request { get; set; }

        [StringLength(1000)]
        public string Response { get; set; }

        public Idempotency(string id, string request, string response)
        {
            Id = id;
            Request = request;
            Response = response;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace JdRunner.Models.Customs
{
    public class Order
    {

        public int Ticket { get; set; }
        [Required]
        public string Status { get; set; }
        public string Requester { get; set; }
        public string Runner { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ReqTime { get; set; }
    }
}

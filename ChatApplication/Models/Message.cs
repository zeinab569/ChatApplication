using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public AppUser Sender { get; set; }
        
        [ForeignKey(nameof(ReceiverId))]
        public AppUser Receiver { get; set; }


    }
}

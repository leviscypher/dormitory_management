using System.ComponentModel.DataAnnotations;

namespace dormitory_management_be.Models
{
    public class DateConfig
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string RoomId { get; set; }
    }
}

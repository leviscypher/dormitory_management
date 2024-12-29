using System.ComponentModel.DataAnnotations;

namespace dormitory_management_be.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string RoomCode { get; set; }

        public string RoomType { get; set; }

        public int? StudentCount { get; set; }

        public string RoomStatus { get; set; }

        public string? RoomRate { get; set; }

        public int? RoomTypeId { get; set; }

        public int? FloorId { get; set; }

        public int? AreaId { get; set; }

        public int Gender { get; set; }

    }
}

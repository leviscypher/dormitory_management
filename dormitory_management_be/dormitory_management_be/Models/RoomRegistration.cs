namespace dormitory_management_be.Models
{
    public class RoomRegistration
    {
        public int Id { get; set; }
        public string Card { get; set; }
        public DateTime? CardDate { get; set; }
        public string AddressCard { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime TimeIn { get; set; }
        public string IDPhoto { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string RoomId { get; set; }
    }
}

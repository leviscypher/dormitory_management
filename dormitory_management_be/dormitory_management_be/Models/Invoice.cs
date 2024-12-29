namespace dormitory_management_be.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentCode { get; set; }
        public string? RoomId { get; set; }
        public string? PaymentMonth { get; set; }
        public string? PaymentYear { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}

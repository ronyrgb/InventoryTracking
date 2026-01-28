namespace Backend.Data.DTOs
{
    public class ProductLoanCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string? Note { get; set; }
    }

    public class ProductLoanUpdateDto
    {
        public string? Note { get; set; }
    }
}

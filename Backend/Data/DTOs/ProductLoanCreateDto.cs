using System;

namespace Backend.Data.DTOs
{
    public class ProductLoanCreateDto
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }
   public string? Note { get; set; }
    }
}

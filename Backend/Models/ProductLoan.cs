using System;

namespace Backend.Models
{
    public class ProductLoan
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK para Product
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }

        // FK para User
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        // Datas de empréstimo e devolução
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }

        // Observação/nota do empréstimo
        public string? Note { get; set; }

        // Status do empréstimo
        public string Status => ReturnDate == null ? "Em uso" : "Disponível";

        // Método de conveniência: marcar devolução
        public void CheckIn()
        {
            if (ReturnDate != null)
                throw new InvalidOperationException("Produto já devolvido.");

            ReturnDate = DateTime.UtcNow;
        }

        // Método de conveniência: marcar empréstimo
        public void CheckOut(Guid userId, string? note = null)
        {
            if (ReturnDate == null && UserId != null)
                throw new InvalidOperationException("Produto ainda está em uso.");

            UserId = userId;
            LoanDate = DateTime.UtcNow;
            ReturnDate = null;
            Note = note;
        }
    }
}

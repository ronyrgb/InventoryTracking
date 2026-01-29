using System;

namespace Backend.Data.DTOs
{
    /// <summary>
    /// DTO usado para atualizar um empréstimo de produto
    /// </summary>
    public class ProductLoanUpdateDto
    {
        /// <summary>
        /// Nota ou observação sobre o empréstimo (opcional)
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Data de devolução, se o produto for devolvido
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}

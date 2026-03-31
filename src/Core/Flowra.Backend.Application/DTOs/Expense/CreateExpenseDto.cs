using System;
using System.Collections.Generic;
using System.Text;

namespace Flowra.Backend.Application.DTOs.Expense
{
    public class CreateExpenseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsCreditCard { get; set; }
        public decimal? MinimumPaymentAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}

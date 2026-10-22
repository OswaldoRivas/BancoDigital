using System;
using System.Collections.Generic;
using System.Text;

namespace BancoDigital.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; } = string.Empty;

        public decimal MonthlyIncome { get; set; }
    }
}

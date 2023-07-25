using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExercise.Core.Entities.Enums
{
    public enum AccountType
    {
        [Description("Ahorros")]
        Savings,
        [Description("Corriente")]
        Funds
    }
}

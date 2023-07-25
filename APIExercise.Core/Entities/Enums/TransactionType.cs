using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExercise.Core.Entities.Enums
{
    public enum TransactionType
    {
        [Description("Ingreso")]
        Income,
        [Description("Salida")]
        Outcome
    }
}

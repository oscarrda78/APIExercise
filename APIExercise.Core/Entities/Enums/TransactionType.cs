using System;
using System.ComponentModel;

namespace APIExercise.Core.Entities.Enums
{
    public enum TransactionType
    {
        [Description("No Especificado")]
        NotSpecified = 0,
        [Description("Ingreso")]
        Income,
        [Description("Salida")]
        Outcome
    }
}
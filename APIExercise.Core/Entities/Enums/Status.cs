using System.ComponentModel;

namespace APIExercise.Core.Entities.Enums
{
    [Flags]
    public enum Status
    {
        [Description("False")]
        Inactive = 1,
        [Description("True")]
        Active = 2
    }
}

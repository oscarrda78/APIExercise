using System.ComponentModel;

namespace APIExercise.Core.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field == null) return string.Empty;

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? attributes[0].Description : enumValue.ToString();
        }

        public static TEnum GetEnumValueFromDescription<TEnum>(string description) where TEnum : Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (TEnum)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentException($"No {typeof(TEnum)} with description {description} found");
        }
    }

}

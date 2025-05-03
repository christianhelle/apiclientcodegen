using System;
using System.ComponentModel;
using System.Globalization;
using Rapicgen.Core.Extensions;

namespace Rapicgen.Core.TypeConverters
{
    /// <summary>
    /// Type converter that converts an enum value to and from its description attribute value
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        /// <summary>
        /// Creates a new instance of the EnumDescriptionTypeConverter class
        /// </summary>
        /// <param name="type">The enum type</param>
        public EnumDescriptionTypeConverter(Type type) : base(type) 
        {
        }

        /// <summary>
        /// Converts the given enum value to its description string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null && value.GetType().IsEnum)
            {
                // Use the existing GetDescription extension method to get the description
                return ((Enum)value).GetDescription();
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Converts a description string to its corresponding enum value
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                foreach (var enumValue in Enum.GetValues(EnumType))
                {
                    var enumValueAsEnum = (Enum)enumValue;
                    if (enumValueAsEnum.GetDescription() == stringValue)
                    {
                        return enumValue;
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
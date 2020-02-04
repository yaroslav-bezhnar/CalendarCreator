using System;

namespace CalendarCreator.Core.Helpers
{
    public static class EnumHelper
    {
        public static T GetEnumAttribute<T>( this Enum @enum ) where T : Attribute
        {
            var type = @enum.GetType();
            var memInfo = type.GetMember( @enum.ToString() );
            var attributes = memInfo[0].GetCustomAttributes( typeof( T ), false );

            return attributes.Length > 0 ? (T) attributes[0] : null;
        }
    }
}
using System;

namespace CalendarCreator.Core.Attributes
{
    /// <summary>
    ///     Specifies a short name value.
    /// </summary>
    public class ShortNameAttribute : Attribute
    {
        #region " Constructors "

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShortNameAttribute" /> class.
        /// </summary>
        /// <param name="shortName">Short name.</param>
        public ShortNameAttribute(string shortName)
        {
            ShortName = shortName;
        }

        #endregion

        #region " Properties "

        /// <summary>
        ///     Gets/sets a short name.
        /// </summary>
        public string ShortName
        {
            get;
            set;
        }

        #endregion
    }
}

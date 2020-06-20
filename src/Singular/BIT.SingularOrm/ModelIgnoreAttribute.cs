using System;

namespace BIT.SingularOrm
{
    /// <summary>
    ///     Indicate that a property will be ignored when the model is generated
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ModelIgnoreAttribute : Attribute
    {
        /// <summary>
        ///     Create an instance of the attribute
        /// </summary>
        /// <param name="name">The name of the property to be ignored</param>
        public ModelIgnoreAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     The name of the property to be ignored
        /// </summary>
        public string Name { get; set; }
    }
}
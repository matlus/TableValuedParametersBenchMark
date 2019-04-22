using System;

namespace ConsoleApp2
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal sealed class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; }
        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }
    }


}

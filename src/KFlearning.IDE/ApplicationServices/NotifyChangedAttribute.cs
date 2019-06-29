using System;

namespace KFlearning.IDE.ApplicationServices
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotifyChangedAttribute : Attribute
    {
    }
}
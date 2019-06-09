using System;

namespace KFlearning.ApplicationServices
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotifyChangedAttribute : Attribute
    {
    }
}
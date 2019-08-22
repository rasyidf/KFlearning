// 
//  PROJECT  :   KFlearning
//  FILENAME :   NotifyChangedAttribute.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;

#endregion

namespace KFlearning.IDE.ApplicationServices
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotifyChangedAttribute : Attribute
    {
    }
}
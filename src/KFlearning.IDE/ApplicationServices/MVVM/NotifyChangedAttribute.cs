// 
//  PROJECT  :   KFlearning
//  FILENAME :   NotifyChangedAttribute.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;

namespace KFlearning.IDE.ApplicationServices
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotifyChangedAttribute : Attribute
    {
    }
}
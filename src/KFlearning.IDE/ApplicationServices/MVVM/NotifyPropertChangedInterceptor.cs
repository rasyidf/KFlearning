// 
//  PROJECT  :   KFlearning
//  FILENAME :   NotifyPropertChangedInterceptor.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Reflection;
using Castle.DynamicProxy;

#endregion

namespace KFlearning.IDE.ApplicationServices
{
    public class NotifyPropertChangedInterceptor : IInterceptor
    {
        private const string SetPrefix = "set_";

        public void Intercept(IInvocation invocation)
        {
            // let the original call go 1st
            invocation.Proceed();

            // check is this set method
            if (!invocation.Method.Name.StartsWith(SetPrefix)) return;
            string propertyName = invocation.Method.Name.Substring(4);
            var pi = invocation.TargetType.GetProperty(propertyName);

            // check for the special attribute
            if (!HasAttribute<NotifyChangedAttribute>(pi)) return;

            // invoke event invocator
            var ev = invocation.TargetType.GetMethod(nameof(IAutoNotifyPropertyChanged.OnPropertyChanged));
            ev?.Invoke(invocation.InvocationTarget, new object[] {propertyName});
        }

        private static bool HasAttribute<T>(PropertyInfo info) where T : Attribute
        {
            return Attribute.IsDefined(info, typeof(T));
        }
    }
}
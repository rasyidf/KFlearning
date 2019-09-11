// 
//  PROJECT  :   KFlearning
//  FILENAME :   ISequenceFactory.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.Generic;

#endregion

namespace KFlearning.Core.Installer
{
    public interface ISequenceFactory
    {
        Queue<ITaskNode> GetInstallSequence(Func<Type, object> resolver);
        Queue<ITaskNode> GetUninstallSequence(Func<Type, object> resolver);
    }
}
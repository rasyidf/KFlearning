// 
//  PROJECT  :   KFlearning
//  FILENAME :   ISequenceFactory.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;

namespace KFlearning.Core.Services
{
    public interface ISequenceFactory
    {
        Queue<ITaskNode> GetInstallSequence();
        Queue<ITaskNode> GetUninstallSequence();
    }
}
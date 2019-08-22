// 
//  PROJECT  :   KFlearning
//  FILENAME :   MathHelper.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;

#endregion

namespace KFlearning.Core.Services
{
    public static class MathHelper
    {
        public static int CalculatePercentage(int current, int total)
        {
            return (int) Math.Round((double) current / total * 100, 0);
        }
    }
}
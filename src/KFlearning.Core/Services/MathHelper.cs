using System;

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

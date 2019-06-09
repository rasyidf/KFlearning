using System;

namespace KFlearning.ApplicationServices
{
    public interface IListener<in T> where T : EventArgs
    {
        void OnEvent(T e);
    }
}
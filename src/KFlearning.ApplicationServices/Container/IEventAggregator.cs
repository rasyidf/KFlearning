using System;

namespace KFlearning.ApplicationServices
{
    public interface IEventAggregator
    {
        void Publish<T>(T eventToPublish) where T : EventArgs;
        void Subsribe(object subscriber);
    }
}
using System;
using System.ComponentModel;
using System.Windows.Threading;
using KFlearning.ApplicationServices.Models;

namespace KFlearning.ApplicationServices
{
    public class ViewModelBase : IAutoNotifyPropertyChanged, IListener<AppEventArgs>
    {
        public static Func<IEventAggregator> AggregatorFunc;

        #region Properties

        protected Dispatcher Synchronization { get; }

        public IEventAggregator Aggregator { get; } = AggregatorFunc();
        
        #endregion

        #region Constructor

        protected ViewModelBase()
        {
            Synchronization = Dispatcher.CurrentDispatcher;
            Aggregator.Subsribe(this);
        }

        #endregion

        #region IAutoNotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = null)
        {
            try
            {
                var invocation = new Action(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
                if (Synchronization != null) Synchronization.Invoke(invocation);
                else invocation();
            }
            catch (Exception e)
            {
                //Logger.Warn("Invocation error.", e);
            }
        }

        #endregion

        #region Event EventAggregator

        public void OnEvent(AppEventArgs e)
        {
            switch (e.Action)
            {
                case AppAction.Bootstrap:
                    OnBootstrap(e);
                    break;
                case AppAction.ActivateReader:
                    break;
                case AppAction.ActivateCreateProject:
                    break;
                case AppAction.RequestUpdateData:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Overridables

        protected virtual void OnBootstrap(AppEventArgs e) { }

        protected virtual void OnActivate(AppEventArgs e) { }

        protected virtual void OnUpdateData(AppEventArgs e) { }

        #endregion
    }
}

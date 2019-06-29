﻿using System;
using System.ComponentModel;
using System.Windows.Threading;
using Castle.Core.Logging;

namespace KFlearning.IDE.ApplicationServices
{
    public class PropertyChangedBase : IAutoNotifyPropertyChanged
    {
        #region Properties

        protected Dispatcher Synchronization { get; }

        public ILogger Logger { get; set; } = NullLogger.Instance;
        
        #endregion

        #region Constructor

        protected PropertyChangedBase()
        {
            Synchronization = Dispatcher.CurrentDispatcher;
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
                Logger.Warn("Invocation error.", e);
            }
        }

        #endregion
    }
}

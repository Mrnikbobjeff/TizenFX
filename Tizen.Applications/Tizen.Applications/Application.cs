/// Copyright 2016 by Samsung Electronics, Inc.,
///
/// This software is the confidential and proprietary information
/// of Samsung Electronics, Inc. ("Confidential Information"). You
/// shall not disclose such Confidential Information and shall use
/// it only in accordance with the terms of the license agreement
/// you entered into with Samsung.


using System;

namespace Tizen.Applications
{
    /// <summary>
    /// The Application handles an application state change or system events and provides mechanisms that launch other applications.
    /// </summary>
    public abstract class Application
    {
        private static Application s_CurrentApplication = null;

        private Interop.AppEvent.SafeAppEventHandle _lowMemoryNativeHandle;
        private Interop.AppEvent.SafeAppEventHandle _localeChangedNativeHandle;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Created;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Terminated;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AppControlReceivedEventArgs> AppControlReceived;

        /// <summary>
        /// The low memory event.
        /// </summary>
        public event EventHandler<LowMemoryEventArgs> LowMemory;

        /// <summary>
        /// The system language changed event.
        /// </summary>
        public event EventHandler<LocaleChangedEventArgs> LocaleChanged;

        /// <summary>
        /// 
        /// </summary>
        public static Application Current { get { return s_CurrentApplication; } }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationInfo ApplicationInfo { get; internal set; }

        /// <summary>
        /// Runs the application's main loop.
        /// </summary>
        /// <param name="args"></param>
        public virtual void Run(string[] args)
        {
            s_CurrentApplication = this;

            Interop.AppEvent.AddEventHandler(Interop.AppEvent.EventNames.LowMemory, HandleAppEvent, IntPtr.Zero, out _lowMemoryNativeHandle);
            Interop.AppEvent.AddEventHandler(Interop.AppEvent.EventNames.LanguageSet, HandleAppEvent, IntPtr.Zero, out _localeChangedNativeHandle);
        }

        /// <summary>
        /// Exits the main loop of application. 
        /// </summary>
        public abstract void Exit();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreate(EventArgs e)
        {
            var eh = Created as EventHandler<EventArgs>;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTerminate(EventArgs e)
        {
            var eh = Terminated as EventHandler<EventArgs>;
            if (eh != null)
            {
                eh(this, e);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            var eh = AppControlReceived as EventHandler<AppControlReceivedEventArgs>;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLowMemory(LowMemoryEventArgs e)
        {
            var eh = LowMemory as EventHandler<LowMemoryEventArgs>;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            var eh = LocaleChanged as EventHandler<LocaleChangedEventArgs>;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        internal void SendCreate()
        {
            ApplicationInfo = new ApplicationInfo();
            OnCreate(EventArgs.Empty);
        }

        private void HandleAppEvent(string eventName, IntPtr eventData, IntPtr data)
        {
            Console.WriteLine("HandleAppEvent!! eventName={0}, eventData={1}", eventName, eventData);
            Bundle b = new Bundle(eventData);
            if (eventName == Interop.AppEvent.EventNames.LowMemory)
            {
                string value = b.GetItem<string>(Interop.AppEvent.EventKeys.LowMemory);
                LowMemoryStatus status = LowMemoryStatus.Normal;
                if (value == Interop.AppEvent.EventValues.MemorySoftWarning)
                {
                    status = LowMemoryStatus.SoftWarning;
                }
                else if (value == Interop.AppEvent.EventValues.MemoryHardWarning)
                {
                    status = LowMemoryStatus.HardWarning;
                }
                OnLowMemory(new LowMemoryEventArgs { LowMemoryStatus = status });
            }
            else if (eventName == Interop.AppEvent.EventNames.LanguageSet)
            {
                string value = b.GetItem<string>(Interop.AppEvent.EventKeys.LanguageSet);
                OnLocaleChanged(new LocaleChangedEventArgs { Locale = value });
            }
        }
    }
}

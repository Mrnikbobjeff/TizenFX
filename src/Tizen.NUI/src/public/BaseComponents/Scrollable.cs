/** Copyright (c) 2017 Samsung Electronics Co., Ltd.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

namespace Tizen.NUI.BaseComponents
{

    using System;
    using System.Runtime.InteropServices;

    public class Scrollable : View
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;

        internal Scrollable(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NDalicPINVOKE.Scrollable_SWIGUpcast(cPtr), cMemoryOwn)
        {
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Scrollable obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.

            DisConnectFromSignals();

            if (swigCPtr.Handle != global::System.IntPtr.Zero)
            {
                if (swigCMemOwn)
                {
                    swigCMemOwn = false;
                    NDalicPINVOKE.delete_Scrollable(swigCPtr);
                }
                swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
            }

            base.Dispose(type);
        }

        private void DisConnectFromSignals()
        {
            // Save current CPtr.
            global::System.Runtime.InteropServices.HandleRef currentCPtr = swigCPtr;

            // Use BaseHandle CPtr as current might have been deleted already in derived classes.
            swigCPtr = GetBaseHandleCPtrHandleRef;

            if (_scrollableCompletedCallbackDelegate != null)
            {
                this.ScrollCompletedSignal().Disconnect(_scrollableCompletedCallbackDelegate);
            }

            if (_scrollableUpdatedCallbackDelegate != null)
            {
                this.ScrollUpdatedSignal().Disconnect(_scrollableUpdatedCallbackDelegate);
            }

            if (_scrollableStartedCallbackDelegate != null)
            {
                this.ScrollStartedSignal().Disconnect(_scrollableStartedCallbackDelegate);
            }

            // BaseHandle CPtr is used in Registry and there is danger of deletion if we keep using it here.
            // Restore current CPtr.
            swigCPtr = currentCPtr;
        }

        public class StartedEventArgs : EventArgs
        {
            private Vector2 _vector2;

            public Vector2 Vector2
            {
                get
                {
                    return _vector2;
                }
                set
                {
                    _vector2 = value;
                }
            }
        }

        public class UpdatedEventArgs : EventArgs
        {
            private Vector2 _vector2;

            public Vector2 Vector2
            {
                get
                {
                    return _vector2;
                }
                set
                {
                    _vector2 = value;
                }
            }
        }

        public class CompletedEventArgs : EventArgs
        {
            private Vector2 _vector2;

            public Vector2 Vector2
            {
                get
                {
                    return _vector2;
                }
                set
                {
                    _vector2 = value;
                }
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void StartedCallbackDelegate(IntPtr vector2);
        private DaliEventHandler<object, StartedEventArgs> _scrollableStartedEventHandler;
        private StartedCallbackDelegate _scrollableStartedCallbackDelegate;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void UpdatedCallbackDelegate(IntPtr vector2);
        private DaliEventHandler<object, UpdatedEventArgs> _scrollableUpdatedEventHandler;
        private UpdatedCallbackDelegate _scrollableUpdatedCallbackDelegate;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void CompletedCallbackDelegate(IntPtr vector2);
        private DaliEventHandler<object, CompletedEventArgs> _scrollableCompletedEventHandler;
        private CompletedCallbackDelegate _scrollableCompletedCallbackDelegate;

        public event DaliEventHandler<object, StartedEventArgs> ScrollStarted
        {
            add
            {
                lock (this)
                {
                    // Restricted to only one listener
                    if (_scrollableStartedEventHandler == null)
                    {
                        _scrollableStartedEventHandler += value;

                        _scrollableStartedCallbackDelegate = new StartedCallbackDelegate(OnStarted);
                        this.ScrollStartedSignal().Connect(_scrollableStartedCallbackDelegate);
                    }
                }
            }

            remove
            {
                lock (this)
                {
                    if (_scrollableStartedEventHandler != null)
                    {
                        this.ScrollStartedSignal().Disconnect(_scrollableStartedCallbackDelegate);
                    }

                    _scrollableStartedEventHandler -= value;
                }
            }
        }

        private void OnStarted(IntPtr vector2)
        {
            StartedEventArgs e = new StartedEventArgs();

            // Populate all members of "e" (StartedEventArgs) with real data
            e.Vector2 = Tizen.NUI.Vector2.GetVector2FromPtr(vector2);

            if (_scrollableStartedEventHandler != null)
            {
                //here we send all data to user event handlers
                _scrollableStartedEventHandler(this, e);
            }

        }

        public event DaliEventHandler<object, UpdatedEventArgs> ScrollUpdated
        {
            add
            {
                lock (this)
                {
                    // Restricted to only one listener
                    if (_scrollableUpdatedEventHandler == null)
                    {
                        _scrollableUpdatedEventHandler += value;

                        _scrollableUpdatedCallbackDelegate = new UpdatedCallbackDelegate(OnUpdated);
                        this.ScrollUpdatedSignal().Connect(_scrollableUpdatedCallbackDelegate);
                    }
                }
            }

            remove
            {
                lock (this)
                {
                    if (_scrollableUpdatedEventHandler != null)
                    {
                        this.ScrollUpdatedSignal().Disconnect(_scrollableUpdatedCallbackDelegate);
                    }

                    _scrollableUpdatedEventHandler -= value;
                }
            }
        }

        private void OnUpdated(IntPtr vector2)
        {
            UpdatedEventArgs e = new UpdatedEventArgs();

            // Populate all members of "e" (UpdatedEventArgs) with real data
            e.Vector2 = Tizen.NUI.Vector2.GetVector2FromPtr(vector2);

            if (_scrollableUpdatedEventHandler != null)
            {
                //here we send all data to user event handlers
                _scrollableUpdatedEventHandler(this, e);
            }

        }

        public event DaliEventHandler<object, CompletedEventArgs> ScrollCompleted
        {
            add
            {
                lock (this)
                {
                    // Restricted to only one listener
                    if (_scrollableCompletedEventHandler == null)
                    {
                        _scrollableCompletedEventHandler += value;

                        _scrollableCompletedCallbackDelegate = new CompletedCallbackDelegate(OnCompleted);
                        this.ScrollCompletedSignal().Connect(_scrollableCompletedCallbackDelegate);
                    }
                }
            }

            remove
            {
                lock (this)
                {
                    if (_scrollableCompletedEventHandler != null)
                    {
                        this.ScrollCompletedSignal().Disconnect(_scrollableCompletedCallbackDelegate);
                    }

                    _scrollableCompletedEventHandler -= value;
                }
            }
        }

        private void OnCompleted(IntPtr vector2)
        {
            CompletedEventArgs e = new CompletedEventArgs();

            // Populate all members of "e" (CompletedEventArgs) with real data
            e.Vector2 = Tizen.NUI.Vector2.GetVector2FromPtr(vector2);

            if (_scrollableCompletedEventHandler != null)
            {
                //here we send all data to user event handlers
                _scrollableCompletedEventHandler(this, e);
            }

        }


        public class Property
        {
            public static readonly int OVERSHOOT_EFFECT_COLOR = NDalicPINVOKE.Scrollable_Property_OVERSHOOT_EFFECT_COLOR_get();
            public static readonly int OVERSHOOT_ANIMATION_SPEED = NDalicPINVOKE.Scrollable_Property_OVERSHOOT_ANIMATION_SPEED_get();
            public static readonly int OVERSHOOT_ENABLED = NDalicPINVOKE.Scrollable_Property_OVERSHOOT_ENABLED_get();
            public static readonly int OVERSHOOT_SIZE = NDalicPINVOKE.Scrollable_Property_OVERSHOOT_SIZE_get();
            public static readonly int SCROLL_TO_ALPHA_FUNCTION = NDalicPINVOKE.Scrollable_Property_SCROLL_TO_ALPHA_FUNCTION_get();
            public static readonly int SCROLL_RELATIVE_POSITION = NDalicPINVOKE.Scrollable_Property_SCROLL_RELATIVE_POSITION_get();
            public static readonly int SCROLL_POSITION_MIN = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MIN_get();
            public static readonly int SCROLL_POSITION_MIN_X = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MIN_X_get();
            public static readonly int SCROLL_POSITION_MIN_Y = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MIN_Y_get();
            public static readonly int SCROLL_POSITION_MAX = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MAX_get();
            public static readonly int SCROLL_POSITION_MAX_X = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MAX_X_get();
            public static readonly int SCROLL_POSITION_MAX_Y = NDalicPINVOKE.Scrollable_Property_SCROLL_POSITION_MAX_Y_get();
            public static readonly int CAN_SCROLL_VERTICAL = NDalicPINVOKE.Scrollable_Property_CAN_SCROLL_VERTICAL_get();
            public static readonly int CAN_SCROLL_HORIZONTAL = NDalicPINVOKE.Scrollable_Property_CAN_SCROLL_HORIZONTAL_get();

        }

        public Scrollable() : this(NDalicPINVOKE.new_Scrollable__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        private bool IsOvershootEnabled()
        {
            bool ret = NDalicPINVOKE.Scrollable_IsOvershootEnabled(swigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        private void SetOvershootEnabled(bool enable)
        {
            NDalicPINVOKE.Scrollable_SetOvershootEnabled(swigCPtr, enable);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        private void SetOvershootEffectColor(Vector4 color)
        {
            NDalicPINVOKE.Scrollable_SetOvershootEffectColor(swigCPtr, Vector4.getCPtr(color));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        private Vector4 GetOvershootEffectColor()
        {
            Vector4 ret = new Vector4(NDalicPINVOKE.Scrollable_GetOvershootEffectColor(swigCPtr), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        private void SetOvershootAnimationSpeed(float pixelsPerSecond)
        {
            NDalicPINVOKE.Scrollable_SetOvershootAnimationSpeed(swigCPtr, pixelsPerSecond);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        private float GetOvershootAnimationSpeed()
        {
            float ret = NDalicPINVOKE.Scrollable_GetOvershootAnimationSpeed(swigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ScrollableSignal ScrollStartedSignal()
        {
            ScrollableSignal ret = new ScrollableSignal(NDalicPINVOKE.Scrollable_ScrollStartedSignal(swigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ScrollableSignal ScrollUpdatedSignal()
        {
            ScrollableSignal ret = new ScrollableSignal(NDalicPINVOKE.Scrollable_ScrollUpdatedSignal(swigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ScrollableSignal ScrollCompletedSignal()
        {
            ScrollableSignal ret = new ScrollableSignal(NDalicPINVOKE.Scrollable_ScrollCompletedSignal(swigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public Vector4 OvershootEffectColor
        {
            get
            {
                Vector4 temp = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
                GetProperty(Scrollable.Property.OVERSHOOT_EFFECT_COLOR).Get(temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.OVERSHOOT_EFFECT_COLOR, new Tizen.NUI.PropertyValue(value));
            }
        }
        public float OvershootAnimationSpeed
        {
            get
            {
                float temp = 0.0f;
                GetProperty(Scrollable.Property.OVERSHOOT_ANIMATION_SPEED).Get(out temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.OVERSHOOT_ANIMATION_SPEED, new Tizen.NUI.PropertyValue(value));
            }
        }
        public bool OvershootEnabled
        {
            get
            {
                bool temp = false;
                GetProperty(Scrollable.Property.OVERSHOOT_ENABLED).Get(out temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.OVERSHOOT_ENABLED, new Tizen.NUI.PropertyValue(value));
            }
        }
        public Vector2 OvershootSize
        {
            get
            {
                Vector2 temp = new Vector2(0.0f, 0.0f);
                GetProperty(Scrollable.Property.OVERSHOOT_SIZE).Get(temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.OVERSHOOT_SIZE, new Tizen.NUI.PropertyValue(value));
            }
        }
        public int ScrollToAlphaFunction
        {
            get
            {
                int temp = 0;
                GetProperty(Scrollable.Property.SCROLL_TO_ALPHA_FUNCTION).Get(out temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.SCROLL_TO_ALPHA_FUNCTION, new Tizen.NUI.PropertyValue(value));
            }
        }
        public Vector2 ScrollRelativePosition
        {
            get
            {
                Vector2 temp = new Vector2(0.0f, 0.0f);
                GetProperty(Scrollable.Property.SCROLL_RELATIVE_POSITION).Get(temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.SCROLL_RELATIVE_POSITION, new Tizen.NUI.PropertyValue(value));
            }
        }
        public Vector2 ScrollPositionMin
        {
            get
            {
                Vector2 temp = new Vector2(0.0f, 0.0f);
                GetProperty(Scrollable.Property.SCROLL_POSITION_MIN).Get(temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.SCROLL_POSITION_MIN, new Tizen.NUI.PropertyValue(value));
            }
        }
        public Vector2 ScrollPositionMax
        {
            get
            {
                Vector2 temp = new Vector2(0.0f, 0.0f);
                GetProperty(Scrollable.Property.SCROLL_POSITION_MAX).Get(temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.SCROLL_POSITION_MAX, new Tizen.NUI.PropertyValue(value));
            }
        }
        public bool CanScrollVertical
        {
            get
            {
                bool temp = false;
                GetProperty(Scrollable.Property.CAN_SCROLL_VERTICAL).Get(out temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.CAN_SCROLL_VERTICAL, new Tizen.NUI.PropertyValue(value));
            }
        }
        public bool CanScrollHorizontal
        {
            get
            {
                bool temp = false;
                GetProperty(Scrollable.Property.CAN_SCROLL_HORIZONTAL).Get(out temp);
                return temp;
            }
            set
            {
                SetProperty(Scrollable.Property.CAN_SCROLL_HORIZONTAL, new Tizen.NUI.PropertyValue(value));
            }
        }

    }

}

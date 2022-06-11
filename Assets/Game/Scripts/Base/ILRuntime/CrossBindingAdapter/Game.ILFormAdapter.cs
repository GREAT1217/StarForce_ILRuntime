using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace Game
{   
    public class ILFormAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(Game.ILForm);
            }
        }

        public override Type AdaptorType
        {
            get
            {
                return typeof(Adapter);
            }
        }

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adapter(appdomain, instance);
        }

        public class Adapter : Game.ILForm, CrossBindingAdaptorType
        {
            CrossBindingMethodInfo<System.Object> mOnInit_0 = new CrossBindingMethodInfo<System.Object>("OnInit");
            CrossBindingMethodInfo mOnRecycle_1 = new CrossBindingMethodInfo("OnRecycle");
            CrossBindingMethodInfo<System.Object> mOnOpen_2 = new CrossBindingMethodInfo<System.Object>("OnOpen");
            CrossBindingMethodInfo<System.Boolean, System.Object> mOnClose_3 = new CrossBindingMethodInfo<System.Boolean, System.Object>("OnClose");
            CrossBindingMethodInfo mOnPause_4 = new CrossBindingMethodInfo("OnPause");
            CrossBindingMethodInfo mOnResume_5 = new CrossBindingMethodInfo("OnResume");
            CrossBindingMethodInfo mOnCover_6 = new CrossBindingMethodInfo("OnCover");
            CrossBindingMethodInfo mOnReveal_7 = new CrossBindingMethodInfo("OnReveal");
            CrossBindingMethodInfo<System.Object> mOnRefocus_8 = new CrossBindingMethodInfo<System.Object>("OnRefocus");
            CrossBindingMethodInfo<System.Single, System.Single> mOnUpdate_9 = new CrossBindingMethodInfo<System.Single, System.Single>("OnUpdate");
            CrossBindingMethodInfo<System.Int32, System.Int32> mOnDepthChanged_10 = new CrossBindingMethodInfo<System.Int32, System.Int32>("OnDepthChanged");
            CrossBindingMethodInfo<System.Boolean> mInternalSetVisible_11 = new CrossBindingMethodInfo<System.Boolean>("InternalSetVisible");

            bool isInvokingToString;
            ILTypeInstance instance;
            ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } }

            protected override void OnInit(System.Object userData)
            {
                if (mOnInit_0.CheckShouldInvokeBase(this.instance))
                    base.OnInit(userData);
                else
                    mOnInit_0.Invoke(this.instance, userData);
            }

            protected override void OnRecycle()
            {
                if (mOnRecycle_1.CheckShouldInvokeBase(this.instance))
                    base.OnRecycle();
                else
                    mOnRecycle_1.Invoke(this.instance);
            }

            protected override void OnOpen(System.Object userData)
            {
                if (mOnOpen_2.CheckShouldInvokeBase(this.instance))
                    base.OnOpen(userData);
                else
                    mOnOpen_2.Invoke(this.instance, userData);
            }

            protected override void OnClose(System.Boolean isShutdown, System.Object userData)
            {
                if (mOnClose_3.CheckShouldInvokeBase(this.instance))
                    base.OnClose(isShutdown, userData);
                else
                    mOnClose_3.Invoke(this.instance, isShutdown, userData);
            }

            protected override void OnPause()
            {
                if (mOnPause_4.CheckShouldInvokeBase(this.instance))
                    base.OnPause();
                else
                    mOnPause_4.Invoke(this.instance);
            }

            protected override void OnResume()
            {
                if (mOnResume_5.CheckShouldInvokeBase(this.instance))
                    base.OnResume();
                else
                    mOnResume_5.Invoke(this.instance);
            }

            protected override void OnCover()
            {
                if (mOnCover_6.CheckShouldInvokeBase(this.instance))
                    base.OnCover();
                else
                    mOnCover_6.Invoke(this.instance);
            }

            protected override void OnReveal()
            {
                if (mOnReveal_7.CheckShouldInvokeBase(this.instance))
                    base.OnReveal();
                else
                    mOnReveal_7.Invoke(this.instance);
            }

            protected override void OnRefocus(System.Object userData)
            {
                if (mOnRefocus_8.CheckShouldInvokeBase(this.instance))
                    base.OnRefocus(userData);
                else
                    mOnRefocus_8.Invoke(this.instance, userData);
            }

            protected override void OnUpdate(System.Single elapseSeconds, System.Single realElapseSeconds)
            {
                if (mOnUpdate_9.CheckShouldInvokeBase(this.instance))
                    base.OnUpdate(elapseSeconds, realElapseSeconds);
                else
                    mOnUpdate_9.Invoke(this.instance, elapseSeconds, realElapseSeconds);
            }

            protected override void OnDepthChanged(System.Int32 uiGroupDepth, System.Int32 depthInUIGroup)
            {
                if (mOnDepthChanged_10.CheckShouldInvokeBase(this.instance))
                    base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
                else
                    mOnDepthChanged_10.Invoke(this.instance, uiGroupDepth, depthInUIGroup);
            }

            protected override void InternalSetVisible(System.Boolean visible)
            {
                if (mInternalSetVisible_11.CheckShouldInvokeBase(this.instance))
                    base.InternalSetVisible(visible);
                else
                    mInternalSetVisible_11.Invoke(this.instance, visible);
            }

            public override string ToString()
            {
                IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
                m = instance.Type.GetVirtualMethod(m);
                if (m == null || m is ILMethod)
                {
                    if (!isInvokingToString)
                    {
                        isInvokingToString = true;
                        string res = instance.ToString();
                        isInvokingToString = false;
                        return res;
                    }
                    else
                        return instance.Type.FullName;
                }
                else
                    return instance.Type.FullName;
            }
        }
    }
}


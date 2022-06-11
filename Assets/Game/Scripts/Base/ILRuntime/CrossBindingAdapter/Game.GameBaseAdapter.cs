using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace Game
{   
    public class GameBaseAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(Game.GameBase);
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

        public class Adapter : Game.GameBase, CrossBindingAdaptorType
        {
            CrossBindingMethodInfo mInitialize_0 = new CrossBindingMethodInfo("Initialize");
            CrossBindingMethodInfo mShutdown_1 = new CrossBindingMethodInfo("Shutdown");
            CrossBindingMethodInfo<System.Single, System.Single> mUpdate_2 = new CrossBindingMethodInfo<System.Single, System.Single>("Update");
            CrossBindingMethodInfo<System.Object, GameFramework.Event.GameEventArgs> mOnShowEntitySuccess_3 = new CrossBindingMethodInfo<System.Object, GameFramework.Event.GameEventArgs>("OnShowEntitySuccess");
            CrossBindingMethodInfo<System.Object, GameFramework.Event.GameEventArgs> mOnShowEntityFailure_4 = new CrossBindingMethodInfo<System.Object, GameFramework.Event.GameEventArgs>("OnShowEntityFailure");

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

            public override void Initialize()
            {
                if (mInitialize_0.CheckShouldInvokeBase(this.instance))
                    base.Initialize();
                else
                    mInitialize_0.Invoke(this.instance);
            }

            public override void Shutdown()
            {
                if (mShutdown_1.CheckShouldInvokeBase(this.instance))
                    base.Shutdown();
                else
                    mShutdown_1.Invoke(this.instance);
            }

            public override void Update(System.Single elapseSeconds, System.Single realElapseSeconds)
            {
                if (mUpdate_2.CheckShouldInvokeBase(this.instance))
                    base.Update(elapseSeconds, realElapseSeconds);
                else
                    mUpdate_2.Invoke(this.instance, elapseSeconds, realElapseSeconds);
            }

            protected override void OnShowEntitySuccess(System.Object sender, GameFramework.Event.GameEventArgs e)
            {
                if (mOnShowEntitySuccess_3.CheckShouldInvokeBase(this.instance))
                    base.OnShowEntitySuccess(sender, e);
                else
                    mOnShowEntitySuccess_3.Invoke(this.instance, sender, e);
            }

            protected override void OnShowEntityFailure(System.Object sender, GameFramework.Event.GameEventArgs e)
            {
                if (mOnShowEntityFailure_4.CheckShouldInvokeBase(this.instance))
                    base.OnShowEntityFailure(sender, e);
                else
                    mOnShowEntityFailure_4.Invoke(this.instance, sender, e);
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


using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace Game
{   
    public class ObjectBaseAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(GameFramework.ObjectPool.ObjectBase);
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

        public class Adapter : GameFramework.ObjectPool.ObjectBase, CrossBindingAdaptorType
        {
            CrossBindingFunctionInfo<System.Boolean> mget_CustomCanReleaseFlag_0 = new CrossBindingFunctionInfo<System.Boolean>("get_CustomCanReleaseFlag");
            CrossBindingMethodInfo mClear_1 = new CrossBindingMethodInfo("Clear");

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

            public override void Clear()
            {
                if (mClear_1.CheckShouldInvokeBase(this.instance))
                    base.Clear();
                else
                    mClear_1.Invoke(this.instance);
            }

            protected override void Release(bool isShutdown)
            {
                
            }

            public override System.Boolean CustomCanReleaseFlag
            {
            get
            {
                if (mget_CustomCanReleaseFlag_0.CheckShouldInvokeBase(this.instance))
                    return base.CustomCanReleaseFlag;
                else
                    return mget_CustomCanReleaseFlag_0.Invoke(this.instance);

            }
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


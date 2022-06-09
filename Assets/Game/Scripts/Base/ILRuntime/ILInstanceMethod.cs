using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ILRuntime实例方法。
    /// </summary>
    public class ILInstanceMethod
    {
        /// <summary>
        /// 热更新层类的实例。
        /// </summary>
        private object m_ILObject;

        /// <summary>
        /// 热更新层类的实例方法。
        /// </summary>
        private IMethod m_ILMethod;

        /// <summary>
        /// 方法参数缓存。
        /// </summary>
        private object[] m_Params;

        public ILInstanceMethod(object ilObject, string typeName, string methodName, int paramCount)
        {
            m_ILObject = ilObject;
            m_ILMethod = GameEntry.ILRuntime.AppDomain.LoadedTypes[typeName].GetMethod(methodName, paramCount);
            m_Params = paramCount > 0 ? new object[paramCount] : null;
        }

        public void Invoke()
        {
            GameEntry.ILRuntime.AppDomain.Invoke(m_ILMethod, m_ILObject, m_Params);
        }

        public void Invoke(object a)
        {
            m_Params[0] = a;
            GameEntry.ILRuntime.AppDomain.Invoke(m_ILMethod, m_ILObject, m_Params);
        }

        public void Invoke(object a, object b)
        {
            m_Params[0] = a;
            m_Params[1] = b;
            GameEntry.ILRuntime.AppDomain.Invoke(m_ILMethod, m_ILObject, m_Params);
        }

        public void Invoke(object a, object b, object c)
        {
            m_Params[0] = a;
            m_Params[1] = b;
            m_Params[2] = c;
            GameEntry.ILRuntime.AppDomain.Invoke(m_ILMethod, m_ILObject, m_Params);
        }
    }
}

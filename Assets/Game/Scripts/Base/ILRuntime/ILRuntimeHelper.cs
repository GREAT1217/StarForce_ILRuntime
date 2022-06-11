using System;
using GameFramework.Event;
using GameFramework.Resource;
using ILRuntime.Runtime.Intepreter;
using UnityEngine.Events;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Game
{
    public static class ILRuntimeHelper
    {
        public static void InitILRuntime(AppDomain appDomain)
        {
            // 这里做一些ILRuntime的注册
            
            // 跨域继承适配器
            appDomain.RegisterCrossBindingAdaptor(new GameBaseAdapter());
            appDomain.RegisterCrossBindingAdaptor(new ILFormAdapter());
            appDomain.RegisterCrossBindingAdaptor(new ILEntityAdapter());
            appDomain.RegisterCrossBindingAdaptor(new ILTargetableObjectAdapter());
            appDomain.RegisterCrossBindingAdaptor(new ProcedureILBaseAdapter());

            // 委托适配器
            appDomain.DelegateManager.RegisterMethodDelegate<float>();
            appDomain.DelegateManager.RegisterMethodDelegate<object, ILTypeInstance>();
            appDomain.DelegateManager.RegisterMethodDelegate<object, GameEventArgs>();
            appDomain.DelegateManager.RegisterMethodDelegate<string, LoadResourceStatus, string, object>();
            appDomain.DelegateManager.RegisterMethodDelegate<string, object, float, object>();
            appDomain.DelegateManager.RegisterMethodDelegate<bool>();
            appDomain.DelegateManager.RegisterMethodDelegate<object>();
            appDomain.DelegateManager.RegisterDelegateConvertor<LoadAssetFailureCallback>(act =>
            {
                return new LoadAssetFailureCallback((assetName, status, errorMessage, userData) =>
                {
                    ((Action<string, LoadResourceStatus, string, object>)act)(assetName, status, errorMessage, userData);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<LoadAssetSuccessCallback>(act =>
            {
                return new LoadAssetSuccessCallback((assetName, asset, duration, userData) =>
                {
                    ((Action<string, object, float, object>)act)(assetName, asset, duration, userData);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<bool>>(act =>
            {
                return new UnityAction<bool>(arg0 =>
                {
                    ((Action<bool>)act)(arg0);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction>(action =>
            {
                return new UnityAction(() =>
                {
                    ((Action)action)();
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<float>>(action =>
            {
                return new UnityAction<float>(a =>
                {
                    ((Action<float>)action)(a);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<GameEventArgs>>(act =>
            {
                return new EventHandler<GameEventArgs>((sender, e) =>
                {
                    ((Action<object, GameEventArgs>)act)(sender, e);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<ILTypeInstance>>(act =>
            {
                return new EventHandler<ILTypeInstance>((sender, e) =>
                {
                    ((Action<object, ILTypeInstance>)act)(sender, e);
                });
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<GameFramework.GameFrameworkAction<object>>((act) =>
            {
                return new GameFramework.GameFrameworkAction<object>((obj) =>
                {
                    ((Action<object>)act)(obj);
                });
            });
        }
    }
}

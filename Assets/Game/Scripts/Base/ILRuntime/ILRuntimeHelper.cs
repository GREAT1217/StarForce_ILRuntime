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
            appDomain.RegisterCrossBindingAdaptor(new GameBaseAdapter());

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
                return new UnityAction<Boolean>(arg0 =>
                {
                    ((Action<Boolean>)act)(arg0);
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
        }
    }
}

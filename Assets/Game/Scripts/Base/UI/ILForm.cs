using ILRuntime.CLR.TypeSystem;
using UnityEngine;

namespace Game
{
    public class ILForm : UGuiForm
    {
        public ReferenceCollector ReferenceCollector
        {
            get;
            private set;
        }

        // 热更新层的方法缓存。
        private ILInstanceMethod m_OnRecycle;
        private ILInstanceMethod m_OnOpen;
        private ILInstanceMethod m_OnClose;
        private ILInstanceMethod m_OnPause;
        private ILInstanceMethod m_OnResume;
        private ILInstanceMethod m_OnCover;
        private ILInstanceMethod m_OnReveal;
        private ILInstanceMethod m_OnRefocus;
        private ILInstanceMethod m_OnUpdate;
        private ILInstanceMethod m_OnDepthChanged;

        /// <summary>
        /// 热更新层的界面实例。
        /// </summary>
        public object HotfixForm
        {
            get;
            set;
        }

        protected override void OnInit(object userData)
        {
            ILFormUserData ilFormUserData = (ILFormUserData)userData;
            if (ilFormUserData == null)
            {
                return;
            }

            base.OnInit(userData);
            
            ReferenceCollector = GetComponent<ReferenceCollector>();

            // 获取热更新层的实例。
            IType type = GameEntry.ILRuntime.AppDomain.LoadedTypes[ilFormUserData.HotLogicTypeFullName];
            HotfixForm = ((ILType)type).Instantiate();
            ilFormUserData.ILForm = this;

            // 获取热更新层的方法并缓存。
            m_OnRecycle = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnRecycle", 0);
            m_OnOpen = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnOpen", 1);
            m_OnClose = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnClose", 2);
            m_OnPause = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnPause", 0);
            m_OnResume = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnResume", 0);
            m_OnCover = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnCover", 0);
            m_OnReveal = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnReveal", 0);
            m_OnRefocus = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnRefocus", 1);
            m_OnUpdate = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnUpdate", 2);
            m_OnDepthChanged = new ILInstanceMethod(HotfixForm, ilFormUserData.HotLogicTypeFullName, "OnDepthChanged", 2);

            // 调用热更新层 UIForm 的 OnInit()
            GameEntry.ILRuntime.AppDomain.Invoke(ilFormUserData.HotLogicTypeFullName, "OnInit", HotfixForm, ilFormUserData);
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();

            m_OnRecycle.Invoke();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_OnOpen?.Invoke(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);

            m_OnClose?.Invoke(userData);
        }

        protected override void OnPause()
        {
            base.OnPause();

            m_OnPause?.Invoke();
        }

        protected override void OnResume()
        {
            base.OnResume();

            m_OnResume?.Invoke();
        }

        protected override void OnCover()
        {
            base.OnCover();

            m_OnCover?.Invoke();
        }

        protected override void OnReveal()
        {
            base.OnReveal();

            m_OnReveal?.Invoke();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);

            m_OnRefocus?.Invoke(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            m_OnUpdate?.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);

            m_OnDepthChanged?.Invoke(uiGroupDepth, uiGroupDepth);
        }
    }
}

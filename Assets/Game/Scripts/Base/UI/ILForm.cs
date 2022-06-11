using ILRuntime.CLR.TypeSystem;

namespace Game
{
    public class ILForm : UGuiForm
    {
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

        public ReferenceCollector ReferenceCollector
        {
            get;
            private set;
        }

        protected override void OnInit(object userData)
        {
            ILUserData data = userData as ILUserData;
            if (data == null)
            {
                return;
            }

            base.OnInit(data.UserData);

            ReferenceCollector = GetComponent<ReferenceCollector>();

            // 获取热更新层的实例。
            IType type = GameEntry.ILRuntime.AppDomain.LoadedTypes[data.HotfixTypeName];
            HotfixForm = ((ILType)type).Instantiate();

            // 获取热更新层的方法并缓存。
            m_OnRecycle = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnRecycle", 0);
            m_OnOpen = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnOpen", 1);
            m_OnClose = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnClose", 2);
            m_OnPause = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnPause", 0);
            m_OnResume = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnResume", 0);
            m_OnCover = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnCover", 0);
            m_OnReveal = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnReveal", 0);
            m_OnRefocus = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnRefocus", 1);
            m_OnUpdate = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnUpdate", 2);
            m_OnDepthChanged = new ILInstanceMethod(HotfixForm, data.HotfixTypeName, "OnDepthChanged", 2);

            // 调用热更新层的 OnInit()
            data.ILLogic = this;
            GameEntry.ILRuntime.AppDomain.Invoke(data.HotfixTypeName, "OnInit", HotfixForm, data);
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();

            m_OnRecycle.Invoke();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_OnOpen.Invoke(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_OnClose.Invoke(userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
            m_OnPause.Invoke();
        }

        protected override void OnResume()
        {
            base.OnResume();
            m_OnResume.Invoke();
        }

        protected override void OnCover()
        {
            base.OnCover();
            m_OnCover.Invoke();
        }

        protected override void OnReveal()
        {
            base.OnReveal();
            m_OnReveal.Invoke();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
            m_OnRefocus.Invoke(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_OnUpdate.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            m_OnDepthChanged.Invoke(uiGroupDepth, uiGroupDepth);
        }
    }
}

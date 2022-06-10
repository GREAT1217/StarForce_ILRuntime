using ILRuntime.CLR.TypeSystem;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public abstract class ILTargetableObject : TargetableObject
    {
        public ReferenceCollector ReferenceCollector
        {
            get;
            private set;
        }

        // 热更新层的方法缓存。
        private ILInstanceMethod m_OnRecycle;
        private ILInstanceMethod m_OnShow;
        private ILInstanceMethod m_OnHide;
        private ILInstanceMethod m_OnAttached;
        private ILInstanceMethod m_OnDetached;
        private ILInstanceMethod m_OnAttachTo;
        private ILInstanceMethod m_OnDetachFrom;
        private ILInstanceMethod m_OnUpdate;
        private ILInstanceMethod m_InternalSetVisible;
        private ILInstanceMethod m_OnTriggerEnter;
        private ILInstanceMethod m_OnTriggerExit;

        /// <summary>
        /// 热更新层的实体实例。
        /// </summary>
        public object HotfixEntity
        {
            get;
            set;
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            ILUserData data = userData as ILUserData;
            if (data == null)
            {
                return;
            }

            base.OnInit(userData);

            ReferenceCollector = GetComponent<ReferenceCollector>();

            // 获取热更新层的实例。
            IType type = GameEntry.ILRuntime.AppDomain.LoadedTypes[data.HotLogicTypeFullName];
            HotfixEntity = ((ILType)type).Instantiate();

            // 获取热更新层的方法并缓存。
            m_OnRecycle = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnRecycle", 0);
            m_OnShow = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnShow", 1);
            m_OnHide = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnHide", 2);
            m_OnAttached = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnAttached", 3);
            m_OnDetached = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnDetached", 2);
            m_OnAttachTo = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnAttachTo", 3);
            m_OnDetachFrom = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnDetachFrom", 2);
            m_OnUpdate = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnUpdate", 2);
            m_InternalSetVisible = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "InternalSetVisible", 1);
            m_OnTriggerEnter = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnTriggerEnter", 1);
            m_OnTriggerExit = new ILInstanceMethod(HotfixEntity, data.HotLogicTypeFullName, "OnTriggerExit", 1);

            // 调用热更新层的 OnInit()
            data.ILLogic = this;
            GameEntry.ILRuntime.AppDomain.Invoke(data.HotLogicTypeFullName, "OnInit", HotfixEntity, data);
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
            m_OnRecycle.Invoke();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_OnShow.Invoke(userData);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            m_OnHide.Invoke(isShutdown, userData);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            m_OnAttached.Invoke(childEntity, parentTransform, userData);
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            m_OnDetached.Invoke(childEntity, userData);
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            m_OnAttachTo.Invoke(parentEntity, parentTransform, userData);
        }

        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
            m_OnDetachFrom.Invoke(parentEntity, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_OnUpdate.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected override void InternalSetVisible(bool visible)
        {
            base.InternalSetVisible(visible);
            m_InternalSetVisible.Invoke(visible);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            m_OnTriggerEnter.Invoke(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            m_OnTriggerExit.Invoke(other);
        }
    }
}

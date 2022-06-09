using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class AboutForm : HotfixForm
    {
        private RectTransform m_Transform = null;
        private float m_ScrollSpeed = 100f;
        
       private float m_InitPosition = 0f;

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);

            CanvasScaler canvasScaler = ILForm.GetComponentInParent<CanvasScaler>();
            if (canvasScaler == null)
            {
                Log.Warning("Can not find CanvasScaler component.");
                return;
            }

            m_InitPosition = -0.5f * canvasScaler.referenceResolution.x * Screen.height / Screen.width;
            
            ReferenceCollector collector = ILForm.ReferenceCollector;
            m_Transform = (RectTransform)collector.Get("trans_Content", typeof(RectTransform));
            (collector.Get("bt_Back", typeof(CommonButton)) as CommonButton).OnClick.AddListener(Close);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_Transform.SetLocalPositionY(m_InitPosition);

            // 换个音乐
            GameEntry.Sound.PlayMusic(3);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);

            // 还原音乐
            GameEntry.Sound.PlayMusic(1);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            m_Transform.AddLocalPositionY(m_ScrollSpeed * elapseSeconds);
            if (m_Transform.localPosition.y > m_Transform.sizeDelta.y - m_InitPosition)
            {
                m_Transform.SetLocalPositionY(m_InitPosition);
            }
        }
    }
}

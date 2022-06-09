//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class HPBarItem
    {
        public ReferenceCollector ReferenceCollector { get; private set; }
        
        private const float AnimationSeconds = 0.3f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutSeconds = 0.3f;

        private Slider m_HPBar;
        private Canvas m_ParentCanvas;
        private RectTransform m_CachedTransform;
        private CanvasGroup m_CachedCanvasGroup;
        private Entity m_Owner;
        private int m_OwnerId;

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public HPBarItem(ReferenceCollector referenceCollector)
        {
            ReferenceCollector = referenceCollector;
            ReferenceCollector.ComponentView.Component = this;

            m_HPBar = ReferenceCollector.Get("HPBar", typeof(Slider)) as Slider;
            
            //获取组件
            m_CachedTransform = ReferenceCollector.CachedTransform as RectTransform;
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }
	
            m_CachedCanvasGroup = ReferenceCollector.GetComponent(typeof(CanvasGroup)) as CanvasGroup;
            if (m_CachedCanvasGroup == null)
            {
                Log.Error("CanvasGroup is invalid.");
            }
        }

        public void Init(Entity owner, Canvas parentCanvas, float fromHPRatio, float toHPRatio)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            m_ParentCanvas = parentCanvas;

            ReferenceCollector.gameObject.SetActive(true);
            ReferenceCollector.StopAllCoroutines();

            m_CachedCanvasGroup.alpha = 1f;
            if (m_Owner != owner || m_OwnerId != owner.Id)
            {
                m_HPBar.value = fromHPRatio;
                m_Owner = owner;
                m_OwnerId = owner.Id;
            }

            Refresh();

            ReferenceCollector.StartCoroutine(HPBarCo(toHPRatio, AnimationSeconds, KeepSeconds, FadeOutSeconds));
        }

        public bool Refresh()
        {
            if (m_CachedCanvasGroup.alpha <= 0f)
            {
                return false;
            }

            if (m_Owner != null && Owner.Available && Owner.Id == m_OwnerId)
            {
                Vector3 worldPosition = m_Owner.CachedTransform.position + Vector3.forward;
                Vector3 screenPosition = GameEntry.Scene.MainCamera.WorldToScreenPoint(worldPosition);

                Vector2 position;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_ParentCanvas.transform, screenPosition,
                    m_ParentCanvas.worldCamera, out position))
                {
                    m_CachedTransform.localPosition = position;
                }
            }

            return true;
        }

        public void Reset()
        {
            ReferenceCollector.StopAllCoroutines();
            m_CachedCanvasGroup.alpha = 1f;
            m_HPBar.value = 1f;
            m_Owner = null;
            ReferenceCollector.gameObject.SetActive(false);
        }

        private IEnumerator HPBarCo(float value, float animationDuration, float keepDuration, float fadeOutDuration)
        {
            yield return m_HPBar.SmoothValue(value, animationDuration);
            yield return new WaitForSeconds(keepDuration);
            yield return m_CachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration);
        }
    }
}

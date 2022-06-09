using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ComponentReference : MonoBehaviour
    {
#if UNITY_EDITOR
        [System.Serializable]
        public class ReferenceData
        {
            public string m_Name;
            public Component m_Component;

            public ReferenceData(string name, Component component)
            {
                m_Name = name;
                m_Component = component;
            }
        }

        [SerializeField]
        public List<ReferenceData> m_ReferenceDatas;
#endif
        
        [SerializeField]
        private List<Component> m_Components;

        public T GetComponent<T>(int index) where T : Component
        {
            if (m_Components == null || index < 0 || index >= m_Components.Count)
            {
                return null;
            }

            // 这里不检查是否为空了。可以在游戏框架内检查并输出。
            return m_Components[index] as T;
        }
    }
}

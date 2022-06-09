//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.ObjectPool;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class HPBarManager
    {
        private GameObject m_HPBarItemTemplate = null;
        private Transform m_HPBarInstanceRoot = null;
        private int m_InstancePoolCapacity = 16;

        private IObjectPool<HPBarItemObject> m_HPBarItemObjectPool = null;
        private List<HPBarItem> m_ActiveHPBarItems = null;
        private Canvas m_CachedCanvas = null;

        public HPBarManager()
        {
            GameObject InstaceObj = new GameObject("HP Bar Instance");
            InstaceObj.transform.SetParent(GameHotfixEntry.m_HotfixNode);
            m_HPBarInstanceRoot = InstaceObj.transform;

            m_CachedCanvas = InstaceObj.AddComponent<Canvas>();
            m_CachedCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = InstaceObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0;

            m_HPBarItemObjectPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<HPBarItemObject>("HPBarItem", m_InstancePoolCapacity);
            m_ActiveHPBarItems = new List<HPBarItem>();

            GameEntry.Resource.LoadAsset(AssetUtility.GetUIItemAsset("HPBarItem"), Constant.AssetPriority.UIItemAsset, new LoadAssetCallbacks(
                    (assetName, asset, duration, userData) =>
                    {
                        Log.Info("Load HPBarItem OK.");
                        m_HPBarItemTemplate = asset as GameObject;
                    },
                    (assetName, status, errorMessage, userData) =>
                    {
                        Log.Error("Can not load HPBarItem from '{0}' with error message '{1}'.", assetName, errorMessage);
                    }
                )
            );
        }

        public void Update()
        {
            for (int i = m_ActiveHPBarItems.Count - 1; i >= 0; i--)
            {
                HPBarItem hpBarItem = m_ActiveHPBarItems[i];
                if (hpBarItem.Refresh())
                {
                    continue;
                }

                HideHPBar(hpBarItem);
            }
        }

        public void ShowHPBar(Entity entity, float fromHPRatio, float toHPRatio)
        {
            if (entity == null)
            {
                Log.Warning("Entity is invalid.");
                return;
            }

            HPBarItem hpBarItem = GetActiveHPBarItem(entity);
            if (hpBarItem == null)
            {
                hpBarItem = CreateHPBarItem(entity);
                m_ActiveHPBarItems.Add(hpBarItem);
            }

            hpBarItem.Init(entity, m_CachedCanvas, fromHPRatio, toHPRatio);
        }

        private void HideHPBar(HPBarItem hpBarItem)
        {
            hpBarItem.Reset();
            m_ActiveHPBarItems.Remove(hpBarItem);
            m_HPBarItemObjectPool.Unspawn(hpBarItem);
        }

        private HPBarItem GetActiveHPBarItem(Entity entity)
        {
            if (entity == null)
            {
                return null;
            }

            for (int i = 0; i < m_ActiveHPBarItems.Count; i++)
            {
                if (m_ActiveHPBarItems[i].Owner == entity)
                {
                    return m_ActiveHPBarItems[i];
                }
            }

            return null;
        }

        private HPBarItem CreateHPBarItem(Entity entity)
        {
            HPBarItem hpBarItem = null;
            HPBarItemObject hpBarItemObject = m_HPBarItemObjectPool.Spawn();
            if (hpBarItemObject != null)
            {
                hpBarItem = (HPBarItem)hpBarItemObject.Target;
            }
            else
            {
                ReferenceCollector item = Object.Instantiate(m_HPBarItemTemplate).GetComponent<ReferenceCollector>();
                hpBarItem = new HPBarItem(item);

                Transform transform = item.transform;
                transform.SetParent(m_HPBarInstanceRoot, false);
                transform.localScale = Vector3.one;
                m_HPBarItemObjectPool.Register(HPBarItemObject.Create(hpBarItem), true);
            }

            return hpBarItem;
        }
    }
}

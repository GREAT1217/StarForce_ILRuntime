﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    /// <summary>
    /// 战机类。
    /// </summary>
    public abstract class Aircraft : HotfixTargetableObject
    {
        private AircraftData m_AircraftData = null;
        protected Thruster m_Thruster = null;
        protected List<Weapon> m_Weapons = new List<Weapon>();
        protected List<Armor> m_Armors = new List<Armor>();

        public override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AircraftData = userData as AircraftData;
            if (m_AircraftData == null)
            {
                Log.Error("Aircraft data is invalid.");
                return;
            }

            GameEntry.Entity.ShowThruster(m_AircraftData.GetThrusterData());

            List<WeaponData> weaponDatas = m_AircraftData.GetAllWeaponDatas();
            for (int i = 0; i < weaponDatas.Count; i++)
            {
                GameEntry.Entity.ShowWeapon(weaponDatas[i]);
            }

            List<ArmorData> armorDatas = m_AircraftData.GetAllArmorDatas();
            for (int i = 0; i < armorDatas.Count; i++)
            {
                GameEntry.Entity.ShowArmor(armorDatas[i]);
            }
        }

        public override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }

        public override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            if (childEntity is Thruster)
            {
                m_Thruster = (Thruster)childEntity;
                return;
            }

            if (childEntity is Weapon)
            {
                m_Weapons.Add((Weapon)childEntity);
                return;
            }

            if (childEntity is Armor)
            {
                m_Armors.Add((Armor)childEntity);
                return;
            }
        }

        public override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);

            if (childEntity is Thruster)
            {
                m_Thruster = null;
                return;
            }

            if (childEntity is Weapon)
            {
                m_Weapons.Remove((Weapon)childEntity);
                return;
            }

            if (childEntity is Armor)
            {
                m_Armors.Remove((Armor)childEntity);
                return;
            }
        }

        public override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);

            GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), m_AircraftData.DeadEffectId)
            {
                Position = ILTargetableObject.CachedTransform.localPosition,
            });
            GameEntry.Sound.PlaySound(m_AircraftData.DeadSoundId);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(m_AircraftData.Camp, m_AircraftData.HP, 0, m_AircraftData.Defense);
        }
    }
}
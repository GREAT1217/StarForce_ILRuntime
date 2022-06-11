using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class EnemyAircraft : Aircraft
    {
        private AircraftData m_AircraftData = null;
        private Vector3 m_TargetPosition;

        public override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AircraftData = userData as AircraftData;
            if (m_AircraftData == null)
            {
                Log.Error("Enemy aircraft data is invalid.");
            }

            m_TargetPosition = ILTargetableObject.CachedTransform.localPosition;
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (m_AircraftData == null)
            {
                return;
            }

            Move(elapseSeconds);

            Attack();
        }

        public void MoveTo(Vector3 position)
        {
            m_TargetPosition = position;
        }

        private void Move(float elapseSeconds)
        {
            ILTargetableObject.CachedTransform.localPosition = Vector3.LerpUnclamped(ILTargetableObject.CachedTransform.localPosition, m_TargetPosition, m_AircraftData.Speed * elapseSeconds);
        }

        private void Attack()
        {
            for (int i = 0; i < m_Weapons.Count; i++)
            {
                m_Weapons[i].TryAttack();
            }
        }
    }
}

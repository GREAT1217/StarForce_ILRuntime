using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class BossGame : GameBase
    {
        private float m_ElapseSeconds = 0f;
        private EnemyAircraft m_EnemyAircraft = null;

        public override void Initialize()
        {
            base.Initialize();

            GameEntry.Entity.ShowHotfixAircraft(typeof(EnemyAircraft), new AircraftData(GameEntry.Entity.GenerateSerialId(), 10001, CampType.Enemy)
            {
                Position = new Vector3(0f, 0f, 10f),
            });
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            if (m_EnemyAircraft == null)
            {
                return;
            }

            if (m_EnemyAircraft.ILTargetableObject.IsDead)
            {
                GameOver = true;
                return;
            }

            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds >= 1f)
            {
                m_ElapseSeconds = 0f;

                Vector3 targetPosition = m_MyAircraft.CachedTransform.localPosition;
                targetPosition.z = 10f;
                m_EnemyAircraft.MoveTo(targetPosition);
            }
        }

        protected override void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            base.OnShowEntitySuccess(sender, e);

            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            ILUserData userData = ne.UserData as ILUserData;
            if (userData == null)
            {
                return;
            }

            if (userData.HotfixTypeName == typeof(EnemyAircraft).ToString())
            {
                ILTargetableObject ilEntity = (ILTargetableObject)userData.ILLogic;
                m_EnemyAircraft = (EnemyAircraft)ilEntity.HotfixEntity;
            }
        }

        protected override void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            base.OnShowEntityFailure(sender, e);

            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }
    }
}
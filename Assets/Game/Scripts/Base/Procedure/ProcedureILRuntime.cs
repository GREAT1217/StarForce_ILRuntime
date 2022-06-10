using System;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ProcedureILRuntime : ProcedureBase
    {
        private bool m_ChangeProcedure;
        private Type m_ProcedureType;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // VarString hotfixProcedure = procedureOwner.GetData<VarString>("procedureName");

            GameEntry.ILRuntime.InitHotfix();
            GameEntry.ILRuntime.StartHotfix(this);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            GameEntry.ILRuntime.UpdateHotfix(elapseSeconds, realElapseSeconds);

            if (!m_ChangeProcedure)
            {
                return;
            }

            ChangeState(procedureOwner, m_ProcedureType);
        }

        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);

            GameEntry.ILRuntime.DestroyHotfix();
        }

        public void ChangeProcedure<T>() where T : ProcedureBase
        {
            m_ProcedureType = typeof(T);
            m_ChangeProcedure = true;
        }

        public void ChangeHotfixProcedure(string hotfixProcedureName)
        {
            
        }
    }
}

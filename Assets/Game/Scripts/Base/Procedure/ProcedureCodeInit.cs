using GameFramework.Fsm;
using GameFramework.Procedure;

namespace Game
{
    public class ProcedureCodeInit : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.ILRuntime.LoadHotfixDll();
        }
    }
}

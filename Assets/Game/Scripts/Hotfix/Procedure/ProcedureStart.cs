using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class ProcedureStart: ProcedureBase
    {
        public override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}

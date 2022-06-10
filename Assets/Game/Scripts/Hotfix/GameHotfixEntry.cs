using UnityEngine;

namespace Game.Hotfix
{
    public class GameHotfixEntry
    {
        public static FsmManager Fsm
        {
            get;
            private set;
        }

        public static ProcedureManager Procedure
        {
            get;
            private set;
        }

        // public static HPBarManager HPBar
        // {
        //     get;
        //     private set;
        // }

        // public static Transform m_HotfixNode
        // {
        //     get;
        //     private set;
        // }

        public static void Start()
        {
            // 删除原生对话框。
            GameEntry.BuiltinData.DestroyDialog();
            // 删除流程组件。
            GameEntry.Fsm.DestroyFsm<IProcedureManager>();

            /*
             * GameFramework 的 FSMManager 中使用的 Dictionary 存储的 FsmState。
             * Key 为 FsmState 的类型和名称组，Value 为 FsmState 的对象，ProcedureBase 作为 FsmState 的子类，也是这样存储的。
             * 但是 ILRuntime 中的类使用跨域继承时，Type 返回的是类的适配器 TypeAdapter，所以在 Hotfix 中使用继承会造成字典的 key 重复。
             * 所以在 Hotfix 中 Procedure 不使用跨域继承，而是这样重新实现一个 Procedure 组件。
             */

            // 创建热更新流程组件，初始化热更新流程。
            Fsm = new FsmManager();
            Procedure = new ProcedureManager();
            ProcedureBase[] procedures =
            {
                new ProcedureChangeScene(),
                new ProcedureMain(),
                new ProcedureMenu(),
                new ProcedurePreload(),
            };
            Procedure.Initialize(Fsm, procedures);
            Procedure.StartProcedure<ProcedurePreload>();

            // 初始化自定义组件。
            // m_HotfixNode = new GameObject("Hotfix").transform;
            // HPBar = new HPBarManager();
        }

        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            Fsm.Update(elapseSeconds, realElapseSeconds);
            Procedure.Update(elapseSeconds, realElapseSeconds);
            // HPBar.Update();
        }

        public static void Shutdown()
        {
            // Fsm.Shutdown();
            // Procedure.Shutdown();
        }
    }
}

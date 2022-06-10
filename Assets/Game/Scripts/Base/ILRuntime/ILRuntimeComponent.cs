using System.IO;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ILRuntimeComponent : GameFrameworkComponent
    {
        private IMethod m_Start;
        private IMethod m_Update;
        private IMethod m_Shutdown;

        /// <summary>
        /// ILRuntime入口对象
        /// </summary>
        public AppDomain AppDomain
        {
            get;
            private set;
        }

        public void InitAppDomain(byte[] dll, byte[] pdb = null)
        {
            // 首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
            AppDomain = new AppDomain();

            ILRuntimeHelper.InitILRuntime(AppDomain);

            if (pdb == null)
            {
                MemoryStream dllStream = new MemoryStream(dll);
                AppDomain.LoadAssembly(dllStream);
                // dll.Dispose();
            }
            else
            {
                // PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
                MemoryStream dllStream = new MemoryStream(dll);
                MemoryStream pdbStream = new MemoryStream(pdb);
                AppDomain.LoadAssembly(dllStream, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                AppDomain.DebugService.StartDebugService(56000);
                // dll.Dispose();
                // pdb.Dispose();
            }
        }

        public void InitHotfix()
        {
            string typeFullName = "Game.Hotfix.GameHotfixEntry";
            IType type = AppDomain.LoadedTypes[typeFullName];

            m_Start = type.GetMethod("Start", 1);
            m_Update = type.GetMethod("Update", 2);
            m_Shutdown = type.GetMethod("Shutdown", 0);
        }

        public void StartHotfix(ProcedureILRuntime procedure)
        {
            // AppDomain.Invoke(typeFullName, "Start", null, procedure);
            using (var ctx = AppDomain.BeginInvoke(m_Start))
            {
                ctx.PushObject(procedure);
                ctx.Invoke();
            }
        }

        public void UpdateHotfix(float elapseSeconds, float realElapseSeconds)
        {
            if (m_Update == null)
            {
                return;
            }

            using (var ctx = AppDomain.BeginInvoke(m_Update))
            {
                ctx.PushFloat(elapseSeconds);
                ctx.PushFloat(realElapseSeconds);
                ctx.Invoke();
            }
        }

        public void DestroyHotfix()
        {
            if (m_Shutdown == null)
            {
                return;
            }

            AppDomain.Invoke(m_Shutdown, null, null);
        }
    }
}

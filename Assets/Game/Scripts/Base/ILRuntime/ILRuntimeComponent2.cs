using System.Collections;
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

        /// <summary>
        /// 加载热更新DLL。
        /// </summary>
        public async void LoadHotfixDll()
        {
            // 首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
            AppDomain = new AppDomain();

            ILRuntimeHelper.InitILRuntime(AppDomain);

            TextAsset dllAsset = await GameEntry.Resource.AwaitLoadAsset<TextAsset>(AssetUtility.GetHotfixAsset("Game.Hotfix.dll"));
            var dll = new MemoryStream(dllAsset.bytes);
            Debug.Log(dll.Length);
            Log.Info("Hotfix.dll load complete.");

#if DEBUG

            // PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
            TextAsset pdbAsset = await GameEntry.Resource.AwaitLoadAsset<TextAsset>(AssetUtility.GetHotfixAsset("Game.Hotfix.pdb"));
            var pdb = new MemoryStream(pdbAsset.bytes);
            Log.Info("Hotfix.pdb load complete.");

            AppDomain.LoadAssembly(dll);
            // AppDomain.LoadAssembly(dll, pdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            // AppDomain.DebugService.StartDebugService(56000);

#else

            AppDomain.LoadAssembly(dll);

#endif

            StartHotfix();
        }

        private void StartHotfix()
        {
            string typeFullName = "Game.Hotfix.GameHotfixEntry";
            IType type = AppDomain.LoadedTypes[typeFullName];

            AppDomain.Invoke(typeFullName, "Start", null, null);

            m_Update = type.GetMethod("Update", 2);
            m_Shutdown = type.GetMethod("Shutdown", 0);
        }

        private void Update()
        {
            if (m_Update == null)
            {
                return;
            }

            using (var ctx = AppDomain.BeginInvoke(m_Update))
            {
                ctx.PushFloat(Time.deltaTime);
                ctx.PushFloat(Time.unscaledDeltaTime);
                ctx.Invoke();
            }
        }

        private void OnDestroy()
        {
            if (m_Shutdown == null)
            {
                return;
            }

            AppDomain.Invoke(m_Shutdown, null, null);
        }
    }
}

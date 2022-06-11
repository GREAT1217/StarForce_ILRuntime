using System.IO;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ProcedureCodeInit : ProcedureBase
    {
        private bool m_DllLoaded;
        private bool m_PdbLoaded;
        private MemoryStream m_DllStream;
        private MemoryStream m_PdbStream;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_DllLoaded = false;
            m_PdbLoaded = false;

            GameEntry.Resource.LoadAsset(AssetUtility.GetHotfixAsset("Game.Hotfix.dll"), new LoadAssetCallbacks(OnDllLoadedSuccess, OnDllLoadedFail));
            GameEntry.Resource.LoadAsset(AssetUtility.GetHotfixAsset("Game.Hotfix.pdb"), new LoadAssetCallbacks(OnPdbLoadedSuccess, OnPdbLoadedFail));
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

#if ILRuntimeDEBUG
            if (m_DllLoaded && m_PdbLoaded)
            {
                GameEntry.ILRuntime.Init(m_DllStream, m_PdbStream);
                ChangeState<ProcedurePreload>(procedureOwner);
            }
#else
            if (m_DllLoaded)
            {
                GameEntry.ILRuntime.InitAppDomain(m_DllStream);
                ChangeState<ProcedurePreload>(procedureOwner);
            }
#endif
        }

        private void OnDllLoadedSuccess(string assetName, object asset, float duration, object userdata)
        {
            TextAsset dll = (TextAsset)asset;
            m_DllStream = new MemoryStream(dll.bytes);
            m_DllLoaded = true;
            Log.Info("Load '{0}' OK.", assetName);
        }

        private void OnDllLoadedFail(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            Log.Error("Can not load '{0}' with error message '{1}'.", assetName, errorMessage);
        }

        private void OnPdbLoadedSuccess(string assetName, object asset, float duration, object userdata)
        {
            TextAsset pdb = (TextAsset)asset;
            m_PdbStream = new MemoryStream(pdb.bytes);
            m_PdbLoaded = true;
            Log.Info("Load '{0}' OK.", assetName);
        }

        private void OnPdbLoadedFail(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            Log.Error("Can not load '{0}' with error message '{1}'.", assetName, errorMessage);
        }
    }
}

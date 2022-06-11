using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            UnityGameFramework_Runtime_Log_Binding.Register(app);
            Game_ILUserData_Binding.Register(app);
            UnityGameFramework_Runtime_EntityLogic_Binding.Register(app);
            Game_EntityData_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            System_Type_Binding.Register(app);
            Game_EntityExtension_Binding.Register(app);
            Game_GameEntry_Binding.Register(app);
            UnityGameFramework_Runtime_DataTableComponent_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DREntity_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_Object_Binding.Register(app);
            Game_DREntity_Binding.Register(app);
            Game_AssetUtility_Binding.Register(app);
            UnityGameFramework_Runtime_EntityComponent_Binding.Register(app);
            Game_ProcedureILManager_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneSuccessEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_EventComponent_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneFailureEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneUpdateEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneDependencyAssetEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_SoundComponent_Binding.Register(app);
            UnityGameFramework_Runtime_SceneComponent_Binding.Register(app);
            UnityGameFramework_Runtime_BaseComponent_Binding.Register(app);
            GameFramework_Fsm_IFsm_1_IProcedureManager_Binding.Register(app);
            UnityGameFramework_Runtime_VarInt32_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRScene_Binding.Register(app);
            Game_DRScene_Binding.Register(app);
            Game_SoundExtension_Binding.Register(app);
            System_Single_Binding.Register(app);
            Game_Hotfix_SurvivalGame_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Byte_GameBase_Binding.Register(app);
            GameFramework_Variable_1_Byte_Binding.Register(app);
            Game_GameBase_Binding.Register(app);
            UnityGameFramework_Runtime_ConfigComponent_Binding.Register(app);
            UnityGameFramework_Runtime_OpenUIFormSuccessEventArgs_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityGameFramework_Runtime_UIFormLogic_Binding.Register(app);
            Game_UGuiForm_Binding.Register(app);
            UnityGameFramework_Runtime_VarByte_Binding.Register(app);
            System_String_Binding.Register(app);
            Game_UIExtension_Binding.Register(app);
            Game_DialogParams_Binding.Register(app);
            UnityGameFramework_Runtime_LocalizationComponent_Binding.Register(app);
            Game_ILForm_Binding.Register(app);
            Game_ReferenceCollector_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            Game_CommonButton_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            UnityGameFramework_Runtime_GameEntry_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}

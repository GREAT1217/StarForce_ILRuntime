using System;
using System.IO;
using GameFramework;
using ILRuntime.Runtime.CLRBinding;
using ILRuntime.Runtime.Enviorment;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Game.Editor
{
    public class ILRuntimeGenerator
    {
        private const string HotfixBuildPath = "Temp/ILRuntime/Build";
        private const string HotfixPath = "Assets/Game/Hotfix";
        private const string HotfixDllName = "Game.Hotfix.dll";
        private const string HotfixPdbName = "Game.Hotfix.pdb";

        private const string AdapterPath = "Assets/Game/Scripts/Base/ILRuntime/CrossBindingAdapter";
        private const string AdapterNameSpace = "Game";

        private const string CLRBindingPath = "Assets/Game/Scripts/Base/ILRuntime/CLRBinding";

        [MenuItem("Generator/Generate HotfixDll", false, 21)]
        public static void GenerateHotfixDLL()
        {
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
            string path = Utility.Text.Format("{0}/{1}", HotfixBuildPath, buildTarget);
            BuildDll(path, buildTarget);
        }

        private static void BuildDll(string path, BuildTarget buildTarget)
        {
            BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(buildTarget);
            ScriptCompilationSettings scriptCompilationSettings = new ScriptCompilationSettings();
            scriptCompilationSettings.group = group;
            scriptCompilationSettings.target = buildTarget;
            scriptCompilationSettings.options = ScriptCompilationOptions.DevelopmentBuild;

            IOUtility.CreateDirectoryIfNotExists(path);
            ScriptCompilationResult scriptCompilationResult = PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, path);
            foreach (var ass in scriptCompilationResult.assemblies)
            {
                Debug.LogFormat("Build assemblies : {0}", ass);
            }

            IOUtility.CreateDirectoryIfNotExists(HotfixPath);
            string oriFileName = Utility.Text.Format("{0}/{1}", path, HotfixDllName);
            string desFileName = AssetUtility.GetHotfixAsset(HotfixDllName);
            File.Copy(oriFileName, desFileName, true);
            oriFileName = Utility.Text.Format("{0}/{1}", path, HotfixPdbName);
            desFileName = AssetUtility.GetHotfixAsset(HotfixPdbName);
            File.Copy(oriFileName, desFileName, true);

            Debug.Log("Hotfix dll & pdb build complete.");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Generator/Generate Cross Binding Adapter", false, 22)]
        private static void GenerateCrossBindingAdapter()
        {
            IOUtility.CreateDirectoryIfNotExists(AdapterPath);

            // TODO Game 根据项目需求设置
            GenerateCrossBindingAdapter<GameBase>();
            GenerateCrossBindingAdapter<ILForm>();
            GenerateCrossBindingAdapter<ILEntity>();
            GenerateCrossBindingAdapter<ILTargetableObject>();
            GenerateCrossBindingAdapter<ProcedureILBase>();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Generator/Generate CLR Binding", false, 23)]
        private static void GenerateCLRBinding()
        {
            IOUtility.CreateDirectoryIfNotExists(CLRBindingPath);

            AppDomain appDomain = new AppDomain();
            string dllFileName = AssetUtility.GetHotfixAsset(HotfixDllName);
            using (FileStream dllStream = new FileStream(dllFileName, FileMode.Open, FileAccess.Read))
            {
                appDomain.LoadAssembly(dllStream);
                ILRuntimeHelper.InitILRuntime(appDomain);
                BindingCodeGenerator.GenerateBindingCode(appDomain, CLRBindingPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GenerateCrossBindingAdapter<T>() where T : class
        {
            Type type = typeof(T);
            string adapterCode = CrossBindingCodeGenerator.GenerateCrossBindingAdapterCode(type, AdapterNameSpace);
            // adapterCode = string.Format("using {0};\n{1}", type.Namespace, adapterCode);
            string adapterFileName = Utility.Path.GetRegularPath(Path.Combine(AdapterPath, type + "Adapter.cs"));
            using (StreamWriter sw = new StreamWriter(adapterFileName))
            {
                sw.WriteLine(adapterCode);
            }

            Debug.Log(Utility.Text.Format("Generate {0} Adapter Succeed.", type.ToString()));
        }
    }
}

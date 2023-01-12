using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

public class PackageVersionEditor
{

    [InitializeOnLoadMethod]
    private static void Init()
    {
        CompilationPipeline.compilationFinished -= OnCompilationFinished;
        CompilationPipeline.compilationFinished += OnCompilationFinished;
    }

    private static void OnCompilationFinished(object obj)
    {
        var assembly = typeof(PackageVersion).Assembly;
        SaveVersionInfo(assembly);
    }

    public static void SaveVersionInfo(System.Reflection.Assembly assembly)
    {
        string assemblyName = assembly.GetName().Name;
        var packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
        if (packageInfo == null)
        {
            Debug.LogError($"can not find {assemblyName}'s package info");
            return;
        }

        string directory = $"{Application.dataPath}/{PackageVersion.ASSET_PATH}";
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string aseetFile = $"Assets/{PackageVersion.ASSET_PATH}/{PackageVersion.ASSET_NAME}.asset";
        var assetData = AssetDatabase.LoadAssetAtPath<PackageVersionObject>(aseetFile);

        if (assetData == null)
        {
            assetData = ScriptableObject.CreateInstance<PackageVersionObject>();
            assetData.name = PackageVersion.ASSET_NAME;
            assetData.hideFlags = HideFlags.NotEditable;
            assetData.packages = new List<PackageInfo>();
            Debug.LogWarning($"Create {aseetFile}");
            AssetDatabase.CreateAsset(assetData, aseetFile);
        }

        bool dirty = false;
        var exits = assetData.packages.Where(package =>
        {
            var exit = package.Name.Equals(packageInfo.name);
            if (exit && (!package.Version.Equals(packageInfo.version) || !package.AssemblyName.Equals(assemblyName)))
            {
                package.Version = packageInfo.version;
                package.AssemblyName = assemblyName;
                dirty = true;
            }
            return exit;
        });

        if (exits.Count<PackageInfo>() == 0)
        {
            assetData.packages.Add(new PackageInfo() { Name = packageInfo.name, Version = packageInfo.version, AssemblyName = assemblyName });
            dirty = true;
        }

        if (dirty)
        {
            EditorUtility.SetDirty(assetData);
            Debug.LogWarning($"{packageInfo.name}({assemblyName}) changed to {packageInfo.version}");
            AssetDatabase.Refresh();
        }
    }
}

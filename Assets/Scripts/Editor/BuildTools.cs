using UnityEngine;
using UnityEditor;

public class BuildTools
{
    [MenuItem("Build/Build APK")]
    public static void BuildApk()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.androidCreateSymbolsZip = true;

        BuildPlayerOptions opt = new BuildPlayerOptions();
        opt.scenes = new string[] { "Assets/Scenes/Lobby.unity" };
        opt.locationPathName = Application.dataPath + "/../OutPut/YVRCraft.apk";
        opt.target = BuildTarget.Android;
        opt.options = BuildOptions.CompressWithLz4;

        PlayerSettings.Android.keystorePass = "123456";
        PlayerSettings.Android.keyaliasPass = "123456";

        PlayerSettings.SplashScreen.show = false;

        BuildPipeline.BuildPlayer(opt);

        Debug.Log("Build App Done!");
    }
}
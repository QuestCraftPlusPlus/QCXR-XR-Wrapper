using UnityEditor;
using UnityEditor.Compilation;


namespace YVR.Platform.Editor
{
    public class PlatformPackageVersionEditor
    {
        [InitializeOnLoadMethod]
        private static void Init()
        {
            CompilationPipeline.compilationFinished -= OnCompilationFinished;
            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }

        private static void OnCompilationFinished(object obj)
        {
            var assembly = typeof(YVRPlatform).Assembly;
            PackageVersionEditor.SaveVersionInfo(assembly);
        }
    }
}
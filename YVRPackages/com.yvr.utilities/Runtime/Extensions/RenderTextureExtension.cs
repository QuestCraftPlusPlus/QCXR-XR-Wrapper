using UnityEngine;

public static class RenderTextureExtension
{
    public static void DownloadTo(this RenderTexture target, string path)
    {
        Texture2D copyingTexture = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        RenderTexture activeBackup = RenderTexture.active;
        RenderTexture.active = target;
        copyingTexture.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = activeBackup;

        byte[] textureBytes = path.Contains("png") ? copyingTexture.EncodeToPNG() : copyingTexture.EncodeToJPG();
        System.IO.File.WriteAllBytes(path, textureBytes);
        Debug.Log("RenderTexture has been saved to " + path);
        Object.DestroyImmediate(copyingTexture);
    }

    public static Color[] GetPixels(this RenderTexture target)
    {
        Texture2D copiedTexture2D = GetCopiedTexture2D(target);

        Color[] pixels = copiedTexture2D.GetPixels();
        Object.DestroyImmediate(copiedTexture2D);

        return pixels;
    }

    public static Color32[] GetPixels32(this RenderTexture target)
    {
        Texture2D copyingTexture = GetCopiedTexture2D(target);

        Color32[] pixels = copyingTexture.GetPixels32();
        Object.DestroyImmediate(copyingTexture);
        return pixels;
    }

    public static Texture2D GetCopiedTexture2D(this RenderTexture target)
    {
        Texture2D copiedTexture = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        RenderTexture activeBackup = RenderTexture.active;
        RenderTexture.active = target;
        copiedTexture.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = activeBackup;
        return copiedTexture;
    }

    public static Color GetPixel(this RenderTexture target, int x, int y)
    {
        Texture2D copyingTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        RenderTexture activeBackup = RenderTexture.active;
        RenderTexture.active = target;

        copyingTexture.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
        RenderTexture.active = activeBackup;

        Color pixel = copyingTexture.GetPixel(0, 0);
        Object.DestroyImmediate(copyingTexture);
        return pixel;
    }
}

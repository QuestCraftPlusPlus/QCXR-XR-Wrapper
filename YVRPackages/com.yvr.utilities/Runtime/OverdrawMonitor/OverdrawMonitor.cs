#if USE_URP
using UnityEngine;

public class OverdrawMonitor : MonoBehaviorSingleton<OverdrawMonitor>
{
    public bool overrideInGame = false;
    public bool overrideInSceneView = true;
    public KeyCode statisticKey = KeyCode.A;
    public Camera targetCamera = null;

    private RenderTexture m_OverdrawStateRT = null;

    protected override void Init()
    {
        base.Init();
        m_OverdrawStateRT = new RenderTexture(Screen.width, Screen.height, 24,
            UnityEngine.Experimental.Rendering.DefaultFormat.LDR);

        if (targetCamera == null)
            targetCamera = Camera.main; // Try to find main camera as target camera
    }

    private void Update()
    {
        if (targetCamera == null) return;

        if (Input.GetKeyDown(statisticKey))
        {
            bool temp = overrideInGame;

            overrideInGame = true;
            targetCamera.targetTexture = m_OverdrawStateRT;
            targetCamera.Render();
            CountOverdrawState(m_OverdrawStateRT.GetPixels(), out float screenFillRate, out float pixelFillRate);
            Debug.Log("Screen Fill Rate is " + screenFillRate);
            Debug.Log("Pixel Fill Rate is " + pixelFillRate);
            overrideInGame = temp;
        }

        targetCamera.targetTexture = null;
    }

    private void CountOverdrawState(Color[] pixels, out float screenFillRate, out float pixelFillRate)
    {
        if (pixels == null || pixels.Length == 0)
        {
            screenFillRate = -1.0f;
            pixelFillRate = -1.0f;
            return;
        }

        int screenPixelsCount = pixels.Length; // The pixels count of the whole screen
        int coveringPixelsCount = 0; // The pixels count of which has been drawn
        float drawingRedValueSum = 0.0f; // The sum of color value in red channel of actual drawing

        for (int i = 0; i != screenPixelsCount; ++i)
        {
            float rValue = pixels[i].r;
            if (rValue != 0)
            {
                coveringPixelsCount++;
                drawingRedValueSum += rValue;
            }
        }

        // As the color indicating overdraw is defined in shader as (0.1, 0.04, 0.02) , the red chanel should be ((int)(0.1*255.0f)/255) -> (25/255.0f)
        float colorBaseR = 25f / 255.0f;

        screenFillRate = drawingRedValueSum / colorBaseR / screenPixelsCount;
        pixelFillRate = drawingRedValueSum / colorBaseR / coveringPixelsCount;
    }
}
#endif
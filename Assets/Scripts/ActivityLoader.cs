using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Oculus.Interaction;
using Oculus.Interaction.Surfaces;

public class ActivityLoader : MonoBehaviour
{
    public string activityClassName;
    public int width = 1920;
    public int height = 1080;
    private AndroidJavaObject m_AppContainer;
    private IntPtr m_NativeTexturePointer;
    private Renderer m_Renderer;
    private bool hasLoaded = false;
    private Texture2D m_ImageTexture2D;
    public RayInteractor interactor;
    private bool isSelecting = false;
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_ImageTexture2D = new Texture2D(width, height, TextureFormat.ARGB32, false)
        { filterMode = FilterMode.Point };
        m_ImageTexture2D.Apply();
    }

    private IEnumerator RenderTexture(IntPtr nativePointer)
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            m_AppContainer.Call("renderTexture", nativePointer.ToInt32());
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetIsSelectingTrue()
    {
        isSelecting = true;
    }

    public void SetIsSelectingFalse()
    {
        isSelecting = false;
    }

    private void Update()
    {
        if (m_AppContainer != null)
        {
            bool KillOpenXR = m_AppContainer.Get<bool>("killOpenXR");
            if (KillOpenXR)
            {
                GetComponent<OpenXRKiller>().KillOpenXR();
            }
        }
    }

    private void FixedUpdate()
    {
        SurfaceHit? interactableRef = interactor.CollisionInfo;
        if(interactableRef != null)
        {
            SurfaceHit hit = interactableRef.Value;
            float y = Math.Abs(GetComponent<Transform>().parent.position.y - hit.Point.y) * 1890;
            if(y > 500)
            {
                y += 1000;
            } else if(y < 500)
            {
                y -= 1000;
            }
            y = Math.Abs(y);
            float x = Math.Abs(GetComponent<Transform>().parent.position.x - hit.Point.x) * 1890;
            if (m_AppContainer != null && isSelecting)
            {
                Debug.LogWarning("X: " + x + ", Y: " + y);
                m_AppContainer.Call("startClick", x, y);
            } else if(!isSelecting && m_AppContainer != null)
            {
                m_AppContainer.Call("endClick", x, y);
            }
        }
    }

    public void OnLoad()
    {
        if (activityClassName != null && hasLoaded == false)
        {
            hasLoaded = true;
            Debug.LogWarning("Loading " + activityClassName);
            AndroidJavaClass activityClass = new AndroidJavaClass(activityClassName);
            AndroidJavaClass appContainerClass = new AndroidJavaClass("com.qcxr.activitywrapper.AppContainer");
            m_AppContainer = appContainerClass.CallStatic<AndroidJavaObject>("getInstance");
        
            m_AppContainer.Call("addActivityView", activityClass, width, height);
            m_Renderer.material.mainTexture = m_ImageTexture2D;
            Debug.LogWarning("Check: " + m_ImageTexture2D.GetNativeTexturePtr());
            Debug.LogWarning("Rendering starts");
            StartCoroutine(RenderTexture(m_ImageTexture2D.GetNativeTexturePtr()));
        }
        else
        {
            Debug.LogWarning("No package found");
        }
    }
}

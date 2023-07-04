using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YVR.Core;
using YVR.Core.Demo;

public class ModifyCanvasSize : MonoBehaviour
{
    public Slider scaleXSlider;
    public Slider scaleYSlider;
    public Slider scaleZSlider;
    public Slider widthSlider;
    public Slider heightSlider;
    public Slider angleSlider;

    public CompositeLayerController compositeLayerController;
    public YVRCompositeLayer compositeLayer;
    public RectTransform canvas;

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        scaleXSlider.value = canvas.localScale.x;
        scaleYSlider.value = canvas.localScale.y;
        scaleZSlider.value = canvas.localScale.z;
        widthSlider.value = canvas.rect.width;
        heightSlider.value = canvas.rect.height;
        angleSlider.value = compositeLayer.angle;

        scaleXSlider.onValueChanged.AddListener(ScaleXValueChange);
        scaleYSlider.onValueChanged.AddListener(ScaleYValueChange);
        scaleZSlider.onValueChanged.AddListener(ScaleZValueChange);
        widthSlider.onValueChanged.AddListener(WidthValueChange);
        heightSlider.onValueChanged.AddListener(HeightValueChange);
        angleSlider.onValueChanged.AddListener(AngleValueChange);

        RefreshCompositeLayerSize();
        RefreshCamera();
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            ScaleZValueChange(canvas.localScale.z + 0.001f);
        }

        if (Input.GetKey(KeyCode.Y))
        {
            ScaleZValueChange(canvas.localScale.z - 0.001f);
        }

        if (Input.GetKey(KeyCode.I))
        {
            AngleValueChange(180);
        }
    }

    private void RefreshCompositeLayerSize()
    {
        compositeLayer.transform.localScale = new Vector3(canvas.rect.width, canvas.rect.height, 1);
    }

    private void RefreshCamera()
    {
        compositeLayerController.orthographicSize = compositeLayer.transform.lossyScale.y / 2f;
        compositeLayerController.aspect = compositeLayer.transform.lossyScale.x / compositeLayer.transform.lossyScale.y;
    }

    public void ScaleXValueChange(float value)
    {
        Vector3 currentScale = canvas.localScale;
        canvas.localScale = new Vector3(value, currentScale.y, currentScale.z);
        RefreshCamera();
    }

    public void ScaleYValueChange(float value)
    {
        Vector3 currentScale = canvas.localScale;
        canvas.localScale = new Vector3(currentScale.x, value, currentScale.z);
        RefreshCamera();
    }

    public void ScaleZValueChange(float value)
    {
        Vector3 currentScale = canvas.localScale;
        canvas.localScale = new Vector3(currentScale.x, currentScale.y, value);
        RefreshCamera();
    }

    public void WidthValueChange(float value)
    {
        Vector2 size = canvas.rect.size;
        canvas.sizeDelta = new Vector2(value, size.y);
        RefreshCompositeLayerSize();
        RefreshCamera();
    }

    public void HeightValueChange(float value)
    {
        Vector2 size = canvas.rect.size;
        canvas.sizeDelta = new Vector2(size.x, value);
        RefreshCompositeLayerSize();
        RefreshCamera();
    }

    public void AngleValueChange(float value)
    {
        compositeLayer.angle = value;
    }

    public void ChangeShape()
    {
        compositeLayer.shape = compositeLayer.shape == YVRRenderLayerType.Quad ? YVRRenderLayerType.Cylinder : YVRRenderLayerType.Quad;
    }
}

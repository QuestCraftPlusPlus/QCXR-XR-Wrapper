using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YVR.Core;

public class YVRCurvedUIRaycaster : YVRGraphicRaycaster
{
    //origin of cylinder
    private Vector3 m_CylinderOriginPoint;
    //circle segment obtain from compositelayer
    private int m_CircleSegments = 50;
    //cylinder angle obtain from compositelayer
    private float m_Angle;
    //cylinder radius obtain from compositelayer
    private float m_Radius;

    private RectTransform m_RectTransform;
    private Vector3 m_RemappedPosition;
    private YVRCompositeLayer m_CompositeLayer;
    private Ray m_OverwriteRay;

    private RectTransform rectTransform
    {
        get
        {
            if (m_RectTransform == null)
            {
                m_RectTransform = GetComponent<RectTransform>();
            }
            return m_RectTransform;
        }
    }

    private bool isChildRaycaster => GetComponentsInParent<YVRCurvedUIRaycaster>().Length > 1;

    private YVRCurvedUIRaycaster parentRaycaster => isChildRaycaster ? GetComponentsInParent<YVRCurvedUIRaycaster>()[1] : null;


    protected override void Awake()
    {
        base.Awake();

        ignoreReversedGraphics = false;
    }

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        Raycast(canvas, eventData, resultAppendList);
    }

    private void Raycast(Canvas uiCanvas, PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        if (isChildRaycaster)
        {
            parentRaycaster.Raycast(uiCanvas, eventData, resultAppendList);
            return;
        }

        if (m_CompositeLayer == null || m_CompositeLayer.shape != YVRRenderLayerType.Cylinder)
        {
            RaycastInternal(uiCanvas, eventData as YVRPointerEventData, resultAppendList);
        }
        else
        {
            CylinderRaycast(uiCanvas, eventData as YVRPointerEventData, resultAppendList);
        }
    }

    private void RaycastInternal(Canvas uiCanvas, YVRPointerEventData eventData, ICollection<RaycastResult> resultAppendList)
    {
        if (uiCanvas == null) return;
        if (eventData == null) return;
        if (eventCamera == null) return;

        float farClipPlane = eventCamera.farClipPlane;
        float maxHitDistanceSqr = farClipPlane * farClipPlane;
        Ray ray = eventData.worldSpaceRay;

        m_RaycastHitResults.Clear();
        GraphicRaycast(uiCanvas, ray, m_RaycastHitResults);

        foreach (RaycastHit hitResult in m_RaycastHitResults)
        {
            // the graphic GO is not valid, if the ray intersects it from the back
            bool graphicGoValid = !ignoreReversedGraphics ||
                                  Vector3.Dot(ray.direction, hitResult.graphic.rectTransform.forward) > 0;

            // the graphic GO is not valid, if it is behind us
            if (eventCamera.transform.InverseTransformPoint(hitResult.hitPointWorldPos).z <= 0)
                graphicGoValid = false;

            if (!graphicGoValid) continue;
            float hitDistanceSqr = (hitResult.hitPointWorldPos - ray.origin).sqrMagnitude;
            if (!(hitDistanceSqr < maxHitDistanceSqr)) continue;
            var result = new RaycastResult()
            {
                // Event systems rely these data to sort the raycast results
                gameObject = hitResult.graphic.gameObject,
                module = this,
                distance = Mathf.Sqrt(hitDistanceSqr),
                index = resultAppendList.Count,
                depth = hitResult.graphic.depth,
                sortingLayer = uiCanvas.sortingLayerID,
                sortingOrder = uiCanvas.sortingOrder,
                worldPosition = hitResult.hitPointWorldPos
            };
            resultAppendList.Add(result);
        }
    }

    private void CylinderRaycast(Canvas uiCanvas, YVRPointerEventData eventData, ICollection<RaycastResult> resultAppendList)
    {
        if (uiCanvas == null) return;
        if (eventData == null) return;
        if (eventCamera == null) return;

        float farClipPlane = eventCamera.farClipPlane;
        float maxHitDistanceSqr = farClipPlane * farClipPlane;
        Ray ray = eventData.worldSpaceRay;

        RaycastToCylinderCanvas(ray, out m_RemappedPosition, out Vector3 hitPoint, maxHitDistanceSqr);

        m_OverwriteRay.origin = ray.origin;
        m_OverwriteRay.direction = (m_RemappedPosition - ray.origin).normalized;

        if (eventData.pressPosition == eventData.position)
            eventData.pressPosition = m_RemappedPosition;

        Raycast(uiCanvas, m_OverwriteRay, resultAppendList, hitPoint);
    }

    private void RaycastToCylinderCanvas(Ray ray, out Vector3 canvasPos, out Vector3 hitPoint, float maxDistance)
    {
        if (Physics.Raycast(ray, out UnityEngine.RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject != gameObject)
            {
                canvasPos = Vector2.zero;
                hitPoint = Vector3.zero;
                return;
            }

            Vector3 lossyScale = transform.lossyScale;
            Vector3 localHitPoint = canvas.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
            localHitPoint.z *= (lossyScale.z / lossyScale.x);
            Vector3 directionFromCylinderCenter = (localHitPoint - m_CylinderOriginPoint).normalized;
            directionFromCylinderCenter.y = 0f;

            //angle between middle of the projected canvas and hit point direction
            float hitPointAngleHorizontal = -AngleSigned(directionFromCylinderCenter, m_Angle < 0 ? Vector3.back : Vector3.forward, Vector3.up);

            Vector2 canvasSize = canvas.GetComponent<RectTransform>().rect.size;

            Vector2 pointOnCanvas = new Vector3(0, 0, 0);
            pointOnCanvas.x = RemapWithAngle(hitPointAngleHorizontal, -m_Angle / 2f, m_Angle / 2f, -canvasSize.x / 2f, canvasSize.x / 2f);
            pointOnCanvas.y = localHitPoint.y;


            canvasPos = canvas.transform.localToWorldMatrix.MultiplyPoint3x4(pointOnCanvas);
            hitPoint = hit.point;
            return;
        }

        canvasPos = Vector3.zero;
        hitPoint = Vector3.zero;
    }

    private void Raycast(Canvas uiCanvas, Ray overwriteRay, ICollection<RaycastResult> resultAppendList, Vector3 hitPosition)
    {
        if (uiCanvas == null) return;
        if (eventCamera == null) return;

        float farClipPlane = eventCamera.farClipPlane;
        float maxHitDistanceSqr = farClipPlane * farClipPlane;
        Ray ray = overwriteRay;

        m_RaycastHitResults.Clear();
        GraphicRaycast(uiCanvas, ray, m_RaycastHitResults);

        foreach (RaycastHit hitResult in m_RaycastHitResults)
        {
            // the graphic GO is not valid, if the ray intersects it from the back
            bool graphicGoValid = !ignoreReversedGraphics ||
                                  Vector3.Dot(ray.direction, hitResult.graphic.rectTransform.forward) > 0;
            // the graphic GO is not valid, if it is behind us
            if (eventCamera.transform.InverseTransformPoint(hitResult.hitPointWorldPos).z <= 0)
                graphicGoValid = false;
            if (!graphicGoValid) continue;
            float hitDistanceSqr = (hitResult.hitPointWorldPos - ray.origin).sqrMagnitude;
            if (!(hitDistanceSqr < maxHitDistanceSqr)) continue;
            var result = new RaycastResult()
            {
                // Event systems rely these data to sort the raycast results
                gameObject = hitResult.graphic.gameObject,
                module = this,
                distance = Mathf.Sqrt(hitDistanceSqr),
                index = resultAppendList.Count,
                depth = hitResult.graphic.depth,
                sortingLayer = uiCanvas.sortingLayerID,
                sortingOrder = uiCanvas.sortingOrder,
                worldPosition = hitPosition
            };
            resultAppendList.Add(result);
        }
    }

    public void CreateCollider(YVRCompositeLayer compositeLayer)
    {
        m_CompositeLayer = compositeLayer;
        List<Collider> cols = new List<Collider>();
        cols.AddRange(this.GetComponents<Collider>());
        for (int i = 0; i < cols.Count; i++)
        {
            DestroyImmediate(cols[i]);
        }
        if (m_CompositeLayer == null || m_CompositeLayer.shape != YVRRenderLayerType.Cylinder) return;

        m_Angle = m_CompositeLayer.angle;
        m_CircleSegments = m_CompositeLayer.circleSegments;

        float arcLength = rectTransform.rect.width;
        m_Radius = arcLength / ((2 * Mathf.PI) * (m_Angle / 360f));

        SetupMeshColliderUsingMesh(CreateCylinderColliderMesh());
    }

    private Mesh CreateCylinderColliderMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Transform canvasTransform = canvas.transform;
        Matrix4x4 canvasWorldToLocalMatrix = canvasTransform.worldToLocalMatrix;
        (canvasTransform as RectTransform)?.GetWorldCorners(vertices);

        m_CylinderOriginPoint = canvasWorldToLocalMatrix.MultiplyPoint3x4(new Vector3((vertices[0].x + vertices[2].x) / 2f, (vertices[0].y + vertices[2].y) / 2f, (vertices[0].z + vertices[2].z) / 2f) - transform.forward * m_Radius) * canvasTransform.lossyScale.z;

        mesh.vertices = vertices;

        vertices[0] = canvasWorldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[1]);
        vertices[1] = canvasWorldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[0]);
        vertices[2] = canvasWorldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[2]);
        vertices[3] = canvasWorldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[3]);

        mesh.vertices = vertices;

        List<Vector3> verts = new List<Vector3>();
        int vertsCount = Mathf.Max(8, Mathf.RoundToInt(m_CircleSegments * Mathf.Abs(m_Angle) / 360f));

        for (int i = 0; i < vertsCount; i++)
        {
            verts.Add(Vector3.Lerp(mesh.vertices[0], mesh.vertices[2], (i * 1.0f) / (vertsCount - 1)));
            verts.Add(Vector3.Lerp(mesh.vertices[1], mesh.vertices[3], (i * 1.0f) / (vertsCount - 1)));
        }

        if (m_Angle != 0)
        {
            Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
            float radius = m_Radius;

            for (int i = 0; i < verts.Count; i++)
            {
                Vector3 newPos = verts[i];
                float theta = (verts[i].x / canvasRect.size.x) * m_Angle * Mathf.Deg2Rad;
                newPos.x = Mathf.Sin(theta) * radius;
                newPos.z += Mathf.Cos(theta) * radius - radius;
                //normalized
                Vector3 lossyScale = transform.lossyScale;
                newPos.z *= (lossyScale.x / lossyScale.z);
                verts[i] = newPos;
            }
        }

        mesh.vertices = verts.ToArray();

        List<int> tris = new List<int>();
        for (int i = 0; i < verts.Count / 2 - 1; i++)
        {
            //forward tris
            tris.Add(i * 2 + 2);
            tris.Add(i * 2 + 1);
            tris.Add(i * 2 + 0);

            tris.Add(i * 2 + 2);
            tris.Add(i * 2 + 3);
            tris.Add(i * 2 + 1);
        }
        mesh.triangles = tris.ToArray();

        return mesh;
    }

    private void SetupMeshColliderUsingMesh(Mesh mesh)
    {
        MeshFilter mf = this.AutoAddingGetComponent<MeshFilter>();
        MeshCollider mc = this.AutoAddingGetComponent<MeshCollider>();
        mf.mesh = mesh;
        mc.sharedMesh = mesh;
    }

    //the cross vector magnitude is |a||b|sin(theta)
    private float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    private float RemapWithAngle(float angle, float from1, float to1, float from2, float to2)
    {
        return (angle - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public override Vector2 GetRaycastResultScreenPosition(RaycastResult raycastResult)
    {
        if (isChildRaycaster)
        {
            return parentRaycaster.GetRaycastResultScreenPosition(raycastResult);
        }

        if (m_CompositeLayer == null || m_CompositeLayer.shape != YVRRenderLayerType.Cylinder)
        {
            return base.GetRaycastResultScreenPosition(raycastResult);
        }
        return eventCamera.WorldToScreenPoint(m_RemappedPosition);
    }

    public Vector3 GetRemappedWorldPosition()
    {
        if (isChildRaycaster)
        {
            return parentRaycaster.GetRemappedWorldPosition();
        }

        return m_RemappedPosition;
    }

    public bool IsCylinderCanvas()
    {
        if (isChildRaycaster)
        {
            return parentRaycaster.IsCylinderCanvas();
        }

        return m_CompositeLayer != null && m_CompositeLayer.shape == YVRRenderLayerType.Cylinder;
    }
}

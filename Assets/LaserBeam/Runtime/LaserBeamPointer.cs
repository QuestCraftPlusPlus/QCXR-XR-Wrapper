using UnityEngine;
using UnityEngine.EventSystems;
using YVR.Core;

/// <summary>
/// The controller of laser beam .
/// </summary>
public class LaserBeamPointer : MonoBehaviour
{
    private Transform m_LaserBeamAnchor = null;

    /// <summary>
    /// X is the min value of the hit distance,Y is the max value of the hit distance
    /// </summary>
    [Tooltip("X is the min value of the hit distance,Y is the max value of the hit distance")]
    public Vector2 hitDistanceRange = new Vector2(0.1f, 1f);

    /// <summary>
    /// The idle configuration of the laser beam and cursor.
    /// </summary>
    public LaserBeamConfiguration idleConfiguration = new LaserBeamConfiguration()
    {
        startWidth = 0.0055f,
        endWidth = 0.0015f,
        startColor = new Color32(255, 255, 255, 205),
        endColor = new Color32(255, 255, 255, 0),
        cursorConfiguration = new CursorConfiguration()
        {
            cursorMinScale = 1f,
            cursorDotColor = Color.white
        }
    };

    /// <summary>
    /// The hold configuration of the laser beam and cursor.
    /// </summary>
    public LaserBeamConfiguration holdConfiguration = new LaserBeamConfiguration()
    {
        startWidth = 0.0041f,
        endWidth = 0.0015f,
        startColor = new Color32(23, 92, 230, 205),
        endColor = new Color32(23, 92, 230, 0),
        cursorConfiguration = new CursorConfiguration
        {
            cursorMinScale = 0.8f,
            cursorDotColor = new Color32(23, 92, 230, 255)
        }
    };

    [SerializeField]
    private GameObject cursorPrefab;

    private DefaultImageCursor m_Cursor;
    private LineRenderer m_LaserLine;

    private void OnDisable()
    {
        //move away the line render
        if (m_LaserLine)
        {
            m_LaserLine.SetPosition(0, m_LaserBeamAnchor.position * 9998);
            m_LaserLine.SetPosition(1, m_LaserBeamAnchor.position * 9999);
        }
    }

    private void Start()
    {
        YVRInputModule.Instance.onInputDataProviderChanged += (inputDataProvider) =>
        {
            m_LaserBeamAnchor = YVRInputModule.Instance.rayTransform;

            transform.SetParent(m_LaserBeamAnchor);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        };

        m_LaserLine = GetComponent<LineRenderer>();
        if (m_LaserLine == null) m_LaserLine = CreateLaserBeam();

        if (m_Cursor == null)
            m_Cursor = CreateDefaultCursor();
    }

    private void Update()
    {
        UpdateRaycast();
    }

    private void UpdateRaycast()
    {
        RaycastResult result = YVRInputModule.Instance.raycastResult;

        var hitSth = result.gameObject != null;
        float distance = hitSth ? Vector3.Distance(result.worldPosition, m_LaserBeamAnchor.position) - 0.01f : 0;

        if (m_Cursor)
            m_Cursor.Show(hitSth);

        Vector3 normal = hitSth ? result.worldNormal * -1 : m_LaserBeamAnchor.forward;

        if (normal.Equals(Vector3.zero))
            normal = result.gameObject.transform.forward;

        bool currentControllerIsPressed = YVRInputModule.Instance.inputDataProvider.pressed || YVRInputModule.Instance.inputDataProvider.longPressed;

        if (currentControllerIsPressed)
        {
            UpdateLaserBeam(holdConfiguration, distance, normal);

            if (m_Cursor)
                m_Cursor.UpdateEffect(holdConfiguration.cursorConfiguration, distance, normal, result.gameObject);
        }
        else
        {
            UpdateLaserBeam(idleConfiguration, distance, normal);

            if (m_Cursor)
                m_Cursor.UpdateEffect(idleConfiguration.cursorConfiguration, distance, normal, result.gameObject);
        }
    }

    private void UpdateLaserBeam(LaserBeamConfiguration configuration, float distance, Vector3 normal)
    {
        if (m_LaserLine)
        {
            m_LaserLine.startColor = configuration.startColor;
            m_LaserLine.endColor = configuration.endColor;
            m_LaserLine.startWidth = configuration.startWidth;
            m_LaserLine.endWidth = configuration.endWidth;

            m_LaserLine.SetPosition(0, m_LaserBeamAnchor.position + m_LaserBeamAnchor.forward * configuration.startPointOffset);
            float beamLength = Mathf.Clamp(distance, hitDistanceRange.x, hitDistanceRange.y);
            m_LaserLine.SetPosition(1, m_LaserBeamAnchor.position + m_LaserBeamAnchor.forward * beamLength);
        }
    }

    private LineRenderer CreateLaserBeam()
    {
        var lineRender = gameObject.AddComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("YFramework/Unlit/VertexColor"));
        return lineRender;
    }

    private DefaultImageCursor CreateDefaultCursor()
    {
        if (cursorPrefab && cursorPrefab.GetComponent<DefaultImageCursor>())
        {
            var cur = Instantiate<GameObject>(cursorPrefab, transform);
            return cur.GetComponent<DefaultImageCursor>();
        }

        return null;
    }
}
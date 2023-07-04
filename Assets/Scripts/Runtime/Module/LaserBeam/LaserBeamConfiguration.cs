using UnityEngine;

/// <summary>
/// The configuration of laser beam.
/// </summary>
[System.Serializable]
public class LaserBeamConfiguration
{
    /// <summary>
    /// The start width of laser line render.
    /// </summary>
    [Header("Laser Beam Configuration")]
    public float startWidth = 0.0055f;
    /// <summary>
    /// The end width of laser line render.
    /// </summary>
    public float endWidth = 0.0015f;
    /// <summary>
    /// The start color of laser line render.
    /// </summary>
    public Color startColor = new Color32(255, 255, 255, 205);
    /// <summary>
    /// The end color of laser line render.
    /// </summary>
    public Color endColor = new Color32(255, 255, 255, 0);
    /// <summary>
    /// Offset of the starting point of the laser beam
    /// </summary>
    [Tooltip("Offset of the starting point of the laser beam")]
    public float startPointOffset = 0.07f;
    /// <summary>
    /// The configuration of cursor
    /// </summary>
    public CursorConfiguration cursorConfiguration;

}
using UnityEngine;

public class FPSMonitor : MonoBehaviour
{
    [Header("Display Settings")]
    public bool showMonitor = true;
    public int fontSize = 20;
    public Color textColor = Color.green;

    [Header("Sampling Settings")]
    public float averageWindow = 1.0f; // seconds to average over

    // Current FPS
    private float currentFPS;

    // Average FPS
    private float averageFPS;
    private float fpsSum;
    private int fpsSampleCount;
    private float sampleTimer;

    // Min/Max FPS
    private float minFPS = float.MaxValue;
    private float maxFPS = float.MinValue;

    // GUI style
    private GUIStyle style;
    private bool styleInitialized = false;

    void Update()
    {
        // Current FPS = 1 divided by the time since last frame
        currentFPS = 1.0f / Time.deltaTime;

        // Update min and max
        if (currentFPS < minFPS) minFPS = currentFPS;
        if (currentFPS > maxFPS) maxFPS = currentFPS;

        // Accumulate samples for average
        fpsSum += currentFPS;
        fpsSampleCount++;
        sampleTimer += Time.deltaTime;

        // Recalculate average every averageWindow seconds
        if (sampleTimer >= averageWindow)
        {
            averageFPS = fpsSum / fpsSampleCount;
            fpsSum = 0;
            fpsSampleCount = 0;
            sampleTimer = 0;
        }
    }

    void OnGUI()
    {
        if (!showMonitor) return;

        // Initialize style once
        if (!styleInitialized)
        {
            style = new GUIStyle();
            style.fontSize = fontSize;
            style.normal.textColor = textColor;
            style.fontStyle = FontStyle.Bold;
            // Dark background for readability
            Texture2D bg = new Texture2D(1, 1);
            bg.SetPixel(0, 0, new Color(0, 0, 0, 0.5f));
            bg.Apply();
            style.normal.background = bg;
            style.padding = new RectOffset(8, 8, 8, 8);
            styleInitialized = true;
        }

        string display = string.Format(
            "FPS: {0:0.0}\nAvg: {1:0.0}\nMin: {2:0.0}\nMax: {3:0.0}",
            currentFPS,
            averageFPS,
            minFPS,
            maxFPS
        );

        // Draw in top-left corner
        GUI.Label(new Rect(10, 10, 150, 100), display, style);
    }

    // Call this to reset min/max if needed
    public void ResetStats()
    {
        minFPS = float.MaxValue;
        maxFPS = float.MinValue;
        fpsSum = 0;
        fpsSampleCount = 0;
        sampleTimer = 0;
    }
}
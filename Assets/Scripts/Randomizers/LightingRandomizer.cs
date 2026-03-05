using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

[AddRandomizerMenu("RoboSub/Lighting Randomizer")]
public class LightingRandomizer : Randomizer
{
    [Header("Intensity Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 3.0f;

    protected override void OnIterationStart()
    {
        // Find the main light in the scene
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            // Set each light to a random intensity
            light.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
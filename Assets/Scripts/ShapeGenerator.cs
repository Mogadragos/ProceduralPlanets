using UnityEngine;

public class ShapeGenerator
{
    float radius;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;

    public ShapeGenerator(float radius)
    {
        this.radius = radius;

        noiseFilters = new INoiseFilter[2];
        noiseFilters[0] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.SimpleNoiseSettings(.07f, 4, 1.15f, 2.2f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), .98f)));
        noiseFilters[1] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.RigidNoiseSettings(1.42f, 4, 1.59f, 3.3f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), .37f, .78f)));

        elevationMinMax = new MinMax();
    }

    public void UpdateSettings(float radius)
    {
        this.radius = radius;
        noiseFilters[0] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.SimpleNoiseSettings(.07f, 4, 1.15f, 2.2f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), .98f)));
        noiseFilters[1] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.RigidNoiseSettings(1.42f, 4, 1.59f, 3.3f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), .37f, .78f)));
        elevationMinMax.Reset();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if(noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            elevation = firstLayerValue;
        }

        for(int i = 1; i < noiseFilters.Length; i++)
        {
            float mask = firstLayerValue;
            elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
        }
        elevation = (1 + elevation) * radius;
        elevationMinMax.AddValue(elevation);
        return elevation * pointOnUnitSphere;
    }
}

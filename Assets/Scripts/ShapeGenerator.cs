using UnityEngine;

// Gestionnaire des formes de la planète
public class ShapeGenerator
{
    // Rayon
    float radius = 1;

    // Bruits
    INoiseFilter[] noiseFilters;

    // Elevation
    public MinMax elevationMinMax;

    public ShapeGenerator(float relativeDistanceToSun)
    {
        noiseFilters = new INoiseFilter[2];
        RandomShape(relativeDistanceToSun);

        elevationMinMax = new MinMax();
    }

    void RandomShape(float relativeDistanceToSun)
    {
        radius = Random.Range(.3f, 4f);

        // Plus on est proche du soleil, plus la planète est rocheuse
        float continentMinValue;

        if (relativeDistanceToSun < .3f) continentMinValue = 0f;
        else if (relativeDistanceToSun < .5f)
        {
            continentMinValue = Random.Range(.3f, Mathf.Lerp(.5f, 1.5f, relativeDistanceToSun));
        } else continentMinValue = Random.Range(.5f, 1.5f);

        // Continents
        noiseFilters[0] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.SimpleNoiseSettings(.07f, 4, 1.15f, 2.2f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), continentMinValue)));
        // Reliefs
        noiseFilters[1] = NoiseFilterFactory.CreateNoiseFilter(new NoiseSettings(new NoiseSettings.RigidNoiseSettings(Random.Range(.8f, 1.6f), 4, 1.59f, 3.3f, .5f, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1), Random.value, Random.Range(0f, 2f))));
    }

    // Calcul de l'élévation en fonction du bruit
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float elevation = firstLayerValue;

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

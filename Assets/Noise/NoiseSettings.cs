using UnityEngine;

// Gestionnaire des paramètres du bruit
[System.Serializable]
public class NoiseSettings
{
    // Type : Simple (continents) ; Rigid (reliefs)
    public enum FilterType { Simple, Rigid };
    public FilterType filterType;

    // Paramètres en cas de filtre Simple
    public SimpleNoiseSettings simpleNoiseSettings;

    // Paramètres en cas de filtre Rigid
    public RigidNoiseSettings rigidNoiseSettings;

    // On crée les paramètres correspondant au type du filtre
    public NoiseSettings(FilterType filterType)
    {
        this.filterType = filterType;
        switch (filterType)
        {
            case FilterType.Simple:
                this.simpleNoiseSettings = new SimpleNoiseSettings();
                break;
            case FilterType.Rigid:
                this.rigidNoiseSettings = new RigidNoiseSettings();
                break;

        }
    }

    public NoiseSettings(SimpleNoiseSettings simpleNoiseSettings)
    {
        this.filterType = FilterType.Simple;
        this.simpleNoiseSettings = simpleNoiseSettings;
    }

    public NoiseSettings(RigidNoiseSettings ridgidNoiseSettings)
    {
        this.filterType = FilterType.Rigid;
        this.rigidNoiseSettings = ridgidNoiseSettings;
    }

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;
        public Vector3 center = Vector3.zero;
        public float minValue = 1f;

        public SimpleNoiseSettings(float strength, int numLayers, float baseRoughness, float roughness, float persistence, Vector3 center, float minValue)
        {
            this.strength = strength;
            this.numLayers = numLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.center = center;
            this.minValue = minValue;
        }

        public SimpleNoiseSettings() {}
    }

    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;

        public RigidNoiseSettings(float strength, int numLayers, float baseRoughness, float roughness, float persistence, Vector3 center, float minValue, float weightMultiplier)
            : base(strength, numLayers, baseRoughness, roughness, persistence, center, minValue)
        {
            this.weightMultiplier = weightMultiplier;
        }

        public RigidNoiseSettings() {}
    }
}
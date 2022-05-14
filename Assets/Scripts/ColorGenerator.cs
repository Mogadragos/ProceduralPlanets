using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    // Pour le shader
    public Material material { get; private set; }
    Texture2D texture;
    const int textureResolution = 50;

    // Couches de couleurs aléatoires
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    // Teinte générale (couleur plus froide / plus chaude)
    Color tint;
    const float tintPercent = .3f;

    public ColorGenerator()
    {
        material = new Material(Shader.Find("Shader Graphs/Planet"));
        texture = new Texture2D(textureResolution, 1);

        gradient = new Gradient();
        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        RandomColor();
    }

    void RandomColor()
    {
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[7];
        colorKey[0].color = Random.ColorHSV(); // new Color32(0, 10, 154, 255);
        colorKey[0].time = 0.0f;
        colorKey[1].color = Random.ColorHSV(); // new Color32(158, 121, 8, 255);
        colorKey[1].time = .041f;
        colorKey[2].color = Random.ColorHSV(); // new Color32(16, 152, 0, 255);
        colorKey[2].time = .126f;
        colorKey[3].color = Random.ColorHSV(); // new Color32(10, 99, 0, 255);
        colorKey[3].time = .226f;
        colorKey[4].color = Random.ColorHSV(); // new Color32(118, 51, 0, 255);
        colorKey[4].time = .441f;
        colorKey[5].color = Random.ColorHSV(); // new Color32(79, 35, 0, 255);
        colorKey[5].time = .685f;
        colorKey[6].color = Random.ColorHSV(); // Color.white;
        colorKey[6].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        tint = Random.ColorHSV(); // TODO: + chaude ou + froide selon distance au soleil
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        material.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[textureResolution];
        for(int i = 0; i < textureResolution; i++)
        {
            colors[i] = gradient.Evaluate(i / (textureResolution - 1f)) * (1 - tintPercent) + tint * tintPercent;
        }
        texture.SetPixels(colors);
        texture.Apply();
        material.SetTexture("_texture", texture);
    }
}

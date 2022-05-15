using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestionnaire des couleurs de la plan�te
public class ColorGenerator
{
    // Shader
    public Material material { get; private set; }
    Texture2D texture;
    const int textureResolution = 50;

    // Couleurs al�atoires

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    // Teinte g�n�rale (couleur plus froide / plus chaude)

    Color tint;
    float tintPercent = .2f;

    public ColorGenerator(float relativeDistanceToSun)
    {
        // On cr�e un Material et une Texture pour notre plan�te
        material = new Material(Shader.Find("Shader Graphs/Planet"));
        texture = new Texture2D(textureResolution, 1);
        
        // On cr�e un Gradient qui sera remplit al�atoirement
        gradient = new Gradient();

        // L'alpha est toujours fix� � 1
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        // On g�n�re les couleurs al�atoirement
        RandomColor();

        /* La teinte est proportionnelle � la distance au soleil :
            Proche du soleil : rouge
            Loin du soleil : bleue
        */
        tint = Color.Lerp(new Color32(187, 11, 11, 255), new Color32(192, 223, 239, 255), relativeDistanceToSun);

        // Plus on est proche des extr�mit�s, plus la teinte � un impact sur la couleur de la plan�te
        if(relativeDistanceToSun > .5f)
        {
            tintPercent = Mathf.Lerp(0, .3f, (relativeDistanceToSun - .5f) * 2);
        }
        else
        {
            tintPercent = Mathf.Lerp(.35f, 0, relativeDistanceToSun * 2);
        }
    }

    // G�n�ration des couleurs al�atoires
    void RandomColor()
    {
        colorKey = new GradientColorKey[7];
        colorKey[0].color = Random.ColorHSV();
        colorKey[0].time = 0.0f;
        colorKey[1].color = Random.ColorHSV();
        colorKey[1].time = .041f;
        colorKey[2].color = Random.ColorHSV();
        colorKey[2].time = .126f;
        colorKey[3].color = Random.ColorHSV();
        colorKey[3].time = .226f;
        colorKey[4].color = Random.ColorHSV();
        colorKey[4].time = .441f;
        colorKey[5].color = Random.ColorHSV();
        colorKey[5].time = .685f;
        colorKey[6].color = Random.ColorHSV();
        colorKey[6].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Envoi du param�tre d'�l�vation au shader
    public void UpdateElevation(MinMax elevationMinMax)
    {
        material.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    // Mise � jour de la texture avec les nouvelles couleurs et envoi de celle-ci au shader
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

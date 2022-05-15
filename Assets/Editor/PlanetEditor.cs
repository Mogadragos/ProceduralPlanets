using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
        }

        if (GUILayout.Button("Generate Planet (for testing only)"))
        {
            planet.GeneratePlanet(planet.relativeDistanceToSun);
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }
}

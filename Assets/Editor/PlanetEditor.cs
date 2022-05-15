using UnityEngine;
using UnityEditor;

// Editeur personnalisé permettant l'ajout du bouton de génération de planète (voir Scène "ExamplePlanet")
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
            planet.GeneratePlanet(0f, planet.relativeDistanceToSun);
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }
}

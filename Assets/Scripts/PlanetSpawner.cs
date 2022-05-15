using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    // Distance maximale d'"énergie" du soleil
    public float MAX_ENERGY_DISTANCE = 50f;

    // Nombre de planètes
    public int MinPlanets;
    public int MaxPlanets;

    // Prefab d'une planète
    public GameObject prefab;

    void Start()
    {
        // Nombre de planète aléatoire
        int nbPlanets = Random.Range(MinPlanets, MaxPlanets);

        // Pour chaque planète
        float minDist = 10f;
        for (int i = 0; i < nbPlanets; i++)
        {
            // Création de la panète
            Transform planetContainer = Instantiate(prefab).transform.GetChild(0);

            // Placement aléatoire des planètes sur un cercle
            float theta = 2 * Mathf.PI * Random.value;
            float distance = Random.Range(minDist, minDist + 8f);

            planetContainer.transform.localPosition = new Vector3(distance * Mathf.Cos(theta), 0f, distance * Mathf.Sin(theta));

            // Agrandissement du cercle minimal (pour ne pas entrer en collision avec d'autres planètes
            minDist = distance + 5f;

            //génération de la planète
            planetContainer.GetChild(0).GetComponent<Planet>().GeneratePlanet(distance, Mathf.Min(1f, distance / MAX_ENERGY_DISTANCE));
        }
    }
}

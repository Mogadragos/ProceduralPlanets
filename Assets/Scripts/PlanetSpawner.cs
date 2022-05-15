using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    // Distance maximale d'"�nergie" du soleil
    public float MAX_ENERGY_DISTANCE = 50f;

    // Nombre de plan�tes
    public int MinPlanets;
    public int MaxPlanets;

    // Prefab d'une plan�te
    public GameObject prefab;

    void Start()
    {
        // Nombre de plan�te al�atoire
        int nbPlanets = Random.Range(MinPlanets, MaxPlanets);

        // Pour chaque plan�te
        float minDist = 10f;
        for (int i = 0; i < nbPlanets; i++)
        {
            // Cr�ation de la pan�te
            Transform planetContainer = Instantiate(prefab).transform.GetChild(0);

            // Placement al�atoire des plan�tes sur un cercle
            float theta = 2 * Mathf.PI * Random.value;
            float distance = Random.Range(minDist, minDist + 8f);

            planetContainer.transform.localPosition = new Vector3(distance * Mathf.Cos(theta), 0f, distance * Mathf.Sin(theta));

            // Agrandissement du cercle minimal (pour ne pas entrer en collision avec d'autres plan�tes
            minDist = distance + 5f;

            //g�n�ration de la plan�te
            planetContainer.GetChild(0).GetComponent<Planet>().GeneratePlanet(distance, Mathf.Min(1f, distance / MAX_ENERGY_DISTANCE));
        }
    }
}

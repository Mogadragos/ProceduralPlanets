using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public float MAX_ENERGY_DISTANCE = 50f;

    public int MinPlanets;
    public int MaxPlanets;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        int nbPlanets = Random.Range(MinPlanets, MaxPlanets);

        float minDist = 10f;
        for (int i = 0; i < nbPlanets; i++)
        {
            Transform planetContainer = Instantiate(prefab).transform.GetChild(0);

            float theta = 2 * Mathf.PI * Random.value;
            float distance = Random.Range(minDist, minDist + 8f);
            minDist = distance + 5f;

            planetContainer.transform.localPosition = new Vector3(distance * Mathf.Cos(theta), 0f, distance * Mathf.Sin(theta));

            planetContainer.GetChild(0).GetComponent<Planet>().GeneratePlanet(distance, Mathf.Min(1f, distance / MAX_ENERGY_DISTANCE));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

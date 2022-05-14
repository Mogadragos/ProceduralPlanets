using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public int MinPlanets;
    public int MaxPlanets;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        int a = Random.Range(MinPlanets, MaxPlanets);
        for (int i = 0; i < a; i++)
        {
            Instantiate(prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

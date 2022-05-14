using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{

    public int MaxPlanets;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MaxPlanets; i++)
        {
            Instantiate(prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

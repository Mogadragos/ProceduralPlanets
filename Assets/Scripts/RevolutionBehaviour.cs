using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Planet;
    float degreesPerSecond;


    void Start()
    {
        degreesPerSecond = Random.Range(0.5f, 5f);
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        if (x > 0)
            x = 1;
        else
            x=-1;
        if (z > 0)
            z = 1;
        else
            z=-1;
        Planet.transform.localPosition = new Vector3(Random.Range(5f, 30f)*x, 0f, Random.Range(5f, 30f) * z);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
}

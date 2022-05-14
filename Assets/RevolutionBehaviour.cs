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
        Planet.transform.localPosition = new Vector3(Random.Range(10f, 50f), 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
}

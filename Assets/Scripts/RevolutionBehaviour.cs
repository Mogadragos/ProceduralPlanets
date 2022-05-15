using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionBehaviour : MonoBehaviour
{
    public GameObject Planet;
    float degreesPerSecond;


    void Awake()
    {
        degreesPerSecond = Random.Range(0.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
}

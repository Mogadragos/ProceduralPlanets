using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionBehaviour : MonoBehaviour
{
    Transform child;
    Vector3 childInitialRotation;
    float degreesPerSecond;


    void Awake()
    {
        child = transform.GetChild(0);
        childInitialRotation = new Vector3(Random.Range(-45f, 45f), 0, Random.Range(-45f, 45f));
        child.eulerAngles = childInitialRotation;

        //vitese de révolution aléatoire
        degreesPerSecond = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        child.eulerAngles = childInitialRotation;
    }
}

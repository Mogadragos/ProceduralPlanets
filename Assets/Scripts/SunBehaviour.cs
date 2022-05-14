using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehaviour : MonoBehaviour
{
    public Material material;
    public Transform SunTransform;

    // Start is called before the first frame update
    void Start()
    {
        float rd  = Random.Range(0.5f, 4f);
        SunTransform.localScale  = new Vector3(rd, rd, rd);
        material.color= new Color(Random.value, Random.value, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

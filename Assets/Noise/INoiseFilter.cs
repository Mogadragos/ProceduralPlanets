using UnityEngine;

// Interface des différents filtres de bruits
public interface INoiseFilter
{
    float Evaluate(Vector3 point);
}

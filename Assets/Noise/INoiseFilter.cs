using UnityEngine;

// Interface des diff�rents filtres de bruits
public interface INoiseFilter
{
    float Evaluate(Vector3 point);
}

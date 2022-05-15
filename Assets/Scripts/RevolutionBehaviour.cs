using UnityEngine;

// Gestionnaire de la rotation de la planète autour du soleil
public class RevolutionBehaviour : MonoBehaviour
{
    // Conteneur de la planète
    Transform child;

    // Rotation initiale
    Vector3 childInitialRotation;

    // Vitesse de rotation autour du soleil
    float degreesPerSecond;

    void Awake()
    {
        // On récupère la planète et on lui donne un axe de rotation initial aléatoire
        child = transform.GetChild(0);
        childInitialRotation = new Vector3(Random.Range(-45f, 45f), 0, Random.Range(-45f, 45f));
        child.eulerAngles = childInitialRotation;

        // Vitese de révolution aléatoire
        degreesPerSecond = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 5f);
    }

    void Update()
    {
        // La planète tourne autour du soleil
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        // Mais son axe de rotation n'est pas changé
        child.eulerAngles = childInitialRotation;
    }
}

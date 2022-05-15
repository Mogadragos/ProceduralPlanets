using UnityEngine;

// Gestionnaire de la rotation de la plan�te autour du soleil
public class RevolutionBehaviour : MonoBehaviour
{
    // Conteneur de la plan�te
    Transform child;

    // Rotation initiale
    Vector3 childInitialRotation;

    // Vitesse de rotation autour du soleil
    float degreesPerSecond;

    void Awake()
    {
        // On r�cup�re la plan�te et on lui donne un axe de rotation initial al�atoire
        child = transform.GetChild(0);
        childInitialRotation = new Vector3(Random.Range(-45f, 45f), 0, Random.Range(-45f, 45f));
        child.eulerAngles = childInitialRotation;

        // Vitese de r�volution al�atoire
        degreesPerSecond = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 5f);
    }

    void Update()
    {
        // La plan�te tourne autour du soleil
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        // Mais son axe de rotation n'est pas chang�
        child.eulerAngles = childInitialRotation;
    }
}

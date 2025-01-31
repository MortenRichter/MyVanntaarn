using UnityEngine;

public class RotateUFO : MonoBehaviour
{
    public float rotationSpeed = 20f; // Juster denne verdien for å endre hastigheten

    void Update()
    {
        // Roter UFO-en sakte rundt Y-aksen (opp/ned)
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

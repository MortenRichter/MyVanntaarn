using UnityEngine;
using UnityEngine.Splines;

public class LiquidMover : MonoBehaviour
{
    public SplineContainer splineContainer; // Referanse til splinen
    public Transform liquidObject; // Sylinderen vi skal vokse
    public float speed = 0.5f; // Hvor raskt væsken fyller røret

    private float t = 0f; // Startposisjon på splinen

    void Update()
    {
        if (splineContainer == null || liquidObject == null) return;

        // Øk t for å simulere at væsken fyller røret
        t += speed * Time.deltaTime;
        t = Mathf.Clamp01(t); // Holder t mellom 0 og 1

        // Hent posisjon og tangent fra splinen
        Vector3 newPosition = splineContainer.EvaluatePosition(t);
        Vector3 tangent = splineContainer.EvaluateTangent(t);

        // Bestem opp-retningen basert på splinens normal (forhindrer 90-graders hopp)
        Vector3 normal = splineContainer.EvaluateUpVector(t);

        // Lag en rotasjon som peker i tangentens retning, men holder normal korrekt
        Quaternion newRotation = Quaternion.LookRotation(tangent, normal);

        // Oppdater sylinderen
        liquidObject.position = newPosition;
        liquidObject.rotation = newRotation;
        liquidObject.localScale = new Vector3(0.2f, t * 10, 0.2f); // Øker høyden på sylinderen
    }
}


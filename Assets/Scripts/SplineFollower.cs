using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    [Header("Spline Referanse")]
    [Tooltip("Dra objektet med SplineRektangel-komponenten hit.")]
    public SplineRektangel splineRektangel;

    [Header("Bevegelsesinnstillinger")]
    [Tooltip("Hastigheten på bevegelsen (enheter per sekund)")]
    public float speed = 2f;

    // Intern variabel for å holde styr på nåværende waypoint-indeks
    private int currentIndex = 0;

    void Update()
    {
        // Sjekk at vi har en gyldig spline referanse og at den inneholder knotPoints
        if (splineRektangel == null || splineRektangel.knotPoints == null || splineRektangel.knotPoints.Count == 0)
        {
            return;
        }

        // Hent posisjonen til det nåværende waypointet
        Vector3 target = splineRektangel.knotPoints[currentIndex];

        // Flytt mot målet
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Når vi er veldig nær waypointet, gå videre til neste (og loop rundt ved siste)
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            currentIndex = (currentIndex + 1) % splineRektangel.knotPoints.Count;
        }
    }
}

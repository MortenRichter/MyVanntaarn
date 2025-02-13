using System.Collections.Generic;
using UnityEngine;

public class SplineRektangel : MonoBehaviour
{
    [Header("Spline Innstillinger")]
    [Tooltip("Fargen på linjene som tegner banen")]
    public Color lineColor = Color.green;

    [Tooltip("Skal banen lukkes (den siste knuten kobles til den første)?")]
    public bool loop = true;

    // Liste som lagrer posisjonene til alle knotpunktene (waypoints)
    public List<Vector3> knotPoints = new List<Vector3>();

    /// <summary>
    /// Oppdaterer listen over knotpunkter ved å hente posisjonene til alle direkte barn.
    /// </summary>
    void UpdateKnotPoints()
    {
        knotPoints.Clear();
        // Gå gjennom alle direkte barn (waypoints) til dette GameObject-et
        foreach (Transform child in transform)
        {
            knotPoints.Add(child.position);
        }
    }

    /// <summary>
    /// Tegner linjer mellom knotpunktene i editoren, slik at du kan se banen.
    /// Bruk OnDrawGizmos slik at linjene vises selv om spillet ikke kjører.
    /// </summary>
    void OnDrawGizmos()
    {
        // Oppdaterer listen med posisjoner
        UpdateKnotPoints();

        Gizmos.color = lineColor;

        // Tegn linjer mellom punktene
        for (int i = 0; i < knotPoints.Count; i++)
        {
            Vector3 currentPoint = knotPoints[i];
            Vector3 nextPoint;

            // Hvis ikke siste punkt, bruk neste punkt
            if (i < knotPoints.Count - 1)
            {
                nextPoint = knotPoints[i + 1];
            }
            // Hvis banen skal lukkes og vi er på siste punkt, koble tilbake til første punkt
            else if (loop && knotPoints.Count > 0)
            {
                nextPoint = knotPoints[0];
            }
            else
            {
                break;
            }

            Gizmos.DrawLine(currentPoint, nextPoint);
        }
    }
}

using System.Collections;
using UnityEngine;

public class SplineCylinderGrowth : MonoBehaviour
{
    public float growthRate = 0.1f; // Hvor raskt sylinderen vokser
    public float maxHeight = 5f; // Maksimal høyde på sylinderen
    public float width = 0.1f; // Bredde på sylinderen

    private Vector3[] splinePoints;
    private int currentPointIndex = 0;
    private float currentHeight = 0f;

    void Start()
    {
        // Definer splinepunktene
        splinePoints = new Vector3[]
        {
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(0.5f, 0, -0.5f)
        };

        // Plasser sylinderen på startpunktet
        transform.position = splinePoints[0];
    }

    void Update()
    {
        GrowCylinder();
    }

    void GrowCylinder()
    {
        if (currentPointIndex < splinePoints.Length)
        {
            // Vokse oppover første punkt til maks høyde
            if (currentHeight < maxHeight)
            {
                currentHeight += growthRate * Time.deltaTime;
                transform.localScale = new Vector3(width, currentHeight, width);
                // Juster posisjonen slik at sylinderen alltid står i midten
                transform.position = new Vector3(splinePoints[currentPointIndex].x, currentHeight / 2, splinePoints[currentPointIndex].z);
            }
            else
            {
                // Sving til neste punkt
                currentPointIndex++;
                currentHeight = 0f; // Reset høyden
                if (currentPointIndex < splinePoints.Length)
                {
                    // Flytt sylinderen til neste hjørne
                    transform.position = splinePoints[currentPointIndex];
                }
            }
        }
    }
}
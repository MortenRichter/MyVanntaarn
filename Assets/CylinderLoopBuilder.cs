using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderLoopBuilder : MonoBehaviour
{
    [Header("Loop Settings")]
    public float roofHeight = 2f;
    public float growthSpeed = 1f;
    public GameObject cylinderPrefab;
    public float thickness = 0.1f;

    // Lagrer segmentenes transform og lengden for hvert segment
    private List<Transform> segmentParents = new List<Transform>();
    private List<float> segmentLengths = new List<float>();

    void Start()
    {
        // Definerer hjørnene til en firkant (rektangel) – du kan justere disse verdiene
        Vector3 p0 = new Vector3(-0.5f, 0f, -0.5f);
        Vector3 p1 = new Vector3(-0.5f, roofHeight, -0.5f);
        Vector3 p2 = new Vector3(0.5f, roofHeight, -0.5f);
        Vector3 p3 = new Vector3(0.5f, 0f, -0.5f);

        // Opprett et overordnet GameObject for loopen
        GameObject loopParent = new GameObject("CylinderLoop");
        loopParent.transform.parent = transform;

        // Opprett segmenter for hver side i loopen:
        CreateSegment(loopParent.transform, p0, p1);
        CreateSegment(loopParent.transform, p1, p2);
        CreateSegment(loopParent.transform, p2, p3);
        CreateSegment(loopParent.transform, p3, p0);

        StartCoroutine(GrowLoop());
    }

    /// <summary>
    /// Lager et segment fra start til slutt, rotert slik at lokal Y-akse peker mot sluttpunktet.
    /// Instansierer også en cylinder som skal "vokse" langs segmentet.
    /// </summary>
    void CreateSegment(Transform parent, Vector3 start, Vector3 end)
    {
        GameObject segment = new GameObject("Segment");
        segment.transform.parent = parent;
        segment.transform.position = start;

        Vector3 dir = end - start;
        float length = dir.magnitude;
        Vector3 normalizedDir = dir.normalized;

        // Roter segmentet slik at den lokale up-aksen (Y) peker i retningen fra start til slutt
        segment.transform.rotation = Quaternion.FromToRotation(Vector3.up, normalizedDir);

        segmentParents.Add(segment.transform);
        segmentLengths.Add(length);

        // Instansier sylinderen som et barn av segmentet.
        GameObject cyl = Instantiate(cylinderPrefab, segment.transform);
        cyl.name = "CylinderSegment";

        // Start med skala 0 i Y og posisjonert slik at bunnen (med korrekt offset) ligger i 0
        // Siden standardcylinderen har pivot i midten (med høyde 2 ved scale.y = 1), skal vi
        // senere flytte den slik at bunnen = (localPosition.y - scale.y) = 0.
        // Her setter vi initialt posisjon til (0, 0, 0).
        cyl.transform.localPosition = Vector3.zero;
        cyl.transform.localRotation = Quaternion.identity;
        cyl.transform.localScale = new Vector3(thickness, 0f, thickness);
    }

    /// <summary>
    /// Animerer veksten av hvert segment ett etter ett.
    /// Oppdaterer både skala og posisjon slik at cylinderens bunn forblir fast i segmentets start.
    /// </summary>
    IEnumerator GrowLoop()
    {
        // For hver segment i den rekkefølgen de ble opprettet
        for (int i = 0; i < segmentParents.Count; i++)
        {
            Transform segment = segmentParents[i];
            float targetLength = segmentLengths[i];
            // Standard cylinder: når scale.y = 1, er høyden 2. Derfor:
            float targetScaleY = targetLength / 2f;

            Transform cyl = segment.GetChild(0);
            float currentScaleY = 0f;

            while (currentScaleY < targetScaleY)
            {
                currentScaleY += growthSpeed * Time.deltaTime;
                if (currentScaleY > targetScaleY)
                    currentScaleY = targetScaleY;

                // Oppdater skalaen i Y slik at cylinderens totale høyde = 2 * currentScaleY
                cyl.localScale = new Vector3(thickness, currentScaleY, thickness);
                // Oppdater posisjonen slik at cylinderens bunn (som er currentScaleY under midten)
                // forblir på 0. Det vil si at vi flytter sylinderen opp med currentScaleY.
                cyl.localPosition = new Vector3(0f, currentScaleY, 0f);

                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

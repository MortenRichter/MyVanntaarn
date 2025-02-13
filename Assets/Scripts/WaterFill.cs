using UnityEngine;

public class WaterFlowScript : MonoBehaviour
{
    // Øk hastigheten betydelig for raskere flyt
    public float flowSpeed = 5000.0f;  // Høyere hastighet

    // Startposisjon for vannstrømmen
    private Vector3 startPos;

    // Start størrelse på vannet
    private float initialScaleX = 2f;
    private float maxScaleX = 100f; // Maksimal lengde på vannet når det er fullt fylt (juster denne verdien etter røret ditt)

    void Start()
    {
        // Sett startposisjon og skala
        startPos = transform.position;
        startPos.x = 2f; // Startposisjon på X = 2
        transform.position = startPos;

        // Sett initial skala (2 enheter på X)
        transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        // Øk skala på X over tid (for å få vannet til å fylles raskt)
        float newScaleX = Mathf.Min(initialScaleX + Mathf.PingPong(Time.time * flowSpeed, maxScaleX - initialScaleX), maxScaleX);
        
        // Sett ny skala på X
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

        // Bevegelse av vannet i X-retning (fra X = 2 til max posisjon)
        transform.position = new Vector3(startPos.x + Mathf.PingPong(Time.time * flowSpeed, maxScaleX), startPos.y, startPos.z);
    }
}

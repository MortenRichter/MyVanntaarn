using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float moveSpeed = 12f; // Juster bevegelseshastigheten
    private Transform playerBody;

    private CharacterController characterController;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Skjuler musepekeren og låser den til midten av skjermen
        playerBody = transform.parent; // Setter Player Body som parent av kameraet (dvs. Player)
        characterController = playerBody.GetComponent<CharacterController>(); // Henter CharacterController fra Player
    }

    void Update()
    {
        // Bevegelse med WASD
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Beveg karakteren
        characterController.Move(move * Time.deltaTime);

        // Mus bevegelse for å se rundt
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Hindrer kameraet fra å snu seg helt rundt

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

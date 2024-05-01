using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        xRotation = 0f;
        yRotation = 0f;

        // Apply rotations and output to console
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        Debug.Log("Initial xRotation: " + xRotation);
        Debug.Log("Initial yRotation: " + yRotation);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamping to avoid flipping over

        yRotation += mouseX; // Horizontal rotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply vertical rotation
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f); // Apply horizontal rotation to the player body
    }
}

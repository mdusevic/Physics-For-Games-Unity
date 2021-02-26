using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 1000.0f;

    public GameObject target;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    private float rotX;
    private float rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse inputs
        rotY -= Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        rotX -= Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        // Clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle + 25.0f);

        // Clamp the horizontal rotation
        rotY = Mathf.Clamp(rotY, minTurnAngle, maxTurnAngle + 90.0f);

        // Rotate the camera
        transform.localRotation = Quaternion.Euler(rotX, -rotY, 0);

        // Move the camera position
        transform.position = target.transform.position + new Vector3(0, 3.5f, 0);
    }
}
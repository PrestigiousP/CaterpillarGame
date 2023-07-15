using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    private const float CAMERA_RADIUS = 0.8f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = new Vector3(orientation.position.x, orientation.position.y, transform.position.z + CAMERA_RADIUS);
        transform.LookAt(orientation.position);
    }

    // Update is called once per frame
    private void Update()
    {
        // get mouse input
        // donne des valeurs autour de 0 positives ou négatives
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseX;
        yRotation -= mouseY;

        yRotation = Mathf.Clamp(yRotation, -1, 1);

        float xCameraPos = Mathf.Cos(xRotation) * CAMERA_RADIUS;
        float yCameraPos = Mathf.Sin(yRotation) * CAMERA_RADIUS;
        float zCameraPos = Mathf.Sin(xRotation) * CAMERA_RADIUS;

        transform.localPosition = new Vector3(
            orientation.position.x + xCameraPos,
            orientation.position.y + yCameraPos,
            orientation.position.z + zCameraPos);


        transform.LookAt(orientation.position);
    }

    public void Move() { }
}

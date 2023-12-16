using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    public float cameraRadius;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = new Vector3(orientation.position.x, orientation.position.y, transform.position.z + cameraRadius);
        transform.LookAt(orientation.position);
    }

    // Update is called once per frame
    private void Update()
    {
        // get mouse input
        // donne des valeurs autour de 0 positives ou négatives
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _xRotation -= mouseX;
        _yRotation -= mouseY;

        _yRotation = Mathf.Clamp(_yRotation, -1, 1);

        float xCameraPos = Mathf.Cos(_xRotation) * cameraRadius;
        float yCameraPos = Mathf.Sin(_yRotation) * cameraRadius;
        float zCameraPos = Mathf.Sin(_xRotation) * cameraRadius;

        transform.localPosition = new Vector3(
            orientation.position.x + xCameraPos,
            orientation.position.y + yCameraPos,
            orientation.position.z + zCameraPos);

        transform.LookAt(orientation.position);
    }
}

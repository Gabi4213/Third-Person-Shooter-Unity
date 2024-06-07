using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform player;

    public float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensitivity = 400.0f;

    public float minYRotation;
    public float maxYRotation;

    public Vector3 positionOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, minYRotation, maxYRotation);

        // Calculate the direction offset to the left of the player
        Vector3 direction = Quaternion.Euler(0, currentX, 0) * new Vector3(-distance, 0, 0);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = lookAt.position + positionOffset;
        transform.position = targetPosition + rotation * direction;

        transform.LookAt(targetPosition);
    }
}

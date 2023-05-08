using UnityEngine;

public class InputControl : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float distance = 25.0f;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float height = 10f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private void Start()
    {
        UpdateRotation();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
            UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (target == null)
            return;

        rotationX += Input.GetAxis("Mouse X") * speed;
        rotationY -= Input.GetAxis("Mouse Y") * speed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = rotation * new Vector3(0.0f, height, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
        rotationX = 0;
        rotationY = 0;
        UpdateRotation();
    }
}

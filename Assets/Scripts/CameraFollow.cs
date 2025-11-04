using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 20f;
    public float smooth = 5f;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - target.position;
        offset.y = height;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.position + offset;
        desiredPos.y = height;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smooth);
    }
}

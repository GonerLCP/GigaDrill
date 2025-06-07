using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void LateUpdate()
    {
        Vector3 CameraPosition = target.position;
        CameraPosition.z = target.position.z - 10.0f;
        transform.position = CameraPosition;
    }

}

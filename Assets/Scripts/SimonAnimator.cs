using UnityEngine;

public class SimonAnimator : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void LateUpdate()
    {
        Vector3 CameraPosition = target.position;
        CameraPosition.z = target.position.z - 1.0f;
        transform.position = CameraPosition;
    }
}

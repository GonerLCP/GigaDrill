using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 CameraPosition = target.position;
        CameraPosition.z = target.position.z - 10.0f;
        transform.position = CameraPosition;
    }

}

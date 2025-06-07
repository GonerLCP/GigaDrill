using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    float movementSpeed;
    float rotationSpeed;

    public bool drilling;
    Vector2 direction;
    public Animator PlayerAnimator;
    public RythmManager _rm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSpeed = 2.0f;
        _rm = RythmManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            drilling = true;
        }
        else
        {
            drilling = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (_rm.RythmWindow == true)
            {
                Debug.Log("Dans la frame");
            }
            else
            {
                Debug.LogError("On est plus bon");
            }
        }
        horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        vertical = Input.GetAxis("Vertical") * movementSpeed;
        direction = new Vector2(horizontal, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


        rb.linearVelocity = direction;
    }
}

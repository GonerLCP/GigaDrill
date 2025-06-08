using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed;

    public InputSystem_Actions action;
    private InputAction move;
    private InputAction YButton;
    private InputAction BButton;
    private InputAction AButton;
    private InputAction XButton;

    public bool drilling;
    public bool exploding;

    Vector2 direction;
    public Animator PlayerAnimator;
    public Animator ExplosionAnimator;
    private GameManager _gm;
    private RythmManager _rm;
    public bool QTESuccess;
    private bool dead;

    public Transform RespawnPoint;

    private void Awake()
    {
        action = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = action.Player.Move;
        move.Enable();

        YButton = action.Player.Y;
        YButton.Enable();
        YButton.performed += YButtonPress;

        BButton = action.Player.B;
        BButton.Enable();
        BButton.performed += BButtonPress;

        AButton = action.Player.A;
        AButton.Enable();
        AButton.performed += AButtonPress;

        XButton = action.Player.X;
        XButton.Enable();
        XButton.performed += XButtonPress;
    }

    private void OnDisable()
    {
        move.Disable();
        YButton.Disable();
        BButton.Disable();
        AButton.Disable();
        XButton.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _rm = RythmManager.Instance;
        _gm = GameManager.Instance;
        exploding = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            drilling = true;
        }
        if (dead) { return; } //empêche le déplacement si on est mort
        direction = move.ReadValue<Vector2>() * movementSpeed;

        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        Vector2 moving = direction.normalized * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moving);
        //rb.linearVelocity = direction;
    }

    private void YButtonPress(InputAction.CallbackContext context)
    {
        print("Y");
        if (true) //A définir quand le joueur est en train de driller
        {
            if (_rm.increment != 0)
            {
                //Faire le script d'explosion
                Explosion();
            }
            if (_rm.RythmWindow == true)
            {
                _rm.RythmCompleted = true;
            }
            else
            {
                Explosion();
            }
        }
    }
    private void BButtonPress(InputAction.CallbackContext context)
    {
        print("B");
        if (true) //A définir quand le joueur est en train de driller
        {
            if (_rm.increment != 1)
            {
                //Faire le script d'explosion
                Explosion();
            }
            if (_rm.RythmWindow == true)
            {
                _rm.RythmCompleted = true;
            }
            else
            {
                Explosion();
            }
        }
    }
    private void AButtonPress(InputAction.CallbackContext context)
    {
        print("A");
        if (true) //A définir quand le joueur est en train de driller
        {
            if (_rm.increment != 2)
            {
                //Faire le script d'explosion
                Explosion();
            }
            if (_rm.RythmWindow == true)
            {
                _rm.RythmCompleted = true;
            }
            else
            {
                Explosion();
            }
        }
    }
    private void XButtonPress(InputAction.CallbackContext context)
    {
        print("X");
        if (true) //A définir quand le joueur est en train de driller
        {
            if (_rm.increment != 3)
            {
                //Faire le script d'explosion
                Explosion();
            }
            if (_rm.RythmWindow == true)
            {
                _rm.RythmCompleted = true;
            }
            else
            {
                Explosion();
            }
            
        }
    }

    public void Explosion()
    {
        dead = true;
        exploding = true;
        ExplosionAnimator.gameObject.SetActive(true);
        _gm.QTE.SetActive(false);
        StartCoroutine(Delai(2f));
    }

    IEnumerator Delai(float delaiLength)
    {
        yield return new WaitForSeconds(delaiLength);
        exploding = false;
        drilling = false;
        dead = false;
        ExplosionAnimator.gameObject.SetActive(false);
        transform.position = RespawnPoint.position;
    }
}

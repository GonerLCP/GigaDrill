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
    public float Impulsion;
    public float ImpulsionSetter;
    public float ImpulsionBlocReduce;

    //les 3 prochaines fonctions la, aucune idée de ce que ça fait, c'est pour l'input manager
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
        if (Input.GetKey(KeyCode.Space)) //Pour commencer le drilling appuyer sur espace
        {
            drilling = true;
        }
        if (dead) { return; } //empêche le déplacement si on est mort

        if (Impulsion >= 0.1f)
        {
            Impulsion -= 0.1f;
        }
        else { Impulsion = 0f; }
        
        if (move.ReadValue<Vector2>().magnitude> 0.1f)
        {
            direction = move.ReadValue<Vector2>() * movementSpeed;
        }
        Vector2 directiontampon = direction;
        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        Vector2 moving = direction.normalized * movementSpeed * Time.fixedDeltaTime*Impulsion;
        rb.MovePosition(rb.position + moving);
        //rb.linearVelocity = direction;
    }

    private void YButtonPress(InputAction.CallbackContext context)
    {
        ButtonPressLogic(0);
    }
    private void BButtonPress(InputAction.CallbackContext context)
    {
        ButtonPressLogic(1);
    }
    private void AButtonPress(InputAction.CallbackContext context)
    {
        ButtonPressLogic(2);
    }
    private void XButtonPress(InputAction.CallbackContext context)
    {
        ButtonPressLogic(3);
    }

    public void ButtonPressLogic(int increment)
    {
        if (drilling) //A définir quand le joueur est en train de driller
        {
            if (_rm.increment != increment) //Sers à detecter si l'on presse une autre touche que celle nécessaire
            {
                Explosion();
            }
            if (_rm.RythmWindow == true) //Si l'on est dans la bonne fenêtre d'opportunité, alors on l'indique au rythmanager
            {
                _rm.RythmCompleted = true;
            }
            else//Si on est pas dedans boumboum
            {
                Explosion();
            }
        }
        else if (_rm.increment == increment)
        {
            drilling = true;
        }
    }
    public void Explosion()
    {
        dead = true; //empeche le déplacement
        exploding = true;//Indique qu'on explose et que tout autour boumboum
        Impulsion = 0; //evite le drift au respawn
        ExplosionAnimator.gameObject.SetActive(true);//Activer et désactiver le gameobject est le seul moyen que j'ai trouvé de lancer l'anim
        _gm.QTE.SetActive(false);//On arrete le QTE
        StartCoroutine(Delai(2f));//Petite pause avant de respawn
    }

    IEnumerator Delai(float delaiLength)
    {
        //On remet tout à l'état de base et on ReTP
        yield return new WaitForSeconds(delaiLength);
        exploding = false;
        drilling = false;
        dead = false;
        ExplosionAnimator.gameObject.SetActive(false);
        transform.position = RespawnPoint.position;
    }

    IEnumerator DecreaseSpeed()
    {
            yield return new WaitForSeconds(1.0f);
        
    }
}

using UnityEngine;

public class Player : MonoBehaviour, Iinteractable
{
    delegate void PlayerDelegate();
    PlayerDelegate playerDelegate;

    private float privStamina=0;
    private string privName;
    private bool staminaDepleted;

    [SerializeField] public GameObject player;

    [Header("Movement")]
    public float walkSpeed=5f;
    public float sprintSpeed;

    public bool isWalking = false;
    

    //public Animator anim;
    private Vector2 movementInput;
    Rigidbody2D rb;
    public float stamina
    {
        get { return privStamina; }
        set { 
            privStamina = value;
            if (privStamina < 0)
            {
                staminaDepleted= true;
            }
            else
            {
                staminaDepleted= false;
            }
            privStamina = Mathf.Clamp(privStamina, 0, 100);
        }
    }

    public string name
    {
        get { return privName; }
        set { privName = value; }
    }

    public Player()
    {
        //constructor
    }

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerDelegate += MyInput;
        playerDelegate += SpeedControl;
    }
    public void Update()
    {
        playerDelegate();
    }

    public void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();
    }

    public void MovePlayer()
    {
        if(rb!= null)
        {
            rb.linearVelocity = movementInput * walkSpeed;
        }
        else
        {
            rb = player.GetComponent<Rigidbody2D>();
            rb.linearVelocity = movementInput * walkSpeed;
        }

        /*if (rb.velocity.magnitude > 0.01f)
        {
            anim.SetBool("isWalking", true); //Controls animation bopping if walking.

        }
        else
        {
            
                anim.SetBool("isWalking", false);
            
        }
        */

    }

    public void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);

        //limit velocity if needed
        if (flatVel.magnitude > walkSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * walkSpeed;
            rb.linearVelocity = new Vector2(limitedVel.x, limitedVel.y);
        }
    }

    public void InteractionActivated(GameObject gameObject)
    {
        //code
    }

    
}

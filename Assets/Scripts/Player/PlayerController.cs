using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxLives = 9;

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }
    #endregion

    #region Ground Check Parameters
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    private Shoot shoot;
    #endregion

    #region Lives and Score
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value > maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                _lives = 0;
            }
            else
            {
                _lives = value;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Max(0, value);
            Debug.Log($"Score has changed to {_score}");
        }
    }
    #endregion

    #region Power-ups
    //set to true when Mario picks up a Fire Flower - lets him shoot fireballs
    public bool hasFirePower = false;
    #endregion

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<Shoot>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(CalculateGroundCheckPos(), groundCheckRadius, groundLayer);
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        bool upPressed = Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0f;
        bool jumpPresssed = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPresssed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Up Arrow while in the air = jump kick
        if (upPressed && !isGrounded)
        {
            anim.SetTrigger("jumpAttack");
        }

        if (moveX != 0f) SpriteFlip(moveX);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));
        if (fireInput)
        {
            anim.SetTrigger("attack");

            if (hasFirePower && shoot != null)
            {
                shoot.Fire();
            }
        }
    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
}

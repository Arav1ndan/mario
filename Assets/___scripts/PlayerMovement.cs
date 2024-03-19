using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    PlayerControll player;
    private float direction =0;
    private float jumpingForce =0;
    private new Camera camera;
    public Rigidbody2D rb;
    public float speed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
   public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
   public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);
   
    private Vector2 moveVector;

    public bool grounded {get; private set;}
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(moveVector.x) > 0.25f || Mathf.Abs(direction) > 0.25f;
    public bool sliding => (direction > 0f && moveVector.x <0f) || (direction < 0f && moveVector.x > 0f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = new PlayerControll();
        player.Enable();

       player.Player.Move.performed += OnMove;
       player.Player.Jump.performed += OnJump;
       camera = Camera.main;
    }
   
   
    void Update()
    {
        Move();
        grounded = rb.Raycast(Vector2.down);

        if(grounded){
            jump();
        }
        
        ApplyGravity();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
         direction = context.ReadValue<float>();
          float value = context.ReadValue<float>();
         Debug.Log("Move input value: " + value);
         direction = value;
         moveVector = new Vector2(direction,0);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        jumpingForce = context.ReadValue<float>();
    }
   private void Move()
    {
        moveVector.x = Mathf.MoveTowards(moveVector.x,direction * speed, speed * Time.deltaTime);

        if(rb.Raycast(Vector2.right * moveVector.x)){
            moveVector.x = 0f;
        }

        if(moveVector.x > 0f){
            transform.eulerAngles = Vector2.zero;
        }else if( moveVector.x <0f){
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }
    /*private void Move()
    {
  
    float targetVelocityX = direction * speed * Time.deltaTime;
    float currentVelocityX = rb.velocity.x;
    float newVelocityX = Mathf.MoveTowards(currentVelocityX, targetVelocityX, speed * Time.deltaTime);

    rb.velocity = new Vector2(newVelocityX, rb.velocity.y);
    }   */ 
    private void jump()
    {
        moveVector.y = Mathf.Max(moveVector.y, 0f);
        jumping = moveVector.y > 0f;
        if(jumpingForce > 0)
        {
            moveVector.y = jumpForce;
            jumping = true;
            
        }else{
            moveVector.y = rb.velocity.y;
        }
    }
    private void ApplyGravity()
    {
        bool falling = moveVector.y <0f || jumpingForce == 0;
        float multiplier = falling ? 2f : 1f;

        moveVector.y += gravity * multiplier * Time.deltaTime;
        moveVector.y = Mathf.Max(moveVector.y, gravity /2f);
    }
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += moveVector * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        position.x = Mathf.Clamp(position.x,leftEdge.x + 0.09f,rightEdge.x -0.09f);
 
        rb.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                moveVector.y = jumpForce /2f;
                jumping = true;
            }
        }
        if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")){
           if(transform.DotTest(collision.transform, Vector2.up)){
            moveVector.y = 0f;
           }
        }
    }
}

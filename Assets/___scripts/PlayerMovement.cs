using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    PlayerControll player;
    private float direction =0;

    public Rigidbody2D rb;
    public float speed = 8f;
    private Vector2 moveVector;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = new PlayerControll();
        player.Enable();

       player.Player.Move.performed += OnMove;
       
    }
   
   
    void Update()
    {
        Move();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
         direction = context.ReadValue<float>();
          float value = context.ReadValue<float>();
         Debug.Log("Move input value: " + value);
         direction = value;
    }
   /* private void Move()
    {
        Vector2 move = transform.forward * moveVector.y;
        rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
    }*/
    private void Move()
{
    // Calculate desired horizontal velocity
    float targetVelocityX = direction * speed * Time.deltaTime;
    float currentVelocityX = rb.velocity.x;
    float newVelocityX = Mathf.MoveTowards(currentVelocityX, targetVelocityX, speed * Time.deltaTime);

    // Apply the adjusted horizontal velocity and maintain vertical velocity
    rb.velocity = new Vector2(newVelocityX, rb.velocity.y);
}

}

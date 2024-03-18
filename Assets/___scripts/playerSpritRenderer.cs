using UnityEngine;

public class playerSpritRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite Idle;
    public Sprite Jump;
    public Sprite Slide;
    public AnimatedSprite Run;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled =true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        Run.enabled = movement.running;

        if(movement.jumping){
            spriteRenderer.sprite = Jump;
        }else if(movement.sliding){
            spriteRenderer.sprite = Slide;
        }else if(!movement.running){
            spriteRenderer.sprite =Idle;
        }
    }
}

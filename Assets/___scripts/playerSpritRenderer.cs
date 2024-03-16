using UnityEngine;

public class playerSpritRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite Idle;
    public Sprite Jump;
    public Sprite Slide;
    public Sprite Run;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if(movement.jumping){
            spriteRenderer.sprite = Jump;
        }else if(movement.sliding){
            spriteRenderer.sprite = Slide;
        }else if(movement.running){
            spriteRenderer.sprite = Run;
        }else {
            spriteRenderer.sprite =Idle;
        }
    }
}

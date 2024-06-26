using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public playerSpritRenderer smallRender;
    public playerSpritRenderer bigRender;

     private playerSpritRenderer activeRenderer;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRender.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRender;
    }
     public void Hit()
    {
        if (!dead && !starpower)
        {
            if (big) {
                Shrink();
            } else {
                Death();
            }
        }
    }
    public void Death()
    {
        smallRender.enabled = false;
        bigRender.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
     public void Grow()
    {
        smallRender.enabled = false;
        bigRender.enabled = true;
        activeRenderer = bigRender;

        capsuleCollider.size = new Vector2(1f, 1.48f);
        capsuleCollider.offset = new Vector2(0f, 0.21f);

        StartCoroutine(ScaleAnimation());
    }
    
     public void Shrink()
    {
        smallRender.enabled = true;
        bigRender.enabled = false;
        activeRenderer = smallRender;

        capsuleCollider.size = new Vector2(0f, 0f);
        capsuleCollider.offset = new Vector2(0.23f, 0.3f);

       StartCoroutine(ScaleAnimation());
    }
     private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRender.enabled = !smallRender.enabled;
                bigRender.enabled = !smallRender.enabled;
            }

            yield return null;
        }

        smallRender.enabled = false;
        bigRender.enabled = false;
        activeRenderer.enabled = true;
    }
     public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }


}

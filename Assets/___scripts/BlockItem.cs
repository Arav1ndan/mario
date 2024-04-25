using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animated());
    }
    private IEnumerator Animated()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.019f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.2f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition ,t );
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;

        rb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;       
    }

   
}

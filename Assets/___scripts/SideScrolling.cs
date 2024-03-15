using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPostition = transform.position;
        cameraPostition.x = Mathf.Max(cameraPostition.x, player.position.x);
        transform.position = cameraPostition;
    }
}

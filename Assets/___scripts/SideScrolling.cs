using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private new Camera camera;
    private Transform player;
    public float height = -1.95f;
    public float undergroundHeight = -8.5f;
    public float undergroundThreshold = -2;


    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPostition = transform.position;
        cameraPostition.x = Mathf.Max(cameraPostition.x, player.position.x);
        transform.position = cameraPostition;
    }


    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}

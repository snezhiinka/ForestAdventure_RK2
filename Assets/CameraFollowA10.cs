
using UnityEngine;

public class CameraFollowA10 : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;
    public float heightThreshold = 1.5f;

    private float startX;
    private float startY;
    private float startZ;
    private float playerStartY;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;

        if (target != null)
            playerStartY = target.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float x = Mathf.Max(startX, target.position.x);

        float height = target.position.y
                     - playerStartY
                     - heightThreshold;

        float y = startY + Mathf.Max(0f, height);

        Vector3 wanted = new Vector3(x, y, startZ);

        transform.position = Vector3.Lerp(
            transform.position,
            wanted,
            smooth * Time.deltaTime
        );
    }
}

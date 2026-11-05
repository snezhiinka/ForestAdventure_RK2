
using UnityEngine;

public class AutoCameraSize : MonoBehaviour
{
    public float normalSize = 4.77f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float size = normalSize * (16f / 9f) / cam.aspect;

        cam.orthographicSize = Mathf.Max(normalSize, size);
    }
}

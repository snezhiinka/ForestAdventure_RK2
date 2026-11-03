using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float rotateSpeed = 100f;

    void Update()
    {
        transform.Rotate(
            Vector3.up * rotateSpeed * Time.deltaTime,
            Space.World
        );
    }
}
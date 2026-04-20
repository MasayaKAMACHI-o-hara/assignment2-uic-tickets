using UnityEngine;

public class scoreUI_rotetion_1: MonoBehaviour
{
    public float rotateSpeed = 80f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}

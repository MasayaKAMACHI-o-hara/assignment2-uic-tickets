using UnityEngine;

public class scoreUI_rotetion : MonoBehaviour
{
    public float rotateSpeed = 80f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}

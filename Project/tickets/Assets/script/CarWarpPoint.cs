using UnityEngine;

public class CarWarpPoint : MonoBehaviour
{
    [Header("ワープ先")]
    public Transform warpTarget;

    [Header("ワープ後に向かう次のポイント")]
    public GameObject nextPointAfterWarp;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        CarMove car = other.GetComponent<CarMove>();
        if (car == null) return;

        other.transform.position = warpTarget.position;
        other.transform.rotation = warpTarget.rotation;

        if (nextPointAfterWarp != null)
        {
            car.nextpoint = nextPointAfterWarp;
        }
    }
}
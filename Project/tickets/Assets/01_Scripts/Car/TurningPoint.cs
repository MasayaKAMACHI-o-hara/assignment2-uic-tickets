using UnityEngine;

public class TurningPoint : MonoBehaviour
{
    public GameObject NextPoint;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {


            CarMove car = other.GetComponent<CarMove>();

            car.TurnMove(gameObject,NextPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

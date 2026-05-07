using UnityEngine;

public class Clash : MonoBehaviour
{
    public GameObject Kan;
    public AudioSource ClashSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Kan)
        {
            Debug.Log("缶を引いちゃったよ。");
            ClashSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

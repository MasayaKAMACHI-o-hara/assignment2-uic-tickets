using UnityEngine;

public class Clash : MonoBehaviour
{
    public GameObject Kan;
    public AudioSource ClashSound;


    public Transform KanPosition;

    private KanMove Kanscr;
    private GameObject End;
    private ClearManegar Endscr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KanPosition = Kan.transform.Find("KanMidPos");
        Kanscr = Kan.GetComponent<KanMove>();
        End = GameObject.Find("ClearUI");
        Endscr = End.GetComponent<ClearManegar>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == KanPosition.gameObject)
        {
            Debug.Log("缶を引いちゃったよ。");
            ClashSound.Play();
            Kanscr.Clash();
            Endscr.StartCoroutine(Endscr.GameEnd());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

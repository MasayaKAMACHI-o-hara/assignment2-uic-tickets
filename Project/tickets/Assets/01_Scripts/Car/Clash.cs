using UnityEngine;

public class Clash : MonoBehaviour
{
    public GameObject Kan;

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
        if (other.gameObject == KanPosition.gameObject && !Kanscr.carInvincible)
        {
            Debug.Log("缶を引いちゃったよ。");
            SoundManager.PlaySE_CarHorn();
            Kanscr.Clash();
            Endscr.StartCoroutine(Endscr.GameEnd());
        }
    }
}

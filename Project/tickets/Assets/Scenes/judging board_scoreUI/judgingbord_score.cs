using UnityEngine;

public class judgingbord_score : MonoBehaviour
{
    public int scoreValue = 10; // �ύXNG�Q���X�̕\�L(�X�R�A�E���O)

    [SerializeField]
    RectTransform canvasRect; //Canvas��w�肷��ϐ�

    [SerializeField]
    judgingbord_score_UI scoreUIPrefab; //UIprefab

    private judgingbord_score_UI scoreUI; //UI�̕ϐ�
    public ClearManegar Clear;
    public KanMove Kan;

    void Start()
    {
        scoreUI = Instantiate(scoreUIPrefab, canvasRect);//�Q�[���J�n����UI��\��

        scoreUI.targetTran = transform; //UI���Ǐ]����Ώ�(�����)

        scoreUI.SetScore(scoreValue); //UI�ɓn���X�R�A�l
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kan")) //can���������Ƃ�
        {
            if(Kan.ActiveMove)
            {
                StartCoroutine(Clear.GameFinish(scoreValue));

            }

            
        }
    }
}
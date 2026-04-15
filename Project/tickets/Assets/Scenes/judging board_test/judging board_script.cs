using UnityEngine;

public class judgingboard_script : MonoBehaviour
{
    public string judgingboard = "judging board"; // �����蔻��^�O
    public string judgingboard_100 = "judging board_100"; // �����蔻��^�O
    public string judgingboard_200 = "judging board_200"; // �����蔻��^�O
    public string judgingboard_300 = "judging board_300"; // �����蔻��^�O

    private bool hasScored = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return; // ���ɃX�R�A���\������Ă����烍�O��o���Ȃ�
        
        if (collision.gameObject.CompareTag("judging board"))
        {
            Debug.Log("Goal"); // judging board�e�X�g
            hasScored = true;
        }
        else if (collision.gameObject.CompareTag("judging board_100"))
        {
            Debug.Log("Goal_100point"); // �X�R�A100
            hasScored = true;
        }
        else if (collision.gameObject.CompareTag("judging board_200"))
        {
            Debug.Log("Goal_200point");// �X�R�A200
            hasScored = true;
        }
        else if (collision.gameObject.CompareTag("judging board_300"))
        {
            Debug.Log("Goal_300point"); // �X�R�A300
            hasScored = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

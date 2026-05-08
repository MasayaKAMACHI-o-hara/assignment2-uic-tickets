
//kanmove
using System.Collections;
using UnityEngine;

public class KanMove : MonoBehaviour
{

    void Awake()
    {
        // FPSを480に固定する
        Application.targetFrameRate = 480;
    }

    public Rigidbody body;

    public Transform Camera;

    public ClearManegar ClearUI;

    public AudioSource SE;
    public AudioSource SERoll;

    public AudioClip JumpSound;
    public AudioClip RollSound;

    public float Speed = 0;

    [Range(4, 10)]
    public float MaxSpeed = 4f;

    [Range(10, 15)]
    public float JumpPower = 10;

    public bool isGrounded = true;

    public SyakaSyaka isFly;

    public bool ActiveMove = false;

    public bool ActiveSound = false;

    public int coin = 0;//所持コイン

    public bool CoinUp = false;

    [Range(0, 5)]
    public int SpeedLv = 0;//スピードレベル
    [Range(0, 5)]
    public int JumpLv = 0;//ジャンプレベル



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void GetCoin()
    {
        if (CoinUp)
            coin += 20;
        else
            coin += 10;
    }


    public void SpeedUp(int L)//自販機によって、スピードLvが上昇する際に実行
    {
        if (SpeedLv < 5)
        {
            SpeedLv += L;
            if (SpeedLv > 5)
                SpeedLv = 5;
            MaxSpeed = 2 * SpeedLv;//現在のスピードレベルに合わせて最高速度を上昇させる
        }
    }

    public void JumpUp(int L)//自販機によって、ジャンプLvが上昇する際に実行
    {
        if (JumpLv < 5)
        {
            JumpLv += L;
            if (JumpLv > 5)
                JumpLv = 5;
            JumpPower = 10 + JumpLv;//現在のジャンプレベルに合わせてジャンプ力を上昇させる
        }
    }


   

    // Update is called once per frame
    void Update()
    {
        if (ActiveMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Speed <= 2)
                {
                    Speed += 0.05f;
                }
            }
            else if (Speed > 0)
            {
                Speed -= 0.05f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (Speed >= -2)
                {
                    Speed -= 0.05f;
                }
            }
            else if (Speed < 0)
            {
                Speed += 0.05f;
            }
            Vector3 rot = this.transform.eulerAngles;



            if (Input.GetKey(KeyCode.A) && !isFly.IsActiveFly)
            {
                // Y軸の数値に -0.5
                rot.y -= 0.5f;

                // オブジェクトに反映
                transform.eulerAngles = rot;

            }

            if (Input.GetKey(KeyCode.D) && !isFly.IsActiveFly)
            {
                // Y軸の数値に +0.5
                rot.y += 0.5f;

                // オブジェクトに反映
                transform.eulerAngles = rot;

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 「Player」レイヤー以外すべてを対象にするための設定（ビット演算っていうのを使う）
                int layerMask = ~(1 << LayerMask.NameToLayer("Kan"));

                // 最後の引数に layerMask を入れることで、自分（Player）を無視して光線を飛ばせる
                isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, layerMask);

                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    body.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                    SE.PlayOneShot(JumpSound);
                }
            }
            // カメラの正面方向を取得
            Vector3 forward = Camera.forward;

            // 上下の向き（Y軸）を無視して、水平方向にだけ進むように調整
            forward.y = 0;
            forward = forward.normalized;


            if (body.linearVelocity.magnitude < MaxSpeed)
                this.body.AddForce(forward * Speed);

            if (transform.position.y < -10)
            {
                ClearUI.IsGameOver = true;
                StartCoroutine(ClearUI.GameEnd());
            }


        }

    }

}

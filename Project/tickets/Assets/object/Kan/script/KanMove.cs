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

    public float Speed = 0;

    public float MaxSpeed = 0.5f;

    public float JumpPower = 30;

    public bool isGrounded = true;

    public SyakaSyaka isFly;

    public bool ActiveMove = false;

     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ActiveMove)
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
                }
            }
            // カメラの正面方向を取得
            Vector3 forward = Camera.forward;

            // 上下の向き（Y軸）を無視して、水平方向にだけ進むように調整
            forward.y = 0;
            forward = forward.normalized;


            if (body.linearVelocity.magnitude < MaxSpeed)
                this.body.AddForce(forward * Speed);



        }

    }
    
}


using UnityEngine;
using TMPro;


public class SyakaSyaka : MonoBehaviour
{

    public bool Syaka = false;
    public bool SyakaFly = false;
    public bool SyakaStart = false;
    public bool ChargeStart = false;
    public bool ChargeEnd = false;
    public bool JumpUp = false;
    public bool IsActiveFly = false;
    public bool IsActiveFall = false;


    public float SyakaPoint;
    public float Syakacount;
    public float SyakaCharge;
    public float SyakaRemove;
    public float SyakaE = 1.0f;//シャカシャカゲージ獲得倍率
    private float timer = 0f;
    private float ignoreTime = 0.1f; // 0.1秒間は判定しない

    public GameObject SyakaUI;
    public GameObject houkou;

    public GameObject ChargeP;
    public GameObject ColaP;
    public GameObject BubbleP;

    public Transform Camera;
    public Rigidbody Rb;

    //private float currentY = 0.0f; // マウスの上下移動量
    public float sensitivity = 3.0f; // マウス感度

    public Vector3 StartPos;
    public Vector3 MidPos;
    public Vector3 lastPos;
    public Vector3 nowPos;
    public Vector3 SyakaPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SyakaE = 1.0f;


    }



    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

        // 「Player」レイヤー以外すべてを対象にするための設定（ビット演算っていうのを使う）
        int layerMask = ~(1 << LayerMask.NameToLayer("Kan"));

        // 最後の引数に layerMask を入れることで、自分（Player）を無視して光線を飛ばせる
        Syaka = Physics.Raycast(transform.position, Vector3.down, 1.1f, layerMask);

        if (!Syaka && !SyakaStart && !IsActiveFly)//もし現在自分の下にオブジェクトがないなら
        {
            StartPos = transform.position;//現在の座標を開始地点に設定する
            lastPos = transform.position;//現在の座標を記録する
            SyakaStart = true;//しゃかしゃか開始！
            JumpUp = true;//現在ジャンプ中という判定をつける。

        }

        if (SyakaStart && !Syaka && !IsActiveFly)//もし自分の下にオブジェクトがないなら
        {
            nowPos = transform.position;//現在の座標を記録する

            if (JumpUp && (lastPos.y > nowPos.y))//もしジャンプ中に前回記録した座標のほうが今回より高かったら、
            {
                MidPos = lastPos;//最高地点を設定する
                JumpUp = false;//ジャンプ終了
            }

            lastPos = nowPos;//座標の位置を更新する

        }

        if (SyakaStart && Syaka && !IsActiveFly)//もしオブジェクトが下についたら
        {
            float mathX;
            float mathY;
            float mathZ;

            mathY = (MidPos.y - StartPos.y) + (MidPos.y - nowPos.y);//移動したy座標の総距離の整数値をぽいんとにする

            if (nowPos.x > StartPos.x)
            {
                mathX = nowPos.x - StartPos.x;
            }
            else
            {
                mathX = StartPos.x - nowPos.x;
            }

            if (nowPos.z > StartPos.z)
            {
                mathZ = nowPos.z - StartPos.z;
            }
            else
            {
                mathZ = StartPos.z - nowPos.z;
            }

            float MathQ = (mathX / 2) + mathY + (mathZ / 2);

            if (MathQ < 0)
            {
                MathQ = 0;
            }

            SyakaPoint += MathQ * SyakaE;//ポイントに獲得倍率の数値を掛けた数を総計する。
            if (SyakaPoint > 100)//ゲージの最大値は100
            {
                SyakaPoint = 100;
            }
            SyakaUI.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(SyakaPoint).ToString();
            SyakaStart = false;//シャカシャカゲージのUI表示
        }

        // 最後の引数に layerMask を入れることで、自分（Player）を無視して光線を飛ばせる
        SyakaFly = Physics.Raycast(transform.position, Vector3.down, 0.5f, layerMask);


        if (IsActiveFly)
        {
            timer += Time.deltaTime;
        }


        if (SyakaFly && IsActiveFly)
        {

            if (timer < ignoreTime) return; // まだ無視する時間ならここで処理を抜ける

            if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
            {
                IsActiveFly = false;
                IsActiveFall = false;
                ColaP.SetActive(false);
                BubbleP.SetActive(false);

            }

        }




        if (Input.GetMouseButtonDown(0) && !IsActiveFly && SyakaPoint > 1)
        {
            SyakaPos = transform.position;
            SyakaCharge = 0;
            ChargeP.SetActive(true);
            ChargeStart = true;
            ChargeEnd = true;
            // 最新のプロパティで速度をリセット
            Rb.linearVelocity = Vector3.zero;

            // 回転速度も新しいやつがあるよ
            Rb.angularVelocity = Vector3.zero;
        }



        if (ChargeStart && Input.GetMouseButton(0))
        {
            SyakaPos.y = transform.position.y;
            transform.position = SyakaPos;

            // 1. まずカメラと同じ向きにする
            Quaternion cameraRotation = Quaternion.LookRotation(Camera.transform.forward);

            // 2. 「上に-130度（X軸回転）」のデータを作る
            Quaternion upOffset = Quaternion.Euler(-130f, 0, 0);

            // 3. 掛け算して合成する（※順番が大事！）
            transform.rotation = cameraRotation * upOffset;

            // currentY -= Input.GetAxis("Mouse Y") * sensitivity;
            // 上下の回転角度を制限（地面に埋まったり真上を過ぎたりしないように）
            //currentY = Mathf.Clamp(currentY, -80f, 80f);


            houkou.SetActive(true);

            if (SyakaPoint > 1)
            {
                SyakaRemove++;
                if (SyakaRemove > 10)
                {
                    SyakaPoint--;
                    SyakaCharge++;
                    SyakaUI.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(SyakaPoint).ToString();
                    SyakaRemove = 0;
                }
            }
            else
            {
                ChargeP.SetActive(false);
            }


        }
        else
        {
            houkou.SetActive(false);
            ChargeStart = false;
        }


        if (Input.GetMouseButtonUp(0) && !IsActiveFly && ChargeEnd)
        {
            ChargeEnd = false;
            ChargeP.SetActive(false);
            ColaP.SetActive(true);
            BubbleP.SetActive(true);

            timer = 0;

            IsActiveFly = true;

            var For = Camera.forward;

            var syakaPower = (SyakaCharge * 100)/2;

            // 「上」と「前」を足すと、45度の斜め上になります
            Vector3 slantDirection = (Vector3.up + For).normalized;

            // これに強さを掛けるて弾き飛ばす
            Rb.AddForce(slantDirection * syakaPower);
        }

        // 飛んでいる最中にスペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space) && IsActiveFly && !IsActiveFall)
        {


            if (Rb != null)
            {
                // 最新のプロパティで速度をリセット
                Rb.linearVelocity = Vector3.zero;

                // 回転速度も新しいやつがあるよ
                Rb.angularVelocity = Vector3.zero;

                // 3. (オプション) 勢いよく落としたいなら下向きに力を加える
                Rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            }

            IsActiveFall = true;
        }

    }

    void FixedUpdate()
    {
        if (IsActiveFly)
        {
            // velocity を linearVelocity に書き換え
            Vector3 velocity = Rb.linearVelocity;

            if (velocity.sqrMagnitude > 0.1f)
            {

                Quaternion targetRotation = Quaternion.LookRotation(velocity) * Quaternion.Euler(110, 180, 0);
                Rb.MoveRotation(targetRotation);
            }

        }

    }
}

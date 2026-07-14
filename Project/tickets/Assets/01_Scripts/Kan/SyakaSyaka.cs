//syakasyaka
using UnityEngine.UI;
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
    public bool IsActiveSpace = false;

    public bool ActiveSyaka = false;

    public AudioSource SE;

    public AudioClip FallSound;

    public AudioClip StartChargeSound;
    public AudioClip ChargeConpleteSound;
    public AudioClip ShotSound;
    public AudioClip SyuwaSound;
    public AudioClip SyuwaEndSound;

    public float SyakaPoint;
    public float Syakacount;
    public float SyakaCharge;
    public float SyakaRemove;

    [Range(0, 5)]
    public int GetSyakaLv = 0;//ゲージ倍率レベル

    [Range(1, 2)]
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

    // syakasyakaUIのゲージを動かすためのやつ
    public Image cola;
    public Image flashcola;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SyakaE = 1.0f;
    }

    public void GageUp(int L)//自販機によって、ゲージ獲得倍率Lvが上昇する際に実行
    {
        if (GetSyakaLv < 5)
        {
            GetSyakaLv += L;
            if (GetSyakaLv > 5)
                GetSyakaLv = 5;
            SyakaE = 1 + (GetSyakaLv * 2) / 10;//現在のジャンプレベルに合わせてジャンプ力を上昇させる
        }
    }

    public void SyakaGageHeal(int value)//シャカシャカゲージを回復する際に実行
    {
        SyakaPoint += value;//ゲージにvalue分数値を加える
        if (SyakaPoint > 100)//ゲージの最大値は100
        {
            SyakaPoint = 100;
        }
        SyakaUI.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(SyakaPoint).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveSyaka)
        {

            Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

            // 「Player」レイヤー以外すべてを対象にするための設定（ビット演算っていうのを使う）
            int layerMask = ~(1 << LayerMask.NameToLayer("Kan"));

            // 最後の引数に layerMask を入れることで、自分（Player）を無視して光線を飛ばせる
            Syaka = Physics.Raycast(transform.position, Vector3.down, 0.3f, layerMask);

            if (!Syaka && !SyakaStart && !IsActiveFly && !ChargeStart) //もし現在自分の下にオブジェクトがないなら
            {
                StartPos = transform.position;//現在の座標を開始地点に設定する
                lastPos = transform.position;//現在の座標を記録する
                SyakaStart = true;//しゃかしゃか開始！
                JumpUp = true;//現在ジャンプ中という判定をつける。

            }

            if (SyakaStart && !Syaka && !IsActiveFly && !ChargeStart)//もし自分の下にオブジェクトがないなら
            {
                nowPos = transform.position;//現在の座標を記録する

                if (JumpUp && (lastPos.y > nowPos.y))//もしジャンプ中に前回記録した座標のほうが今回より高かったら、
                {
                    MidPos = lastPos;//最高地点を設定する
                    JumpUp = false;//ジャンプ終了
                }

                lastPos = nowPos;//座標の位置を更新する

            }

            if (SyakaStart && Syaka && !IsActiveFly && !ChargeStart)//もしオブジェクトが下についたら
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

                float MathQ = (mathX / 4) + mathY + (mathZ / 4);
                //　シャカゲのたまり方を無理やり半減してほとんど解決かもしれない可能性なきにしもあらず→                MathQ = MathQ / 2;

                Debug.Log("MathQ=" + MathQ);

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

                if (MathQ * SyakaE >= 10)
                {
                    SoundManager.PlaySE_KanLanding();
                }


                SyakaStart = false;//シャカシャカゲージ貯め終了
            }

            // 最後の引数に layerMask を入れることで、自分（Player）を無視して光線を飛ばせる
            SyakaFly = Physics.Raycast(transform.position, Vector3.down, 0.2f, layerMask);


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
                    SoundManager.StopAS_Kan();
                    SoundManager.PlaySE_KanSquirtEnd();

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
                Rb.angularVelocity = Vector3.zero;
                SoundManager.PlaySE_KanCharge();
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

                if (SyakaPoint >= 1.0f)//もしシャカシャカゲージをたまっているなら
                {
                    SyakaRemove += 1.0f;
                    if (SyakaRemove > 10.0f)//10フレーム毎で
                    {
                        SyakaPoint -= 1.0f;//シャカシャカポイントを１減少させる。
                        SyakaCharge += 1.0f;//１ptチャージする
                        SyakaUI.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(SyakaPoint).ToString();//UI表記を変更する。
                        SyakaRemove = 0;
                    }

                    if (SyakaPoint < 1)
                    {
                        SoundManager.StopAS_Kan();
                        SoundManager.PlaySE_KanChargeComplete();//チャージが完了したら音を出す。
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

                var syakaPower = (SyakaCharge * 100) / 2;

                // 「上」と「前」を足すと、45度の斜め上になります
                Vector3 slantDirection = (Vector3.up + For).normalized;

                // これに強さを掛けるて弾き飛ばす
                Rb.AddForce(slantDirection * syakaPower);
                
                SoundManager.StopAS_Kan();
                SoundManager.PlaySE_KanSquirtStart();
                SoundManager.PlaySE_KanSquirting();//発射するときの音を出す
            }

            // 飛んでいる最中にスペースキーが押されたら
            if (Input.GetKeyDown(KeyCode.Space) && IsActiveFly && !IsActiveFall && IsActiveSpace)
            {
                
                if (Rb != null)
                {
                    // 最新のプロパティで速度をリセット
                    Rb.linearVelocity = Vector3.zero;

                    // 回転速度も新しいやつがあるよ
                    Rb.angularVelocity = Vector3.zero;

                    // 3. (オプション) 勢いよく落としたいなら下向きに力を加える
                    Rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
                    
                    SoundManager.PlaySE_KanFall();
                }

                IsActiveFall = true;
            }

            // コーラのゲージ変動
            if (cola != null && flashcola != null)
            {
                cola.fillAmount = SyakaPoint / 100f;
                flashcola.fillAmount = SyakaPoint / 100f;
            }
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

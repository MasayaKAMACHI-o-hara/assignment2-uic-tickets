using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    [Header("NPCのオブジェクト")]
    [SerializeField] private Transform Transform;
    [SerializeField] private Animator animator;
    [Header("移動経由地点のオブジェクト")]
    [SerializeField] private Transform[] wayTransform;
    [SerializeField] private Collider[] wayCollider;
    [Header("缶")]
    [SerializeField] private Rigidbody KanRigidbody;
    [SerializeField] private AudioSource KanAudioSource;
    [SerializeField] private AudioClip KanAudioClip;
    [Header("スピーカー")]
    [SerializeField] private AudioSource audioSource;
    //アニメーションのbool関数
    bool idle = true;    //停止
    bool walk = false;   //歩き
    bool kickNow = false;//蹴り
    [Header("蹴り")]
    [SerializeField] float kickPower = 30f;
    [SerializeField] public bool HumanCoffee = false;
    [Header("ポーズ")]
    [SerializeField] private PauseSistem pauseSistem;
    //停止時間カウンター
    float idleTime = 0;

    //移動速度
    float spped = 1.5f;

    //経由地点のナンバー
    int waypoint = 0;
    //経由地点の要素数
    int pointMax;

    //初期設定
    void Start()
    {
        //アニメーションの初期状態設定
        animator.SetBool("idle", true);
        animator.SetBool("walk", false);

        //経由地点の要素数を取得
        pointMax = wayTransform.Length;
    }

    //メイン関数
    void Update()
    {
        //蹴っていなければ
        if (!kickNow)
        {
            // idle中
            if (idle)
            {
                //停止時間カウント
                idleTime += Time.deltaTime;

                //停止時間が終わったら
                if (idleTime >= 4)
                {
                    //状態・アニメーション切り替え
                    idle = false;
                    animator.SetBool("idle", false);
                    walk = true;
                    animator.SetBool("walk", true);

                    //停止時間リセット
                    idleTime = 0;

                    //最後の経由地点ナンバーなら
                    if (waypoint + 1 == pointMax)
                        waypoint = 0;//初期位置の経由地点ナンバーを指定
                                     //最後以外なら
                    else
                        waypoint++;//次の経由地点ナンバーを指定

                    //指定された経由地点の方向を向く
                    Transform.LookAt(wayTransform[waypoint]);

                    //足音再生
                    audioSource.Play();

                }
            }
            // walk中
            if (walk)
            {
                //経由地点方向のベクトルを計算
                Vector3 direction = (wayTransform[waypoint].position - Transform.position).normalized;

                //経由地点に向かって歩く
                transform.position += direction * spped * Time.deltaTime;

                //ポーズ中なら
                if (pauseSistem.IsActivePause)
                {
                    //足音一時停止
                    audioSource.Pause();
                }
                else//ポーズ中じゃなければ
                {
                    //足音一時停止解除
                    audioSource.UnPause();
                }
            }
        }
    }

    //経由地点到着時関数
    private void OnTriggerEnter(Collider other)
    {
        //経由地点に触れれば
        if (other == wayCollider[waypoint])
        {
            //状態・アニメーション切り替え
            idle = true;
            animator.SetBool("idle", true);
            walk = false;
            animator.SetBool("walk", false);
            //足音停止
            audioSource.Stop();
        }
    }

    //缶発見時関数
    public void OnKanEnter()
    {
        //足音停止
        audioSource.Stop();

        // キックアニメーションを再生
        animator.SetTrigger("kick");

        //蹴っている
        kickNow = true;
    }

    //アニメーションイベント・蹴る関数
    public void kick()
    {
        if (HumanCoffee == false)
        {
            if (KanRigidbody != null)
            {
                // 1. 飛ばす方向を決定（NPCの正面方向）
                Vector3 kickDirection = Transform.forward;

                // 2. 少し上に浮かせる力を加えると「蹴った感」が出ます（お好みで）
                kickDirection += Vector3.up * 1.5f;

                // 3. 力を加える（速度をリセットしてから加えると安定します）
                KanRigidbody.linearVelocity = Vector3.zero; // 前の動きをリセット(Unity2023以降はlinearVelocity)
                KanRigidbody.AddForce(kickDirection.normalized * kickPower, ForceMode.Impulse);
                KanAudioSource.PlayOneShot(KanAudioClip);
                Debug.Log("缶を蹴飛ばしました！");
            }
        }
    }
    //アニメーションイベント・キックアニメーション終了時関数
    public void KickEnd()
    {
        //足音再生
        audioSource.Play();

        //蹴っていない
        kickNow = false;
    }
}


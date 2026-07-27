using UnityEngine;
public enum NPCType
{
    Male=1,
    Female=2,
    Boy=3
}
public class NPCAction : MonoBehaviour
{
    private Animator animator;
    private AudioSource SE_NPCFootsteps;
    private Material nomalMaterial;
    
    [Header("種類")]
    [SerializeField] private NPCType type;
    [Header("缶")]
    [SerializeField] public Rigidbody KanRigidbody;
    [Header("ポーズ")]
    [SerializeField] private PauseSistem pauseSistem;
    [Header("マテリアル関係")]
    [Tooltip("強化マテリアル")][SerializeField]
    private Material powerUpMaterial;
    [Tooltip("メッシュ")]
    private SkinnedMeshRenderer mesh;
    [SerializeField] private SkinnedMeshRenderer mesh2_Boy;
    [Header("移動経由地のオブジェクト")]
    [SerializeField] private Transform[] wayTransform;
    [SerializeField] private Collider[] wayCollider;
    
    // アニメーション用bool変数
    bool idle = true;    // 停止
    bool walk;   // 歩き
    bool kickNow;// 蹴り中
    
    // 停止時間カウンター
    float idleTime;

    // 移動速度
    float spped = 1.5f;

    // 経由地のナンバー
    int waypoint = 0;
    // 経由地の要素数
    int pointMax;

    void Start()
    {
        // コンポーネント値代入
        animator = GetComponent<Animator>();
        SE_NPCFootsteps = GetComponent<AudioSource>();
        nomalMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        // アニメーションの初期状態設定
        animator.SetBool("idle", true);
        animator.SetBool("walk", false);

        // 経由地の要素数を取得
        pointMax = wayTransform.Length;
    }

    void Update()
    {
        if (NPCManager.Instance.humanCoffee)
        {
            mesh.material = powerUpMaterial;
            if(type == NPCType.Boy)mesh2_Boy.material = powerUpMaterial;
        }
        else
        {
            mesh.material = nomalMaterial;
            if(type == NPCType.Boy)mesh2_Boy.material = nomalMaterial;
        }
        // 蹴っていないとき
        if (!kickNow)
        {
            // idle時
            if (idle)
            {
                // 停止時間カウント
                idleTime += Time.deltaTime;

                // 停止時間が終了したら
                if (idleTime >= 4)
                {
                    // 状態・アニメーション切り替え
                    idle = false;
                    animator.SetBool("idle", false);
                    walk = true;
                    animator.SetBool("walk", true);

                    // 停止時間リセット
                    idleTime = 0;

                    // 最後の経由地ナンバーなら
                    if (waypoint + 1 == pointMax)
                        waypoint = 0;// 初期位置の経由地ナンバーを指定
                    // 最後以外なら
                    else
                        waypoint++;// 次の経由地ナンバーを指定

                    // 指定された経由地の方向を向く
                    transform.LookAt(wayTransform[waypoint]);

                    // 音声再生
                    SE_NPCFootsteps.Play();
                }
            }
            // walk時
            if (walk)
            {
                // 経由地までのベクトルを計算
                Vector3 direction = (wayTransform[waypoint].position - transform.position).normalized;

                // 経由地に向かって歩く
                transform.position += direction * spped * Time.deltaTime;

                // ポーズ中なら
                if (pauseSistem.IsActivePause)
                {
                    // 音声一時停止
                    SE_NPCFootsteps.Pause();
                }
                else// ポーズ中ではないとき
                {
                    // 音声一時停止解除
                    SE_NPCFootsteps.UnPause();
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 経由地に触れたら
        if (other == wayCollider[waypoint])
        {
            // 状態・アニメーション切り替え
            idle = true;
            animator.SetBool("idle", true);
            walk = false;
            animator.SetBool("walk", false);
            // 音声停止
            SE_NPCFootsteps.Stop();
        }
    }
    
    [Tooltip("缶接近時関数")]
    public void OnKanEnter()
    {
        // 音声停止
        SE_NPCFootsteps.Stop();

        // キックアニメーションを再生
        animator.SetTrigger("kick");

        // 蹴っている
        kickNow = true;
    }
    
    [Tooltip("缶蹴り関数<AnimationEvent>")]
    public void kick()
    {
        if (KanRigidbody != null)
        {
            //飛ばしたい方向を指定
            Vector3 kickDirection = transform.forward;

            //斜め上に飛ばす力を加える
            kickDirection += Vector3.up * 1.5f;

            //力を加える
            KanRigidbody.linearVelocity = Vector3.zero; // 前の速度をリセット
            float kickPower = NPCManager.GetKickPower(type);
            //ヒューマンコーヒー状態なら力を強めに
            if (NPCManager.Instance.humanCoffee) KanRigidbody.AddForce(kickDirection.normalized * kickPower * NPCManager.Instance.coffeePower, ForceMode.Impulse);
            //通常の力
            else KanRigidbody.AddForce(kickDirection.normalized * kickPower, ForceMode.Impulse);
            SoundManager.PlaySE_NPCKick();
        }
    }

    [Tooltip("アニメ終了時関数<AnimationEvent>")]
    public void KickEnd()
    {
        // 音声再生
        SE_NPCFootsteps.Play();

        // 蹴っていない
        kickNow = false;
    }
}
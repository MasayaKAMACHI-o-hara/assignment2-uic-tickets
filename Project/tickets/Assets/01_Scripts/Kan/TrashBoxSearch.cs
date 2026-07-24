using System.Linq;
using UnityEngine;

public class TrashBoxSearch : MonoBehaviour
{
    private static TrashBoxSearch Instance;
    private void Awake() => Instance = this;
    
    [Header("ポジジンジャーエール")]
    [Tooltip("ポジジンジャーエール")] [SerializeField]
    public bool posiGingerAle;

    [Header("矢印")]
    [Tooltip("矢印全部")] [SerializeField]
    GameObject arrows;
    [Tooltip("矢印")] [SerializeField]
    Transform[] arrow;
    [Tooltip("矢印のレンダー")][SerializeField]
    Renderer[] arrowRenderer;

    [Header("ゴミ箱")]
    [Tooltip("ゴミ箱座標")] [SerializeField]
    Transform[] trashBoxes;
    
    [Tooltip("一番目の座標")] Transform FirstPos;
    [Tooltip("二番目の座標")] Transform SecondPos;
    [Tooltip("三番目の座標")] Transform ThirdPos;
    
    [SerializeField] float maxDistance = 150f;
    [SerializeField] float minDistance = 30f;

    void Start()
    {
        arrows.SetActive(false);
    }

    void Update()
    {   //表示/非表示切り替え
        if (posiGingerAle)
        { 
            arrows.SetActive(true);
        }
        else
        {
            arrows.SetActive(false);
        }
        
        //三番目までの距離を指定
        var nearest = trashBoxes
            .OrderBy(t => Vector3.Distance(transform.position, t.position)).Take(3).ToArray();
        
        //座標を指定
        if (nearest.Length > 0) FirstPos = nearest[0];
        if (nearest.Length > 1) SecondPos = nearest[1];
        if (nearest.Length > 2) ThirdPos = nearest[2];
        
        //ゴミ箱の方向に向ける
        arrow[0].LookAt(FirstPos);
        arrow[1].LookAt(SecondPos);
        arrow[2].LookAt(ThirdPos);
        
        //距離を測定
        float dis1 = Vector3.Distance(transform.position, FirstPos.position);
        float dis2 = Vector3.Distance(transform.position, SecondPos.position);
        float dis3 = Vector3.Distance(transform.position, ThirdPos.position);
        
        //透明度変更
        SetAlpha(0, dis1);
        SetAlpha(1, dis2);
        SetAlpha(2, dis3);
    }
    [Tooltip("透明度変更関数")]
    void SetAlpha(int index, float distance)
    {   //アルファ値を距離で変更
        float alpha = Mathf.Lerp(0.1f, 1f, 1 - Mathf.InverseLerp(minDistance, maxDistance, distance));
        
        //アルファ値変更
        Color color = arrowRenderer[index].material.color;
        color.a = alpha;
        arrowRenderer[index].material.color = color;
    }
    [Tooltip("ジンジャーエール状態切り替え関数")]
    public static void ActivePosiGingerAle(bool active) => Instance.posiGingerAle = active;
}

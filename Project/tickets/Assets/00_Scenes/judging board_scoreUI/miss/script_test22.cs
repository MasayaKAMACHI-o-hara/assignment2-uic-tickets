////using UnityEngine;
////using UnityEngine.InputSystem;

////public class script_test22 : MonoBehaviour
////{

////    [SerializeField]
////    RectTransform canvasRect;

////    [SerializeField]
////    script_test2 overHeadMsgPrefab;

////    script_test2 overHeadMsg;

////    public int scoreValue = 10;       // 板ごとのスコア

////    void OnEnable()
////    {
////        overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
////        overHeadMsg.targetTran = transform;
////    }

////    void OnDisable()
////    {
////        if (overHeadMsg == null) return;

////        Destroy(overHeadMsg.gameObject);
////    }
////}

//using UnityEngine;

//public class script_test22 : MonoBehaviour
//{
//    [SerializeField]
//    RectTransform canvasRect;

//    [SerializeField]
//    script_test2 overHeadMsgPrefab;   // UIプレハブ

//    script_test2 overHeadMsg;

//    public int scoreValue = 10;       // ゴミ箱ごとのスコア
//    private bool hasScored = false;

//    void OnCollisionEnter(Collision collision)
//    {
//        if (hasScored) return;

//        if (collision.gameObject.CompareTag("can"))
//        {
//            Debug.Log("Goal! スコア: " + scoreValue);

//            // UIをゴミ箱の上に生成
//            overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
//            overHeadMsg.targetTran = transform;

//            hasScored = true;
//        }
//    }

//    void OnDisable()
//    {
//        if (overHeadMsg == null) return;
//        Destroy(overHeadMsg.gameObject);
//    }
//}




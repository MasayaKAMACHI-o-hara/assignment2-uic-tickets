using UnityEngine;
using UnityEngine.InputSystem;

public class script_test_11 : MonoBehaviour
{
    [SerializeField]
    RectTransform canvasRect;

    [SerializeField]
    script_test overHeadMsgPrefab;

    script_test overHeadMsg;

    InputAction spaceAction;

    void OnEnable()
    {
        overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
        overHeadMsg.targetTran = transform;
    }

    void OnDisable()
    {
        if (overHeadMsg == null) return;

        Destroy(overHeadMsg.gameObject);
    }
}



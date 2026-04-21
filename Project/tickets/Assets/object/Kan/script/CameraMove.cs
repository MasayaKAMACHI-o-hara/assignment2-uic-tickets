// Github

using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform target;     // 追いかける主人公
    public float Playerdistance = 1.0f; // キャラとの距離
    public float sensitivity = 3.0f; // マウス感度
    public float PreventingIndentation = 0.15f; // 地面にめり込まないように少し上げる

    private float currentX = 0.0f; // マウスの左右移動量
    private float currentY = 0.0f; // マウスの上下移動量
    private float distance = 0.0f;

    public bool ActiveMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    void LateUpdate()
    {
        if(ActiveMove)
        {
            if (target == null) return;

            // マウスの移動量を取得
            currentX += Input.GetAxis("Mouse X") * sensitivity;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity;

            // 上下の回転角度を制限（地面に埋まったり真上を過ぎたりしないように）
            currentY = Mathf.Clamp(currentY, -80f, 80f);

            distance = Playerdistance;

            if (currentY < 10)
            {
                var sa = 10 - currentY;
                var newDis = distance - (sa * 0.01f);
                distance = newDis;
            }


            // 角度をクォータニオン（回転）に変換
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

            // キャラクターの位置から、回転させた方向へ distance 分だけ離れた位置を計算
            // (0, 0, -distance) を回転させて target.position を足すイメージ
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;


            // カメラの位置と向きを更新
            transform.rotation = rotation;
            transform.position = position;

            if (transform.position.y < target.position.y + PreventingIndentation)
            {
                var CameraYPos = transform.position;
                CameraYPos.y = target.position.y + PreventingIndentation;
                transform.position = CameraYPos;
            }
        }   
    }
}

// Occlusion Culling を OFF にする
using UnityEngine;

public class KickSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 相手がプレイヤー（缶）なら、親のスクリプトに通知する
        if (other.CompareTag("Kan"))
        {
            // 親オブジェクトにあるNPCスクリプトの関数を呼ぶ
            SendMessageUpwards("OnKanEnter");
        }
    }
}

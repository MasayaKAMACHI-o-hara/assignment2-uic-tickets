using UnityEngine;

public class KickSensor : MonoBehaviour
{
    private NPCAction _action;
    void Start()
    {
        _action = GetComponentInParent<NPCAction>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _action.KanRigidbody.gameObject.transform.Find("KanMidPos").gameObject)
        {
            SendMessageUpwards("OnKanEnter");
        }
    }
}

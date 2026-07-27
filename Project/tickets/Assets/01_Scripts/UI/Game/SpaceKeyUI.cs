using UnityEngine;

public class SpaceKeyUI : MonoBehaviour
{
    [Tooltip("Space keyUI")] [SerializeField]
    GameObject ui;
    [Tooltip("SyakaSyaka")] [SerializeField]
    SyakaSyaka syaka;
    void Update()
    {
        if(syaka.IsActiveFly && !syaka.IsActiveFall && syaka.IsActiveSpace)
            ui.SetActive(true);
        else 
            ui.SetActive(false);
    }
}

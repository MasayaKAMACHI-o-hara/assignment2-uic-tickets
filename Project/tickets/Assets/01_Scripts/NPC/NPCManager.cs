using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    private float kickPower;
    [Tooltip("コーヒー倍率")] public float coffeePower = 2;
    [Tooltip("ヒューマンコーヒー")]public bool humanCoffee;
    private void Awake() => Instance = this;
    public static float GetKickPower(NPCType type)
    {
        if (type == NPCType.Male) Instance.kickPower = 40f;
        else  if (type == NPCType.Female) Instance.kickPower = 30f;
        else if  (type == NPCType.Boy) Instance.kickPower = 50f;
        return Instance.kickPower;
    }
    public static void ChangeMaterial(SkinnedMeshRenderer mesh)
    {
        if (Instance.humanCoffee) mesh.material.EnableKeyword("_EMISSION");
        else mesh.material.DisableKeyword("_EMISSION");
    }
}

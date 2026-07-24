using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    private static SoundManager Instance;
    private void Awake() => Instance = this;
    
    [SerializeField] AudioSource BGM_Title;
    [SerializeField] AudioSource BGM_Game;
    
    [SerializeField] AudioSource AS_Kan;
    [SerializeField] AudioClip SE_KanJump;
    [SerializeField] AudioClip SE_KanCharge;
    [SerializeField] AudioClip SE_KanChargeComplete;
    [SerializeField] AudioClip SE_KanSquirtStart;
    [SerializeField] AudioClip SE_KanSquirting;
    [SerializeField] AudioClip SE_KanSquirtEnd;
    [SerializeField] AudioClip SE_KanFall;
    [SerializeField] AudioClip SE_KanLanding;
    [SerializeField] AudioSource SE_Coin;
    [SerializeField] AudioSource SE_VendingCoin;
    [SerializeField] AudioSource SE_VendingNotCoin;
    [SerializeField] AudioSource SE_VendingBuy;
    [SerializeField] AudioSource SE_NPCKick;
    [SerializeField] AudioSource SE_CarHorn;
    [SerializeField] AudioSource SE_UIOpen;
    [SerializeField] AudioSource SE_UIClose;
    [SerializeField] AudioSource SE_GameOverAndGameClear;
    [SerializeField] AudioSource SE_ScoreCountUp;
    [SerializeField] AudioSource SE_ScoreBonus;
    [SerializeField] AudioSource SE_ScoreTotal;
    [SerializeField] AudioSource SE_SettingOpen;
    
    private void Start() => BGM_Title.Play();
    public static void StopBGM_Title() => Instance.BGM_Title.Stop();
    public static void PlayBGM_Game() => Instance.BGM_Game.Play();
    
    public static void StopAS_Kan() => Instance.AS_Kan.Stop();
    public static void PlaySE_KanJump() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanJump);
    public static void PlaySE_KanCharge() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanCharge);
    public static void PlaySE_KanChargeComplete() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanChargeComplete);
    public static void PlaySE_KanSquirtStart() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanSquirtStart);
    public static void PlaySE_KanSquirting() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanSquirting);
    public static void PlaySE_KanSquirtEnd() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanSquirtEnd);
    public static void PlaySE_KanFall() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanFall);
    public static void PlaySE_KanLanding() => Instance.AS_Kan.PlayOneShot(Instance.SE_KanLanding);
    public static void PlaySE_Coin() => Instance.SE_Coin.Play();
    public static void PlaySE_VendingCoin() => Instance.SE_VendingCoin.Play();
    public static void PlaySE_VendingNotCoin() => Instance.SE_VendingNotCoin.Play();
    public static void PlaySE_VendingBuy() => Instance.SE_VendingBuy.Play();
    public static void PlaySE_NPCKick() => Instance.SE_NPCKick.Play();
    public static void PlaySE_CarHorn() => Instance.SE_CarHorn.Play();
    public static void PlaySE_UIOpen() => Instance.SE_UIOpen.Play();
    public static void PlaySE_UIClose() => Instance.SE_UIClose.Play();
    public static void PlaySE_GameOverAndGameClear() => Instance.SE_GameOverAndGameClear.Play();
    public static void PlaySE_ScoreCountUp() => Instance.SE_ScoreCountUp.Play();
    public static void StopSE_ScoreCountUp() => Instance.SE_ScoreCountUp.Stop();
    public static void PlaySE_ScoreBonus() => Instance.SE_ScoreBonus.Play();
    public static void PlaySE_ScoreTotal() => Instance.SE_ScoreTotal.Play();
    public static void PlaySE_SettingOpen() => Instance.SE_SettingOpen.Play();
}

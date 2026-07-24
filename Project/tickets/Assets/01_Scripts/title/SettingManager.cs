using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    public Slider SESlider;
    public Slider BGMSlider;
    public Slider CameraSlider;

    public AudioMixer AUDIO;

    public Sprite[] VOLicon; 

    public Image SEicon;
    public Image BGMicon;

    public TextMeshProUGUI BGMNum;
    public TextMeshProUGUI SENum;
    public TextMeshProUGUI CameraNum;

    public CameraMove CameraSCR;

    public GameObject CheckDeleteUI;
    public GameObject DeleteButtonUI;
    public GameObject StartBuckButtonUI;
    public GameObject BuckSettingButtonUI;
    public PauseSistem pauseSistemSCR;
    
    public bool InGame = false;
    
    private void Start()
    {
        if(PlayerPrefs.HasKey("BGM"))
            AUDIO.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
        if(PlayerPrefs.HasKey("SE"))
            AUDIO.SetFloat("SE", PlayerPrefs.GetFloat("SE"));
        if(PlayerPrefs.HasKey("Camera"))
            CameraSCR.sensitivity = PlayerPrefs.GetFloat("Camera");
        Debug.Log($"{(int)(PlayerPrefs.GetFloat("BGMInt")* 100)}");
 
        if (PlayerPrefs.HasKey("SEInt"))
        {
            SESlider.value = PlayerPrefs.GetFloat("SEInt");
            SENum.text = $"{(int)(PlayerPrefs.GetFloat("SEInt")* 100)}";
        }
         if (PlayerPrefs.HasKey("BGMInt"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("BGMInt"); 
            BGMNum.text = $"{(int)(PlayerPrefs.GetFloat("BGMInt")* 100)}";
        } 
         if (PlayerPrefs.HasKey("CameraInt"))
        {
            CameraSlider.value = PlayerPrefs.GetFloat("CameraInt"); 
            CameraNum.text = $"{PlayerPrefs.GetFloat("CameraInt"):F2}";
        }
    }
    
    public void OpenSetting()
    {
        transform.localPosition = new Vector2(-2857, 0);
        if (InGame)
        {
            DeleteButtonUI.SetActive(false);
            StartBuckButtonUI.SetActive(false);
            BuckSettingButtonUI.SetActive(true);
            pauseSistemSCR.IsActiveSetting = true;
        }
 
        SoundManager.PlaySE_SettingOpen();
    }

    public void OnClick_SettingBuck()
    {
        transform.localPosition = new Vector3(4000, 0, 0);
        if (InGame)
        {
            pauseSistemSCR.IsActiveSetting = false;
        }
        SoundManager.PlaySE_UIClose();
    }

    public void SliderChangesSE(float Value)
    {
        Debug.Log(Value);
        float ChangeVOL = Mathf.Log10(Value) * 20f;
        AUDIO.SetFloat("SE", ChangeVOL);
        PlayerPrefs.SetFloat("SE", ChangeVOL);
        PlayerPrefs.SetFloat("SEInt", Value);
        
        var Num = Value * 100f;
        Num = (int)Num;
        SENum.text = $"{Num}";
        if (Num >= 50)
        {
            SEicon.sprite = VOLicon[2];
        }
        else if (Num == 0)
        {
            SEicon.sprite = VOLicon[0];
        }
        else
        {
            SEicon.sprite = VOLicon[1];
        }
    }
    public void SliderChangesBGM(float Value)
    {
        Debug.Log(Value);
        float ChangeVOL = Mathf.Log10(Value) * 20f;
        AUDIO.SetFloat("BGM", ChangeVOL);
        PlayerPrefs.SetFloat("BGM", ChangeVOL);
        PlayerPrefs.SetFloat("BGMInt", Value);
        
        var Num = Value * 100f;
        Num = (int)Num;
        BGMNum.text = $"{Num}";
        if (Num >= 50)
        {
            BGMicon.sprite = VOLicon[2];
        }
        else if (Num == 0)
        {
            BGMicon.sprite = VOLicon[0];
        }
        else
        {
            BGMicon.sprite = VOLicon[1];
        }
    }
    
    public void SliderChangesCameraSEN(float Value)
    {
        CameraSCR.sensitivity = Value;
        PlayerPrefs.SetFloat("Camera", CameraSCR.sensitivity);
        PlayerPrefs.SetFloat("CameraInt", Value);
        CameraNum.text = $"{Value:F2}";
    }

    public void TestSound()
    {
        SoundManager.PlaySE_NPCKick();
    }

    public void CheckDeleteData()
    {
        CheckDeleteUI.SetActive(true);
        SoundManager.PlaySE_UIOpen();
    }
    public void BackDeleteData()
    {
        CheckDeleteUI.SetActive(false);
        SoundManager.PlaySE_UIClose();
    }
    public void RunDeleteData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

   
}

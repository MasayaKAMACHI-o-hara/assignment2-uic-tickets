using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Settings_Manager : MonoBehaviour
{

    public Slider SESlider;
    public Slider BGMSlider;

    public AudioMixer AUDIO;

    public Sprite[] VOLicon; 

    public GameObject StartBuckBottown_3;

    public Image SEicon;
    public Image BGMicon;


    public void OpenSetting()
    {
        transform.localPosition = new Vector2(-4156, 206);
        //oto
        Debug.Log("open");
    }

    public void OnClick_SettingBuck()
    {
        transform.localPosition = new Vector3(4000, 0, 0);
    }

    public void SliderChangesSE(float Value)
    {
        Debug.Log(Value);
        float ChangeVOL = Mathf.Log10(Value) * 20f;
        AUDIO.SetFloat("SE", ChangeVOL);
      
    }
    public void SliderChangesBGM(float Value)
    {
        Debug.Log(Value);
        float ChangeVOL = Mathf.Log10(Value) * 20f;
        AUDIO.SetFloat("BGM", ChangeVOL);
       
    }

    public void IconChange(float VOL, Image icon)
    {
        if(VOL >= 0.5)
        {
            //icon.sprite = 
        }
    }
}

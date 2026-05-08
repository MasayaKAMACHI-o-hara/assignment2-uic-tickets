using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        // �^�C�g����ʂŎ����Đ�
        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }
}

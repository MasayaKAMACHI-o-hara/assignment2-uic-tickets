using UnityEngine;

public class GameBGM_Urban : MonoBehaviour
{
    public AudioSource bgmSource;

    public void PlayBGM()
    {
        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }
}

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioFade audioFade;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioFade = GetComponent<AudioFade>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioFade != null)
        {
            audioFade.FadeIn();
        }
    }

    public void StopMusicSmooth()
    {
        if (audioFade != null)
        {
            audioFade.FadeOut();
        }
    }
}
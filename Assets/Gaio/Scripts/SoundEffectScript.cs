using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public static SoundEffectScript Instance;

    [SerializeField] private AudioSource soundEffectObject;
    public AudioSource officeSource;
    public AudioSource ambienceSource;
    public AudioSource flashlightSource;
    public AudioSource breathingSource;

    public AudioClip officeLoop;
    public AudioClip ambienceLoop;
    public AudioClip flashlightLoop;
    public AudioClip breathingLoop;

    public float Volume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        officeSource.clip = officeLoop;
        ambienceSource.clip = ambienceLoop;
        flashlightSource.clip = flashlightLoop;
        breathingSource.clip = breathingLoop;
        Volume = 1;

        officeSource.Play();
        ambienceSource.Play();
    }

    public void StopAmbience()
    {
        officeSource.Stop();
        ambienceSource.Stop();
    }

    public void StartAmbience()
    {
        officeSource.Play();
        ambienceSource.Play();
    }

    public void StartFlashlight()
    {
        flashlightSource.Play();
    }

    public void StopFlashlight()
    {
        flashlightSource.Stop();
    }

    public void StartBreathing()
    {
        breathingSource.Play();
    }

    public void StopBreathing()
    {
        breathingSource.Stop();
    }

    public void PlaySoundEffect(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject);

        audioSource.clip = audioClip;

        audioSource.volume = volume * Volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}

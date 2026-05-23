using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public static SoundEffectScript Instance;

    [SerializeField] private AudioSource soundEffectObject;
    [SerializeField] AudioSource officeSource;
    [SerializeField] AudioSource ambienceSource;

    public AudioClip officeLoop;
    public AudioClip ambienceLoop;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        officeSource.clip = officeLoop;
        ambienceSource.clip = ambienceLoop;

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

    public void PlaySoundEffect(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}

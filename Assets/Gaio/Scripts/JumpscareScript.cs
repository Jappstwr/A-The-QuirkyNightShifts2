using Unity.VisualScripting;
using UnityEngine;

public class JumpscareScript : MonoBehaviour
{
    public NightScript nightScript;
    public float time;
    public float staticTime;
    public bool _hasPlayedJumpscare;
    public AudioClip jumpscareSound;

    [SerializeField] private float currentTime;
    [SerializeField] private float currentStaticTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetJumpscare();
    }
    // Update is called once per frame

    public void ResetJumpscare()
    {
        currentTime = time;
        currentStaticTime = staticTime;
        _hasPlayedJumpscare = false;
    }
    void Update()
    {
        if (_hasPlayedJumpscare == false)
        {
            SoundEffectScript.Instance.PlaySoundEffect(jumpscareSound, 0.5f);
            _hasPlayedJumpscare = true;
        }

        currentTime -= Time.deltaTime;
        nightScript._monitorOpen = false;

        if (currentTime <= 0)
        {
            currentStaticTime -= Time.deltaTime;
            if (currentStaticTime <= 0)
            {
                SoundEffectScript.Instance.StopAmbience();
                nightScript._isDead = true;
            }
        }
    }
}

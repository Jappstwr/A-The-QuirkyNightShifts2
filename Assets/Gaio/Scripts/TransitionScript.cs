using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public NightScript nightScript;
    public VentScanScript scanScript;
    public VentAnimatronicsScript ventAnimScript;

    public AudioClip melody;

    public Animator leftAnimator;
    public Animator rightAnimator;

    private float transitionTime;
    private bool _hasAddedNight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        leftAnimator.SetTrigger("6AM");
        rightAnimator.SetTrigger("6AM");

        transitionTime = 10f;
        _hasAddedNight = false;
        SoundEffectScript.Instance.PlaySoundEffect(melody, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transitionTime -= Time.deltaTime;

        if (transitionTime < 1 && _hasAddedNight == false)
        {
            nightScript.Night++;

            if (nightScript.Night >= 7)
            {
                SceneManager.LoadScene(3);
            }

            nightScript.nightTime = 0;
            nightScript.currentPower = 99;

            nightScript.nightText.text = $"Night {nightScript.Night}";
            nightScript.UpdateAMText();
            nightScript.powerText.text = $"Power:99%";

            nightScript.ResetNight();


            _hasAddedNight = true;
        }
        if (transitionTime <= 0)
        {
            nightScript.ResetNight();
        }
    }
}

using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public NightScript nightScript;
    public VentScanScript scanScript;

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
    }

    // Update is called once per frame
    void Update()
    {
        transitionTime -= Time.deltaTime;

        if (transitionTime < 5 && _hasAddedNight == false)
        {
            nightScript.Night++;
            nightScript.nightTime = 0;
            nightScript.currentPower = 99;

            nightScript.nightText.text = $"Night {nightScript.Night}";
            nightScript.UpdateAMText();
            nightScript.powerText.text = $"Power:99%";

            scanScript.ResetScan();

            _hasAddedNight = true;
        }
        if (transitionTime <= 0)
        {
            nightScript.ResetNight();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class WindUpButton : MonoBehaviour
{
    [SerializeField] private MainCameraScript cameraSystem;
    [SerializeField] private Button button;
    [SerializeField] private Image fillBar;

    [SerializeField] private GameObject songUI;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource warningSource;

    [SerializeField] private AudioClip[] songs;
    //[SerializeField] private AudioClip song2;
    [SerializeField] private AudioClip TireMeterSound; 

    [Header("Settings")]
    [SerializeField] private int requieredCameraIndex = 2;
    [SerializeField] private float holdTime = 2f;

    private float currentHoldTime = 0f;

    private float decreaseTimerPerNight = 0.08f;
    private float currentModifier = 0.02f;

    private bool nightPassed;
    private bool nighPassedTrigger; 

    private bool isHoldingButton = false;
    private bool songChanged = false;
    private int lastSongIndex = -1;

    private bool warningActive = false;

    private bool BithovenAwake;

    void Start()
    {
        fillBar.fillAmount = 1;
        currentHoldTime = holdTime;

        ChangeSong();
    }

    // Update is called once per frame
    void Update()
    {
        bool correctCamera = cameraSystem.ActiveCamera == requieredCameraIndex && cameraSystem.camerasOpen;

        button.gameObject.SetActive(correctCamera);

        if (correctCamera)
        {
            musicSource.volume = 0.5f; 
        }
        else
        {
            musicSource.volume = 0f; 
        }

        //fillBar.gameObject.SetActive(correctCamera);

        //songUI.SetActive(correctCamera);

        if (!correctCamera)
        {
            fillBar.fillAmount = currentHoldTime / holdTime;

            //songUI.SetActive(false);

            currentHoldTime -= Time.deltaTime * currentModifier;



            return;
        }

        if (correctCamera && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E)))
        {
            currentHoldTime += Time.deltaTime * 0.5f;
        }
        else
        {
            currentHoldTime -= Time.deltaTime * currentModifier;   
        }

        currentHoldTime = Mathf.Clamp(currentHoldTime, 0f, holdTime);

        if (currentHoldTime < holdTime)
        {
            songChanged = false;
        }

        fillBar.fillAmount = currentHoldTime / holdTime;

        if (currentHoldTime <= 0f)
        {
           if (!BithovenAwake)
           {
                BithovenAwake = true; 
                musicSource.Stop();
                Debug.Log("Bithoven has woken up!");
           }
        }
        if (currentHoldTime >= 0f) 
        {
            BithovenAwake = false; 
        }

        if (currentHoldTime >= holdTime && !songChanged)
        {
            songChanged = true;

            ChangeSong();

            //currentHoldTime = 0f;
            //fillBar.fillAmount = Mathf.Lerp(currentHoldTime, 1f, holdTime);

            currentHoldTime = holdTime;
            fillBar.fillAmount = 1f;

        }

        if (fillBar.fillAmount <= 0.3f)
        {
            if (!warningActive)
            {
                warningActive = true;

                warningSource.clip = TireMeterSound;
                warningSource.loop = true;
                warningSource.Play();
            }
        }
        else
        {
            if (warningActive)
            {
                warningActive = false;

                warningSource.Stop();
            }
        }

    }
    private void FixedUpdate()
    {
        
    }
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isHoldingButton = true;
    //}

    //// Mouse released
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    isHoldingButton = false;
    //}

    public void ChangeSong()
    {
        if (songs.Length == 0)
        {
            Debug.Log("No songs assigned");
            return; 
        }

        int randomIndex; 

        do
        {
            randomIndex = Random.Range(0, songs.Length);
        }
        while (randomIndex == lastSongIndex && songs.Length > 1);

        lastSongIndex = randomIndex;
        musicSource.clip = songs[randomIndex];

        musicSource.Play();

        Debug.Log("Song Changed"); 
    }


}

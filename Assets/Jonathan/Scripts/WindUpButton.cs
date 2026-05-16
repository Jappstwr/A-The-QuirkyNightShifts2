using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio; 

public class WindUpButton : MonoBehaviour
{
    [SerializeField] private MainCameraScript cameraSystem;
    [SerializeField] private Button button;
    [SerializeField] private Image fillBar;

    [SerializeField] private GameObject songUI;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip song1;
    [SerializeField] private AudioClip song2;

    [Header("Settings")]
    [SerializeField] private int requieredCameraIndex = 2;
    [SerializeField] private float holdTime = 2f;

    private float currentHoldTime = 0f;

    private float decreaseTimerPerNight = 0.08f;
    private float currentModifier = 0.02f;

    private bool isHoldingButton = false;
    private bool songChanged = false;

    void Start()
    {
        fillBar.fillAmount = 1;
        currentHoldTime = holdTime; 
    }

    // Update is called once per frame
    void Update()
    {
        bool correctCamera = cameraSystem.ActiveCamera == requieredCameraIndex && cameraSystem.camerasOpen;

        button.gameObject.SetActive(correctCamera);
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
            currentHoldTime += Time.deltaTime;
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
            Debug.Log("Bithoven has woken up!"); 
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
        if (audioSource.clip == song1)
        {
            audioSource.clip = song2;
        }
        else
        {
            audioSource.clip = song1;
        }

        audioSource.Play();

        Debug.Log("Song Changed"); 
    }


}

using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class WindUpButton : MonoBehaviour
{
    [SerializeField] private MainCameraScript cameraSystem;
    [SerializeField] private Button button;
    [SerializeField] private Image fillBar;

    [SerializeField] private GameObject songUI;


    [Header("Settings")]
    [SerializeField] private int requieredCameraIndex = 2;
    [SerializeField] private float holdTime = 2f;

    private float currentHoldTime = 0f;

    private float decreaseTimerPerNight = 0.08f;
    private float currentModifier = 0.02f; 


    void Start()
    {
        fillBar.fillAmount = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        bool correctCamera = cameraSystem.ActiveCamera == requieredCameraIndex && cameraSystem.camerasOpen;

        button.gameObject.SetActive(correctCamera);
        //fillBar.gameObject.SetActive(correctCamera);

        //songUI.SetActive(correctCamera);

        if (!correctCamera )
        {
            fillBar.fillAmount = currentHoldTime / holdTime;

            //songUI.SetActive(false);

            currentHoldTime -= Time.deltaTime * currentModifier; 



            return; 
        }

        if (Input.GetMouseButton(0) && correctCamera)
        {
            currentHoldTime += Time.deltaTime;
        }
        else
        {
            currentHoldTime -= Time.deltaTime * currentModifier;   
        }

        currentHoldTime = Mathf.Clamp(currentHoldTime, 0f, holdTime);

        fillBar.fillAmount = currentHoldTime / holdTime;

        if (currentHoldTime <= 0f)
        {
            Debug.Log("Bithoven has woken up!"); 
        }

        if (currentHoldTime >= holdTime)
        {
            ChangeSong();

            //currentHoldTime = 0f;
            //fillBar.fillAmount = Mathf.Lerp(currentHoldTime, 1f, holdTime);

            currentHoldTime = holdTime;
            fillBar.fillAmount = 1f; 
            
        }
    }
    public void ChangeSong()
    {
        Debug.Log("Song Changed"); 
    }


}

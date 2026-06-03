using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class NightScript : MonoBehaviour
{
    public VentScanScript scanScript;
    public VentAnimatronicsScript ventAnimScript;

    public GameObject office;
    public GameObject deathmenu;
    public GameObject transitionObject;
    public GameObject jumpscaresHolder;
    
    public AudioClip pressSound;
    public AudioClip cameraSound;
    public AudioClip ambience1Sound;
    public AudioClip ambience2Sound;
    public AudioClip doorSound;
    public AudioClip flashSound;

    [SerializeField] private float ambienceTimer;

    public int Night;

    public List<GameObject> offices = new List<GameObject>();
    [SerializeField] private int officeIndex;

    public GameObject suit;
    public bool _inSuit;

    public GameObject turnLeftButton;
    public GameObject turnRightButton;
    public GameObject monitorButton;
    public GameObject suitButton;
    public GameObject closeButton;

    public GameObject monitor;
    public GameObject ventSystem;
    public GameObject camSystem;

    [SerializeField] private bool _isOnCam;

    public GameObject flashlight;
    public bool _isFlashing;
    public bool _canFlash;

    public int maxPower;
    public float currentPower;
    [SerializeField] private int powerUsage;
    public float defaultPower;
    public bool _powerOutage;

    public float powerCooldown;
    [SerializeField] private float powerTimer;

    public TMP_Text powerText;
    public TMP_Text nightText;
    public TMP_Text amText;

    public float nightTime;
    public bool _is6AM;

    public List<GameObject> powerLights;

    [SerializeField] private float shakePower;

    [SerializeField] private List<RectTransform> PLrects = new List<RectTransform>();

    [SerializeField] private Vector2[] originalPLPositions;
    [SerializeField] private Vector2[] currentPLPositions;
    [SerializeField] private Vector2[] targetPLPositions;

    public bool _leftClosed;
    public bool _rightClosed;
    public bool _monitorOpen;

    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;

    public Sprite leftOpen;
    public Sprite rightOpen;
    public Sprite leftClosed;
    public Sprite rightClosed;

    public bool _isJumpscared;
    public bool _isDead;

    public float scanTimer;

    [SerializeField] private ScrappedAnimatronicScript scrapped;
    [SerializeField] private ScrappedAnimatronicScript scrapped2;
    [SerializeField] private KlokerScriptMovement normal;
    [SerializeField] private KlokerScriptMovement normal2;
    [SerializeField] private WindUpButton bithoven;
    [SerializeField] private SnattarenScript snattaren;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetNight();

        UpdateFlash();
        UpdateOffice();
        ResetPowerLights();
    }

    public void PressSoundEffect()
    {
        SoundEffectScript.Instance.PlaySoundEffect(pressSound, 1f);
    }

    // Update is called once per frame
    public void ResetNight()
    {
        scanScript.ResetScan();

        ventAnimScript.ResetAnimatronic();

        /*scrapped.ResetScrappedAnimatronics();
        scrapped2.ResetScrappedAnimatronics();
        normal.ResetAnimatronics();
        normal2.ResetAnimatronics();
        bithoven.ResetBithoven();
        snattaren.ResetSnattaren();*/


        nightText.text = $"Night {Night}";

        officeIndex = 1;
        nightTime = 0f;
        _is6AM = false;
        currentPower = maxPower;
        scanTimer = 0;

        office.SetActive(true);
        deathmenu.SetActive(false);
        transitionObject.SetActive(false);
        jumpscaresHolder.SetActive(false);
        monitor.SetActive(false);
        suit.SetActive(false);
        turnLeftButton.SetActive(true);
        turnRightButton.SetActive(true);
        monitorButton.SetActive(true);
        suitButton.SetActive(true);
        closeButton.SetActive(false);
        camSystem.SetActive(true);
        ventSystem.SetActive(false);
        _isDead = false;
        _monitorOpen = false;
        _inSuit = false;
        _isOnCam = true;
        _leftClosed = false;
        _rightClosed = false;
        powerUsage = 1;
        _isFlashing = false;
        _canFlash = true;
        ambienceTimer = 10f;

        SoundEffectScript.Instance.StartAmbience();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void UpdateAMText()
    {
        int am = (int)nightTime / 60;
       amText.text = $"{am} AM";
    }
    public void ResetPowerLights()
    {
        for (int i = 0; i < powerLights.Count; i++)
        {
            PLrects.Add(powerLights[i].GetComponent<RectTransform>());
        }
        int count = PLrects.Count;

        originalPLPositions = new Vector2[count];
        currentPLPositions = new Vector2[count];
        targetPLPositions = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            originalPLPositions[i] = PLrects[i].anchoredPosition;
            targetPLPositions[i] = Random.insideUnitCircle * shakePower;
        }
    }
    public void CloseButton()
    {
        if (scanTimer <= 0)
        {
            monitor.SetActive(false);
            suit.SetActive(false);
            SoundEffectScript.Instance.StopBreathing();
            turnLeftButton.SetActive(true);
            turnRightButton.SetActive(true);
            monitorButton.SetActive(true);
            suitButton.SetActive(true);
            closeButton.SetActive(false);
            _monitorOpen = false;
            _inSuit = false;

            PressSoundEffect();
        }
    }
    public void ToggleMonitor()
    {
        if (_powerOutage == false && scanTimer <= 0)
        {
            _monitorOpen = !_monitorOpen;
            UpdateMonitor();
            UpdateFlash();

            PressSoundEffect();
        }
    }
    public void ToggleMonitorView()
    {
        if (scanTimer <= 0)
        {
            SoundEffectScript.Instance.PlaySoundEffect(cameraSound, 0.5f);

            _isOnCam = !_isOnCam;
            if (_isOnCam)
            {
                camSystem.SetActive(true);
                ventSystem.SetActive(false);
            }
            else
            {
                camSystem.SetActive(false);
                ventSystem.SetActive(true);
            }
        }
    }
    public void ToggleSuit()
    {
        _inSuit = !_inSuit;
        UpdateSuit();
        UpdateFlash();

        PressSoundEffect();
    }
    public void TurnLeft()
    {
        officeIndex--;
        if (officeIndex < 0)
        {
            officeIndex = 0;
        }
        UpdateOffice();
        UpdateFlash();

        PressSoundEffect();
    }
    public void TurnRight()
    {
        officeIndex++;
        if (officeIndex > 2)
        {
            officeIndex = 2;
        }
        UpdateOffice();
        UpdateFlash();

        PressSoundEffect();
    }
    public void UpdateMonitor()
    {
        if (_monitorOpen)
        {
            monitor.SetActive(true);
            turnLeftButton.SetActive(false);
            turnRightButton.SetActive(false);
            monitorButton.SetActive(false);
            suitButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }
    public void UpdateOffice()
    {
        for (int i = 0; i < offices.Count; i++)
        {
            if (i == officeIndex)
            {
                offices[i].SetActive(true);
            }
            else
            {
                offices[i].SetActive(false);
            }
        }

        if (officeIndex == 0)
        {
            turnLeftButton.SetActive(false);
            turnRightButton.SetActive(true);
            monitorButton.SetActive(false);
            suitButton.SetActive(false);
        }
        else if (officeIndex == 1)
        {
            turnLeftButton.SetActive(true);
            turnRightButton.SetActive(true);
            monitorButton.SetActive(true);
            suitButton.SetActive(true);
        }
        else
        {
            turnLeftButton.SetActive(true);
            turnRightButton.SetActive(false);
            monitorButton.SetActive(false);
            suitButton.SetActive(false);
        }
    }
    public void UpdateSuit()
    {
        if (_inSuit)
        {
            SoundEffectScript.Instance.StartBreathing();

            suit.SetActive(true);
            turnLeftButton.SetActive(false);
            turnRightButton.SetActive(false);
            monitorButton.SetActive(false);
            suitButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }
    public void UpdateFlash()
    {
        if (_isFlashing && _inSuit == false && _monitorOpen == false && officeIndex == 1 && _canFlash == true)
        {
            flashlight.SetActive(true);
            SoundEffectScript.Instance.PlaySoundEffect(flashSound, 1f);
            SoundEffectScript.Instance.StartFlashlight();
        }
        else
        {
            if(_isFlashing)
            _isFlashing = false;
            flashlight.SetActive(false);
            SoundEffectScript.Instance.StopFlashlight();
        }
    }
    public void ToggleLeft()
    {
        SoundEffectScript.Instance.PlaySoundEffect(doorSound, 0.5f);
        _leftClosed = !_leftClosed;
        UpdateLeft();
    }
    public void UpdateLeft()
    {
        if (_leftClosed)
        {
            leftSprite.sprite = leftClosed;
        }
        else
        {
            leftSprite.sprite = leftOpen;
        }
    }
    public void ToggleRight()
    {
        SoundEffectScript.Instance.PlaySoundEffect(doorSound, 0.5f);
        _rightClosed = !_rightClosed;
        UpdateRight();
    }
    public void UpdateRight()
    {
        if (_rightClosed)
        {
            rightSprite.sprite = rightClosed;
        }
        else
        {
            rightSprite.sprite = rightOpen;
        }
    }
    public void CalculatePowerUsage()
    {
        int calculatedUsage = 1;
        if (_isFlashing)
        {
            calculatedUsage++;
        }
        if (_leftClosed)
        {
            calculatedUsage++;
        }
        if (_rightClosed)
        {
            calculatedUsage++;
        }
        if (_monitorOpen)
        {
            calculatedUsage++;
        }
        if (calculatedUsage > 4)
        {
            calculatedUsage = 4;
        }
        powerUsage = calculatedUsage;
    }
    public void SubtractPower()
    {
        powerTimer -= Time.deltaTime;

        if (powerTimer <= 0)
        {
            float calculatedPower = defaultPower * Mathf.Pow(1.1f, Night) * powerUsage/1.5f;
            currentPower -= calculatedPower;

            powerTimer = powerCooldown;

            if (currentPower <= 0)
            {
                TurnOffPower();
            }

            int visualPower = (int)currentPower;
            powerText.text = $"Power:{visualPower.ToString()}%";
        }
    }
    public void TurnOffPower()
    {
        currentPower = 0;
        powerUsage = 0;
        _powerOutage = true;
        UpdatePowerLights();

        if (_monitorOpen)
        {
            closeButton.SetActive(false);
            monitor.SetActive(false);
            _monitorOpen = false;
        }
        
        _leftClosed = false;
        _rightClosed = false;
        _isFlashing = false;

        UpdateOffice();
        UpdateFlash();
        UpdateLeft();
        UpdateRight();
    }
    public void UpdatePowerLights()
    {
        //TURN ON ACTIVE LIGHTS
        for (int i = 0; i < powerLights.Count; i++)
        {
            if (i >= powerUsage)
            {
                powerLights[i].SetActive(false);
            }
            else
            {
                powerLights[i].SetActive(true);
            }
        }

        //SHAKE LIGHTS
        shakePower = powerUsage - 1;

        for (int i = 0; i < PLrects.Count; i++)
        {
            currentPLPositions[i] = targetPLPositions[i];

            PLrects[i].anchoredPosition = originalPLPositions[i] + currentPLPositions[i];

            targetPLPositions[i] = Random.insideUnitCircle * shakePower;
        }
    }

    public void TurnOnJumpscare()
    {
        jumpscaresHolder.SetActive(true);
    }
    public void CheckAmbience()
    {
        if (ambienceTimer <= 0)
        {
            ambienceTimer = Random.Range(20, 30);

            int ambienceDecider = Random.Range(1, 3);
            if (ambienceDecider == 1)
            {
                SoundEffectScript.Instance.PlaySoundEffect(ambience1Sound, 1f);
            }
            else if (ambienceDecider == 2)
            {
                SoundEffectScript.Instance.PlaySoundEffect(ambience2Sound, 1f);
            }
        }
    }
    public void PlayerInput()
    {
        if (Input.GetButtonDown("Flashlight") && _powerOutage == false)
        {
            _isFlashing = !_isFlashing;
            UpdateFlash();
            if (officeIndex == 0)
            {
                ToggleLeft();
            }
            else if (officeIndex == 2)
            {
                ToggleRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
        if (Input.GetButtonDown("MonitorToggle"))
        {
            ToggleMonitorView();
        }
    }
    void Update()
    {
        if (_isDead)
        {
            office.SetActive(false);
            deathmenu.SetActive(true);
        }
        else if(_is6AM)
        {
            if (office.activeSelf)
            {
                SoundEffectScript.Instance.StopAmbience();

                scanTimer = 0;
                _leftClosed = false;
                _rightClosed = false;
                _isFlashing = false;
                _inSuit = false;

                UpdateLeft();
                UpdateRight();
                UpdateFlash();
                UpdateSuit();
                CloseButton();

                CalculatePowerUsage();
                UpdatePowerLights();

                office.SetActive(false);
                transitionObject.SetActive(true);
            }
        }
        else
        {
            scanTimer -= Time.deltaTime;
            nightTime += Time.deltaTime;
            ambienceTimer -= Time.deltaTime;
            if (nightTime >= 360)
            {
                _is6AM = true;
            }
            CheckAmbience();
            UpdateAMText();
            PlayerInput();
            if (_powerOutage == false)
            {
                CalculatePowerUsage();
                UpdatePowerLights();
                SubtractPower();
            }
        }
    }
}

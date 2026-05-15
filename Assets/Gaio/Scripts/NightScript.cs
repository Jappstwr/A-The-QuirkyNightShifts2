using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.InputSystem;

public class NightScript : MonoBehaviour
{
    public List<GameObject> offices = new List<GameObject>();
    public int officeIndex;

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

    public bool _isOnCam;

    public GameObject flashlight;
    public bool _isFlashing;

    public int maxPower;
    public float currentPower;
    public int powerUsage;

    public List<GameObject> powerLights;

    public float shakePower;

    public List<RectTransform> PLrects = new List<RectTransform>();

    public Vector2[] originalPLPositions;
    public Vector2[] currentPLPositions;
    public Vector2[] targetPLPositions;

    public bool _leftClosed;
    public bool _rightClosed;
    public bool _monitor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isOnCam = true;
        _monitor = false;
        _leftClosed = false;
        _rightClosed = false;
        powerUsage = 1;
        _isFlashing = false;
        UpdateFlash();
        closeButton.SetActive(false);
        _inSuit = false;
        CloseButton();
        officeIndex = 1;
        UpdateOffice();
        ResetPowerLights();
    }

    // Update is called once per frame
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
        monitor.SetActive(false);
        suit.SetActive(false);
        turnLeftButton.SetActive(true);
        turnRightButton.SetActive(true);
        monitorButton.SetActive(true);
        suitButton.SetActive(true);
        closeButton.SetActive(false);
        _monitor = false;
        _inSuit = false;
    }
    public void ToggleMonitor()
    {
        _monitor = !_monitor;
        UpdateMonitor();
        UpdateFlash();
    }
    public void ToggleView()
    {
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
    public void ToggleSuit()
    {
        _inSuit = !_inSuit;
        UpdateSuit();
        UpdateFlash();
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
    }
    public void UpdateMonitor()
    {
        if (_monitor)
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
        if (_isFlashing && _inSuit == false && officeIndex == 1)
        {
            flashlight.SetActive(true);
        }
        else
        {
            _isFlashing = false;
            flashlight.SetActive(false);
        }
    }
    public void CalculatePowerUsage()
    {
        int calculatedUsage = 2;
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
        if (_monitor)
        {
            calculatedUsage++;
        }
        if (calculatedUsage > 4)
        {
            calculatedUsage = 4;
        }
        powerUsage = calculatedUsage;
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
    public void PlayerInput()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            _isFlashing = !_isFlashing;
            UpdateFlash();
        }
        if (Input.GetButtonDown("MonitorToggle"))
        {
            ToggleView();
        }
    }
    void Update()
    {
        PlayerInput();
        CalculatePowerUsage();
        UpdatePowerLights();
    }
}

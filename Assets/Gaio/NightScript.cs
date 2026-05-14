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

    public GameObject flashlight;
    public bool _isFlashing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isFlashing = false;
        UpdateFlash();
        closeButton.SetActive(false);
        _inSuit = false;
        UpdateSuit();
        officeIndex = 1;
        UpdateOffice();
    }

    // Update is called once per frame

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
        else
        {
            suit.SetActive(false);
            turnLeftButton.SetActive(true);
            turnRightButton.SetActive(true);
            monitorButton.SetActive(true);
            suitButton.SetActive(true);
            closeButton.SetActive(false);
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
    public void PlayerInput()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            _isFlashing = !_isFlashing;
            UpdateFlash();
        }
    }
    void Update()
    {
        PlayerInput();
    }
}

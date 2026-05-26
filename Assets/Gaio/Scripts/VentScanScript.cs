using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class VentScanScript : MonoBehaviour
{
    public NightScript nightScript;
    public VentAnimatronicsScript ventAnimScript;
    public List<Vector3> scanPositions;

    public GameObject scanMarker;
    public GameObject scanMarker2;
    public GameObject doubleScanMarker;

    public float scanTime;
    public Image scanSlider;

    public AudioClip scanSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scanTime = 0;
    }
    public void UpdateSlider()
    {
        if (scanTime <= 0f)
        {
            scanSlider.fillAmount = 0;
        }
        else
        {
            float percent = scanTime / 4;
            scanSlider.fillAmount = percent;
        }
    }
    public void Scan()
    {
        if (scanTime <= 0)
        {
            SoundEffectScript.Instance.PlaySoundEffect(scanSound, 1f);

            nightScript.scanTimer = 4;
            scanTime = 4;

            if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.c && ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.c)
            {
                doubleScanMarker.SetActive(true);
                doubleScanMarker.GetComponent<ScanMarkerScript>().Activate();
            }
            else
            {
                if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.c)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[0];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.b1)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[1];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.b2)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[2];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.a1)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[3];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.a2)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[4];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.x1)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[5];
                }
                else if (ventAnimScript.clankarenPosition == VentAnimatronicsScript.Positions.x2)
                {
                    scanMarker.SetActive(true);
                    scanMarker.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[6];
                }




                if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.c)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[0];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.b1)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[1];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.b2)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[2];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.a1)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[3];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.a2)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[4];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.x1)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[5];
                }
                else if (ventAnimScript.ferdinandPosition == VentAnimatronicsScript.Positions.x2)
                {
                    scanMarker2.SetActive(true);
                    scanMarker2.GetComponent<ScanMarkerScript>().Activate();
                    scanMarker2.GetComponent<ScanMarkerScript>().scanPosition = scanPositions[6];
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        scanTime -= Time.deltaTime;
        UpdateSlider();
    }
}

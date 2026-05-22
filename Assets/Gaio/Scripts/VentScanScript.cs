using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class VentScanScript : MonoBehaviour
{
    public VentAnimatronicsScript ventAnimScript;
    public List<Vector3> scanPositions;

    public GameObject scanMarker;
    public GameObject scanMarker2;
    public GameObject doubleScanMarker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Scan()
    {
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

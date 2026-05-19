using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using System.Net;
using Unity.VisualScripting;

public class KlokerScriptMovement : MonoBehaviour
{
    [SerializeField] public Waypoints[] paths; 

    //[SerializeField] private NightScript nighscript;

    [SerializeField] private int baseAI = 1;

    [SerializeField] private float checkInterval = 5f; 

    private int currentPathIndex = 0;
    public int aiLevel = 0;

    public float aiTimer = 0; 

    public float moveTimer;

    public int currentRoom;
    public SpriteRenderer sr; 

    void Start()
    {
        aiLevel = baseAI;

        MoveToWaypoint(0);

        moveTimer = checkInterval; 
    }

    // Update is called once per frame
    void Update()
    {
        MainCameraScript cam = FindFirstObjectByType<MainCameraScript>();

        bool visible = cam.ActiveCamera == currentRoom && cam.camerasOpen;

        sr.enabled = visible;



        aiTimer += Time.deltaTime; 

        UpdateAILevel();

        moveTimer -= Time.deltaTime; 

        if (moveTimer <= 0f)
        {
            moveTimer = checkInterval;

            OpportunityMovement(); 
        }

    }   
    public void UpdateAILevel()
    {
        //float hour = nighscript.nightTime;

        if (aiTimer <= 60f)
        {
            aiLevel = 0; 
        }
        else 
        {
            aiLevel = baseAI;
        }

       

        if (aiTimer >= 120f)
        {
            aiLevel += 1; 
        }

        if (aiTimer >= 240)
        {
            aiLevel += 2; 
        }
    }

    void MoveToWaypoint(int index)
    {
        Waypoints wp = paths[index];

        transform.position = wp.transform.position;

        currentRoom = wp.roomIndex;

        //int layer = LayerMask.NameToLayer(wp.roomLayer);

        //if (layer != -1)
        //{
        //    gameObject.layer = layer;
        //}

        Debug.Log($"Kloker moved to: {wp.name}");
    }

    public void OpportunityMovement()
    {
        int roll = Random.Range(1,21);

        Debug.Log($"Kloker Roll: {roll} / {aiLevel}");

        if (roll <= aiLevel)
        {
            MoveForward(); 
        }
    }
    public void MoveForward()
    {
        if (currentPathIndex >= paths.Length - 1)
        {
            return; 
        }

        currentPathIndex++;

        MoveToWaypoint(currentPathIndex); 
    }


}

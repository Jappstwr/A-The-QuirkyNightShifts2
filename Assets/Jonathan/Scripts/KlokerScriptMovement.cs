using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using System.Net;
using Unity.VisualScripting;

public class KlokerScriptMovement : MonoBehaviour
{
    [SerializeField] public Waypoints[] paths;

    [SerializeField] private NightScript nightscript;

    [SerializeField] private int baseAI = 1;

    [SerializeField] private float checkInterval = 5f; 

    private int currentPathIndex = 0;
    public int aiLevel = 0;

    public float aiTimer = 0; 

    public float moveTimer;

    public int currentRoom;
    public SpriteRenderer sr;

    public bool backHallwayPoint;
    public bool hallwayPoint;

    public bool isFladderlappen; 

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

        if (currentPathIndex == 4)
        {
            AnimAttack();
        }

        if  (moveTimer <= 0f)
        {
            moveTimer = checkInterval;

            OpportunityMovement(); 
        }

    }   
    public void UpdateAILevel()
    {
        //float hour = nighscript.nightTime;

        //removed because he can be like benny (bonnie). starts with an ai lvl of 1. 
        
        if (isFladderlappen)
        {
            if (aiTimer <= 60f)
            {
                aiLevel = 0;
            }
            if (aiTimer >= 60)
            {
                baseAI = 1;
                aiLevel = baseAI;
            }
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

        if (isFladderlappen)
        {
            Debug.Log($"FladderLappen moved to: {wp.name}"); 
        }
        else
        {
            Debug.Log($"Kloker moved to: {wp.name}");
        }
    }

    public void OpportunityMovement()
    {
        int roll = Random.Range(1,21);

        if (isFladderlappen)
        {
            Debug.Log($"Fladder Roll: {roll} / {aiLevel}");
        }
        else
        {
            Debug.Log($"Kloker Roll: {roll} / {aiLevel}");
        }

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

        //if (currentPathIndex == 1)
        //{
        //    int roll = Random.Range(1, 21);

        //    if (roll >= 5)
        //    {
        //        currentPathIndex = 3;
        //        MoveToWaypoint(currentPathIndex);
        //        Debug.Log("Failed to move to Hallway");
        //    }
        //    else
        //    {
        //        currentPathIndex = 4;
        //        MoveToWaypoint(currentPathIndex);
        //        Debug.Log("Successfully moved to Hallway");
        //    }
        //}

        currentPathIndex++;

        MoveToWaypoint(currentPathIndex);

    }
    public void AnimAttack()
    {
        float attackTimer = 8f;
        float resetTimer = 8f; 
        attackTimer -= Time.deltaTime;
        
        float retreatTimer = Random.Range(3f, 5f); 
        
        
        if (isFladderlappen)
        {
            if (nightscript._isFlashing)
            {
                retreatTimer -= Time.deltaTime;
                attackTimer = resetTimer; 
                if (retreatTimer <= 0f)
                {
                    MoveToWaypoint(1);
                }
            }
            else if (!nightscript._isFlashing && attackTimer <= 0f)
            {
                JumpScare2(); 
            }
        }
        else
        {
            if (nightscript._inSuit)
            {
                retreatTimer -= Time.deltaTime;
                attackTimer = resetTimer; 
                if (retreatTimer <= 0f)
                {
                    MoveToWaypoint(0);
                }
            }
            else if (!nightscript._inSuit && attackTimer <= 0f)
            {
                JumpScare();
                Debug.Log($"Game Over! You survived {aiTimer} minutes");
            }
        }

       
        
        // if suit on: wait 3-4 sec, walk away. 
    }

    public void JumpScare()
    {
        //Kloker jumspcare
    }
    public void JumpScare2()
    {
        //FladderLappen jumpscare
    }
}

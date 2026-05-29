using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KlokerScriptMovement : MonoBehaviour
{
    [SerializeField] public Waypoints[] paths;

    [SerializeField] private NightScript nightscript;

    [SerializeField] private int baseAI = 1;

    [SerializeField] private float checkInterval = 5f;

    [SerializeField] private MainCameraScript maincamerascript; 
    

    private int currentPathIndex = 0;
    public int aiLevel = 0;

    public float aiTimer = 0;

    public int currentAIModifier; 

    public float moveTimer;

    public int currentRoom;
    //public SpriteRenderer sr;
    [SerializeField] private SpriteRenderer sr;

    public bool backHallwayPoint;
    public bool hallwayPoint;

    public bool isFladderlappen;

    private float attackTimer = 8f;
    private float retreatTimer;
    private bool attacking;

    private Waypoints currentWaypoint;

    void Start()
    {
        //maincamerascript = FindFirstObjectByType<MainCameraScript>();

        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        aiLevel = baseAI;

        GetNight(); 

        MoveToWaypoint(0);

        moveTimer = checkInterval;

        retreatTimer = Random.Range(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisibility();


        aiTimer += Time.deltaTime; 

        UpdateAILevel();

        moveTimer -= Time.deltaTime;

        if (isFladderlappen && currentWaypoint.isHallway && !attacking)
        {
            attacking = true;
            attackTimer = 8f;
            retreatTimer = Random.Range(2f, 3f);
        }
        else
        {
            if (currentWaypoint.isOffice && !attacking)
            {
                attacking = true;
                attackTimer = 8f;
                retreatTimer = Random.Range(2f, 3f);
            }
        }
        if (attacking)
        {
            AnimAttack();
            return;
        }

        if (moveTimer <= 0f)
        {
            moveTimer = checkInterval;

            OpportunityMovement();
        }
    }   
    public void UpdateAILevel()
    {
        aiLevel = baseAI; 

        if (isFladderlappen)
        {
            if (nightscript.Night == 1)
            {
                aiLevel = 0;
                return; 
            }

            if (aiTimer < 60f)
            {
                aiLevel = 0;
                return; 
            }

            if (aiTimer >= 60f)
            {
                aiLevel = baseAI; 
            }

            if (nightscript.Night >= 6)
            {
                baseAI = 10; 
            }
        }

        if (nightscript.Night >= 6)
        {
            baseAI = 10;
        }

        if (aiTimer >= 120f)
        {
            aiLevel += 1;
        }

        if (aiTimer >= 240)
        {
            aiLevel += currentAIModifier;
        }

        
    }
    public void GetNight()
    {
        if (nightscript.Night == 1)
        {
            currentAIModifier += 1; 
        }

        if (nightscript.Night == 2)
        {
            currentAIModifier += 2; 
        }

        if (nightscript.Night == 3)
        {
            currentAIModifier += 3; 
        }

        if (nightscript.Night == 4)
        {
            currentAIModifier += 5; 
        }

        if (nightscript.Night == 5)
        {
            currentAIModifier += 6; 
        }
        if (nightscript.Night >= 6)
        {
            currentAIModifier += 8; 
        }

    }
    void MoveToWaypoint(int index)
    {
        Waypoints wp = paths[index];


        currentWaypoint = wp;

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

        if (currentWaypoint.isOuterStage)
        {
            int roll = Random.Range(1, 21);

            if (roll <= aiLevel)
            {
                currentPathIndex = 3;
                MoveToWaypoint(currentPathIndex); 
            }
        }
        else
        {
            currentPathIndex++;

            MoveToWaypoint(currentPathIndex);
        }
       
    }
    public void AnimAttack()
    {
        attackTimer -= Time.deltaTime;
        retreatTimer -= Time.deltaTime;

        if (isFladderlappen)
        {
            if (nightscript._isFlashing)
            {
                if (retreatTimer <= 0f)
                {
                    attacking = false;
                    currentPathIndex = 1;
                    MoveToWaypoint(currentPathIndex);
                }
            }
            else if (!nightscript._isFlashing && attackTimer <= 0f)
            {
                JumpScare2();
                Debug.Log($"Game Over! FladderLappen got you!");
            }
        }
        else
        {
            if (nightscript._inSuit)
            {
                
                if (retreatTimer <= 0f)
                {
                    attacking = false;
                    currentPathIndex = 0;
                    MoveToWaypoint(currentPathIndex);
                }
            }
            else if (!nightscript._inSuit && attackTimer <= 0f)
            {
                JumpScare();
                Debug.Log($"Game Over! Kloker got you!");
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
    public void UpdateVisibility()
    {
        if (currentWaypoint == null)
        {
            sr.enabled = false;
            return;
        }

        if ((currentWaypoint.isHallway || currentWaypoint.isOffice) && nightscript._monitorOpen)
        {
            sr.enabled = false;
            return;
        }

        if (currentWaypoint.isLeftOffice && nightscript._leftClosed)
        {
            sr.enabled = false;
            return;
        }

        if (currentWaypoint.isRightOffice && nightscript._rightClosed)
        {
            sr.enabled = false;
            return;
        }

        if (currentWaypoint.isHallway)
        {
            sr.enabled = nightscript._isFlashing;
            return;
        }

        if (currentWaypoint.isOffice)
        {
            sr.enabled = true;
            return;
        }
        
      

        
        

        bool visible = maincamerascript != null && maincamerascript.ActiveCamera == currentRoom && maincamerascript.camerasOpen;

        sr.enabled = visible;
    }

    public void ResetAnimatronics()
    {
        baseAI = 0;
        aiLevel = 0;

        aiTimer = 0f;

        currentPathIndex = 0;
        MoveToWaypoint(currentPathIndex);

        moveTimer = checkInterval;
    }
}

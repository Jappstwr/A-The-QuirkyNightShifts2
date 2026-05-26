using UnityEngine;

public class SnattarenScript : MonoBehaviour
{
    [SerializeField] public Waypoints[] paths;

    [SerializeField] private NightScript nightscript;

    [SerializeField] private int BaseAI = 0;

    [SerializeField] private float checkInterval = 5f;

    [SerializeField] private MainCameraScript maincamerascript;

    private int currentPathIndex;
    public int aiLvl = 0;

    public float aiTimer = 0;

    public int currentAIModifier;

    public float moveTimer;

    public int currentRoom;
    [SerializeField] private SpriteRenderer sr;

    public float attackTimer = 8f;
    public float retreatTimer;

    public bool attacking;
    private bool hasAttackStarted;

    private Waypoints currentWaypoint;





    void Start()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>(); 
        }

        MoveToWaypoint(0);

        moveTimer = checkInterval;

        retreatTimer = Random.Range(1,3); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisibility();
        UpdateAI();

        aiTimer += Time.deltaTime;


        moveTimer -= Time.deltaTime; 

        if (currentWaypoint.isOffice && !hasAttackStarted)
        {
            hasAttackStarted = true; 
            attacking = true;
            attackTimer = 6f;
            retreatTimer = Random.Range(1,3); 
        }
        if (attacking)
        {
            AnimAttack(); 
        }

        if (moveTimer <= 0f)
        {
            moveTimer = checkInterval;

            OpportunityMovement(); 
        }

    }

    public void UpdateAI()
    {
        BaseAI = 0; 

        if (nightscript.Night == 3 && aiTimer >= 240f)
        {
            BaseAI = 2; 
        }
        else if (nightscript.Night >= 4)
        {
            BaseAI = 1; 
        }


        if (nightscript.Night >= 4 && aiTimer >= 120f)
        {
            aiLvl += 1; 
        }
        if (nightscript.Night >= 4 && aiTimer >= 240f)
        {
            aiLvl += 2; 
        }

        aiLvl = BaseAI;
    }

    public void MoveToWaypoint(int index)
    {
        Waypoints wp = paths[index];

        currentWaypoint = wp;

        transform.position = wp.transform.position;

        currentRoom = wp.roomIndex;

        Debug.Log($"Snattaren moved to {currentRoom}"); 
    }

    public void OpportunityMovement()
    {
        int roll = Random.Range(1,21);
        
        if (roll <= aiLvl)
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
    public void AnimAttack()
    {
        attackTimer -= Time.deltaTime;
        retreatTimer -= Time.deltaTime; 

        if (nightscript._isFlashing && retreatTimer <= 0f)
        {
            hasAttackStarted = false;
            attacking = false; 

            currentPathIndex = 0;

            MoveToWaypoint(currentPathIndex); 
        }
        else if (!nightscript._isFlashing)
        {
            //take 'em bloody batteries
        }
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
}

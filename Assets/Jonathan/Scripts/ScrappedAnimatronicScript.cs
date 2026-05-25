using Unity.VisualScripting;
using UnityEngine;

public class ScrappedAnimatronicScript : MonoBehaviour
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

    public bool isBenny;
    public bool isFredrik;

    [SerializeField] private Sprite backroomSprite;
    [SerializeField] private Sprite activeSprite;

    void Start()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>(); 
        }

        aiLvl = BaseAI;

        MoveToWaypoint(0); 

        moveTimer = checkInterval;


        retreatTimer = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisibility(); 
        UpdateAI();

        aiTimer += Time.deltaTime;

        //if (BaseAI <= 0f && !attacking)
        //{
        //    return; 
        //}

        moveTimer -= Time.deltaTime;

        if (currentWaypoint.isOffice && !hasAttackStarted)
        {
            hasAttackStarted = true; 
            attacking = true;
            attackTimer = 8f;
            retreatTimer = Random.Range(2f, 4f);
        }
        if (attacking)
        {
            Debug.Log($"Attack started {attackTimer}");
            AnimAttack();
            return; 
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

        //fredrik movement
        if (isFredrik)
        {
            if (nightscript.Night == 3 && aiTimer >= 240f)
            {
                BaseAI = 2; 
            }

            if (nightscript.Night >= 4)
            {
                BaseAI = 1;
            }
        }

        //benny movement
        if (isBenny)
        {
            if (nightscript.Night == 4 && aiTimer >= 240f)
            {
                BaseAI = 2; 
            }
            if (nightscript.Night >= 5)
            {
                BaseAI = 1; 
            }
        }

        aiLvl = BaseAI; 
        
    }
    public void MoveToWaypoint(int index)
    {
        Waypoints wp = paths[index];

        currentWaypoint = wp;

        transform.position = wp.transform.position;

        currentRoom = wp.roomIndex;

        if (index == 0)
        {
            sr.sprite = backroomSprite;
        }
        else
        {
            sr.sprite = activeSprite;
        }

        if (isBenny)
        {
            Debug.Log($"ScrappedBenny moved to: {wp.name}");
        }
        else
        {
            Debug.Log($"ScrappedFredrik moved to: {wp.name}");
        }
    }

    public void OpportunityMovement()
    {
        int roll = Random.Range(1,21);

      
        if (roll <= aiLvl)
        {
            MoveForward(); 
        }

        if (isBenny)
        {
            Debug.Log($"ScrappedBenny Roll: {roll} / {aiLvl}");
        }
        if (isFredrik)
        {
            Debug.Log($"ScrappedFredrik Roll: {roll} / {aiLvl}");
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

        if (nightscript._inSuit)
        {
            if (retreatTimer <= 0f)
            {
                hasAttackStarted = false; 
                attacking = false;

                currentPathIndex = 0;

                MoveToWaypoint(currentPathIndex);
            }
        }
        else if (!nightscript._inSuit && attackTimer <= 0f)
        {
          
            Jumpscare();

            if (isBenny)
            {
                Debug.Log("Benny got you! Game Over!");
            }
            else if (isFredrik)
            {
                Debug.Log("Fredrik got you! Game Over!");
            }
            
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

    public void Jumpscare()
    {
        //here goes jumpscares mister gaio

        // Benny jumpscare 
        if (isBenny)
        {

        }
        
        //Fredrik jumpscare
        if (isFredrik)
        {

        }
    }
}

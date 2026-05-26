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
}

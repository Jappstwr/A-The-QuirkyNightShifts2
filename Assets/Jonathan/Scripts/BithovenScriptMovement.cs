using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BithovenScriptMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private WindUpButton windUpSystem;
    [SerializeField] private SpriteRenderer bithovenRenderer;
    [SerializeField] private int roomIndex = 2;
    [SerializeField] private MainCameraScript camSystem;

    [SerializeField] private NightScript nightScript;
    [SerializeField] public GameObject BithovenJumpscare; 

    [Header("Sprite")]
    [SerializeField] private Sprite sleepSprite;
    [SerializeField] private Sprite awakeSprite;

    private bool awake = false;
    private bool hasAttacked = false;
    private bool Attacking = false; 
    public float AttackTimer;
    public bool isJumpscareActive; 

    [Header("WayPoint")]
    [SerializeField] private Waypoints waypoints; 

    void Start()
    {
        transform.position = waypoints.transform.position;

        int Layer = LayerMask.NameToLayer(waypoints.roomLayer);

        if (Layer != -1)
        {
            gameObject.layer = Layer;
        }

        AttackTimer = 50f;
        //bithovenRenderer.sortingLayerName = waypoints.roomLayer;
        //bithovenRenderer.sortingOrder = 10;

        bithovenRenderer.sprite = sleepSprite;

        isJumpscareActive = false; 
    }

    // Update is called once per frame
    public void Update()
    {
        bool visible = camSystem.ActiveCamera == roomIndex && camSystem.camerasOpen;

        bithovenRenderer.enabled = visible;
        
        float fill = windUpSystem.FillPercent;

        

        if (fill <= 0.3f)
        {
            awake = true;

            bithovenRenderer.sprite = awakeSprite;
        }
        else if (awake)
        {
            if (fill >= 1f)
            {
                 
                awake = false;
                AttackTimer = 50f; 
                bithovenRenderer.sprite = sleepSprite;
            }
        }
        else 
        {
            bithovenRenderer.sprite = sleepSprite; 
        }

        if (!Attacking)
        {
            hasAttacked = false;
        }
        if (awake)
        {
            AttackTimer -= Time.deltaTime;
        }
        
        if (awake && AttackTimer <= 0 && !hasAttacked && (fill <= 0 || fill >= 0) || fill <= 0)
        {
            hasAttacked = true;
            Attacking = true; 
            Debug.Log("Bithoven has attacked!");
            
            if (!isJumpscareActive)
            {
                isJumpscareActive = true; 
                Jumpscare();
            }

            return; 
        }


        //if (fill <= 0f && !hasAttacked)
        //{
        //    hasAttacked = true; 
        //    Debug.Log("Bithoven attack! Game Over!");

        //    Jumpscare(); 
        //}

    }

    public void Jumpscare()
    {

        BithovenJumpscare.gameObject.SetActive(true);
        nightScript.TurnOnJumpscare(); 
    }
}

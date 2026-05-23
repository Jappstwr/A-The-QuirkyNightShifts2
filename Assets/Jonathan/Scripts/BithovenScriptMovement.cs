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

    [Header("Sprite")]
    [SerializeField] private Sprite sleepSprite;
    [SerializeField] private Sprite awakeSprite;

    private bool awake = false;
    private bool hasAttacked = false;
    private bool Attacking = false; 
    public float AttackTimer = 10f;

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

        AttackTimer = 10f;
        //bithovenRenderer.sortingLayerName = waypoints.roomLayer;
        //bithovenRenderer.sortingOrder = 10;

        bithovenRenderer.sprite = sleepSprite; 
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

                bithovenRenderer.sprite = sleepSprite;
            }
        }
        else 
        {
            bithovenRenderer.sprite = sleepSprite; 
        }

        if (Attacking == false)
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
            Jumpscare();
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
       //Gaio, here goes the jumpscare       
    }
}

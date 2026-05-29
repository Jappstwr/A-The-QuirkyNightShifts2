using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VentAnimatronicsScript : MonoBehaviour
{
    public NightScript nightScript;

    public GameObject clankaren;
    public GameObject ferdinand;

    public enum Positions {x1, x2, a1, a2, b1, b2, c, d }

    public Positions clankarenPosition;
    public Positions ferdinandPosition;

    public int clankarenAILevel;
    public int ferdinandAILevel;

    public float clankarenCooldown;
    public float ferdinandCooldown;

    public float clankarenTimer;
    public float ferdinandTimer;

    public bool _clankarenActiveThisNight;
    public bool _ferdinandActiveThisNight;

    public bool _hasAddedOnce;
    public bool _hasAddedTwice;

    public float counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetAnimatronic();
    }

    public void ResetAnimatronic()
    {
        ferdinand.SetActive(false);
        clankaren.SetActive(false);
        clankarenPosition = Positions.d;
        ferdinandPosition = Positions.d;
        clankarenTimer = 0;
        ferdinandTimer = 0;
        _hasAddedOnce = false;
        _hasAddedTwice = false;
        counter = 0;
        SetStartingValues();
    }
    void SetStartingValues()
    {
        if (nightScript.Night == 1)
        {
            clankarenAILevel = 0;
            ferdinandAILevel = 0;
            _clankarenActiveThisNight = false;
            _ferdinandActiveThisNight = false;
        }
        else if (nightScript.Night == 2)
        {
            clankarenAILevel = 2;
            ferdinandAILevel = 0;
            _clankarenActiveThisNight = true;
            _ferdinandActiveThisNight = false;
        }
        else if (nightScript.Night == 3)
        {
            clankarenAILevel = 5;
            ferdinandAILevel = 0;
            _clankarenActiveThisNight = true;
            _ferdinandActiveThisNight = false;
        }
        else if (nightScript.Night == 4)
        {
            clankarenAILevel = 7;
            ferdinandAILevel = 0;
            _clankarenActiveThisNight = true;
            _ferdinandActiveThisNight = true;
        }
        else if (nightScript.Night == 5)
        {
            clankarenAILevel = 10;
            ferdinandAILevel = 10;
            _clankarenActiveThisNight = true;
            _ferdinandActiveThisNight = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 120 && _hasAddedOnce == false)
        {
            _hasAddedOnce = true;

            if (_clankarenActiveThisNight)
            {
                clankarenAILevel += 2;
            }
            if (_ferdinandActiveThisNight)
            {
                ferdinandAILevel += 2;
            }
}
        else if (counter >= 240 && _hasAddedTwice == false)
        {
            _hasAddedTwice = true;

            if (_clankarenActiveThisNight)
            {
                clankarenAILevel += 2;
            }
            if (_ferdinandActiveThisNight)
            {
                ferdinandAILevel += 2;
            }
        }

        clankarenTimer += Time.deltaTime;
        ferdinandTimer += Time.deltaTime;

        if (clankarenTimer >= clankarenCooldown)
        {
            clankarenTimer = 0;
            int rng = Random.Range(1, 21);

            if (rng <= clankarenAILevel)
            {
                if (clankarenPosition == Positions.d)
                {
                    clankarenPosition = Positions.c;
                }
                else if(clankarenPosition == Positions.c)
                {
                    int rng2 = Random.Range(1, 3);
                    if (rng2 == 1)
                    {
                        if (ferdinandPosition != Positions.b1)
                        {
                            clankarenPosition = Positions.b1;
                        }
                    }
                    else
                    {
                        if (ferdinandPosition != Positions.b2)
                        {
                            clankarenPosition = Positions.b2;
                        }
                    }
                }
                else if (clankarenPosition == Positions.b1)
                {
                    if (ferdinandPosition != Positions.a1)
                    {
                        clankarenPosition = Positions.a1;
                    }
                }
                else if (clankarenPosition == Positions.b2)
                {
                    if (ferdinandPosition != Positions.a2)
                    {
                        clankarenPosition = Positions.a2;
                    }
                }
                else if (clankarenPosition == Positions.a1)
                {
                    if (nightScript._leftClosed == true)
                    {
                        clankarenPosition = Positions.c;
                    }
                    else
                    {
                        if (ferdinandPosition != Positions.x1)
                        {
                            clankarenPosition = Positions.x1;
                        }
                    }
                }
                else if (clankarenPosition == Positions.a2)
                {
                    if (nightScript._rightClosed == true)
                    {
                        clankarenPosition = Positions.c;
                    }
                    else
                    {
                        if (ferdinandPosition != Positions.x2)
                        {
                            clankarenPosition = Positions.x2;
                        }
                    }
                }
                else if (clankarenPosition == Positions.x1)
                {
                    if (nightScript._leftClosed == true)
                    {
                        clankarenPosition = Positions.c;
                    }
                    else
                    {
                        nightScript.TurnOnJumpscare();
                        clankaren.SetActive(true);
                    }
                }
                else if (clankarenPosition == Positions.x2)
                {
                    if (nightScript._rightClosed == true)
                    {
                        clankarenPosition = Positions.c;
                    }
                    else
                    {
                        nightScript.TurnOnJumpscare();
                        clankaren.SetActive(true);
                    }
                }
            }
        }
        if (ferdinandTimer >= ferdinandCooldown)
        {
            ferdinandTimer = 0;
            int rng = Random.Range(1, 21);

            if (rng <= ferdinandAILevel)
            {
                if (ferdinandPosition == Positions.d)
                {
                    ferdinandPosition = Positions.c;
                }
                else if (ferdinandPosition == Positions.c)
                {
                    int rng2 = Random.Range(1, 3);
                    if (rng2 == 1)
                    {
                        if (clankarenPosition != Positions.b1)
                        {
                            ferdinandPosition = Positions.b1;
                        }
                    }
                    else
                    {
                        if (clankarenPosition != Positions.b2)
                        {
                            ferdinandPosition = Positions.b2;
                        }
                    }
                }
                else if (ferdinandPosition == Positions.b1)
                {
                    if (clankarenPosition != Positions.a1)
                    {
                        ferdinandPosition = Positions.a1;
                    }
                }
                else if (ferdinandPosition == Positions.b2)
                {
                    if (clankarenPosition != Positions.a2)
                    {
                        ferdinandPosition = Positions.a2;
                    }
                }
                else if (ferdinandPosition == Positions.a1)
                {
                    if (nightScript._leftClosed == true)
                    {
                        ferdinandPosition = Positions.c;
                    }
                    else
                    {
                        if (clankarenPosition != Positions.x1)
                        {
                            ferdinandPosition = Positions.x1;
                        }
                    }
                }
                else if (ferdinandPosition == Positions.a2)
                {
                    if (nightScript._rightClosed == true)
                    {
                        ferdinandPosition = Positions.c;
                    }
                    else
                    {
                        ferdinandPosition = Positions.x2;
                    }
                }
                else if (ferdinandPosition == Positions.x1)
                {
                    if (nightScript._leftClosed == true)
                    {
                        ferdinandPosition = Positions.c;
                    }
                    else
                    {
                        nightScript.TurnOnJumpscare();
                        ferdinand.SetActive(true);
                    }
                }
                else if (ferdinandPosition == Positions.x2)
                {
                    if (nightScript._rightClosed == true)
                    {
                        ferdinandPosition = Positions.c;
                    }
                    else
                    {
                        nightScript.TurnOnJumpscare();
                        ferdinand.SetActive(true);
                    }
                }
            }
        }


    }
}

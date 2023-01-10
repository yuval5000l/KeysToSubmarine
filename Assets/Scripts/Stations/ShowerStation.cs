using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ShowerStation : StationScript
{
    //[SerializeField] private bool always_active;
    [SerializeField] private float holdTime = 0;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Animator station_animation;
    [SerializeField] private float bonus_time = 1.5f;
    [SerializeField] private DoorScript door;
    private float timeHeld = 0;
    private bool door_activated = true;
    [SerializeField] private GameObject stationExplainer;




    new void Start()
    {
        base.Start();
        missions.Add(HoldKey);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        gameObject.tag = "None";
        stationPopup = Instantiate(Resources.Load("LightBulb")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,1.5f,0);
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {

        if(timeWindowToPress >= maximalTime)
        {
            timeWindowToPress = 0;
            pressKeysInARowCount = 0;
            timeHeld = 0;
            indicator.transform.localScale = new Vector3((timeHeld) * 1f, indicator.transform.localScale.y);
            station_animation.SetTrigger("EndShower");
        }
        //temporary fix for presKeyInRow, needs better solution
        if (station_active || always_active)
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(true);
            }
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                station_animation.SetTrigger("Touched");
            }
            else
            {
                station_animation.SetTrigger("idle");
            }
        }
        else
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(false);
            }
            station_animation.SetTrigger("idle");
        }
        timeWindowToPress += Time.deltaTime;
        if (door && door.DoorState())
        {
            if (stationExplainer)
            {
                stationExplainer.SetActive(false);
            }
        }
    }


    private void HoldKey()
    {
        foreach ( var action_key in players_action_key)
        {
            if (Input.GetKey(action_key))
            {
                station_animation.SetTrigger("StartShower");
                gameObject.tag = "ActiveShower";
                timeHeld += Time.deltaTime;
                timeWindowToPress = 0;
                if (!always_active)
                {
                    indicator.transform.localScale = new Vector3((timeHeld) * 1f, indicator.transform.localScale.y);
                }
                if (door && door_activated)
                {
                    door.OpenDoor();
                    door_activated = false;
                }
            }
            else
            {
                timeHeld = 0;
                gameObject.tag = "None";
                if (door & !door_activated)
                {
                    door.StopTouchDoor();
                    door_activated = true;
                }
            }
       }
       //foreach(var action_key in players_controller_in_station)
       // {
       //     if (action_key.Item2 == true)
       //     {
       //         station_animation.SetTrigger("StartShower");
       //         gameObject.tag = "ActiveShower";
       //         timeHeld += Time.deltaTime;
       //         indicator.transform.localScale = new Vector3((timeHeld) * 1f, indicator.transform.localScale.y);
       //         timeWindowToPress = 0;

       //     }
       // }
        if (!always_active && timeHeld >= holdTime)
        {
            station_animation.SetTrigger("EndShower");
            timeHeld = 0;
            gameObject.tag = "None";
            station_active = false;
            indicator.transform.localScale = new Vector3(0.01f, indicator.transform.localScale.y);
            deActivatePopup();
            missionManager.missionDone(bonus_time, points_award);
        }
    }
}


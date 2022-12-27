using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ShowerStation : StationScript
{

    [SerializeField] private float holdTime = 0;
    [SerializeField] private GameObject indicator;
    private float timeHeld = 0;

    void Start()
    {
        missions.Add(HoldKey);
        missionsNumberOfPlayers.Add(1);

        stationPopup = Instantiate(Resources.Load("StationPopup")) as GameObject;
        stationPopup.transform.position = gameObject.transform.position + new Vector3(0,2,0);
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {
        //temporary fix for presKeyInRow, needs better solution
        if (station_active)
        {
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                missions[mission_index]();
                 
            }
        }

    }


    private void HoldKey()
    {
       foreach( var action_key in players_action_key)
       {
            if (Input.GetKey(action_key))
            {
                timeHeld += Time.deltaTime;
                indicator.transform.localScale = new Vector3((timeHeld)*1f, indicator.transform.localScale.y);
            }
            else
            {
                timeHeld = 0;
            }
       }
       if (timeHeld >= holdTime) 
       {
        timeHeld = 0;
        station_active = false;
        indicator.transform.localScale = new Vector3(0.01f, indicator.transform.localScale.y);
        deActivatePopup();
        missionManager.missionDone(1, 1);
       }
    }
}


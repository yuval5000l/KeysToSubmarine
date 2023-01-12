using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollderStation : StationScript
{
    // Start is called before the first frame update
    List<bool> check_pressed_once = new List<bool>() { false,false,false,false};
    [SerializeField] private Rigidbody2D rigi;

    new void Start()
    {
        base.Start();
        missions.Add(getAllKeysDown);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        mission_index = 0;
        station_active = true;
        numOfPlayersIndicator[numberOfPlayers - 1].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        numOfPlayersIndicator[numberOfPlayers - 1].transform.position = transform.localPosition;
        pressKeysInARowCount = 0;
        if (rigi != null)
        {
            if (rigi.bodyType != RigidbodyType2D.Static)
            {
                rigi.bodyType = RigidbodyType2D.Static;
            }
        }

        
        if (station_active)
        {
            foreach (PlayerController player in players_in_station)
            {
                if (Input.GetKey(player.GetPlayerActionButton()))
                {
                    player.AnimationPush(Vector3.zero);
                }
                else
                {
                    player.StopAnimationPush(Vector3.zero);
                }
            }
            if (players_in_station.Count == missionsNumberOfPlayers[mission_index])
            {
                // station_animation.SetTrigger("Hover");
                missions[mission_index]();

            }
        //else
        //{
        //    PlayerAnimationIdle();
        //}
    }
        else
        {
            PlayerAnimationIdle();
        }
        
        for (int i = 0; i < players_controller_in_station.Count; i++)
        {
            if (players_controller_in_station[i].Item2 == false)
            {
                check_pressed_once[i] = false;
            }
            else
            {
                check_pressed_once[i] = true;
            }
        }
    }
    
    
    //private void PlayerAnimationPush()
    //{
    //    Vector3 loc = gameObject.transform.localPosition;
    //    foreach (PlayerController player in players_in_station)
    //    {
    //        player.AnimationPush(loc);
    //    }
    //}
    private void getAllKeysDown()
    {

        foreach (var action_key in players_action_key)
        {
            if (Input.GetKey(action_key))
            {
                pressKeysInARowCount += 1;
            }
        }
        
        int counter = 0;
        foreach (var action_key in players_controller_in_station) // For Controller
        {
            if (action_key.Item2)
            {
                pressKeysInARowCount += 1;
                timeWindowToPress = 0;
            }
            counter++;
        }
        if (pressKeysInARowCount == missionsNumberOfPlayers[mission_index])
        {
            //PlayerAnimationPush();
            pressKeysInARowCount = 0;
            rigi.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

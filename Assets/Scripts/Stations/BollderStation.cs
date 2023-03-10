using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollderStation : StationScript
{
    // Start is called before the first frame update
    List<bool> check_pressed_once = new List<bool>() { false, false, false, false };
    [SerializeField] private Rigidbody2D rigi;
    [SerializeField] private AudioSource StationSound;
    //private float soundLoop = 0f;
    private bool isPlaying = false;
    new void Start()
    {
        base.Start();
        missions.Add(getAllKeysDown);
        missionsNumberOfPlayers.Add(numberOfPlayers);
        mission_index = 0;
        station_active = true;
        stationPopup.SetActive(true);
        stationPopup.GetComponent<PopUpWithPlayersController>().withoutRedBall();
        stationPopup.transform.position = gameObject.transform.position;
        stationPopup.transform.SetParent(gameObject.transform);

        //numOfPlayersIndicator[numberOfPlayers - 1].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //numOfPlayersIndicator[numberOfPlayers - 1].transform.position = transform.localPosition;
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
                if (player.playerPressed())
                {
                    player.AnimationPush(Vector3.zero);
                }
                else
                {
                    player.StopAnimationPush(Vector3.zero);
                }
            }
            if (players_in_station.Count >= missionsNumberOfPlayers[mission_index])
            {
                // station_animation.SetTrigger("Hover");
                missions[mission_index]();

            }
            else
            {
                isPlaying = false;
                PlayerAnimationIdle();
            }
        }
        //Debug.Log(StationSound.isPlaying);

    }
    
    

    private void getAllKeysDown()
    {
        for (int i = 0; i < players_in_station.Count; i++)
        {
            if (players_in_station[i].playerPressed())
            {
                check_pressed_once[i] = true;
            }
            else
            {
                check_pressed_once[i] = false;
            }
        }
        for (int i = 0; i < players_in_station.Count; i++)
        {
            if (check_pressed_once[i])
            {
                pressKeysInARowCount += 1;
            }
        }
            //foreach (PlayerController player in players_in_station)
            //{
            //    if (player.playerPressed())
            //    {
            //        pressKeysInARowCount += 1;
            //    }
            //    else
            //    {

            //    }
            //}

        if (pressKeysInARowCount >= missionsNumberOfPlayers[mission_index])
        {
            //PlayerAnimationPush();
            //pressKeysInARowCount = 0;
            rigi.bodyType = RigidbodyType2D.Dynamic;
            if (!isPlaying)
            {
                StationSound.Play();
                isPlaying = true;
            }
        }

        else
        {
            isPlaying = false;
            StationSound.Pause();
        }
    }
}

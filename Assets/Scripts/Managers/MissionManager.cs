using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine.SceneManagement;
public class MissionManager : MonoBehaviour
{
    Random rnd = new Random( );


    [SerializeField] private OurEventHandler GM;
    
    // Text
    [SerializeField] private TMP_Text timer_text;
    [SerializeField] private TMP_Text score_text;

    // Score
    private int score = 0;
    [SerializeField] private int missionsToWinTarget = 10;
    
    // Timer
    [SerializeField] private float time_left = 30;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject Orb;
    [SerializeField] private SpriteRenderer GreenScreen;

    // Stations
    [SerializeField] private List<StationScript> stations = new List<StationScript>();
    [SerializeField] private int MaxStationsAtTime = 3;
    private int numActiveStations = 0;

    // TODO Make a weighted probability. TODO Make a list of stations with stations.
    //[SerializeField] private List<string> stationsNames = new List<string>();
    //[SerializeField] private List<string> missionsExplanation = new List<string>();

    private float initial_time;
    private bool isGameFinsihed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        refillStations();
        updateText();
        initial_time = time_left;

        //stationsNames.Add("Red");
        //missionsExplanation.Add("Click 1 time on M");
        //missionsExplanation.Add("Click 5 time on M");
    }

    private void refillStations()
    {
        foreach (StationScript station in GetComponentsInChildren<StationScript>())
        {
            stations.Add(station);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameFinsihed)
        {
            return;
        }
        if (time_left != -100f)
        {
            time_left -= Time.deltaTime;
        }
        updateText();

        if (score >= missionsToWinTarget)
        {
            Debug.Log("You Win! You Win! You Win! You Win!");
            isGameFinsihed = true;
            GM.NextlevelCanvas();
            return;
        }
        if (time_left <= 0 && time_left != -100f)
        {
            isGameFinsihed = true;

            Debug.Log("You Lose! You Lose! You Lose! You Lose!");
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndScreenLost");
        }

        rollTheDice();

        // For Gadi
        

        //
        if (Orb != null)
        {
            float scaleForOrb = (1 - (time_left / initial_time)) * 0.5f + 0.1f;
            Orb.transform.localScale = new Vector3(scaleForOrb, scaleForOrb, scaleForOrb);
        }
        
        indicator.transform.localScale = new Vector3((1 - (time_left / initial_time))*16f, indicator.transform.localScale.y);
    }

    public void missionDone(float bonus_time, int pointsWorth)
    {
        time_left += bonus_time;
        score += pointsWorth;
        numActiveStations -= 1;
    }

    public void AddTime(float bonus_time)
    {
        time_left += bonus_time;
    }

    private void updateText()
    {
        timer_text.text = "Timer: " + time_left.ToString("0.00") + " Seconds";
        score_text.text = "Score: " + score.ToString() + "/" + missionsToWinTarget.ToString();
    }

    private void rollTheDice()
    {
        if (numActiveStations < MaxStationsAtTime)
        {
            for (int j = 0; j < stations.Count; j++)
            {
                if (!stations[j].getStationActiveState() && !stations[j].hasPlayersInStation()) // if station is not active
                {
                    //Debug.Log("Station number: " + j.ToString() +" is about to rollTheDice!!");
                    int diceResult = (int)rnd.Next(250);
                    //Debug.Log("Dice Result == " + diceResult.ToString());
                    if (diceResult == 1) // Has a 1/100 chance to generate a new mission
                    {
                        numActiveStations += 1;
                        int mission_index = rnd.Next(stations[j].getMissionsCount());
                        //Debug.Log("Station number: " + j.ToString() + " Has Won its self the " +mission_index.ToString() + "th Mission!!");

                        // todo  Add here some information to the screen that can give the users info about the new mission 
                        printMissionInfo(mission_index, j);
                        //todo some stations can't have certian missions, we can add if else logic here
                        stations[j].setMissionIndex(mission_index);


                    }
                }

            }
        }
        

    }
    
    private void printMissionInfo(int mission_index, int station_index)
    {
        //Debug.Log("Go to the "+ stationsNames[station_index]  + " Station"  + " And " + missionsExplanation[mission_index]);
    }
}

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
    [SerializeField] private SpriteRenderer Noise;
    [SerializeField] private SpriteRenderer GreenVignette1;
    [SerializeField] private GameObject GreenVignette2;

    // Stations
    [SerializeField] private List<StationScript> stations = new List<StationScript>();
    [SerializeField] private int MaxStationsAtTime = 3;
    //private int numActiveStations = 0;

    // TODO Make a weighted probability. TODO Make a list of stations with stations.
    private List<List<StationScript>> stationScripts = new List<List<StationScript>>();
    //[SerializeField] private List<string> stationsNames = new List<string>();
    //[SerializeField] private List<string> missionsExplanation = new List<string>();
    private List<StationScript> stationsActive = new List<StationScript>();
    private float initial_time;
    private bool isGameFinsihed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        refillStrategies();
        refillStations();
        updateText();
        addActiveStations();
        initial_time = time_left;
    }

    private void addActiveStations()
    {
        foreach (List<StationScript> strategy in stationScripts)
        {
            foreach (StationScript station in strategy)
            {
                if (station.getStationActiveState())
                {
                    if (!stationsActive.Contains(station))
                    {
                        stationsActive.Add(station);
                    }
                }
            }
        }
    }
    private void refillStrategies()
    {
        foreach(Transform strategy in gameObject.transform)
        {
            List<StationScript> new_list = new List<StationScript>();
            if (strategy.GetComponent<StationScript>() == null)
            {
                foreach (StationScript station in strategy.GetComponentsInChildren<StationScript>())
                {
                    new_list.Add(station);
                }
            }
            if (new_list.Count > 0)
            {
                stationScripts.Add(new_list);
            }
        }

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

        //rollTheDice();
        if (stationScripts.Count != 0)
        {
            ChooseStratgies();
        }
        if (stationScripts.Count == MaxStationsAtTime)
        {
            refillStrategies();
        }
        // For Gadi
        if (Noise != null)
        {
            Noise.color = new Color(1f, 1f, 1f, (1 - (time_left / initial_time)));
            GreenVignette1.color = new Color(1f, 1f, 1f, (1 - (time_left / initial_time)));
            float x = 0.5f + (time_left / initial_time) * 0.5f;
           GreenVignette2.transform.localScale = new Vector3(x, x, x);
        }
        //
        if (Orb != null)
        {
            float scaleForOrb = (1 - (time_left / initial_time)) * 0.5f + 0.1f;
            Orb.transform.localScale = new Vector3(scaleForOrb, scaleForOrb, scaleForOrb);
        }
        if (indicator)
        {
            indicator.transform.localScale = new Vector3((1 - (time_left / initial_time)) * 16f, indicator.transform.localScale.y);
        }
    }

    public void missionDone(float bonus_time, int pointsWorth)
    {
        //printStationScript();
        StationScript station_to_remove = null;
        List<StationScript> strategy_to_remove = null;
        foreach (List<StationScript> strategy in stationScripts)
        {
            foreach (StationScript station in strategy)
            {
                if (stationsActive.Contains(station) && station.getStationActiveState() == false)
                {
                    stationsActive.Remove(station);
                    station_to_remove = station;
                    strategy_to_remove = strategy;
                }
            }
            if (station_to_remove != null)
            {
                strategy.Remove(station_to_remove);
                station_to_remove = null;
            }
        }
        if (strategy_to_remove.Count == 0)
        {
            stationScripts.Remove(strategy_to_remove);
        }
        else
        {
            ActivateStation(strategy_to_remove);
        }
        time_left += bonus_time;
        score += pointsWorth;
        //printStationScript();
        //Debug.Log(stationScripts.Count);
    }

    private void printStationScript()
    {
        Debug.Log("printing");
        foreach(List<StationScript> stationL in stationScripts)
        {
            foreach(StationScript station in stationL)
            {
                Debug.Log(station);
            }
        }
    }
    private void ActivateStation(List<StationScript> strategy)
    {
        strategy[0].setMissionIndex(0);
        stationsActive.Add(strategy[0]); // Station Active
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

    private void ChooseStratgies()
    {
        if (stationsActive.Count < MaxStationsAtTime)
        //while (stationsActive.Count < MaxStationsAtTime)
            {
                int diceResult = (int)rnd.Next(stationScripts.Count);
            if (stationScripts[diceResult].Count > 0)
            {
                ActivateStation(stationScripts[diceResult]);
            }
        }
    }

    private void rollTheDice()
    {
        if (stationsActive.Count < MaxStationsAtTime)
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
                        int mission_index = rnd.Next(stations[j].getMissionsCount());
                        //Debug.Log("Station number: " + j.ToString() + " Has Won its self the " +mission_index.ToString() + "th Mission!!");

                        // todo  Add here some information to the screen that can give the users info about the new mission 
                        printMissionInfo(mission_index, j);
                        //todo some stations can't have certian missions, we can add if else logic here
                        stations[j].setMissionIndex(mission_index);
                        stationsActive.Add(stations[j]); // Station Active
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

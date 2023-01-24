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
    [SerializeField] private GameObject TimeIndicator;
    private TimeIndicatorScript timeIndicatorScript;
    [SerializeField] private List<GameObject> Orbs = new List<GameObject>();
    [SerializeField] private GameObject ScoreIndicatorBottom;
    [SerializeField] private SpriteRenderer Noise;
    [SerializeField] private SpriteRenderer GreenVignette1;
    [SerializeField] private GameObject GreenVignette2;

    // Stations
    [SerializeField] private List<StationScript> stations = new List<StationScript>();
    [SerializeField] private int MaxStrategiesAtTime = 3;
    [SerializeField] private bool ReadAsBatch = false;
    [SerializeField] private bool refillStrategy = false;
    // A Random directory
    //[SerializeField] private bool Manual;
    //private int numActiveStations = 0;

    // TODO Make a weighted probability. TODO Make a list of stations with stations.
    private List<List<StationScript>> stationScripts = new List<List<StationScript>>();
    private List<List<StationScript>> stationScriptsReadOnly = new List<List<StationScript>>();

    //[SerializeField] private List<string> stationsNames = new List<string>();
    //[SerializeField] private List<string> missionsExplanation = new List<string>();
    private List<StationScript> stationsActive = new List<StationScript>();
    private float initial_time;
    private List<float> OrbsInitialScale = new List<float>();
    private bool isGameFinsihed = false;
    private int strategiessActive = 0;

    private GameObject ScoreIndicatorPrefab;

    private CameraZoom zoom;
    private bool hold_time = false;
    private int hold_time_count = 3;
    private float hold_time_counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeIndicatorScript = TimeIndicator.GetComponent<TimeIndicatorScript>();
        zoom = FindObjectOfType<CameraZoom>();
        ScoreIndicatorPrefab = Resources.Load("ScoreIndicator") as GameObject;
        //ScoreIndicatorPrefab = ScoreIndicatorPrefabhelper.GetComponent<ScoreIndicator>();
        refillStrategiesReadOnly();
        refillStrategies();
        refillStations();
        updateText();
        addActiveStations();
        initial_time = time_left;
        if (Orbs.Count !=0)
        {
            foreach(GameObject orb in Orbs)
            {
                orb.GetComponent<SpriteRenderer>().sortingOrder = 19;
                OrbsInitialScale.Add(orb.transform.localScale.x);
            }
        }
    }
    private void refillStrategiesReadOnly()
    {
        foreach (Transform strategy in gameObject.transform)
        {
            List<StationScript> new_list = new List<StationScript>();
            if (strategy.GetComponent<StationScript>() == null)
            {
                foreach (StationScript station in strategy.GetComponentsInChildren<StationScript>())
                {
                    new_list.Add(station);
                }
            }
            if (new_list.Count > 0 && !stationScripts.Contains(new_list))
            {
                stationScriptsReadOnly.Add(new_list);
            }
        }
        stationScriptsReadOnly.Remove(stationScriptsReadOnly[stationScriptsReadOnly.Count - 1]);
    }
    private void addActiveStations()
    {
        foreach (List<StationScript> strategy in stationScripts)
        {
            foreach (StationScript station in strategy)
            {
                if (station.getStationActiveState())
                {
                    if ((!stationsActive.Contains(station) && !station.isAlwaysActive()))
                    {
                        if (ReadAsBatch)
                        {
                            ActivateBatch();
                        }
                        else
                        {
                            //ActivateStation(station);
                            stationsActive.Add(station);
                            station.activatePopup();
                        }
                    }
                }
            }
        }
    }

    private bool checkIfstrategyInGame(List<StationScript> strategies)
    {
        foreach(List<StationScript> strategies2 in stationScripts)
        {
            foreach(StationScript strategy in strategies2)
            {
                if (strategies.Contains(strategy))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void refillStrategies()
    {
        foreach(List<StationScript> strategies in stationScriptsReadOnly)
        {
            List<StationScript> new_list = new List<StationScript>();
            if (!checkIfstrategyInGame(strategies))
            {
                foreach (StationScript station in strategies)
                {
                    new_list.Add(station);
                }
                if (new_list.Count > 0 && !stationScripts.Contains(new_list))
                {
                    stationScripts.Add(new_list);
                }
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

    private bool DoFinishAnimation()
    {
        if (zoom)
        {
            zoom.ActivateZoom(Orbs[0].transform.localPosition);
        }
        time_left = Mathf.Lerp(time_left, initial_time, Time.deltaTime);
        return time_left >= (initial_time - 2);
    }


    private void updateIndicator()
    {
        if (time_left > initial_time)
        {
            initial_time = time_left;
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
        if (Orbs.Count != 0)
        {
            for (int i = 0; i < Orbs.Count; i++)
            {
                float scaleForOrb = (1 - (time_left / initial_time)) * 0.5f + OrbsInitialScale[i];
                Orbs[i].transform.localScale = new Vector3(scaleForOrb, scaleForOrb, scaleForOrb);

            }
        }
        if (TimeIndicator != null)
        {
            timeIndicatorScript.updateTime(time_left, initial_time);
            //TimeIndicator.transform.localScale = new Vector3((1 - (time_left / initial_time)) * 7.4f, TimeIndicator.transform.localScale.y);
        }
        if (ScoreIndicatorBottom != null)
        {
            ScoreIndicatorBottom.transform.localScale = new Vector3((((float)score / (float)missionsToWinTarget)) * 7.4f, ScoreIndicatorBottom.transform.localScale.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameFinsihed)
        {
            if (hold_time)
            {
                hold_time_counter += Time.deltaTime;
                if (hold_time_count <= hold_time_counter)
                {
                    hold_time_counter = 0;
                    Time.timeScale = 1f;
                    hold_time = false;

                    GM.NextlevelCanvas();
                }
            }
            return;
        }
        if (time_left != -100f)
        {
            time_left -= Time.deltaTime;
        }
        updateText();
        updateIndicator();
        if (score >= missionsToWinTarget)
        {
            //Debug.Log("You Win! You Win! You Win! You Win!");
            if (DoFinishAnimation())
            {
                if (zoom)
                {
                    zoom.DeActivateZoom();
                }
                else
                {
                    hold_time = true;
                }
                isGameFinsihed = true;
                if (!hold_time)
                {
                    Time.timeScale = 0f;
                    GM.NextlevelCanvas();
                }
            }
            return;
        }
        if (time_left <= 0 && time_left != -100f)
        {
            isGameFinsihed = true;

            //Debug.Log("You Lose! You Lose! You Lose! You Lose!");
            GM.GameOver(Orbs);
        }


        if (stationScripts.Count == MaxStrategiesAtTime && refillStrategy)
        {
            refillStrategies();
        }
        if (stationScripts.Count != 0 )
        {
            ChooseStratgies();
        }
        
        
    }


    private void MakeScoreIndicator(Vector3 stationPos)
    {
        if (ScoreIndicatorBottom != null)
        {
            //Debug.Log("HERE");
            //GameObject scoreobj = Instantiate(ScoreIndicatorPrefab);
            ScoreIndicator scoreind = Instantiate(ScoreIndicatorPrefab).GetComponent<ScoreIndicator>();
            scoreind.transform.position = stationPos;
            scoreind.orb_loc = ScoreIndicatorBottom.transform.localPosition;
        }
        
    }

    public IEnumerator MakeScoreIndicatorEnumerator(Vector3 stationPos, float xSeconds)
    {

        if (xSeconds == 0)
        {
            MakeScoreIndicator(stationPos);
            yield break;
        }
        yield return new WaitForSeconds(xSeconds);
        MakeScoreIndicator(stationPos);

    }

    public void missionDone(float bonus_time, int pointsWorth)
    {
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
                    if (pointsWorth > 0 || bonus_time > 0)
                    {
                        for (int i = 0; i < pointsWorth; i++)
                        {
                            float t = ((float) i) * 0.25f;
                            StartCoroutine(MakeScoreIndicatorEnumerator(station_to_remove.transform.localPosition, t));
                        }
                    }
                }
            }
                if (station_to_remove != null)
                {
                    strategy.Remove(station_to_remove);
                    station_to_remove = null;
                }
            
        }
        if (strategy_to_remove != null && strategy_to_remove.Count == 0)
        {
            if (ReadAsBatch)
            {
                strategiessActive -= 1;
            }
            //else if(refillStrategy)
            //{
            //    foreach(List<StationScript> stationScripts)
            //}
            stationScripts.Remove(strategy_to_remove);

        }
        else if (!ReadAsBatch)
        {
            ActivateStation(strategy_to_remove[0]);
        }
        time_left += bonus_time;
        score += pointsWorth;
            //Debug.Log(strategiessActive);
            //Debug.Log(stationScripts.Count);
            //Debug.Log(stationScripts[0].Count);

            //printStationScript();
        
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
    private void ActivateStation(StationScript strategy)
    {
        if (!strategy.getStationActiveState())
        {
            strategy.setMissionIndex(0);
            stationsActive.Add(strategy); // Station Active

        }
    }
    public void AddTime(float bonus_time)
    {
        time_left += bonus_time;
    }

    private void updateText()
    {

        timer_text.text = time_left.ToString("00.0");
        score_text.text = "Score: " + score.ToString() + "/" + missionsToWinTarget.ToString();
    }

    private void ActivateBatch()
    {
        int temp = strategiessActive;
        for (int i = temp; i < MaxStrategiesAtTime; i++)
        {
            foreach (StationScript station in stationScripts[i])
            {
                ActivateStation(station);
            }
            strategiessActive += 1;
        }
    }

    private bool checkIfStationsActive(List<StationScript> lst)
    {
        foreach( StationScript station in lst)
        {
            if (station.getStationActiveState())
            {
                return false;
            }
        }
        return true;
    }

    private void ChooseStratgies()
    {
        if (ReadAsBatch)
        {
            while (strategiessActive < MaxStrategiesAtTime && MaxStrategiesAtTime <= stationScripts.Count)
            {
                ActivateBatch();
            }
        }
        else if (stationsActive.Count < MaxStrategiesAtTime)
        {
            if (stationScripts.Count < MaxStrategiesAtTime)
            {
                refillStations();
            }
            int diceResult = (int)rnd.Next(stationScripts.Count);
            if (stationScripts[diceResult].Count > 0 && checkIfStationsActive(stationScripts[diceResult]))
            {
                ActivateStation(stationScripts[diceResult][0]);
            }
        }
    }

    private void rollTheDice()
    {
        if (stationsActive.Count < MaxStrategiesAtTime)
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

    private void printDebugStationsActive()
    {
    }
}

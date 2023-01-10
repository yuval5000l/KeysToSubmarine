using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStation : StationScript
{
    [SerializeField] private GameObject tut;
    private int framecounter = 0;

    // Start is called before the first frame update
    new void Start()
    {
        station_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (framecounter == 1)
        {
            missionManager.missionDone(0, points_award);
            gameObject.SetActive(false);
        }
    }
    public override void setMissionIndex(int i) 
    {
        tut.SetActive(true);
        framecounter = 1;
    }
}

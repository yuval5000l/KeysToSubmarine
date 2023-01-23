using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    List<Transform> AllMaSons = new List<Transform>();
    List<Animator> MaSparks = new List<Animator>();
    
    private float num_seconds = 2f;
    private float timer = 0f;
    //private float mini_timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        bool skip_first = false;
        foreach (Transform obj in GetComponentsInChildren<Transform>())
        {
            if (skip_first)
            {
                if (obj.gameObject.GetComponent<Animator>() != null)
                {
                    MaSparks.Add(obj.GetComponent<Animator>());
                }
                else
                {
                    AllMaSons.Add(obj);
                }
            }
            else
            {
                skip_first = true;
            }
        }
        // 0 top left
        // 1-16 up
        // 17 top right
        // 18-24 left
        // 25 left down
        // 26-41 down
        // 42 right down
        // 43-49 right
        //int i = 0;
        //foreach (Transform obj in AllMaSons)
        //{
        //    Debug.Log(obj +" "+ i);
        //    i++;
        //}
        //Debug.Log("YO");
        //foreach (Animator obj in MaSparks)
        //{
        //    Debug.Log(obj);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= num_seconds)
        {
            int num_sparks = Random.Range(1, 4);
            for (int i = 0; i < num_sparks; i++)
            {
                int location = Random.Range(0, 50);
                setSpark(location, i);
            }
            timer = 0;
        }
    }

    private void setSpark(int loc, int spark)
    {
        MaSparks[spark].transform.localPosition = AllMaSons[loc].localPosition;
        int kind_sparks = Random.Range(1, 12);

        MaSparks[spark].Play("Spark" + kind_sparks);
        //Debug.Log("SETSPARK");
    }
}

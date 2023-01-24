using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIndicatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool changeToRed = false;
    private float flicker = -1f;
    private float timer = 0f;
    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (flicker > 0)
        {
            timer += Time.deltaTime;
            if (timer > flicker)
            {
                timer = 0f;
                if (changeToRed)
                {
                    sprite.color = Color.green;
                    changeToRed = false;

                }
                else
                {
                    sprite.color = Color.red;
                    changeToRed = true;

                }

            }
        }
    }
    public void updateTime(float time_left, float initial_time)
    {

        transform.localScale = new Vector3((1 - (time_left / initial_time)) * 7.4f, transform.localScale.y);

        if (time_left <= 7.5f)
        {
            flicker = 0.5f;
        }
        else if (time_left <= 15)
        {
            flicker = 1f;
        }
        else if (time_left <= 25)
        {
            flicker = 2f;
        }
    }
}

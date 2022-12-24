using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timer_text;
    [SerializeField] private float time_left = 30;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        updateText();
        if (time_left <= 0)
        {
            Debug.Log("You Lose!");
            Time.timeScale = 0f;
        }
    }

    public void AddTime(float bonus_time)
    {
        time_left += bonus_time;
    }

    private void updateText()
    {
        timer_text.text = "Timer: " + time_left.ToString("0.00") + " Seconds";
    }
}

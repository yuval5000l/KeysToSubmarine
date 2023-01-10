using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GenericEvent : MonoBehaviour
{

    private int counter = 0;
    private int NumOfClicks = 10;
    [SerializeField] private TMP_Text counter_text;
    [SerializeField] private bool pause_permanent = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        counter_text.text = (NumOfClicks - counter).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            counter++;
            counter_text.text = (NumOfClicks - counter).ToString();
            if (counter == NumOfClicks)
            {
                if (!pause_permanent)
                {
                    Time.timeScale = 1f;
                }

                gameObject.SetActive(false);
            }

        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}

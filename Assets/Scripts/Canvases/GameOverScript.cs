using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    //[SerializeField] private MaEventHandler GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgain()
    {
        //GM.replay();
        Time.timeScale = 1f;
        PauseMenu.isGamePaused = false;
        GameOverScreen.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

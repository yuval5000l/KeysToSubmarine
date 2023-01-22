using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    //[SerializeField] private GameObject GameOverScreen;
    [SerializeField] private OurEventHandler GM;
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgain()
    {
        GM.restartlevel();
    }
    public void levelselect()
    {
        SceneManager.LoadScene("ChooselevelsScreen");

    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

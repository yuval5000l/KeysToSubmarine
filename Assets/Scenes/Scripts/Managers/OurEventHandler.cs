using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OurEventHandler : MonoBehaviour
{
    private static int NumberOfPlayers = 2;
    [SerializeField] private PlayerManager playerManager;
    private int levelNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string[] nameAndNum = sceneName.Split(" ");
        if (nameAndNum[0] == "level")
        {
            levelNumber = int.Parse(nameAndNum[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlayer()
    {
        if (NumberOfPlayers < 4)
        {
            NumberOfPlayers += 1;
        }
    }
    public void RemovePlayer()
    {
        if (NumberOfPlayers > 2)
        {
            NumberOfPlayers -= 1;
        }
    }

    public void Setlevel(int numlevel)
    {
        levelNumber = numlevel;
    }

    public void Nextlevel(int numlevel = 0)
    {
        levelNumber += numlevel + 1;
        SceneManager.LoadScene("level " + levelNumber);
        Time.timeScale = 1f;
    }

    public int getNumOfPlayers()
    {
        return NumberOfPlayers;
    }
    public void restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("level " + levelNumber);
        Time.timeScale = 1f;
    }
}

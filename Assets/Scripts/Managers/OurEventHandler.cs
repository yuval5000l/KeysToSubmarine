using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OurEventHandler : MonoBehaviour
{
    [SerializeField] private static int NumberOfPlayers = 3;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private NextLevelCanva nextlevel;
    private List<PlayerController> players = new List<PlayerController>();
    private int players_in_game = 0;
    private static int levelNumber = 1;
    [SerializeField] private int MaxlevelNum = 3;
    private CameraZoom zoom;
    private bool game_over = false;
    private GameObject orb;
    private float counter = 2;
    // Start is called before the first frame update
    void Start()
    {
        zoom = FindObjectOfType<CameraZoom>();
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
        if (game_over)
        {
            counter -= Time.deltaTime;
            if (orb)
            {
                float x = orb.transform.localScale.x + orb.transform.localScale.x * 2 * Time.deltaTime;
                orb.transform.localScale = new Vector3(x, x, x);
            }
            if (counter <= 0)
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene("EndScreenLost");
            }
        }
    }
    public void AddPlayer(PlayerController player)
    {
        if (players.Count < 3)
        {
            players.Add(player);
        }
    }
    public void RemovePlayer(PlayerController player)
    {
        players.Remove(player);
    }
    public int getPlayersInGame()
    {
        return players_in_game;
    }
    public void setPlayersInGame(int num)
    {
        players_in_game+= num;
    }
    public void Setlevel(int numlevel)
    {
        levelNumber = numlevel;
    }

    public void Nextlevel(int numlevel = 0)
    {
        levelNumber += numlevel + 1;
        if (levelNumber >= MaxlevelNum)
        {
            SceneManager.LoadScene("EndScreenWon");
        }
        else
        {
            SceneManager.LoadScene("level " + levelNumber);
        }
        Time.timeScale = 1f;
    }

    public void NextlevelCanvas()
    {
        Time.timeScale = 0f;
        nextlevel.gameObject.SetActive(true);
    }
    public int getNumOfPlayers()
    {
        return NumberOfPlayers;
    }
    public void restartlevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("level " + levelNumber);
        Time.timeScale = 1f;
    }

    public void GameOver(GameObject Orb)
    {
        orb = Orb;
        game_over = true;
    }
}

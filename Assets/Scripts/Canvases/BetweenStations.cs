using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BetweenStations : MonoBehaviour
{
    [SerializeField] private BorderScript upperBorder;
    [SerializeField] private BorderScript lowerBorder;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private Button button;
    //private bool slideOut = false;
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        button.enabled = false;
        button.image.color = new Color(1f, 1f, 1f, 0.2f);
        Time.timeScale = 0.000001f;
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
        StartCoroutine(PauseWaitResume(2f));
    }
    private IEnumerator PauseWaitResume(float pauseDelay)
    {
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(pauseDelay * Time.timeScale);
        //yield WaitForSeconds(pauseDelay* Time.timeScale);
        //Time.timeScale = 1f;
        ButtonActive();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");

    }

    public void SlideOut()
    {
        upperBorder.SlideOut();
        lowerBorder.SlideOut();
        gameMusic.Play();
        menuMusic.Stop();
    }
    public void ButtonActive()
    {
        button.enabled = true;
        button.image.color = new Color(1f, 1f, 1f, 1f);
    }

}

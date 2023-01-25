using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource music;
    public static MenuMusic instance; 
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            music.Play();
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        if(instance != this && instance != null)
        {
            Destroy(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Destroy(this.gameObject);

#endif
    }

    void OnEnable() {
        OurEventHandler.TutorialEnded.AddListener(destroyMusic);
    }

    void OnDisable() {
        OurEventHandler.TutorialEnded.RemoveListener(destroyMusic);
    }

    public void destroyMusic()
    {
        Destroy(gameObject);
    }
}

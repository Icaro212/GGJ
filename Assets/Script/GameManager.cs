using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool stateColor = false;

    [HideInInspector]
    public GameObject[] colorList;
    [HideInInspector]
    public GameObject[] noColorList;

    public GameObject[] fairyList;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        noColorList = GameObject.FindGameObjectsWithTag("noColor");

        colorList = GameObject.FindGameObjectsWithTag("color");

        fairyList = GameObject.FindGameObjectsWithTag("Fairy");

        foreach (var b in colorList)
        {
            b.SetActive(false);
        }
    }


    public void ChangeScene(string sc)
    {
        if (sc == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.instance.Awake();
        }
        else 
        {
            if (sc != "Exit")
            {
                SceneManager.LoadScene(sc);
            }
            else
            {
                Application.Quit();
            };
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


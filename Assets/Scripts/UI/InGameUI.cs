
//using ElephantSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InGameUI : MonoBehaviour
{


    [Header("LEVEL COMPONENTS")]

    public List<Image> Stages;
    public Color[] StageColors;
    public int levelCountOfGame;

    public GameObject levelIndicator;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nextlevelText;

    int currentState;
    [Header("CANVAS GAMEOBJECTS")]



    public GameObject tapToStartPanel;




    [Header("LEVEL COMPLETE COMPONENTS")]
    public GameObject winCam;
    public GameObject levelCompletePanel;


    [Header("LEVEL FAIL COMPONENTS")]
    public GameObject levelFailPanel;


    [HideInInspector] public bool levelFinished;
    [HideInInspector] public bool levelStarted;


    LevelGenerator level_Generator;
    PlayerBehaviour player_Behaviour;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 1;
        Time.fixedDeltaTime = .01f * Time.timeScale;

        //if (SaveLoadManager.GetLastPlayedLevel() != SceneManager.GetActiveScene().buildIndex)
        //{
        //    SceneManager.LoadScene(SaveLoadManager.GetLastPlayedLevel());
        //}
        level_Generator = FindObjectOfType<LevelGenerator>();
        player_Behaviour = FindObjectOfType<PlayerBehaviour>();
        levelText.text = SaveLoadManager.GetFakeLevel().ToString();
        nextlevelText.text = (SaveLoadManager.GetFakeLevel() + 1).ToString();
        level_Generator.GenerateLevelOnPlaying(2);
        RescaleCanvas();

    }

    void RescaleCanvas()
    {
        if ((float)Screen.height / (float)Screen.width < 1.6f)
            GetComponent<CanvasScaler>().referenceResolution = new Vector2(4100, 1440); //2048 screen
        else if ((float)Screen.height / (float)Screen.width < 1.95f)
            GetComponent<CanvasScaler>().referenceResolution = new Vector2(3000, 1440); //1080 screen
        else
            GetComponent<CanvasScaler>().referenceResolution = new Vector2(2560, 1440); //1242 screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            levelCompletePanel.SetActive(false);
            StartCoroutine(LevelComplete());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            levelFailPanel.SetActive(false);
            StartCoroutine(LevelFail());
        }

    }

    //  START - PAUSE - RESUME   **********************************************************************************
    public void StartLevel()
    {
        levelFinished = false;
        levelStarted = true;
        tapToStartPanel.SetActive(false);
        ResetStageUI();
        player_Behaviour.LevelStart();
        //ObjectPooler.Instance.GenerateAllPool();
    }

    public void PauseGame()
    {


        Time.timeScale = 0;
        Time.fixedDeltaTime = .01f * Time.timeScale;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = .01f * Time.timeScale;

    }



    //  LOAD SCENE & RESTART   **********************************************************************************
    bool isLoading;
    public void LoadNextLevel()
    {
     
        level_Generator.DecraseSpawnedCount();
        SaveLoadManager.IncreaseFakeLevel();
        levelText.text = SaveLoadManager.GetFakeLevel().ToString();
        nextlevelText.text = (SaveLoadManager.GetFakeLevel() + 1).ToString();
        level_Generator.GenerateLevelOnPlaying(1);
        levelIndicator.SetActive(true);
        levelCompletePanel.SetActive(false);
        StartLevel();
        //if (!isLoading)
        //    StartCoroutine(Load());
    }
    //IEnumerator Load()
    //{
    //    isLoading = true;
    //    Time.timeScale = 1;
    //    Time.fixedDeltaTime = .01f * Time.timeScale;

    //    SaveLoadManager.IncreaseFakeLevel();
    //    yield return new WaitForSecondsRealtime(.5f);
    //    if (SceneManager.GetActiveScene().buildIndex == levelCountOfGame)
    //    {
    //        SaveLoadManager.SetLastPlayedLevel(1);
    //        SceneManager.LoadScene(1);
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //        SaveLoadManager.SetLastPlayedLevel(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}

    public void RestartLevel()
    {
        if (!isLoading)
            StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        isLoading = true;

        Time.timeScale = 1;
        Time.fixedDeltaTime = .01f * Time.timeScale;

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //  LEVEL COMPLETE & FAIL   **********************************************************************************
    public IEnumerator LevelComplete()
    {


        levelFinished = true;
        yield return new WaitForSeconds(0.2f);
        DisableUIObjs();

        levelCompletePanel.SetActive(true);

    }


    public IEnumerator LevelFail()
    {
        levelFinished = true;
        yield return new WaitForSeconds(0.2f);
        DisableUIObjs();
        levelFailPanel.SetActive(true);
    }

    void DisableUIObjs()
    {
        levelIndicator.SetActive(false);

    }

    public void StageComplete()
    {
        if (currentState < 3)
        {
            Stages[currentState].color = StageColors[1];
            currentState++;
        }

    }
    void ResetStageUI()
    {
        for (int i = 0; i < Stages.Count; i++)
        {
            Stages[i].color = StageColors[0];
        }
    }



}
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Day1Control : MonoBehaviour
{

    public static Action PreSceneChange;
    public static Action NewSceneLoaded;

    /// <summary>
    /// DEV NOTE: 
    ///     SCENE STRING NAMES --- index in build settings:
    ///         "PlayerHouse" --- 0
    ///         "CampusGrounds" --- 1
    ///         
    /// </summary>

    public Collider2D playerCollider;
    public Collider2D interactingCollider;

    private GameObject player;
    private bool objectivesFinished;

    private string nextSceneString;
    private bool disableCode;

    public static Day1Control instance;
    public string playerName;

    private string comparisonString;


    /// <summary>
    /// Need to create a method to reset variables upon 'replay'
    /// </summary>
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        disableCode = false;
    }

    private void OnEnable()
    {
        DialogueManagerOriginal.DialogueScriptFinished += ClassEnded; 
        NewSceneLoaded += ResetBool;
        NewSceneLoaded += InitialisePlayer;
    }

    private void OnDisable()
    {
        DialogueManagerOriginal.DialogueScriptFinished -= ClassEnded;
        NewSceneLoaded -= ResetBool;
        NewSceneLoaded -= InitialisePlayer;
    }

    private void Start()
    {
        player = GameObject.Find("PlayerObj");
        playerCollider = player.GetComponent<Collider2D>();
        comparisonString = ContinuousData.instance.currentSceneName;
        Debug.Log("STRING VALUE: "+comparisonString);
        switch (comparisonString)
        {
            case "PlayerHouse":
                interactingCollider = GameObject.Find("ExitCollider1").GetComponent<Collider2D>();
                Debug.Log(" 1 ASSIGNED");
                break;
            case "CampusGrounds":
                interactingCollider = GameObject.Find("ReturnCollider1").GetComponent<Collider2D>();
                Debug.Log(" 2 ASSIGNED");
                break;
            case "Midday":
                interactingCollider = GameObject.Find("debuggingCollider").GetComponent<Collider2D>();
                Debug.Log(" 3 ASSIGNED");
                break;
            default:
                Debug.Log("NONE ASSIGNED ");
                break;
        }
        
    }
    void Update()
    {
        if (ContinuousData.instance.currentSceneName == "PlayerHouse")
        {
            interactingCollider = GameObject.Find("ExitCollider1").GetComponent<Collider2D>();
        }
        if (ContinuousData.instance.currentSceneName == "Midday")
        {
            interactingCollider = GameObject.Find("debuggingCollider").GetComponent<Collider2D>();
        }
        if (ContinuousData.instance.currentSceneName == "CampusGrounds")
        {
            interactingCollider = GameObject.Find("ExitCollider1").GetComponent<Collider2D>();
        }

        //Variable check updates
        objectivesFinished = ObjectiveSkip.objectivesSkipped;
        if (Physics2D.IsTouching(interactingCollider, playerCollider)) //Leaving house in morning - TIMEFRAME UPDATE: Morning to Midday
                                                                //SCENE UPDATE: PlayerHouse to CampusGrounds
        {
            if(!disableCode)
            {
                disableCode= true;
                TimeManager.OnTimeFrameChanged?.Invoke();
                PreSceneChange?.Invoke();
                nextSceneString = "Midday";
                StartCoroutine(SceneLoad());
            }
            
        }
       
        if (Physics2D.IsTouching(interactingCollider, playerCollider)) //If the player is trying to cross the bridge in the uni campus scene and has completed all objectives necessary to do so.
                                                                                        //(check if current scene is "CampusGrounds"), find this collider by name to allow script to run in all Day 1 scenes without reference error
        {
            TimeManager.OnTimeFrameChanged?.Invoke();
            PreSceneChange?.Invoke();
            nextSceneString = "PlayerHouse";
            StartCoroutine(SceneLoad());
        }

    }

    public void ClassEnded()
    {
        TimeManager.OnTimeFrameChanged?.Invoke();
        PreSceneChange?.Invoke();
        nextSceneString = "CampusGrounds";
        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        //Debug.Log("Loading delay... - 1.5s");
        yield return new WaitForSeconds(0f);

        SceneManager.LoadScene(nextSceneString);
        NewSceneLoaded?.Invoke();
        

    }

    public void ResetBool()
    {
        disableCode= false;
    }

    private void InitialisePlayer()
    {
        if (ContinuousData.instance.currentSceneName == "CampusGrounds")
        {
            player.transform.position = new Vector3(40.8f, -12.7f, 0);
        }
    }


}

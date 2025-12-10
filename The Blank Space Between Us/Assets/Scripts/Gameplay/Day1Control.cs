using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Day1Control : MonoBehaviour
{

    public static Action PreSceneChange;
    public static Action NewSceneLoaded;
    public static Action ObjUpdate1;
    public static Action ObjUpdate2;

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
        NewSceneLoaded += ObjectiveLoad;
    }

    private void OnDisable()
    {
        DialogueManagerOriginal.DialogueScriptFinished -= ClassEnded;
        NewSceneLoaded -= ResetBool;
        NewSceneLoaded -= InitialisePlayer;
        NewSceneLoaded -= ObjectiveLoad;
    }

    private void Start()
    {
        player = GameObject.Find("PlayerObj");
        playerCollider = player.GetComponent<Collider2D>();
        
        
    }
    void Update()
    {
        if (ContinuousData.instance.interactionsHad == 5)
        {
            ObjUpdate2?.Invoke();
        }
        player = GameObject.Find("PlayerObj");
        playerCollider = player.GetComponent<Collider2D>();
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
            interactingCollider = GameObject.Find("ReturnCollider1").GetComponent<Collider2D>();
        }

        //Variable check updates
        objectivesFinished = ObjectiveSkip.objectivesSkipped;
        if (Physics2D.IsTouching(interactingCollider, playerCollider)) //Leaving house in morning - TIMEFRAME UPDATE: Morning to Midday
                                                                //SCENE UPDATE: PlayerHouse to CampusGrounds
        {
            if(!disableCode)
            {
                if (ContinuousData.instance.currentSceneName == "PlayerHouse")
                {
                    disableCode = true;
                    PreSceneChange?.Invoke();
                    nextSceneString = "Midday";
                    StartCoroutine(SceneLoad());
                }
                if (ContinuousData.instance.currentSceneName == "Midday")
                {
                    Debug.Log("Player shouldn't be here");

                }
                if (ContinuousData.instance.currentSceneName == "CampusGrounds" && ContinuousData.instance.interactionsHad >= 5)
                {
                    disableCode = true;
                    PreSceneChange?.Invoke();
                    nextSceneString = "PlayerHouse";
                    StartCoroutine(SceneLoad());
                }
            }
            
        }

    }

    public void ClassEnded()
    {
        
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
    private void ObjectiveLoad()
    {
        
        if (ContinuousData.instance.currentSceneName == "CampusGrounds")
        {
            ObjUpdate1?.Invoke();
        }
        



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

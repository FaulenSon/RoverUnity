using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;     
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;              
    public CameraControl m_CameraControl;      
    public Text m_MessageText;                 
    public GameObject m_RoverPrefab;    
    public RoverManager[] m_Rovers;      

    private int m_RoundNumber;                  
    private WaitForSeconds m_StartWait;         
    private WaitForSeconds m_EndWait;            
    private RoverManager m_RoundWinner;         
    private RoverManager m_GameWinner;           

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);    
        m_EndWait = new WaitForSeconds(m_EndDelay);    

        SpawnAllRovers();      
        SetCameraTargets();    

        StartCoroutine(GameLoop());     
    }


    private void SpawnAllRovers()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)
        {
               
            m_Rovers[i].m_Instance = Instantiate(m_RoverPrefab, m_Rovers[i].m_Spawn.position, m_Rovers[i].m_Spawn.rotation) as GameObject;
            m_Rovers[i].m_PlayerNumber = i + 1;    
            m_Rovers[i].Setup();     
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Rovers.Length];    

        for (int i = 0; i < targets.Length; i++)    
        {
            targets[i] = m_Rovers[i].m_Instance.transform;    
        }

        m_CameraControl.m_Targets = targets;    
    }


    private IEnumerator GameLoop()    
    {
        yield return StartCoroutine(RoundStarting());    
        yield return StartCoroutine(RoundPlaying());     
        yield return StartCoroutine(RoundEnding());        

        if (m_GameWinner != null)    
        {
            Application.LoadLevel(Application.loadedLevel);    
        }
        else
        {
            StartCoroutine(GameLoop());    
        }
    }


    private IEnumerator RoundStarting()    
    {
        ResetAllRover();    
        DisableRoverControl();    
        m_CameraControl.SetStartPositionAndSize();    
        m_RoundNumber++;    
        m_MessageText.text = "РАУНД " + m_RoundNumber;    
        yield return m_StartWait;    
    }


    private IEnumerator RoundPlaying()    
    {
        EnableRoverControl();    
        m_MessageText.text = string.Empty;    

        while (!OneRoverLeft())    
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()    
    {
        DisableRoverControl();    
        m_RoundWinner = null;    
        m_RoundWinner = GetRoundWinner();    
        if (m_RoundWinner != null)     
            m_RoundWinner.m_Wins++;    
        m_GameWinner = GetGameWinner();    
        string message = EndMessage();    
        m_MessageText.text = message;
        yield return m_EndWait;
    }


    private bool OneRoverLeft()    
    {
        int numRoversLeft = 0;    

        for (int i = 0; i < m_Rovers.Length; i++)    
        {
            if (m_Rovers[i].m_Instance.activeSelf)    
                numRoversLeft++;    
        }

        return numRoversLeft <= 1;
    }

    private RoverManager GetRoundWinner()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)    
        {
            if (m_Rovers[i].m_Instance.activeSelf)    
                return m_Rovers[i];    
        }

        return null;    
    }


    private RoverManager GetGameWinner()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)    
        {
            if (m_Rovers[i].m_Wins == m_NumRoundsToWin)    
                return m_Rovers[i];    
        }

        return null;
    }


    private string EndMessage()    
    {
        string message = "НИЧЬЯ!";    

        if (m_RoundWinner != null)    
            message = m_RoundWinner.m_ColoredPlayerText + " ВЫИГРЫВАЕТ РАУНД!";    

        message += "\n\n\n\n";    

        for (int i = 0; i < m_Rovers.Length; i++)    
        {
            message += m_Rovers[i].m_ColoredPlayerText + ": " + m_Rovers[i].m_Wins + " ПОБЕД\n";    
        }

        if (m_GameWinner != null)    
            message = m_GameWinner.m_ColoredPlayerText + " ВЫИГРЫВАЕТ ИГРУ!";    

        return message;
    }


    private void ResetAllRover()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)
        {
            m_Rovers[i].Reset();
        }
    }


    private void EnableRoverControl()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)
        {
            m_Rovers[i].EnableControl();
        }
    }


    private void DisableRoverControl()    
    {
        for (int i = 0; i < m_Rovers.Length; i++)
        {
            m_Rovers[i].DisableControl();
        }
    }
}
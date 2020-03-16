using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text m_MessageText;
    public Text m_TimerText;
    public GameObject[] m_swimmers;
    public GameObject m_passenger;

    private float m_startTimer = 3;
    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Start()
    {
        OnNewGame();
    }

    public void OnNewGame()
    {
        m_GameState = GameState.Start;

        m_TimerText.text = "";
        m_MessageText.text = "";

        m_passenger.SetActive(false);

        for (int i = 0; i < m_swimmers.Length; i++)
        {
            m_swimmers[i].SetActive(true);
        }
    }

    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                m_startTimer -= Time.deltaTime;

                m_MessageText.text = "Game starts in " + (int)(m_startTimer + 1);

                if (Input.GetKeyUp(KeyCode.Return) == true || m_startTimer < 0)
                {
                    m_gameTime = 0;
                    m_startTimer = 3;
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;
                }
                break;
            case GameState.Playing:
                if (AllRescued() == true)
                {
                    m_GameState = GameState.GameOver;
                    m_MessageText.text = "You Win";
                }
                else
                {
                    m_gameTime += Time.deltaTime;
                    int seconds = Mathf.RoundToInt(m_gameTime);

                    m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                                                (seconds / 60), (seconds % 60));
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    OnNewGame();
                }
                break;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private bool AllRescued()
    {
        int numRescuedLeft = 0;

        for (int i = 0; i < m_swimmers.Length; i++)
        {
            if (m_swimmers[i].activeSelf == true)
            {
                numRescuedLeft++;
            }
        }

        if (numRescuedLeft <= 0 && m_passenger.activeSelf == false)
            return true;

        return false;
    }
}

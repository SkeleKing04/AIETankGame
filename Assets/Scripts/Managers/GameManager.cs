using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;
    public Text m_MessageText;
    public Text m_FinalScore;
    public Text m_TimerNum;
    public Text m_TimerText;
    public Text m_BombCountNum;
    public Text m_BombCountText;
    public Text m_ScoreNum;
    public Text m_ScoreText;
    public Image m_lowHealthEdge;
    public List<GameObject> m_Tanks = new List<GameObject>();
    public GameObject m_PlayerTank;
    private float m_gameTime = 0;
    private int  m_Score;
    public float GameTime { get { return m_gameTime; } }
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }
    private void Awake()
    {
        m_GameState = GameState.Start;
        m_Tanks.Add(m_PlayerTank);
    }
    private void Start()
    {
        for (int i = 0; i < m_Tanks.Count; i++)
        {
            m_Tanks[i].SetActive(false);
        }
        m_BombCountNum.gameObject.SetActive(false);
        m_BombCountText.gameObject.SetActive(false);
        m_ScoreNum.gameObject.SetActive(false);
        m_ScoreText.gameObject.SetActive(false);
        m_TimerNum.gameObject.SetActive(false);
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";
        m_FinalScore.gameObject.SetActive(false);
        m_lowHealthEdge.gameObject.SetActive(false);
    }
    private void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if(Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_BombCountNum.gameObject.SetActive(true);
                    m_BombCountText.gameObject.SetActive(true);
                    m_ScoreNum.gameObject.SetActive(true);
                    m_ScoreText.gameObject.SetActive(true);
                    m_TimerNum.gameObject.SetActive(true);
                    m_TimerText.gameObject.SetActive(true);
                    m_lowHealthEdge.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;
                    for (int i = 0; i < m_Tanks.Count; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                    TankSpawner spTank = Object.FindObjectOfType<TankSpawner>();
                    spTank.spawnTank();
                }
                break;
            case GameState.Playing:
                //if(m_Tanks.Contains(m_PlayerTank) == false)
                //{
                //}
                bool isGameOver = false;
                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerNum.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
                m_Score = Mathf.RoundToInt(0 + (m_gameTime * (m_Tanks.Count - 1)));
                m_ScoreNum.text = m_Score.ToString();
                if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }
                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                    m_BombCountNum.gameObject.SetActive(false);
                    m_BombCountText.gameObject.SetActive(false);
                    m_ScoreNum.gameObject.SetActive(false);
                    m_ScoreText.gameObject.SetActive(false);
                    m_TimerNum.gameObject.SetActive(false);
                    m_TimerText.gameObject.SetActive(false);
                    m_FinalScore.gameObject.SetActive(true);
                    m_lowHealthEdge.gameObject.SetActive(false);
                    m_Score = Mathf.RoundToInt(0 + ( m_gameTime * (m_Tanks.Count - 1)));
                    m_MessageText.text = "Your final score is:";
                    m_FinalScore.text = m_Score.ToString();
                    //save the score
                    m_HighScores.AddScore(m_Score);
                    m_HighScores.SaveScoresToFile();
                }
                else
                {
                    TankShooting countBomb = Object.FindObjectOfType<TankShooting>();
                    m_BombCountNum.text = countBomb.m_bombCount.ToString();
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_BombCountNum.gameObject.SetActive(true);
                    m_BombCountText.gameObject.SetActive(true);
                    m_ScoreNum.gameObject.SetActive(true);
                    m_ScoreText.gameObject.SetActive(true);
                    m_TimerNum.gameObject.SetActive(true);
                    m_TimerText.gameObject.SetActive(true);
                    m_FinalScore.gameObject.SetActive(false);
                    foreach (GameObject tank in m_Tanks)
                    {
                        tank.SetActive(false);
                    }
                    foreach (Bomb bomb in FindObjectsOfType<Bomb>())
                    {
                        bomb.Kill();
                    }
                    m_Tanks.RemoveRange(0, m_Tanks.Count);
                    m_Tanks.Add(m_PlayerTank);
                    m_Tanks[0].SetActive(true);
                    m_PlayerTank.transform.position = new Vector3(0, 0, 0);
                    m_PlayerTank.transform.rotation = new Quaternion(0, 0, 0, 0);
                    TankSpawner spTank = Object.FindObjectOfType<TankSpawner>();
                    spTank.spawnTank();
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Count; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }
}

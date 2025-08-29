using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  private int score;

  [field: SerializeField] public ScoreSystem ScoreSystem { get; private set; }
  public GameState State { get; private set; }

  public int Score
  {
    get => score;
    private set
    {
      score = value;
      OnChangeScore?.Invoke(value);
    }
  }

  public event Action<int> OnChangeScore;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  public void SetState(GameState parState)
  {
    State = parState;
  }

  public void AddScore(ColorType parColor)
  {
    Score += ScoreSystem.GetPoints(parColor);
  }

  public void ResetScore()
  {
    Score = 0;
  }

  public void GoToMenu()
  {
    LoadScene("MainMenu");
  }

  public void StartGame()
  {
    ResetScore();
    LoadScene("Game");
  }

  public void ShowResult()
  {
    LoadScene("Result");
  }

  private void LoadScene(string parName)
  {
    SceneManager.LoadScene(parName);
  }
}
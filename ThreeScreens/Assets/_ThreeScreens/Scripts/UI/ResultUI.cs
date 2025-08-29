using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreText;

  [Header("Buttons")]
  [SerializeField] private Button _replayButton;
  [SerializeField] private Button _menuButton;

  private void Start()
  {
    GameManager.Instance.SetState(GameState.Result);
    _scoreText.text = $"{GameManager.Instance.Score}";

    _replayButton.onClick.AddListener(() => StartGame());
    _menuButton.onClick.AddListener(() => GoToMenu());
  }

  private void OnDestroy()
  {
    _replayButton.onClick.RemoveListener(() => StartGame());
    _menuButton.onClick.RemoveListener(() => GoToMenu());
  }

  private void StartGame()
  {
    GameManager.Instance.StartGame();
  }

  private void GoToMenu()
  {
    GameManager.Instance.GoToMenu();
  }
}
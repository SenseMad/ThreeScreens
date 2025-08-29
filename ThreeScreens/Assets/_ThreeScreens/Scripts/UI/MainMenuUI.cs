using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  [SerializeField] private Button _playButton;

  private void Start()
  {
    GameManager.Instance.SetState(GameState.Menu);
    _playButton.onClick.AddListener(() => StartGame());
  }

  private void OnDestroy()
  {
    _playButton.onClick.RemoveListener(() => StartGame());
  }

  private void StartGame()
  {
    GameManager.Instance.StartGame();
  }
}
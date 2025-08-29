using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreText;

  private void Start()
  {
    UpdateScoreText(GameManager.Instance.Score);

    GameManager.Instance.OnChangeScore += UpdateScoreText;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnChangeScore -= UpdateScoreText;
  }

  private void UpdateScoreText(int parScoreValue)
  {
    _scoreText.text = $"{parScoreValue}";
  }
}
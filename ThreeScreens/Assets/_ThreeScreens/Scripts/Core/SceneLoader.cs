using UnityEngine;

public class SceneLoader : MonoBehaviour
{
  public void LoadMenu() => GameManager.Instance.GoToMenu();
  public void LoadGame() => GameManager.Instance.StartGame();
  public void LoadResult() => GameManager.Instance.ShowResult();
  public void Quit() => Application.Quit();
}
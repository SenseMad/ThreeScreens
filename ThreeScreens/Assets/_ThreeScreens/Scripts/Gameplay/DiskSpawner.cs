using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
  [SerializeField] private PendulumController _pendulum;
  [SerializeField] private Disk _diskPrefab;

  [Header("Sprites")]
  [SerializeField] private Sprite _redSprite;
  [SerializeField] private Sprite _greenSpite;
  [SerializeField] private Sprite _blueSpite;

  private Disk currentDisk;

  private void Start()
  {
    SpawnNew();
  }

  public void SpawnNew()
  {
    if (GridController.Instance.IsBoardFull())
    {
      GameManager.Instance.ShowResult();
      return;
    }

    currentDisk = Instantiate(_diskPrefab, _pendulum.Hook.position, Quaternion.identity, _pendulum.Hook);
    var color = (ColorType)Random.Range(0, 3);
    currentDisk.Initialize(color, GetSprite(color));
    currentDisk.SetKinematic(true);
  }

  public void DropCurrentDisk()
  {
    if (currentDisk == null)
      return;

    currentDisk.transform.SetParent(null);
    currentDisk.SetKinematic(false);
    currentDisk = null;
  }

  private Sprite GetSprite(ColorType parColor) => parColor switch
  {
    ColorType.Red => _redSprite,
    ColorType.Green => _greenSpite,
    ColorType.Blue => _blueSpite,
    _ => _redSprite
  };
}
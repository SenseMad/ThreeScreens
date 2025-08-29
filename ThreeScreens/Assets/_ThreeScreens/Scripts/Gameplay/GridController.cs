using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
  public static GridController Instance { get; private set; }

  [Serializable]
  public struct CellSlot
  {
    public Transform Point;
    [NonSerialized] public Disk Disk;
  }

  [SerializeField] private DiskSpawner _spawner;
  [SerializeField] private CellSlot[] _cells = new CellSlot[9];
  [SerializeField] private ParticleSystem _matchFxPrefab;

  private void Awake()
  {
    Instance = this;
  }

  private void CollapseColumn(int col)
  {
    for (int row = 0; row < 3; row++)
    {
      int index = Index(col, row);
      if (_cells[index].Disk == null)
      {
        for (int upperRow = row + 1; upperRow < 3; upperRow++)
        {
          int upperIndex = Index(col, upperRow);
          var upperDisk = _cells[upperIndex].Disk;
          if (_cells[upperIndex].Disk != null)
          {
            _cells[index].Disk = upperDisk;
            _cells[upperIndex].Disk = null;

            _cells[index].Disk.transform.position = _cells[index].Point.position;
            break;
          }
        }
      }
    }
  }

  public bool IsBoardFull()
  {
    for (int i = 0; i < _cells.Length; i++)
      if (_cells[i].Disk == null)
        return false;

    return true;
  }

  public bool TrySnapDiskToColumn(Disk parDisk, int parCol)
  {
    int row = GetLowestEmptyRow(parCol);
    if (row < 0)
    {
      Debug.Log("Колонка переполнена. Игра окончена!");
      GameManager.Instance.ShowResult();
      return false;
    }

    int index = Index(parCol, row);
    _cells[index].Disk = parDisk;

    var tr = parDisk.transform;
    tr.SetPositionAndRotation(_cells[index].Point.position, Quaternion.identity);
    parDisk.SetKinematic(true);

    var matches = MatchChecker.FindMatches(GetColorsGrid());
    if (matches.Count > 0)
    {
      foreach (var match in matches)
      {
        foreach (var cellIndex in match)
        {
          var d = _cells[cellIndex].Disk;
          if (d == null)
            continue;

          if (_matchFxPrefab)
            Instantiate(_matchFxPrefab, d.transform.position, Quaternion.identity);

          GameManager.Instance.AddScore(d.Color);

          Destroy(d.gameObject);
          _cells[cellIndex].Disk = null;
        }
      }
    }

    foreach (var col in new int[] { 0, 1, 2 })
      CollapseColumn(col);

    if (IsBoardFull())
      GameManager.Instance.ShowResult();
    else
      _spawner.SpawnNew();

    return true;
  }

  private int GetLowestEmptyRow(int parCol)
  {
    for (int row = 0; row < 3; row++)
    {
      if (_cells[Index(parCol, row)].Disk == null)
        return row;
    }

    return -1;
  }

  private int Index(int parCol, int parRow)
  {
    return parRow * 3 + parCol;
  }

  private ColorType?[,] GetColorsGrid()
  {
    var g = new ColorType?[3, 3];
    for (int row = 0; row < 3; row++)
    {
      for (int col = 0; col < 3; col++)
      {
        var d = _cells[Index(col, row)].Disk;
        g[row, col] = d ? d.Color : null;
      }
    }

    return g;
  }
}
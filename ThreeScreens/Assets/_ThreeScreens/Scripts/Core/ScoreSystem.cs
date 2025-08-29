using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSystem", menuName = "Config/ScoreSystem")]
public class ScoreSystem : ScriptableObject
{
  [field: SerializeField] public int RedPoints { get; private set; }
  [field: SerializeField] public int GreenPoints { get; private set; }
  [field: SerializeField] public int BluePoints { get; private set; }

  public int GetPoints(ColorType parColor) => parColor switch
  {
    ColorType.Red => RedPoints,
    ColorType.Green => GreenPoints,
    ColorType.Blue => BluePoints,
    _ => 0
  };
}
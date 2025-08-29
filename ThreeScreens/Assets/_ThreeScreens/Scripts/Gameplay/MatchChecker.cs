using System.Collections.Generic;

public static class MatchChecker
{
  public static List<List<int>> FindMatches(ColorType?[,] parG)
  {
    var res = new List<List<int>>();

    bool Eq((int c, int r) a, (int c, int r) b, (int c, int r) c3, out ColorType parColor, out List<int> parIndexs)
    {
      parColor = default;
      parIndexs = null;

      var ca = parG[a.c, a.r];
      var cb = parG[b.c, b.r];
      var cc = parG[c3.c, c3.r];

      if (ca.HasValue && cb.HasValue && cc.HasValue && ca.Value == cb.Value && cb.Value == cc.Value)
      {
        parColor = ca.Value;
        parIndexs = new List<int> { Index(a.c, a.r), Index(b.c, b.r), Index(c3.c, c3.r) };
        return true;
      }

      return false;
    }

    for (int r = 0; r < 3; r++)
      if (Eq((0, r), (1, r), (2, r), out _, out var parIndexH))
        res.Add(parIndexH);

    for (int c = 0; c < 3; c++)
      if (Eq((c, 0), (c, 1), (c, 2), out _, out var parIndexV))
        res.Add(parIndexV);

    if (Eq((0, 0), (1, 1), (2, 2), out _, out var parIndexD1))
      res.Add(parIndexD1);
    if (Eq((0, 2), (1, 1), (2, 0), out _, out var parIndexD2))
      res.Add(parIndexD2);

    return res;
  }

  private static int Index(int parCol, int parRow)
  {
    return parCol * 3 + parRow;
  }
}
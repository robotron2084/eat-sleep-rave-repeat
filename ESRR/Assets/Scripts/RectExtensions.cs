using UnityEngine;

namespace GameJamStarterKit
{
  public static class RectExtensions
  {
    public static Vector2 RandomPoint(this Rect rect)
    {
      return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }
  }
}
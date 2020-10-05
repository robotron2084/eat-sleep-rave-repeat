using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
  public class ClubEmitterConfig : MonoBehaviour
  {
    public List<Color> colors;
    public List<Clubber> clubberPrefabs;
    public List<string> descriptions;
    public float waitSecondsMin;
    public float waitSecondsMax;
    public int totalToSpawn;
    public int spawnMin;
    public int spawnMax;

  }
}
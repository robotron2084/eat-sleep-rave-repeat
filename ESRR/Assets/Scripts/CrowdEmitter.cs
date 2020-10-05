using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using GameJamStarterKit;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
  public class CrowdEmitter : MonoBehaviour
  {
    public Rect spawnArea;
    public Rect danceArea;
  
    private List<string> descriptions = new List<string>()
    {
      "\U0001F60A",
      "\U0001F60B",
      "\U0001F60D",
      "\U0001F60E",
      "\U0001F600",
      "\U0001F601",
      "\U0001F602",
      "\U0001F603",
      "\U0001F604",
      "\U0001F605",
      "\U0001F606",
      "\U0001F609",
      "\U0001F923",
    };

    private List<Clubber> spawnedItems = new List<Clubber>();
    public void EmitClubbers(ClubEmitterConfig config)
    {
      StartCoroutine(emitClubbers(config));
    }
    
    IEnumerator emitClubbers(ClubEmitterConfig config)
    {
      int numSpawned = 0;
      while (numSpawned < config.totalToSpawn)
      {
        int numToSpawn = UnityEngine.Random.Range(config.spawnMin, config.spawnMax);
        for (int i = 0; i < numToSpawn; i++)
        {
          Clubber prefab = config.clubberPrefabs.RandomItem();
          Color clubberColor = config.colors.RandomItem();
          Clubber c = Instantiate(prefab);
          spawnedItems.Add(c);
          c.clubberRenderer.color = clubberColor;
          c.transform.position = spawnArea.RandomPoint();
          c.selectable.description = descriptions.RandomItem();
          c.dancePosition = danceArea.RandomPoint();
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(config.waitSecondsMin, config.waitSecondsMax));

        numSpawned += numToSpawn;
      }
    }

    public void MakeClubJammin()
    {
      StartCoroutine(makeClubJammin());
    }

    IEnumerator makeClubJammin()
    {
      foreach (Clubber c in spawnedItems)
      {
        c.fsm.ChangeState(States.Jamming);
        yield return new WaitForSeconds(0.1f);
      }
    }

    public void Cleanup()
    {
      foreach (Clubber c in spawnedItems)
      {
        Destroy(c.gameObject);
      }
      spawnedItems.Clear();
    }
    
    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(danceArea.center, danceArea.size);
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
    }
  }
}
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
  public class IntroScene : Scene
  {
    public override IEnumerator transitionOut()
    {
      Player p = WorldManager.instance.player;
      p.gameObject.SetActive(true);
      yield break;
    }
    
    public override IEnumerator transitionIn()
    {
      WorldManager.instance.music.PlayCollection(backgroundMusic);
      Player p = WorldManager.instance.player;
      p.gameObject.SetActive(false);
      Show();
      yield break;
    }

  }
}
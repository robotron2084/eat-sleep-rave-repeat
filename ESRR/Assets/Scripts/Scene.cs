using System.Collections;
using System.Runtime.InteropServices;
using GameJamStarterKit;
using UnityEngine;

namespace DefaultNamespace
{
  public class Scene : MonoBehaviour
  {
    public string sceneName;
    public AudioClipCollection backgroundMusic;
    public Transform entrance;
    public GameObject sceneRoot;
    public States initialPlayerState;


    public void Hide()
    {
      sceneRoot.gameObject.SetActive(false);
    }

    public void Show()
    {
      sceneRoot.gameObject.SetActive(true);
    }
    
    public virtual IEnumerator transitionOut()
    {
      WorldManager.instance.curtain.TransitionIn();
      yield return new WaitForSeconds(1.0f);
      Hide();
    }
    
    public virtual IEnumerator transitionIn()
    {
      WorldManager.instance.music.PlayCollection(backgroundMusic);
      Player p = WorldManager.instance.player;
      p.fsm.ChangeState(initialPlayerState);
      // if the entrance isn't set then the player won't move.
      if (entrance != null)
      {
        p.transform.position = entrance.position;
      }

      Show();

      WorldManager.instance.curtain.TransitionOut();
      yield return new WaitForSeconds(0.33f);

      
    }
  }
}
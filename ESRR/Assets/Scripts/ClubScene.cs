using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
  public class ClubScene : Scene
  {

    public SuperRaveQuest quest;

    public Dialogue djDialogue;
    public int dancesRequired;
    public Scene nextClubScene;

    [System.Serializable]
    public class ClubSceneEmitterConfig
    {
      public CrowdEmitter emitter;
      public ClubEmitterConfig config;
    }
    
    public List<ClubSceneEmitterConfig> emitters;

    public override IEnumerator transitionOut()
    {
      yield return base.transitionOut();
      foreach (ClubSceneEmitterConfig config in emitters)
      {
        config.emitter.Cleanup();
      }

    }
    
    public override IEnumerator transitionIn()
    {
      Show(); // show stuff.
      // config emitters....
      foreach (ClubSceneEmitterConfig config in emitters)
      {
        config.emitter.EmitClubbers(config.config);
      }

      DJ dj = WorldManager.instance.dj;
      dj.dialogue = djDialogue;
      dj.dancesRequired = dancesRequired;

      WorldManager.instance.portal.nextScene = nextClubScene;

      WorldManager.instance.currentQuest = quest;
      yield return base.transitionIn();
      
    }
  }
}
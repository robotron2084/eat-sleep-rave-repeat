using System.Collections;
using GameJamStarterKit;
using MonsterLove.StateMachine;
using UnityEngine;

namespace DefaultNamespace
{
  public class SuperHorseQuest : SuperRaveQuest
  {
    public Dialogue startingDialogue;

    public Horse horse;
    public Collider2D[] gateColliders;
    public GameObject horseArea;
    public AudioClipCollection superRaveMusic;

    private StateMachine<States> fsm;
    public override void BeginQuest()
    {
      base.BeginQuest();
      fsm = StateMachine<States>.Initialize(this);
      fsm.ChangeState(States.Idle);
    }

    public override void CompleteQuest()
    {
      base.CompleteQuest();
      fsm.ChangeState(States.DeathDropped);
    }


    IEnumerator Idle_Enter()
    {
      horse.fsm.ChangeState(States.Idle);
      DialogueController dialog = WorldManager.instance.dialogue; 
      dialog.SetDialog(startingDialogue);
      while (dialog.fsm.State != States.Disabled)
      {
        yield return null;
      }
      horseArea.SetActive( true );
      foreach (Collider2D collider in gateColliders)
      {
        collider.enabled = false;
      }
    }
    
    IEnumerator DeathDropped_Enter()
    {
      WorldManager.instance.music.PlayCollection(superRaveMusic);
      yield return new WaitForSeconds(2.0f);
      ClubScene scene = (ClubScene)WorldManager.instance.currentScene;
      foreach (var emitter in scene.emitters)
      {
        emitter.emitter.MakeClubJammin();
      }

      yield return new WaitForSeconds(2.0f);
      WorldManager.instance.completedUI.Show();
      
    }
  }
}
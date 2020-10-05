using System;
using GameJamStarterKit;
using UnityEngine;

namespace DefaultNamespace
{
  public class DJ : Interactable
  {
    public int dancesRequired = 1;
    public int dances;
    
    public Dialogue dialogue;
    private DialogueController dialog;
    [NonSerialized] public Selectable selectable;

    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Idle);
      selectable = GetComponent<Selectable>();
      dialog = WorldManager.instance.dialogue;
    }

    void OnDisable()
    {
      //reset!
      fsm.ChangeState(States.Idle);
      selectable.fsm.ChangeState(States.Idle);
    }

    void Idle_Enter()
    {
      dances = 0;
    }
    
    void Interacting_Enter()
    {
      dialog.SetDialog(dialogue);
      selectable.fsm.ChangeState(States.Disabled);
      // can't re-enable the dj.
    }
    
    void Interacting_Update()
    {
      if (dialog.fsm.State == States.Disabled)
      {
        //enable the player.
        WorldManager.instance.player.fsm.ChangeState(States.Idle);
        fsm.ChangeState(States.Jamming);

        if (WorldManager.instance.currentQuest == null)
        {
            WorldManager.instance.GotoScene("Bedroom");
        }
        else
        {
          WorldManager.instance.currentQuest.BeginQuest();
        }
      }
    }


    public void IncreaseTheDancing()
    {
      dances++;
      if (dances >= dancesRequired)
      {
        fsm.ChangeState(States.Jamming);
      }
    }
  }
}
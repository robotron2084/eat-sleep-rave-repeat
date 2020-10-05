using System;
using GameJamStarterKit;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
  public class Bouncer : Interactable
  {
    public Dialogue[] GuardingDialogues;
    public Dialogue[] IdleDialogues;

    public Transform idlePosition;
    public Transform guardingPosition;
    
    [NonSerialized]
    Dialogue[] dialogues;
    private DialogueController dialog;
    [NonSerialized] public Selectable selectable;

    private DJ dj;
    
    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Guarding);
      selectable = GetComponent<Selectable>();
      dialog = WorldManager.instance.dialogue;
      dj = WorldManager.instance.dj;
    }

    private void OnDisable()
    {
      fsm.ChangeState(States.Guarding);
    }

    void Guarding_Enter()
    {
      t.position = guardingPosition.position;
    }

    void Idle_Update()
    {
      t.position = Vector3.Lerp(t.position, idlePosition.position, Time.deltaTime);
    }

    void Interacting_Enter()
    {
      if (dj.State == States.Jamming)
      {
        dialogues = IdleDialogues;
      }
      else
      {
        dialogues = GuardingDialogues;
      }
      
      dialog.SetDialog(dialogues.RandomItem());
      selectable.fsm.ChangeState(States.Disabled);
    }
    
    void Interacting_Update()
    {
      if (dialog.fsm.State == States.Disabled)
      {
        //enable the player.
        WorldManager.instance.player.fsm.ChangeState(States.Idle);
        States newState = dj.State == States.Jamming ? States.Idle : States.Guarding;
        fsm.ChangeState(newState);
        selectable.fsm.ChangeState(States.Idle);
      }
    }


  }
}
using System;
using UnityEngine;

namespace DefaultNamespace
{
  public class Fridge : Interactable
  {
    public Dialogue[] dialogues;
    [NonSerialized] public Selectable selectable;
    private DialogueController dialog;
    
    protected override void Start()
    {
      base.Start();
      selectable = GetComponent<Selectable>();
      fsm.ChangeState(States.Idle);
      dialog = WorldManager.instance.dialogue;
    }

    void Interacting_Enter()
    {
      dialog.SetDialog(dialogues[UnityEngine.Random.Range(0, dialogues.Length)]);
      selectable.fsm.ChangeState(States.Disabled);
    }

    void Interacting_Update()
    {
      if (dialog.fsm.State == States.Disabled)
      {
        WorldManager.instance.giraffe.fsm.ChangeState(States.Interacting);
        fsm.ChangeState(States.Idle);
        //selectables auto-disable!
        selectable.fsm.ChangeState(States.Idle);
        
      }
    }

    public bool IsEnabled()
    {
      return fsm.State != States.Disabled;
    }
  }
}
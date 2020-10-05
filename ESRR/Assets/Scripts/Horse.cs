using System;
using UnityEngine;

namespace DefaultNamespace
{
  public class Horse : Interactable
  {
    public Dialogue dialogue;
    public Dialogue platformDialogue;

    public Transform platformPos;

    private bool onPlatform;

    private DialogueController dialog;
    [NonSerialized] public Selectable selectable;
    protected override void Start()
    {
      base.Start();
      selectable = GetComponent<Selectable>();
      fsm.ChangeState(States.Disabled);
      dialog = WorldManager.instance.dialogue;
    }

    void Disabled_Enter()
    {
      gameObject.SetActive(false);
    }

    void Disabled_Exit()
    {
      gameObject.SetActive(true);
    }

    void Interacting_Enter()
    {
      if (!onPlatform)
      {
        gameObject.SetActive(true); // ???
        dialog.SetDialog(dialogue);
      }
      else
      {
        dialog.SetDialog(platformDialogue);
      }
    }
    
    void Interacting_Update()
    {
      if (dialog.fsm.State == States.Disabled)
      {
        //enable the player to ride and hide this.
        if (!onPlatform)
        {
          WorldManager.instance.player.fsm.ChangeState(States.Riding);
          fsm.ChangeState(States.Disabled);

        }
        else
        {
          // trigger death drop!
          Debug.Log("Droooop");
          fsm.ChangeState(States.Idle);
          WorldManager.instance.horseArea.SetActive(false);
          WorldManager.instance.deathDropArea.SetActive(true);
          WorldManager.instance.player.fsm.ChangeState(States.DeathDropping);
        }
      }
    }

    void IdlePlatforming_Enter()
    {
      selectable.fsm.ChangeState(States.Idle);
      selectable.description = "It is time.";
      t.position = platformPos.position;
      onPlatform = true;
      
      
    }
    
  }
}
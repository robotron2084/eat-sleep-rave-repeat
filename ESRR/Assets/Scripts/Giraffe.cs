using UnityEngine;

namespace DefaultNamespace
{
  public class Giraffe : Interactable
  {
    public Dialogue[] dialogues;

    private DialogueController dialog;
    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Disabled);
      dialog = WorldManager.instance.dialogue;
    }

    void OnDisable()
    {
      fsm.ChangeState(States.Disabled);
    }

    void Disabled_Enter()
    {
      gameObject.SetActive(false);
    }

    void Interacting_Enter()
    {
      gameObject.SetActive(true);
      dialog.SetDialog(dialogues[UnityEngine.Random.Range(0, dialogues.Length)]);
    }
    
    void Interacting_Update()
    {
      if (dialog.fsm.State == States.Disabled)
      {
        //enable the player.
        WorldManager.instance.player.fsm.ChangeState(States.Idle);
        WorldManager.instance.portal.fsm.ChangeState(States.Idle);
      }
    }


  }
}
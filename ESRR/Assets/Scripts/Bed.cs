using System;
using UnityEngine;

namespace DefaultNamespace
{
  public class Bed : Interactable
  {

    public Transform InBed;
    public Transform OutOfBed;

    [NonSerialized] public Selectable selectable;
    
    protected override void Start()
    {
      base.Start();
      selectable = GetComponent<Selectable>();
      fsm.ChangeState(States.Sleeping);
    }
    
      void OnDisable()
      {
        fsm.ChangeState(States.Sleeping);
      }


    void Sleeping_Enter()
    {
      selectable.fsm.ChangeState(States.Disabled);
      rBod.isKinematic = true;
    }

    void Sleeping_Exit()
    {
      rBod.isKinematic = false;
      selectable.fsm.ChangeState(States.Idle);
    }

    void Interacting_Enter()
    {
      WorldManager.instance.player.fsm.ChangeState(States.Sleeping);
      fsm.ChangeState(States.Sleeping);
      
    }
    
  }
}
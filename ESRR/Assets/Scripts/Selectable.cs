using System;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
  public class Selectable : MonoBehaviour
  {
    [System.NonSerialized]
    public StateMachine<States> fsm;

    public string description; 
    HighlightUI ui;

    [SerializeField]
    private Transform highlightPosition = null;

    private Interactable interactable;
    
    public States State; // this exists solely to see what's up in the editor. setting this won't do anything.

    
    protected virtual void Awake()
    {
      fsm = StateMachine<States>.Initialize(this);
      interactable = GetComponent<Interactable>();
    }

    protected virtual void Start()
    {
      ui = WorldManager.instance.highlightUI;
      fsm.ChangeState(States.Idle);
    }

    void Highlighting_Enter()
    {
      if (description != "")
      {
        ui.Show(description, highlightPosition);
      }
    }

    void Highlighting_Exit()
    {
      ui.Hide();
    }

    public void Interact()
    {
      if (IsEnabled())
      {
        Debug.Log("Interacting with " + interactable);
        interactable.fsm.ChangeState(States.Interacting);
      }
     }

    public bool IsEnabled()
    {
      return fsm.State != States.Disabled;
    }

    void Update()
    {
      State = fsm.State;
    }

    void OnDisable()
    {
      fsm.ChangeState(States.Idle);
    }
  }
}
using MonsterLove.StateMachine;
using UnityEngine;

namespace DefaultNamespace
{
  public class Interactable : MonoBehaviour
  {
    [System.NonSerialized]
    public StateMachine<States> fsm;

    [System.NonSerialized]
    protected Transform t;
    
    [System.NonSerialized]
    protected Rigidbody2D rBod;

    [System.NonSerialized] 
    protected Animator anim;

    public States State; // this exists solely to see what's up in the editor. setting this won't do anything.


    protected virtual void Awake()
    {
      rBod = GetComponent<Rigidbody2D>();
      t = transform;
      anim = GetComponent<Animator>();

      fsm = StateMachine<States>.Initialize(this);
    }

    protected virtual void Start()
    {
      //stub for now! maybe I want to make all things have an rbod?
    }

    void Update()
    {
      State = fsm.State;
    }

  }
}
using System;
using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;

namespace DefaultNamespace
{
  public class Player : Interactable
  {
    
    [SerializeField]
    private float moveSpeed = 1.0f;

    public float jumpForce = 100.0f;
    private Vector2 move;

    private int snores = 0;
    private int snoresRequiredToWakeUp = 3;

    private int dances;

    public Transform highlightPos;

    public ColliderProxy sensorProxy;

    public Selectable currentInteraction;
    public List<Selectable> interactions = new List<Selectable>();
    private bool updateInteractions;
    
    [SerializeField]
    private Transform spriteT = null;

    private bool canMoveVertically = true;
    
    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Idle);
      WorldManager.instance.player = this;
    }

    void onSensorTriggerEnter(Collider2D collider)
    {
      Selectable selectable = collider.GetComponentInParent<Selectable>();
      if (selectable != null && selectable.IsEnabled())
      {
        Debug.Log("trigger:" + collider);

        updateInteractions = true;
        if (!interactions.Contains(selectable))
        {
          interactions.Add(selectable);
        }
      }
    }

    void onSensorTriggerExit(Collider2D collider)
    {
      Selectable selectable = collider.GetComponentInParent<Selectable>();
      if (selectable != null && selectable.IsEnabled())
      {
        Debug.Log("trigger:" + collider);
        updateInteractions = true;
        interactions.Remove(selectable);
      }
    }

    void Sleeping_Enter()
    {
      t.position = WorldManager.instance.bed.InBed.position;
      rBod.isKinematic = true;
      anim.SetBool("Sleeping", true);

    }

    void Sleeping_Update()
    {
      if (isUp())
      {
        snores++;
        if (snores == snoresRequiredToWakeUp)
        {
          fsm.ChangeState(States.Waking);
          snores = 0;
        }
        else
        {
          fsm.ChangeState(States.Snore);
        }
      }
    }

    void Sleeping_Exit()
    {
      
    }

    IEnumerator Snore_Enter()
    {
      anim.SetTrigger("Snore");
      yield return new WaitForSeconds(0.25f); //sure need to add a signaller
      fsm.ChangeState(States.Sleeping);
    }

    void Waking_Enter()
    {
      snores = 0;
      Bed bed = WorldManager.instance.bed;
      t.position = bed.OutOfBed.position;
      bed.fsm.ChangeState(States.Idle);
      
      fsm.ChangeState(States.Idle);
    }

    void Idle_Enter()
    {
      anim.SetBool("Sleeping", false);
      rBod.isKinematic = false;
      canMoveVertically = true;
      listenForTriggers();
    }

    void listenForTriggers()
    {
      sensorProxy.onTriggerEnter.AddListener(onSensorTriggerEnter);
      sensorProxy.onTriggerExit.AddListener(onSensorTriggerExit);
    }

    void dontlistenForTriggers()
    {
      sensorProxy.onTriggerEnter.RemoveListener(onSensorTriggerEnter);
      sensorProxy.onTriggerExit.RemoveListener(onSensorTriggerExit);
      
    }

    void Idle_Update()
    {
      if (updateInteractions)
      {
        updateInteractions = false;
        if (currentInteraction == null)
        {
          if (interactions.Count > 0)
          {
            currentInteraction = interactions[0];
            currentInteraction.fsm.ChangeState(States.Highlighting);
          }
        }
        else
        {
          // we have an interaction...is it still in our list?
          if (!interactions.Contains(currentInteraction))
          {
            //nope, not any more.
            if (currentInteraction.IsEnabled())
            {
              currentInteraction.fsm.ChangeState(States.Idle);
            }
            currentInteraction = null;
            if (interactions.Count > 0)
            {
              currentInteraction = interactions[0];
              currentInteraction.fsm.ChangeState(States.Highlighting);
            }
          }
        }
        
      }
      if (Input.GetKeyDown(KeyCode.Space))
      {
        if (currentInteraction != null)
        {
          // we have something to interact with.
          fsm.ChangeState(States.Interacting);
        }
        else
        {
          Debug.Log("null interaction");
        }
      }
      
    }

    void Idle_FixedUpdate()
    {
      float moveVert = 0.0f;
      float moveHorz = 0.0f;
      if (canMoveVertically)
      {
        if (isUp())
        {
          moveVert = 1.0f;
        }
        if (isDown())
        {
          moveVert = -1.0f;
        }
      }
      if (isLeft())
      {
        moveHorz = -1.0f;
      }
      if (isRight())
      {
        moveHorz = 1.0f;
      }
      
      move.y = moveVert * moveSpeed;
      move.x = moveHorz * moveSpeed;
      Vector2 moveAmount = new Vector2(move.x * Time.fixedDeltaTime, move.y * Time.fixedDeltaTime);
      anim.SetBool("Walking", moveAmount.magnitude > 0.0f);
      float moveScale = move.x < 0.0f ? 1.0f : -1.0f;
      if (move.x != 0.0f)
      {
        spriteT.localScale = new Vector3(moveScale, 1.0f, 1.0f);
      }
      
      rBod.AddForce(moveAmount);
    }

    void Idle_Exit()
    {
      dontlistenForTriggers();
    }

    void Interacting_Enter()
    {
      anim.SetBool("Walking", false);
      if (currentInteraction.IsEnabled())
      {
        currentInteraction.Interact();
        interactions.Remove(currentInteraction);
        currentInteraction = null;
      }
      else
      {
        fsm.ChangeState(States.Idle);
      }
      
    }

    void Dancing_Enter()
    {
      dances = 0;
      anim.SetBool("Dancing", true);
    }

    void Dancing_Update()
    {
      if (dances == 1)
      {
        fsm.ChangeState(States.Idle);
      }
    }

    void Dance_Completed()
    {
      dances++;
      anim.SetBool("Dancing", false);
    }

    void Riding_Enter()
    {
      WorldManager.instance.highlightUI.ShowForSeconds( 3.0f, "OMG I HAVE A HORSE!", WorldManager.instance.player.highlightPos);
      anim.SetBool("Riding", true);
      setPlatformerPhysics();
    }

    void setPlatformerPhysics()
    {
      rBod.gravityScale = 1.0f;
      rBod.mass = 1.0f;
      rBod.drag = 0.0f;
    }


    private bool doJump;
    private float jumpWindow;
    private bool weakJump = false;
    void Riding_Update()
    {
      if (jumpWindow > 0.0f)
      {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          doJump = true;
        }
      }

      jumpWindow -= Time.deltaTime;
    }

    void Riding_FixedUpdate()
    {
      
      float moveVert = 0.0f;
      if (doJump)
      {
        Debug.Log("Jumping in here");
        moveVert = weakJump ? jumpForce * 0.5f : jumpForce;
        anim.SetTrigger("Jump");
        doJump = false;
      }

      float moveHorz = 0.0f;
      if (isLeft())
      {
        moveHorz = -1.0f;
      }
      if (isRight())
      {
        moveHorz = 1.0f;
      }
      
      move.y = moveVert * moveSpeed;
      move.x = moveHorz * moveSpeed;
      Vector2 moveAmount = new Vector2(move.x * Time.fixedDeltaTime, move.y * Time.fixedDeltaTime);
      anim.SetBool("Walking", moveAmount.magnitude > 0.0f);
      float moveScale = move.x < 0.0f ? 1.0f : -1.0f;
      if (move.x != 0.0f)
      {
        spriteT.localScale = new Vector3(moveScale, 1.0f, 1.0f);
      }
      
      rBod.AddForce(moveAmount);
    }

    void Riding_Exit()
    {
      ResetPhysics();
      anim.SetBool("Riding", false);
      
    }

    void ResetPhysics()
    {
      rBod.mass = 0.2f;
      rBod.gravityScale = 0.0f;
      rBod.drag = 20.0f;

    }

    private void OnCollisionStay2D(Collision2D other)
    {
      if (fsm.State == States.Riding)
      {
        if (other.gameObject.tag == "Ground")
        {
          jumpWindow = 0.5f;
          weakJump = false;
        }
        
        if (other.gameObject.tag == "DeathDrop")
        {
          Debug.Log("We are here.");
        }
      }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (fsm.State == States.Riding)
      {
        if (other.gameObject.tag == "Ground")
        {
          jumpWindow = 0.2f;
          weakJump = true;
        }
        
        if (other.gameObject.tag == "DeathDrop")
        {
          fsm.ChangeState(States.IdlePlatforming);
          WorldManager.instance.horse.fsm.ChangeState(States.IdlePlatforming);
        }
      }

      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (fsm.State == States.DeathDropping)
      {
        if (other.gameObject.tag == "DeathDrop")
        {
          fsm.ChangeState(States.DeathDropped);
          
        }
      }
    }

    /// <summary>
    /// Pretty much identical to Idle, but with gravity
    /// </summary>
    private void IdlePlatforming_Enter()
    {
      canMoveVertically = false;
      setPlatformerPhysics();
      listenForTriggers();
    }

    private void IdlePlatforming_Update()
    {
      Idle_Update();
    }

    private void IdlePlatforming_FixedUpdate()
    {
      Idle_FixedUpdate();
    }
    
    private void IdlePlatforming_Exit()
    {
      ResetPhysics();
      dontlistenForTriggers();
      canMoveVertically = true;
    }

    private void DeathDropping_Enter()
    {
      setPlatformerPhysics();
      anim.SetBool("DeathDropping", true);
    }

    private void DeathDropping_FixedUpdate()
    {
      float force = 10.0f;
      float xForce = t.position.x < 0 ? force : -force;
      rBod.AddForce(new Vector2(xForce, 0.0f));
    }
  
    private void DeathDropped_Enter()
    {
      anim.SetTrigger("Dropped");
      WorldManager.instance.currentQuest.CompleteQuest();
    }

    bool isLeft()
    {
      return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }
    
    bool isRight()
    {
      return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }
    
    bool isUp()
    {
      return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }
    
    bool isDown()
    {
      return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }
    
    


  }
}
using System;
using GameJamStarterKit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
  public class Clubber : Interactable
  {
    public Dialogue[] dialogues;
    public Vector2 noiseScale;
    public float noiseSpeed;
    public Vector2 originX;
    public Vector2 originY;

    public Vector2 dancePosition;
    public float walkTime;
    public float maxWalkTime;
    
    private Collider2D col;

    public SpriteRenderer clubberRenderer;
    
    [NonSerialized] public Selectable selectable;

    protected override void Awake()
    {
      base.Awake();
      col = GetComponent<Collider2D>();
      selectable = GetComponent<Selectable>();
    }

    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Walking);
      originX = new Vector2(Random.value * 100.0f, Random.value * 100.0f );
    }


    void Walking_Enter()
    {
      walkTime = 0.0f;
      maxWalkTime = Random.Range(0.0f, 2.0f);
    }
    void Walking_Update()
    {
      if (walkTime > 10.0f)
      {
        fsm.ChangeState(States.Idle);
        return;
      }
      Vector3 pos = t.position;
      float speed = Time.deltaTime * .2f;
      pos.x = Mathf.Lerp(pos.x, dancePosition.x, speed);
      pos.y = Mathf.Lerp(pos.y, dancePosition.y, speed);
      t.position = pos;
      walkTime += speed;
    }
    
    void Interacting_Enter()
    {
      WorldManager.instance.player.fsm.ChangeState(States.Dancing);
      selectable.fsm.ChangeState(States.Disabled);
    }

    void Interacting_Update()
    {
      Player p = WorldManager.instance.player;
      if (p.fsm.State != States.Dancing)
      {
        // we are now jamming!
        
        fsm.ChangeState(States.Jamming);
        if (WorldManager.instance.dj.dances == WorldManager.instance.dj.dancesRequired)
        {
          WorldManager.instance.dialogue.SetDialog(dialogues.RandomItem());
        }
      }
    }

    void Jamming_Enter()
    {
      anim.SetBool("Jamming", true);
      rBod.isKinematic = true;
      col.isTrigger = true;
      WorldManager.instance.dj.IncreaseTheDancing();
    }

    void Jamming_Update()
    {
      originX = new Vector2(originX.x + noiseScale.x * Time.deltaTime,
        originX.y + noiseScale.y * Time.deltaTime
      );
      originY = new Vector2(originY.x + noiseScale.x * Time.deltaTime,
        originY.y + noiseScale.y * Time.deltaTime
      );
      
      float newX = Mathf.PerlinNoise(originX.x, originX.y) ;
      float newY = Mathf.PerlinNoise(originY.x, originY.y) ;
      Vector3 pos = t.localPosition;
      
      pos.x += (2.0f * newX - 1.0f) * noiseSpeed;
      pos.y += (2.0f * newY - 1.0f) * noiseSpeed;
      t.localPosition = pos;
    }

  }
}
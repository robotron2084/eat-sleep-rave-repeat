using UnityEngine;

namespace DefaultNamespace
{
  public class Portal : Interactable
  {
  
    public Scene nextScene = null; 
    protected override void Start()
    {
      base.Start();
      fsm.ChangeState(States.Disabled);
    }
    
    void OnDisable()
    {
      fsm.ChangeState(States.Disabled);
    }

    void Disabled_Enter()
    {
      gameObject.SetActive(false);
    }

    void Idle_Enter()
    {
      gameObject.SetActive(true);
    }
    
    void Interacting_Enter()
    {
      if(nextScene == null)
      {
        WorldManager.instance.GotoScene("ClubStage1");
      }else{
        WorldManager.instance.GotoScene(nextScene);
      }
      Debug.Log("Time to go clubbing!");
      
    }
  }
}
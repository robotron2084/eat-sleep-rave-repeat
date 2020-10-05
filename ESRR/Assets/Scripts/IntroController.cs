using UnityEngine;

namespace DefaultNamespace
{
  public class IntroController : MonoBehaviour
  {
    public Animator anim;
    
    public void EndIntro()
    {
      anim.SetBool("Out", true);
      WorldManager.instance.GotoScene("Bedroom");
    }
  }
}
using UnityEngine;

namespace DefaultNamespace
{
  public class Curtain : MonoBehaviour
  {
    [SerializeField]
    private Animator anim = null;

    public void TransitionIn()
    {
      anim.SetBool("Visible", true);
    }
    
    public void TransitionOut()
    {
      anim.SetBool("Visible", false);
    }

  }
}
using UnityEngine;

namespace esrr
{
  public class AnimationSpeedController : MonoBehaviour
  {
    public float AnimationSpeed = 1.0f;

    public Animator anim;
    
    public void Update()
    {
      if (anim.gameObject.activeInHierarchy)
      {
        anim.SetFloat("AnimationSpeed", AnimationSpeed);
      }

    }
  }
}
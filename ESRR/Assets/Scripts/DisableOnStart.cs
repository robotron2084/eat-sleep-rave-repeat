using UnityEngine;

namespace DefaultNamespace
{
  public class DisableOnStart : MonoBehaviour
  {
    void Start()
    {
      gameObject.SetActive(false);
    }
  }
}
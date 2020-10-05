using UnityEngine;

namespace DefaultNamespace
{
  public class BouncePad : MonoBehaviour
  {
    private Player p;
    private Rigidbody2D rBod;
    void Start()
    {
      p = WorldManager.instance.player;
    }

  }
}
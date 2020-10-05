using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
  public class ColliderProxy : MonoBehaviour
  {
    
    public class ColliderProxyEvent : UnityEvent<Collider2D>
    {
      
    }

    public ColliderProxyEvent onTriggerEnter = new ColliderProxyEvent();
    public ColliderProxyEvent onTriggerExit = new ColliderProxyEvent();
    public ColliderProxyEvent onTriggerStay = new ColliderProxyEvent();
    public void OnTriggerEnter2D(Collider2D other)
    {
      onTriggerEnter.Invoke(other);
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
      onTriggerExit.Invoke(other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
      onTriggerStay.Invoke(other);
    }
  }
}
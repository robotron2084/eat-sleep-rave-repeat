using UnityEngine;

public class AudioTweaker : MonoBehaviour
{

  public float minPitch = 1.0f;
  public float maxPitch = 1.0f;

  void Start()
  {
    AudioSource audio = GetComponent<AudioSource>(); 
    if(audio != null)
    {
      audio.pitch = Random.Range(minPitch, maxPitch);
    }
  }
}

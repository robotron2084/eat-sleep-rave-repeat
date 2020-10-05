using UnityEngine;
using UnityEngine.UI;

namespace ESSR
{
  public class HelpOverlay : MonoBehaviour
  {
    public Slider musicVolumeSlider;

    public void Start()
    {
      musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
    }
    
    public void OnControlMusicVolumeSlider(float newValue)
    {
      AudioManager.Instance.MusicVolume = newValue;
    }

  }
  

}
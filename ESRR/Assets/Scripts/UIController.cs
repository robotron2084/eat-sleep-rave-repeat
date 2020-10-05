using UnityEngine;

namespace ESSR
{
  public class UIController :MonoBehaviour
  {
    public HelpOverlay helpScreen;


    public void Start()
    {
      helpScreen.gameObject.SetActive(false);
    }
    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
        ToggleHelp();
      }
    }


    void ToggleHelp()
    {
      var newMenuActiveState = !helpScreen.gameObject.activeSelf;
      helpScreen.gameObject.SetActive(newMenuActiveState);
    }
  }
}
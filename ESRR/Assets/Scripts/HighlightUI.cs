using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
  public class HighlightUI : MonoBehaviour
  {
    public TMP_Text label;

    private Transform follow;
    private RectTransform rt;
    private RectTransform canvasRect;
    public Vector2 margin;

    private Camera cam;

    void Awake()
    {
      rt = (RectTransform) transform;
      gameObject.SetActive(false);
    }

    void Start()
    {
      canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
      cam = Camera.main;
    }

    public void Show(string description, Transform follow)
    {
      gameObject.SetActive(true);
      this.follow = follow;
      label.text = description;
      
    }
    
    public void ShowForSeconds(float duration, string description, Transform follow)
    {
      Show(description, follow);
      StartCoroutine(showForSeconds(duration, description, follow));
    }
    
    IEnumerator showForSeconds(float duration, string description, Transform follow)
    {
      yield return new WaitForSeconds(duration);
      Hide();
    }
 
    public void Update()
    {
      Vector2 size = label.GetPreferredValues(label.text);
      rt.sizeDelta = size + margin;

      if (follow != null)
      {
        Vector2 screenPoint = cam.WorldToViewportPoint(follow.position);
        Vector2 WorldObject_ScreenPosition=new Vector2(
          ((screenPoint.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)),
          ((screenPoint.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f)));
        rt.anchoredPosition = WorldObject_ScreenPosition;
      }
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

  }
}
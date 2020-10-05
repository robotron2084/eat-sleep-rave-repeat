using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace com.enemyhideout.animatron
{

  [System.Serializable]
  public class StateSelection
  {
    public AnimatorState state;
    public bool selected = false;

    public StateSelection(AnimatorState state)
    {
      this.state = state;
    }

    public void Draw()
    {
      if(GUILayout.Button(state.name + " selected? " + selected))
      {
        selected = !selected;
      }
    }
  }
}
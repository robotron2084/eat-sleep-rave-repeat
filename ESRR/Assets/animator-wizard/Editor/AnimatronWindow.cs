using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace com.enemyhideout.animatron
{

  public class AnimatronWindow : EditorWindow
  {

      public AnimatorController animator;
      public AnimatorState selectedState;
      public AnimatorStateMachine selectedMachine;

      public List<StateSelection> stateSelections;
      public string randomParamName;
      public float exitTime = 1;
      public bool fixedDuration = true;
      public float transitionDuration = 0;
      public float transitionOffset = 0;

      public Vector3 origin = new Vector3(500.0f, -100.0f, 0.0f);
      public float radius = 200.0f;
      public bool sortAlphabetically = true;

      SerializedObject so;


      [MenuItem("Window/Animation/Animatron")]
      static void Init()
      {
          AnimatronWindow window = (AnimatronWindow)EditorWindow.GetWindow(typeof(AnimatronWindow), false, "Animatron");
          window.Show();
      }

      void OnFocus()
      {
      }

      void OnGUI()
      {
        so = new SerializedObject(this);

        SerializedProperty animatorProp = so.FindProperty("animator");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(animatorProp);
        if(EditorGUI.EndChangeCheck())
        {
          selectedState = null;
        }
        EditorGUILayout.HelpBox(animatorProp.objectReferenceValue?.ToString(), MessageType.None);
        if(animator != null)
        {
          EditorGUILayout.BeginHorizontal();
          EditorGUILayout.BeginVertical();

          foreach(AnimatorControllerLayer layer in animator.layers)
          {
            GUILayout.Label("Layer:" + layer.name);
            drawStateMachine(layer.stateMachine);
          }
          EditorGUILayout.EndVertical();


          // Column 2 - States
          EditorGUILayout.BeginVertical(GUILayout.Width(200));
          if(selectedState != null)
          {
            EditorGUILayout.LabelField("Selected state:" + selectedState.name);
            //null check!
            for(int i=0; i< stateSelections.Count; i++)
            {
              if(stateSelections[i].state == null)
              {
                stateSelections.RemoveAt(i);
                i--;
              }
            }
            foreach(StateSelection ss in stateSelections)
            {
              ss.Draw();
            }
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("All"))
            {
              foreach(StateSelection ss in stateSelections)
              {
                ss.selected = true;
              }
            }
            if(GUILayout.Button("None"))
            {
              foreach(StateSelection ss in stateSelections)
              {
                ss.selected = false;
              }
            }

            if(GUILayout.Button("Invert"))
            {
              foreach(StateSelection ss in stateSelections)
              {
                ss.selected = !ss.selected;
              }
            }
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Delete Transitions"))
            {
              while(selectedState.transitions.Length > 0)
              {
                selectedState.RemoveTransition(selectedState.transitions[0]);
              }
            }
          }
          EditorGUILayout.EndVertical();
          
          // Column 3 - Action!
          EditorGUILayout.BeginVertical();
          EditorGUILayout.LabelField("Column 3");
          SerializedProperty randomParamNameProp = so.FindProperty("randomParamName");
          EditorGUILayout.PropertyField(randomParamNameProp);

          SerializedProperty exitTimeProp = so.FindProperty("exitTime");
          EditorGUILayout.PropertyField(exitTimeProp);
          SerializedProperty fixedDurationProp = so.FindProperty("fixedDuration");
          EditorGUILayout.PropertyField(fixedDurationProp);
          SerializedProperty transitionDurationProp = so.FindProperty("transitionDuration");
          EditorGUILayout.PropertyField(transitionDurationProp);
          SerializedProperty transitionOffsetProp = so.FindProperty("transitionOffset");
          EditorGUILayout.PropertyField(transitionOffsetProp);

          if(GUILayout.Button("Setup Randomized Transitions"))
          {
            if(!animatorHasParameter(animator,randomParamName))
            {
              animator.AddParameter(randomParamName, AnimatorControllerParameterType.Float);
            }

            // RandomTransitionBehaviour rtb = getStateMachineBehaviour<RandomTransitionBehaviour>(selectedState);
            // RandomTransitionBehaviour rtb = selectedState.AddStateMachineBehaviour<RandomTransitionBehaviour>();
            // rtb.parameterName = randomParamName;


            List<StateSelection> enabledSelections = new List<StateSelection>();
            foreach(StateSelection ss in stateSelections)
            {
              if(ss.selected)
              {
                enabledSelections.Add(ss);
              }
            }


            for( int i=0; i < enabledSelections.Count; i++ )
            {
              StateSelection ss = enabledSelections[i];
              AnimatorStateTransition t = selectedState.AddTransition(ss.state);
              t.hasExitTime = true;
              t.exitTime = exitTime;
              t.hasFixedDuration = fixedDuration;
              t.offset = transitionOffset;
              t.duration = transitionDuration;
              t.name = "Random To " + ss.state.name;
              float lessThan = 1.0f / (float)enabledSelections.Count * (i + 1);
              t.AddCondition(AnimatorConditionMode.Less, lessThan, randomParamName);
            } 
          }
          EditorGUILayout.EndVertical();

          EditorGUILayout.EndHorizontal();

        }


        so.ApplyModifiedProperties();
      }

      void drawStateMachine(AnimatorStateMachine stateMachine)
      {
        EditorGUILayout.LabelField("FSM:" + stateMachine.name);
        EditorGUI.indentLevel++;

        foreach(ChildAnimatorState childState in stateMachine.states)
        {
          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(EditorGUI.indentLevel * 20.0f);
          GUI.enabled = selectedState != childState.state;
          if(GUILayout.Button(childState.state.name, GUILayout.Width(200)))
          {
            selectedMachine = stateMachine;
            selectedState = childState.state;
            stateSelections = new List<StateSelection>();
            foreach(ChildAnimatorState selectableState in stateMachine.states)
            {
              if(selectableState.state != selectedState)
              {
                StateSelection ss = new StateSelection(selectableState.state);
                stateSelections.Add(ss);
              }
            }
          }
          EditorGUILayout.EndHorizontal();
          GUI.enabled = true;
          foreach(ChildAnimatorStateMachine childSM in stateMachine.stateMachines)
          {
            AnimatorStateMachine subMachine = childSM.stateMachine;
            drawStateMachine(subMachine);
            
          }
        }

        SerializedProperty radiusProp = so.FindProperty("radius");
        EditorGUILayout.PropertyField(radiusProp);
        SerializedProperty originProp = so.FindProperty("origin");
        EditorGUILayout.PropertyField(originProp);
        SerializedProperty sortAlphabeticallyProp = so.FindProperty("sortAlphabetically");
        EditorGUILayout.PropertyField(sortAlphabeticallyProp);

        if(GUILayout.Button("Circular Layout"))
        {
          ChildAnimatorState[] updatedStates = new ChildAnimatorState[stateMachine.states.Length];
          for(int i=0; i < stateMachine.states.Length; i++ )
          {
            updatedStates[i] = stateMachine.states[i];
          }
          if(sortAlphabetically)
          {
            // updatedStates = updatedStates.OrderBy( _ => _.state.name).ToArray();
          }

          for(int i=0; i < updatedStates.Length; i++)
          {
            ChildAnimatorState childState = updatedStates[i];
            float radiansPerSegment = (2f * Mathf.PI) / (float)updatedStates.Length * i;
            radiansPerSegment += -90.0f * Mathf.Deg2Rad;
            float x = Mathf.Cos(radiansPerSegment) * radius;
            float y = Mathf.Sin(radiansPerSegment) * radius;
            childState.position = origin + new Vector3(x, y, 0.0f);
            updatedStates[i] = childState;
          }
          stateMachine.states = updatedStates;

        }
        EditorGUI.indentLevel--;
            

      }

      void createInstance<T>(string defaultName, ref T value) where T : ScriptableObject
      {
        string path = generatePathName(defaultName);
        value = ScriptableObject.CreateInstance<T>();
        saveAsset(path, value);
      }

      string generatePathName(string defaultName)
      {
        string path = AssetDatabase.GetAssetPath (Selection.activeObject);
        if (path == "") 
        {
          path = "Assets";
        } 
        else if (Path.GetExtension (path) != "") 
        {
          path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
        }
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/"+defaultName+".asset");
        return assetPathAndName;

      }

      void saveAsset(string assetPathAndName, UnityEngine.Object obj)
      {
        AssetDatabase.CreateAsset (obj, assetPathAndName);
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow ();
      }


      bool animatorHasParameter(AnimatorController anim, string searchParam)
      {
        foreach(AnimatorControllerParameter param in anim.parameters)
        {
          if(searchParam == param.name)
          {
            return true;
          }
        }
        return false;
      }

      T getStateMachineBehaviour<T>(AnimatorState state) where T : StateMachineBehaviour
      {
        T retVal = null;
        foreach(StateMachineBehaviour smb in state.behaviours)
        {
          if(smb.GetType() == typeof(T))
          {
            retVal = (T)smb;
            break;
          }
        }
        if(retVal == null)
        {
          retVal = (T)state.AddStateMachineBehaviour(typeof(T));
        }
        return retVal;
      }
  }
}
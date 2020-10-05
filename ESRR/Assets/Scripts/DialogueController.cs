using System;
using System.Collections;
using MonsterLove.StateMachine;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
  public class DialogueController : MonoBehaviour
  {
    [System.NonSerialized]
    public StateMachine<States> fsm;

    public GameObject dialogRoot;
    public TMP_Text dialogLabel;
    private Dialogue dialogue;
    private int dialogueIndex;
    [SerializeField]
    float lettersPerSecond = 30.0f;

    public void Start()
    {
      fsm = StateMachine<States>.Initialize(this);
      fsm.ChangeState(States.Disabled);
    }

    public void SetDialog(Dialogue dialogue)
    {
      this.dialogue = dialogue;
      dialogueIndex = 0;
      fsm.ChangeState(States.Talking);
    }

    void Disabled_Enter()
    {
      dialogRoot.SetActive(false);
    }

    IEnumerator Talking_Enter()
    {
      dialogRoot.SetActive(true);
      float letters = 0.0f;
      
      dialogLabel.maxVisibleCharacters = 0;
      if (dialogue.lines.Count == 0)
      {
        Debug.Log("No lines in dialog!");
        dialogLabel.text = "No dialog.";
        fsm.ChangeState(States.Idle);
        yield break;
      }
      dialogLabel.text = dialogue.lines[dialogueIndex];
      yield return null;
      while (letters < dialogLabel.textInfo.characterCount)
      {
        dialogLabel.maxVisibleCharacters = (int)letters;
        letters += lettersPerSecond * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
          fsm.ChangeState(States.Idle);
          yield break;
        }

        yield return null;
      }
      fsm.ChangeState(States.Idle);

    }

    void Idle_Enter()
    {
      dialogLabel.maxVisibleCharacters = Int32.MaxValue;

    }
    void Idle_Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        // do we have more conversation?
        if (dialogueIndex + 1 >= dialogue.lines.Count )
        {
          //emit an event here?
          fsm.ChangeState(States.Disabled);
        }
        else
        {
          // keep going!
          dialogueIndex++;
          fsm.ChangeState(States.Talking);
        }
      }
    }

  }
}
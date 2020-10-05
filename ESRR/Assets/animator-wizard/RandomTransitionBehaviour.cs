using UnityEngine;

namespace com.enemyhideout.animatron
{
    public class RandomTransitionBehaviour : StateMachineBehaviour
    {

        [SerializeField]
        public string parameterName = "Random";

        [SerializeField]
        float min = 0.0f;

        [SerializeField]
        float max = 1.0f;
        
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float val = Random.Range(min, max);
            animator.SetFloat(parameterName, val);
        }
    }
}
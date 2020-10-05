using UnityEngine;

namespace GameJamStarterKit
{
    public class LookAtTarget : MonoBehaviour
    {
        public Transform Target;

        private void LateUpdate()
        {
            transform.LookAt(Target);
        }
    }
}
using UnityEngine;

namespace GameJamStarterKit
{
    public class RandomizeTransform : MonoBehaviour
    {
        public bool Position = true;
        public Vector3 PositionMinOffset;
        public Vector3 PositionMaxOffset;

        public bool Rotation = true;
        public Vector3 RotationMinOffset;
        public Vector3 RotationMaxOffset;

        public bool Scale = true;
        public Vector3 ScaleMinOffset;
        public Vector3 ScaleMaxOffset;

        private void Start()
        {
            if (Position)
            {
                DoPosition();
            }

            if (Rotation)
            {
                DoRotation();
            }

            if (Scale)
            {
                DoScale();
            }
        }

        private void DoPosition()
        {
            transform.localPosition += KitRandom.Vector3(PositionMinOffset, PositionMaxOffset);
        }

        private void DoRotation()
        {
            transform.rotation *= KitRandom.Rotation(RotationMinOffset, RotationMaxOffset);
        }

        private void DoScale()
        {
            transform.localScale += KitRandom.Vector3(ScaleMinOffset, ScaleMaxOffset);
        }
    }
}
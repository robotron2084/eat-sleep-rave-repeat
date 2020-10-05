using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Moves a GameObject a long a given distance.
    /// </summary>
    public class MoveAlongDistance : MonoBehaviour
    {
        public float Distance = 1f;
        public float Speed = 2f;
        public Axis Axis;

        public bool OnlyRight;
        public bool OnlyLeft;

        private float _start;
        private float _end;
        private float _target;

        private void Awake()
        {
            GetStartAndEnd(out _start, out _end);

            _target = _start;
        }

        private void Update()
        {
            var position = transform.position;
            float pos;
            switch (Axis)
            {
                default:
                case Axis.X:
                    pos = position.x;
                    break;
                case Axis.Y:
                    pos = position.y;
                    break;
                case Axis.Z:
                    pos = position.z;
                    break;
            }

            var distance = Mathf.Abs(pos - _target);
            if (Mathf.Approximately(distance, 0))
            {
                _target = Mathf.Approximately(_target, _start) ? _end : _start;
            }

            switch (Axis)
            {
                default:
                case Axis.X:
                    position.x = _target;
                    break;
                case Axis.Y:
                    position.y = _target;
                    break;
                case Axis.Z:
                    position.z = _target;
                    break;
            }

            transform.position = Vector3.MoveTowards(transform.position, position, Speed * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;

            var position = transform.position;
            GetStartAndEnd(out var axisStart, out var axisEnd);

            Vector3 start;
            Vector3 end;

            switch (Axis)
            {
                default:
                case Axis.X:
                    start = position.WithX(axisStart);
                    end = position.WithX(axisEnd);
                    break;
                case Axis.Y:
                    start = position.WithY(axisStart);
                    end = position.WithY(axisEnd);
                    break;
                case Axis.Z:
                    start = position.WithZ(axisStart);
                    end = position.WithZ(axisEnd);
                    break;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);
            Gizmos.DrawCube(start, Vector3.one * 0.1f);
            Gizmos.DrawCube(end, Vector3.one * 0.1f);
        }

        private void GetStartAndEnd(out float start, out float end)
        {
            var position = transform.position;
            switch (Axis)
            {
                default:
                case Axis.X:
                    if (OnlyRight)
                    {
                        start = position.x + Distance * 2f;
                        end = position.x;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.x - Distance * 2f;
                        end = position.x;
                    }
                    else
                    {
                        start = position.x + Distance * 2f;
                        end = position.x - Distance * 2f;
                    }

                    break;
                case Axis.Y:
                    if (OnlyRight)
                    {
                        start = position.y + Distance * 2f;
                        end = position.y;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.y - Distance * 2f;
                        end = position.y;
                    }
                    else
                    {
                        start = position.y + Distance * 2f;
                        end = position.y - Distance * 2f;
                    }

                    break;
                case Axis.Z:
                    if (OnlyRight)
                    {
                        start = position.z + Distance * 2f;
                        end = position.z;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.z - Distance * 2f;
                        end = position.z;
                    }
                    else
                    {
                        start = position.z + Distance * 2f;
                        end = position.z - Distance * 2f;
                    }

                    break;
            }
        }
    }
}
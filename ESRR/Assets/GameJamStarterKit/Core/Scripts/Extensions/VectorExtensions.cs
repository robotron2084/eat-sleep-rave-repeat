using UnityEngine;

namespace GameJamStarterKit
{
    public static class VectorExtensions
    {
        /// <summary>
        /// returns a new Vector3 with the given x
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 WithX(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }

        /// <summary>
        /// returns a new Vector3 with the given y
        /// </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector3 WithY(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }

        /// <summary>
        /// returns a new Vector3 with the given z
        /// </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 WithZ(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }

        /// <summary>
        /// returns a new Vector2 with the given x
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector2 WithX(this Vector2 v, float x)
        {
            v.x = x;
            return v;
        }

        /// <summary>
        /// returns a new Vector2 with the given y
        /// </summary>
        /// <param name="a"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector2 WithY(this Vector2 a, float y)
        {
            a.y = y;
            return a;
        }

        /// <summary>
        /// returns this as a new Vector3 with the given z
        /// </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector3 AsVector3WithZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        /// <summary>
        /// use this vector to make a euler angle quaternion
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(this Vector3 v)
        {
            return Quaternion.Euler(v);
        }

        /// <summary>
        /// Gets the direction vector from this vector3 to the other.
        /// </summary>
        /// <param name="a">this vector</param>
        /// <param name="target">target vector</param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Vector3 a, Vector3 target)
        {
            return (target - a).normalized;
        }
    }
}
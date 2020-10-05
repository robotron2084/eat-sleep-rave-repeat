using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameJamStarterKit
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// returns Object.Instantiate for a random item with the given parent.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="parent">parent for the instantiated item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Transform parent)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), parent);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given parent in world position or not.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="parent">parent for the instantiated item</param>
        /// <param name="inWorldPosition">spawn in world position or not</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Transform parent, bool inWorldPosition)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), parent, inWorldPosition);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="position">position to instantiate the item at</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Vector3 position)
            where T : Object
        {
            var item = objects.RandomItem();
            var go = item as GameObject;
            var rotation = go != null ? go.transform.rotation : Quaternion.identity;
            return Object.Instantiate(item, position, rotation);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position and rotation
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="position">position to instantiate the item at</param>
        /// <param name="rotation">rotation for the item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects, Vector3 position, Quaternion rotation)
            where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), position, rotation);
        }

        /// <summary>
        /// returns Object.Instantiate for a random item with the given position and rotation
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="position">position to instantiate the item at</param>
        /// <param name="rotation">rotation for the item</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InstantiateRandomItem<T>(this IEnumerable<T> objects,
            Vector3 position,
            Transform parent,
            Quaternion rotation) where T : Object
        {
            return Object.Instantiate(objects.RandomItem(), position, rotation, parent);
        }

        /// <summary>
        /// returns a list of the given type inside the given radius around this GameObject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="radius">how far to check for objects</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindTypeInRadius<T>(this GameObject gameObject, float radius) where T : Object
        {
            return gameObject.transform.FindTypeInRadius<T>(radius);
        }

        /// <summary>
        /// returns a list of the given type inside the given radius around this GameObject without using Physics.
        /// <para><b>This uses FindObjectsOfType and is an expensive method. Avoid using in Update</b></para>
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="radius"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindTypeInRadiusNoCollider<T>(this GameObject gameObject, float radius)
            where T : Component
        {
            return gameObject.transform.FindTypeInRadiusNoCollider<T>(radius);
        }

        /// <summary>
        /// returns true if this GameObject has a component with the given type
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// returns the distance from this GameObject to the target GameObject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="target">game object to get the distance to</param>
        /// <returns></returns>
        public static float Distance(this GameObject gameObject, GameObject target)
        {
            return gameObject.transform.Distance(target.transform);
        }

        /// <summary>
        /// returns the distance from this GameObject to the target transform
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="target">transform to get the distance to</param>
        /// <returns></returns>
        public static float Distance(this GameObject gameObject, Transform target)
        {
            return gameObject.transform.Distance(target);
        }

        /// <summary>
        /// Gets or adds a component of the given type
        /// </summary>
        /// <param name="gameObject">this game object</param>
        /// <typeparam name="T">component type to get or add</typeparam>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// Gets or adds a component of the given type
        /// </summary>
        /// <param name="gameObject">this game object</param>
        /// <param name="type">component type ot get or add</param>
        /// <returns></returns>
        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }

            return component;
        }

        /// <summary>
        /// Returns the path to this game object with an optional separator string
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="separator">string to separate the parent and child. Ex: Parent/Child/Child2</param>
        /// <returns></returns>
        public static string GetPath(this GameObject gameObject, string separator = "/")
        {
            return gameObject.transform.GetPath(separator);
        }
    }
}
using System;
using UnityEngine;


namespace Core.Uitls
{
    public static class Util
    {
        public static T EnsureNotNull<T>(this T? value, string? message = null)
        {
            if (value == null)
            {
                throw new NullReferenceException($"{typeof(T).FullName} {message}");
            }

            return value;
        }


        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }


        public static T Cache<T>(this T value, out T cached)
        {
            cached = value;
            return value;
        }
    }
}
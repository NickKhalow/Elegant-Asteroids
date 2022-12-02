using UnityEngine;


namespace Game
{
    public class Boundaries
    {
        private readonly Vector2Int origin;
        private readonly Vector2 scaled;


        public Boundaries(Vector2Int origin)
        {
            this.origin = origin;
            scaled = new Vector2(100, ((float) origin.y / origin.x) * 100); //TODO fix camera;
        }


        public Vector2 Center()
        {
            return scaled / 2;
        }


        public Vector2 Scaled()
        {
            return scaled;
        }


        public void ApplyTo(Camera camera, float scale = 0.9f)
        {
            var origin = camera.transform.position;
            var newer = (Vector3) scaled / 2;
            newer.z = origin.z;
            camera.transform.position = newer;
            camera.orthographicSize *= scale;
        }


        public Vector2 RandomPointOnEdge()
        {
            bool Chance() => Random.value > 0.5f;
            
            var point = scaled * Random.value;
            if (Chance())
            {
                point.x = Chance() ? 0 : scaled.x;
            }
            else
            {
                point.y = Chance() ? 0 : scaled.y;
            }

            return point;
        }
    }
}
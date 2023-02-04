using UnityEngine;

namespace Util.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 GetRandomVector2PointOnScreen(this Camera camera)
        {
            var spawnY = Random.Range(camera.ScreenToWorldPoint(new Vector2(0, 0)).y, camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            var spawnX = Random.Range(camera.ScreenToWorldPoint(new Vector2(0, 0)).x, camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            return new Vector2(spawnX, spawnY);
        }
        
        public static Vector3 GetRandomVector3PointOnScreen(this Camera camera)
        {
            var spawnY = Random.Range(camera.ScreenToWorldPoint(new Vector2(0, 0)).y, camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            var spawnX = Random.Range(camera.ScreenToWorldPoint(new Vector2(0, 0)).x, camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            return new Vector3(spawnX, spawnY, 0f);
        }

        public static bool IsPositionInScreen(this Vector3 position, Camera camera, float offset = 0f)
        {
            return position.y > camera.ScreenToWorldPoint(new Vector2(0, 0)).y - offset
                   && position.y < camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + offset
                   && position.x > camera.ScreenToWorldPoint(new Vector2(0, 0)).x - offset
                   && position.x < camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + offset;
        }
    }
}

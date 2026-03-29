using UnityEngine;

public class MainMenuClock : MonoBehaviour
{
  public float speed = 100f; // degrees per second

        void Update()
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    
}

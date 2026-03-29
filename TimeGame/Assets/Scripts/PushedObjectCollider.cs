using UnityEngine;

public class PushedObjectCollider : MonoBehaviour
{
   
    public Direction pushDirection;
    private void OnTriggerEnter(Collider other)
    {
        print("start");
        if (other.gameObject.CompareTag("Player"))
        {
            print("start");
            GetComponentInParent<Pushable>().Push(pushDirection, other.GetComponent<PlayerController>());
        }
      
    }
}
public enum Direction { Up, Down, Left, Right }
using UnityEngine;

public class Ferryman : Lock
{
    public GameObject openJaw;
    public GameObject closeJaw;

    
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        print("E");
    }



    public void OpenJaw()
    {
        openJaw.SetActive(true);
        closeJaw.SetActive(false);
    }
}

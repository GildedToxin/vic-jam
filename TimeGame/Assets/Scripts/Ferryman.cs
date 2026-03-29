using UnityEngine;

public class Ferryman : Lock
{
    public GameObject openJaw;
    public GameObject closeJaw;
    public GameObject coin;

    
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
    }


    public override void UseItem()
    {
        coin.SetActive(true);
    }
    public void OpenJaw()
    {
        openJaw.SetActive(true);
        closeJaw.SetActive(false);
    }
}

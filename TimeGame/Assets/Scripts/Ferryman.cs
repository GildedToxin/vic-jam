using UnityEngine;

public class Ferryman : Lock
{
    public GameObject openJaw;
    public GameObject closeJaw;
    public GameObject coin;

    public GameObject fire;
    public GameObject blue;

    
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
    }


    public override void UseItem()
    {
        coin.SetActive(true);
        fire.SetActive(true);
        blue.SetActive(true);
        PromptText = "";
    }
    public void OpenJaw()
    {
        openJaw.SetActive(true);
        closeJaw.SetActive(false);
    }
}

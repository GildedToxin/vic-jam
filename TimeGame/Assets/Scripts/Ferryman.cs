using UnityEngine;

public class Ferryman : Lock
{
    public GameObject openJaw;
    public GameObject closeJaw;
    public GameObject coin;

    public GameObject fire;
    public GameObject blue;

    public AudioSource fireLoopSource;
    public AudioSource fireLightSource;

    private void Start()
    {
        PromptText = "";
    }

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

        PlaySound();
    }
    public void OpenJaw()
    {
        PromptText = "Press E to give the coin";
        openJaw.SetActive(true);
        closeJaw.SetActive(false);
    }

    public void PlaySound()
    {
        fireLightSource.Play();
        fireLoopSource.Play();
    }
}

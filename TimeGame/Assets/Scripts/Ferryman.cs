using UnityEngine;

public class Ferryman : Interactable
{
    public GameObject openJaw;
    public GameObject closeJaw;






    public void OpenJaw()
    {
        openJaw.SetActive(true);
        closeJaw.SetActive(false);
    }
}

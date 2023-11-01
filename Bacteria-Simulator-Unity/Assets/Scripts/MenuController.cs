using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void NewGame() {
        GameManager.Instance.GotoScene("SimulationScene");
    }

    public void GotoLaboratory() {
        GameManager.Instance.GotoScene("LaboratoryScene");
    }

    public void QuitGame() {
        GameManager.Instance.QuitGame();
    }

}

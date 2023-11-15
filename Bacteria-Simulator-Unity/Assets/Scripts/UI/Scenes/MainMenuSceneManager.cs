using UnityEngine;
using TMPro;

public class MainMenuSceneManager : MonoBehaviour
{

    public  TextMeshProUGUI buildVersionText;

    // Start is called before the first frame update
    void Start()
    {
        buildVersionText.text = "Build: " + Application.version;
    }


    public void NewGame() {
        GameManager.Instance.GotoScene("SimulationScene");
    }

    public void GotoLaboratory() {
        GameManager.Instance.GotoScene("LaboratoryScene");
    }

    public void ExitGame() {
        GameManager.Instance.ExitGame();
    }

}

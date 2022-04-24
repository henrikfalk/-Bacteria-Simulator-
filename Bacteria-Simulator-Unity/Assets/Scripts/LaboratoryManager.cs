using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class LaboratoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitLaboratory() {

         // original code to quit Unity player
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            if (GameManager.Instance != null) {
                GameManager.Instance.GotoScene("Menu");
            }
        #endif
    }
}

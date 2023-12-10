using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionHandler : MonoBehaviour, ISelectHandler  { //, IDeselectHandler

    public LaboratorySceneManager laboratorySceneManager;

    public bool IsSelected { get; private set; } = false;
     
    public void OnSelect(BaseEventData eventData)
    {
        IsSelected = true;
        laboratorySceneManager.SimulationSelected(gameObject);
    }
/*
    public void OnDeselect(BaseEventData eventData)
    {
        IsSelected = false;
        laboratorySceneManager.SimulationDeselected(gameObject);
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class TerminalInteraction : MonoBehaviour
{
    public GameObject menu;
    public GameObject player; 
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && !IsMenuOpen())
            {
                ToggleMenu();
            }
        }
    }

    private void ToggleMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public bool IsMenuOpen()
    {
        return menu.activeInHierarchy;
    }
    
    
    
}

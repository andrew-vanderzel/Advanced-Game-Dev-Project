using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public DoorBehavior firstDoor;
    public DoorBehavior secondDoor;

    private void Start()
    {
        firstDoor.open = true;
        secondDoor.open = true;
    }
}

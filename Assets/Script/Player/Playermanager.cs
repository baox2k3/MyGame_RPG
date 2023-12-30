using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermanager : MonoBehaviour
{
    public static Playermanager instance;
    public Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}

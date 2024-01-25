using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterManager
{
    private Jester JesterToFind;
    private static JesterManager instance;
    private JesterManager() { }

    public static JesterManager GetInstance()
    {
        if(instance == null)
        {
            instance = new JesterManager();
        }

        return instance;
    }
    public void GenerateCombinaisonToFind()
    {

    }
}

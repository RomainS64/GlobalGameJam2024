using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJester : MonoBehaviour
{
    // Start is called before the first frame update

    JesterManager JesterMgr;
    void Start()
    {
        JesterMgr = JesterManager.GetInstance();
        if (JesterMgr == null)
        {
            Debug.Log("Unable to create Jester Manager");
            return;
        }
        JesterMgr.GenerateCombinaisonToFind();

        Jester playerJester = new Jester();
        ColorProperty colorProperty = new ColorProperty();
        IJesterPropertyInfo info = new SColor(EColor.Red);
        colorProperty.Info = info;
        playerJester.AddProperty(colorProperty);

        int goodPropertiesFound = JesterMgr.CheckCombinaison(playerJester);

        if(goodPropertiesFound == 1)
        {
            Debug.Log("Found !");
        }
        else
        {
            Debug.Log("Not found...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

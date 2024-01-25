using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAuthorizedProperty
{
    Color,
    Pompom,
    Voice,
    Mask,
    Action
}
public class JesterManager
{
    private Jester JesterToFind;
    private static JesterManager instance;
    public List<EAuthorizedProperty> authorizedProperties;
    private JesterManager() 
    {
        authorizedProperties = new List<EAuthorizedProperty>();
    }

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
        JesterToFind = new Jester();
        ColorProperty colorProperty = new ColorProperty();
        IJesterPropertyInfo info = new SColor(EColor.Red);
        colorProperty.Info = info;
        JesterToFind.AddProperty(colorProperty);
    }

    public int CheckCombinaison(Jester jester)
    {
        return JesterToFind.CompareJester(jester);
    }
}

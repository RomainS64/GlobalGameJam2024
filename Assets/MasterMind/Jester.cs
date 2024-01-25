using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Jester
{
    private List<IJesterProperty> jesterProperties;

    public void AddProperty(IJesterProperty property)
    {
        if (!jesterProperties.Contains(property))
        {
            jesterProperties.Add(property);
        }
    }

    public void SetProperty(IJesterPropertyInfo _info)
    {
        //Change a property ( like color )
    }

    public static bool operator ==(Jester jester1, Jester jester2)
    {
        if (jester1 == null || jester2 == null || jester1.jesterProperties.Count != jester2.jesterProperties.Count)
        {
            return false;
        }
        List<IJesterProperty> tmpJesterProperties = jester2.jesterProperties;
        bool found = false;
        foreach(IJesterProperty curProperty in jester1.jesterProperties)
        {
            foreach (IJesterProperty comparedProperty in tmpJesterProperties)
            {
                if(curProperty == comparedProperty)
                {
                    tmpJesterProperties.Remove(comparedProperty);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return tmpJesterProperties.Count == 0;
    }

    public static bool operator !=(Jester jester1, Jester jester2)
    {
        return !(jester1 == jester2);
    }
}

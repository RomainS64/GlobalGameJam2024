using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Jester
{
    public Jester()
    {
        jesterProperties = new List<IJesterProperty>();
    }

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

    public int CompareJester(Jester jester)
    {
        if(jester == null)
        {
            return 0;
        }
        int goodPropertiesCount = 0;

        List<IJesterProperty> tmpJesterProperties = jester.jesterProperties;
        foreach (IJesterProperty curProperty in this.jesterProperties)
        {
            foreach (IJesterProperty comparedProperty in tmpJesterProperties)
            {
                if (curProperty.CompareProperty(comparedProperty.Info))
                {
                    tmpJesterProperties.Remove(comparedProperty);
                    goodPropertiesCount++;
                    break;
                }
            }
        }

        return goodPropertiesCount;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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
        bool newProperty = true;
        foreach (IJesterProperty curProperty in jesterProperties)
        {
            if (curProperty.GetType() == property.GetType())
            {
                newProperty = false;
            }
        }
        if (newProperty)
        {
            jesterProperties.Add(property);
        }
    }

    public void SetProperty(IJesterPropertyInfo _info)
    {
        foreach(IJesterProperty curProperty in jesterProperties)
        {
            if (curProperty.Info.GetType() == _info.GetType() )
            {
                curProperty.Info = _info;
                break;
            }
        }
    }

    public void RemoveProperty(IJesterProperty property)
    {
        int count = 0;
        foreach (IJesterProperty curProperty in jesterProperties)
        {
            if (curProperty.GetType() == property.GetType())
            {
                jesterProperties.RemoveAt(count);
            }
            count++;
        }
    }

    public int CompareJester(Jester jester)
    {
        if(jester == null)
        {
            return 0;
        }
        int goodPropertiesCount = 0;

        List<IJesterProperty> tmpJesterProperties = new List<IJesterProperty>(jester.jesterProperties);
        foreach (IJesterProperty curProperty in this.jesterProperties)
        {
            foreach (IJesterProperty comparedProperty in tmpJesterProperties)
            {
                if (curProperty.CompareProperty(comparedProperty.Info))
                {
                    tmpJesterProperties.Remove(comparedProperty);
                    goodPropertiesCount++;
                    if (EquipmentManager.Instance.propertiesFoundInRound.TryGetValue(curProperty.Info.GetType().Name, out SFoundInLastRounds propertyStats))
                    {
                        Debug.Log("Dictionnary found");
                        propertyStats.OnFind();
                        EquipmentManager.Instance.propertiesFoundInRound[curProperty.Info.GetType().Name] = propertyStats;
                    }
                    
                    break;
                }
            }
        }
        foreach (var item in EquipmentManager.Instance.propertiesFoundInRound)
        {
            if(item.Value.isNew) 
            {
                EquipmentManager.Instance.OnNewPropertyFound();
                break;
            }
        }

        return goodPropertiesCount;
    }
}

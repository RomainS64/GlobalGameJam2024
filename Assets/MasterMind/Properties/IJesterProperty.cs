using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJesterPropertyInfo
{

}
public interface IJesterProperty
{
    public IJesterPropertyInfo Info { get; set; }
    public bool CompareProperty(IJesterPropertyInfo _info);

}


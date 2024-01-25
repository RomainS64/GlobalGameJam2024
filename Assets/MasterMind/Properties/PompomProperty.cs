using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct SNumberOfPompom : IJesterPropertyInfo
{
    public SNumberOfPompom(int _nbPompom)
    {
        NbPompom = _nbPompom;
    }

    public int NbPompom;

    public bool ComparePompom(SNumberOfPompom numberOfPompom)
    {
        return this.NbPompom == numberOfPompom.NbPompom;
    }
}
public class PompomProperty : IJesterProperty 
{ 
    private SNumberOfPompom nbPompom;
    public IJesterPropertyInfo Info { get => nbPompom; set => nbPompom = (SNumberOfPompom)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SNumberOfPompom)
        {
            return nbPompom.ComparePompom((SNumberOfPompom)Info);
        }
        else
        {
            return false;
        }
    }
}

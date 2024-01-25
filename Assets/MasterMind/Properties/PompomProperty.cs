using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct NumberOfPompom : IJesterPropertyInfo
{
    public NumberOfPompom(int _nbPompom)
    {
        NbPompom = _nbPompom;
    }

    public int NbPompom;

    public bool ComparePompom(NumberOfPompom numberOfPompom)
    {
        return this.NbPompom == numberOfPompom.NbPompom;
    }
}
public class PompomProperty : IJesterProperty 
{ 
    private NumberOfPompom nbPompom;
    public IJesterPropertyInfo Info { get => nbPompom; set => nbPompom = (NumberOfPompom)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        return nbPompom.ComparePompom((NumberOfPompom)Info);
    }
}

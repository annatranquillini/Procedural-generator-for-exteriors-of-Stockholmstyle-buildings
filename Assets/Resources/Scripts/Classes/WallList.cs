using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallList : Symbol
{
    int lenght;
    public Material wallMaterial;

    public WallList(string name) : base(name)
    {

    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        Transform t = this.gameObject.transform;
        lenght = t.childCount;

        for (int i = 0; i < lenght; i++)
        {
            Transform children = t.GetChild(i);
            children.position = t.position + new Vector3(0, 0, i-(float)lenght/2+0.5f);
        }
    }
}

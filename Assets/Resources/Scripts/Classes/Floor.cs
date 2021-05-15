using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Floor : Symbol
{
    public int floorNumber;
    public int sideLenght;
    public bool ground;
    public Material wallMaterial;

    public Floor(string name, bool ground = false) : base(name)
    {

        this.ground = ground;
    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        List<Symbol> wl = symbolsChildren.FindAll(c => c is WallList);
        if (wl.Count > 0)
        {
            sideLenght = wl.Count;

            float angle = 0;

            float s = wl.Count;
            float angleStep = 360 / s;
            foreach (Symbol child in wl)
            {
                WallList w = child as WallList;
                Vector3 vec = new Vector3(0, 0, (sideLenght / (2 * (Mathf.Tan(Mathf.PI / s))))+0.05f);
                w.gameObject.transform.position += Quaternion.Euler(0, angle, 0) * vec;
                w.gameObject.transform.rotation = Quaternion.Euler(0, angle - 90, 0);
                angle += angleStep;
            }
        }

    }
}

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

    public Floor(string name, bool ground=false) : base(name)
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
                Vector3 vec = new Vector3(0, 0, sideLenght / (2 * (Mathf.Tan(Mathf.PI / s))));
                w.gameObject.transform.position += Quaternion.Euler(0, angle, 0) * vec;
                w.gameObject.transform.rotation = Quaternion.Euler(0, angle - 90, 0);
                angle += angleStep;
            }
            return;
        }
        Floor f = symbolsChildren.Find(c => c is Floor) as Floor;
        Door d = symbolsChildren.Find(c => c is Door) as Door;
        Cornice co = symbolsChildren.Find(c => c is Cornice) as Cornice;
        if (f.gameObject!=null && d.gameObject!=null)
        {

            d.gameObject.transform.position = f.gameObject.transform.position + new Vector3(f.sideLenght / 2, 0, 0);
            return;
        }
        
    }
}

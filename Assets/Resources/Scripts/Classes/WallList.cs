using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallList : Symbol
{
    int lenght;

    public WallList(string name) : base(name)
    {

    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        Transform t = this.gameObject.transform;
        List<Symbol> walls = symbolsChildren.FindAll(e => !( e is Door));
        int i = 0;
        Debug.Log("walls: "+walls.Count);
        foreach ( Symbol w in walls)
        {
            w.gameObject.transform.position = t.position + new Vector3(0, 0, i-((float)walls.Count/2)+0.5f);
            i++;
            //MeshRenderer meshRenderer = w.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            Material night = Resources.Load("Materials/Glass", typeof(Material)) as Material;
            //meshRenderer.material = night;
        }

        if (symbolsChildren.Exists(c => c is Door))
        {
            Door d = symbolsChildren.Find(c => c is Door) as Door;
            //d.gameObject.transform.position = new Vector3(0, 0, 0);
        }

    }
}

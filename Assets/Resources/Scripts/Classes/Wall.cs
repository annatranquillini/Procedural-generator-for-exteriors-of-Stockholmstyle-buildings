using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [System.Serializable]

public class Wall : Symbol 
{
    public WallType wallType { get; private set; }

    public enum WallType
    {
        normal
    }

    public Wall(string name,WallType wallType = WallType.normal) : base(name)
    {
        this.wallType = wallType;
    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}

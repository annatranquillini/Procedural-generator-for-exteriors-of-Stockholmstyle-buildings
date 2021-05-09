using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Building :Symbol
{
    int nFloors = 0;
    public Building(string name): base(name)
    {
   
    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        int i = 0;
        Floor lastFloor =null;
        foreach (Symbol el in symbolsChildren)
        {
            Assert.IsNotNull(el.gameObject);
            if (el is Floor)
            {
                (el as Floor).floorNumber = i;
                el.gameObject.transform.position += new Vector3(0, i, 0);
                i++;
                lastFloor = el as Floor;
            }
            else if( el is Roof)
            {
                el.gameObject.transform.position += new Vector3(0, lastFloor.floorNumber+0.5f, 0);
                el.gameObject.transform.localScale +=new  Vector3(lastFloor.sideLenght-1, 0, lastFloor.sideLenght-1);
            }
           
        }
    }

}

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
        float h = 0;
        //Floor lastFloor= symbolsChildren.Find(c => c is Floor && (c as Floor).sideLenght>0) as Floor; 
        float sideLenght = (symbolsChildren.Find(c => c is Body && (c as Body).sideLenght > 0) as Body).sideLenght;
        foreach (Symbol el in symbolsChildren)
        {
            Assert.IsNotNull(el.gameObject);
            if (el is Cornice)
            {
               
                el.gameObject.transform.position += new Vector3(0, h, 0);
                el.gameObject.transform.localScale += new Vector3(sideLenght-1 , 0, sideLenght -1);
                h += 0.1f;
            }
            if (el is Floor)
            {
                (el as Floor).floorNumber = i;
                el.gameObject.transform.position += new Vector3(0, h+0.5f, 0);
                h+=1;
            }
            else if( el is Roof)
            {
                el.gameObject.transform.position += new Vector3(0, h, 0);
                el.gameObject.transform.localScale +=new  Vector3(sideLenght-1, 0, sideLenght-1);
            }
            else if(el is Base)
            {
                el.gameObject.transform.position += new Vector3(0, h, 0);
                el.gameObject.transform.localScale += new Vector3(sideLenght - 1, 0, sideLenght - 1);
                h += 0.2f;
            }
            else if(el is Body)
            {
                el.gameObject.transform.position += new Vector3(0, h, 0);
                h += (el as Body).height;
                sideLenght = (el as Body).sideLenght;
            }
           
        }
    }

}

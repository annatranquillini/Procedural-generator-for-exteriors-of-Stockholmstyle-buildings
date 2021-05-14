using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class Body : Symbol
{
    public float height = 0;
    public float sideLenght = 0;
    public Body(string name) : base(name)
    {

    }

    public override void adapt(List<Symbol> symbolsChildren)
    {
        int i = 0;
        float h = 0;
        foreach (Symbol el in symbolsChildren)
        {
            Assert.IsNotNull(el.gameObject);
            if (el is Floor)
            {
                (el as Floor).floorNumber = i;
                el.gameObject.transform.position += new Vector3(0, h + 0.5f, 0);
                h += 1;
                this.sideLenght = (el as Floor).sideLenght;
            }
            if (el is Cornice)
            {

                el.gameObject.transform.position += new Vector3(0, h, 0);
                el.gameObject.transform.localScale += new Vector3(sideLenght - 1, 0, sideLenght - 1);
                h += 0.1f;
            }
        }
        this.height = h;
        
    }
}

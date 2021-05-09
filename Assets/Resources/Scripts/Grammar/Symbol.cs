using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Symbol : MonoBehaviour
{
    public string name;
    SymbolType symbolType;
    public GameObject gameObject;
    public GameObject prefab;
    enum SymbolType { Terminal, NonTerminal, Start }
    static GameObject g = new GameObject();

    public Symbol(string name)
    {
        this.name = name;
        if (name.Equals("_Building"))
        {
            this.symbolType = SymbolType.Start;
            this.prefab = g;
        }
         else if (!name[0].Equals('_'))
        {
            this.symbolType = SymbolType.Terminal;
            this.prefab = Resources.Load<GameObject>("Modular Buildings/"+name);
        }
        else
        {
            this.symbolType = SymbolType.NonTerminal;
            this.prefab = g;
        }

    }

    public bool isTerminal()
    {
        return this.symbolType == SymbolType.Terminal;
    }
    public bool isNonTerminal()
    {
        return this.symbolType == SymbolType.NonTerminal;
    }
    public bool isStart()
    {
        return this.symbolType == SymbolType.Start;
    }

    public void spawnObject ()
    {
        Assert.IsNotNull(this.prefab);
        GameObject myObject = Instantiate(this.prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        myObject.name = this.name;
        this.gameObject = myObject;
    }

    public virtual void adapt(List<Symbol> symbolsChildren=null)
    {

    }
}

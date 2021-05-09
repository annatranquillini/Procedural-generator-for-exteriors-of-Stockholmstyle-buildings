using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production 
{
    public List<Symbol> children;
    public Symbol father;
    public Production(Symbol father, List<Symbol> children)
    {
        this.father = father;
        this.children = children;
    }

    public bool isAllTerminal()
    {
        return children.TrueForAll(c => c.isTerminal());
    }
}

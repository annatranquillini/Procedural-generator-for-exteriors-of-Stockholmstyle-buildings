using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public Symbol father { get; private set; }
    public List<Production> children { get; private set; }



    public Rule(Symbol father,List<List<Symbol>> children)
    {
        this.father = father;
        this.children = new List<Production>() ;
        for (int i=0; i<children.Count; i++)
        {
            Production p = new Production(father, children[i]);
            this.children.Add(p);
        }        
    }

    public Production getProduction()
    {
        int n = Random.Range(0, children.Count - 1);
        return children[n];
    }

    public Production getAllTerminalProduction()
    {
        for (int i=0; i<children.Count; i++)
        {
            if (children[i].isAllTerminal())
                return children[i];
        }
        return null;
    }

}

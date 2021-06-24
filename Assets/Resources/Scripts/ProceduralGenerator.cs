using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ProceduralGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;
    private Grammar grammar;
    [SerializeField]
    public Material materialWall;
    [SerializeField]
    public Material materialRoof;
    // Start is called before the first frame update
    void Start()
    {
        
        grammar = new Grammar();
        grammar.readRules();
        Production prod = grammar.getStartProduction();
        if (prod != null)
        {
            for(int i=0;i<4; i++)
            {
                setTheme();
                Symbol s=recursive(prod);
                s.gameObject.transform.position += new Vector3(5 * i, 0, 5 * i);
            }

        }

    }
    void setTheme()
    {
        List<Color> wallColors = new List<Color>();
        wallColors.Add(new Color(0.64f, 0.64f, 0.64f));
        //wallColors.Add(new Color(0.64f, 0.64f, 0.80f));
        wallColors.Add(new Color(0.36f, 0.38f, 0.42f));
        wallColors.Add(new Color(0.63f, 0.51f, 0.44f));
        int rnd = UnityEngine.Random.Range(0, wallColors.Count);
        Debug.Log("rnd: "+rnd);
        materialWall.SetColor("_Color", wallColors[rnd]);
        List<Color> roofColors = new List<Color>();
        roofColors.Add(new Color(0.33f, 0.11f, 0.004f));
        roofColors.Add(new Color(0.24f, 0.26f, 0.247f));
        roofColors.Add(new Color(0.19f, 0.29f, 0.23f));
        roofColors.Add(new Color(0.0f, 0.0f, 0.0f));
        rnd = UnityEngine.Random.Range(0, roofColors.Count);
        materialRoof.SetColor("_Color", roofColors[rnd]);
    }

    Symbol recursive(Production prod)
    {
        foreach (Symbol child in prod.children)
        {
            if (child.isNonTerminal())
            {
                Production p = grammar.getProduction(child);
                p.father = child;
                recursive(p);
                Assert.IsNotNull(child.gameObject);
            }
            else
            {
                child.spawnObject();
                child.adapt();
                Assert.IsNotNull(child.gameObject);
            }
        }
        prod.father.spawnObject();
        foreach (Symbol element in prod.children)
        {
            Assert.IsNotNull(element.gameObject);
            element.gameObject.transform.parent = prod.father.gameObject.transform;
        }
        prod.father.adapt(prod.children);
        return prod.father;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

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
        setTheme();
        grammar = new Grammar();
        grammar.readRules();
        Production prod = grammar.getStartProduction();
        if (prod != null)
        {
            GameObject go = GameObject.Find("ProGen");
            //Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);
            recursive(prod);
        }

    }
    void setTheme()
    {
        List<Color> colors = new List<Color>();
        colors.Add(new Color(0.64f, 0.64f, 0.64f));
        colors.Add(new Color(0.64f, 0.64f, 0.80f));
        colors.Add(new Color(0.36f, 0.38f, 0.42f));
        colors.Add(new Color(0.63f, 0.51f, 0.44f));
        int rnd = UnityEngine.Random.Range(0, colors.Count);
        materialWall.SetColor("_Color", colors[rnd]);
        colors.RemoveAt(rnd);
        rnd = UnityEngine.Random.Range(0, colors.Count);
        materialRoof.SetColor("_Color", colors[rnd]);
    }
    void recursive(Production prod)
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}

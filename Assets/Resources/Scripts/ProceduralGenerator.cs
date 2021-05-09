using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ProceduralGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;
    private Grammar grammar;
    // Start is called before the first frame update
    void Start()
    {
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

    void recursive(Production prod)
    {
        //List<Transform> childrenTransform = new List<Transform>();
        foreach (Symbol child in prod.children)
        {
            if (child.isNonTerminal())
            {
                Production p = grammar.getProduction(child);
                p.father = child;
                recursive(p);
                //childrenTransform.Add(p.father.gameObject.transform);
                Assert.IsNotNull(child.gameObject);
            }
            else
            {
                child.spawnObject();
                child.adapt();
                //childrenTransform.Add(prod.children[i].gameObject.transform);
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

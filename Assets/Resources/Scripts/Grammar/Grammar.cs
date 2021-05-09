﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class Grammar
{
    List<Rule> rules;
    Dictionary<string, Symbol> symbols;
    string ruleFileName = "rules.txt";
    int lineNr = 0;


    public Grammar()
    {
        this.rules = new List<Rule>();
        this.symbols = new Dictionary<string, Symbol>();
    }

    public void readRules()
    {
        string filePath = Application.dataPath + "/" + ruleFileName;
        if (!File.Exists(filePath))
        {
            throw new Exception("Rules file " + ruleFileName + " was not found. Has rules not been generated or otherwise defined?");
        }
        string line;
        StreamReader reader = new StreamReader(filePath, Encoding.Default);
        using (reader)
        {
            do
            {
                lineNr++;
                line = reader.ReadLine();
                if (line != null)
                {
                    handleRuleLine(line, lineNr);
                }
            } while (line != null);

            reader.Close();

        }
    }

    public Production getAllTerminalProduction()
    {
        for (int i = 0; i < this.rules.Count; i++)
        {
            Production res = rules[i].getAllTerminalProduction();
            if (res != null)
                return res;
        }
        return null;
    }

    public Production getStartProduction()
    {
        Rule r = rules.Find(ru => ru.father.isStart());
        int rnd = UnityEngine.Random.Range(0, r.children.Count);
        return r.children[rnd];
    }

    public Production getProduction(Symbol symbol)
    {
        //for (int i = 0; i < rules.Count; i++)
        //{
        //    string a = rules[i].father.name;
        //    string b = symbol.name;
        //    if (a.Equals(b))
        //        return rules[i].children[0];
        //}
        Rule  r = rules.Find(ru => ru.father.name.Equals(symbol.name));
        int rnd = UnityEngine.Random.Range(0, r.children.Count );
        if(r.children.Count>1)
            Debug.Log("Text: " + rnd);
        return r.children[rnd];
    }

    private void handleRuleLine(string line, int lineNr)
    {
        // If the line is empty, skip it
        if (line.Trim(' ') == "")
        {
            return;
        }
        // First element should be the From shape, the second an arrow, the third the rule type
        string[] splitLine = line.Split(' ');

        if (splitLine.Length <= 2)
        {
            throwSyntaxError("Rule contains too few components.");
        }
        else if (splitLine[1] != "->")
        {
            throwSyntaxError("Second element is not an arrow ('->').");
        }
        else
        {
            Symbol father = getSymbol(splitLine[0]);
            List<List<Symbol>> rules = new List<List<Symbol>>();
            rules.Add(new List<Symbol>());
            int j = 0;
            for (int i = 2; i < splitLine.Length; i++)
            {
                if (splitLine[i].Equals("|"))
                {
                    j++;
                    rules.Add(new List<Symbol>());
                    continue;
                }
                Symbol symbol = getSymbol(splitLine[i]);
                rules[j].Add(symbol);
            }

            Rule rule = new Rule(father, rules);
            this.rules.Add(rule);

        }
    }
    private Symbol getSymbol(string symbolString)
    {
        Symbol symbol;


        switch (symbolString)
        {
            case "Wall":
                symbol = new Wall(symbolString);
                break;
            case "Window":
                symbol = new Window(symbolString);
                break;
            case "_Roof":
                symbol = new Roof(symbolString);
                break;
            case "_Cornice":
                symbol = new Cornice(symbolString);
                break;
            case "_Door":
                symbol = new Door(symbolString);
                break;
            case "_Floor":
                symbol = new Floor(symbolString);
                break;
            case "_GroundFloor":
                symbol = new Floor(symbolString,true);
                break;
            case "_WallList":
                symbol = new WallList(symbolString);
                break;
            case "_Building":
                symbol = new Building(symbolString);
                break;
            default:
                symbol = new Symbol(symbolString);
                break;
        }


        return symbol;
    }
    private void throwSyntaxError(string error)
    {
        string errorText = "Invalid rule syntax: " + error + "  (line: " + lineNr + ")";
        Debug.LogError(errorText);
        throw new Exception(errorText);
    }


}

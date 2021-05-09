## Procedural generator for exteriors of Stockholm style buildings

<h1> Week 1 </h1>

### Background

This project aims to develop a procedural generator for exteriors (building facades) of Stockholm-style buildings.

The creation of compelling models is a crucial task in the development of successful movies and computer games. However, modeling complex three-dimensional elements, such as buildings, is a very expensive process and can require several man-years worth of labor.\citep{muller} 
A solution to this can either be
to reuse content throughout the world or to utilize procedural-generated content.

The main advantages of procedural generation are to avoid the repetition given by reused content, allow for more realistic scenarios and also give the possibility to generate personalized environments. On the other side, the drawbacks of this technology are the increased computational complexity and the risk of generating non-realistic or non-plausible structures.


### Stockholm style building

Stockholm's architecture has a variegated history that dates back to the 13th century, therefore one of the biggest challenges will be to define what a Stockholm-style building is. 

The project will focus on the generation of buildings in a specific area of the city: the main idea is to create residential/office buildings of the Nyrenässans ( 1800-1900) architectural style which is really common in areas such as Odenplan and Östermalm.

Some of the common characteristics of these houses are:
- Heavy  use of pilasters around windows and everywhere on the facade,
- Small temple-like roofs  above windows and doors,
- Each floor is usually marked with rustics or band effects

Below some pictures showing the chosen architectural style.
<p float="left">
<img src="https://user-images.githubusercontent.com/37111311/115450819-ae2e8380-a21c-11eb-9015-6a48c76fd9a8.jpg" alt="drawing" width="200"/>
<img src="https://user-images.githubusercontent.com/37111311/115450823-aec71a00-a21c-11eb-8bf2-9b6c3f8fcbdb.jpg" alt="drawing" width="200"/>
<img src="https://user-images.githubusercontent.com/37111311/115450826-af5fb080-a21c-11eb-9584-2091c36a41aa.jpg" alt="drawing" width="200"/>
</p>

### Implementation Steps


The procedural generator will be built with the Unity engine, but before starting the implementation further research must be completed.

In particular, the following are the steps that I'm gonna take to work on this project:


1. Creation of the blog
2. Research on procedural generation of buildings and Shape Grammars\cite{muller}
3. Manual generation of a simple Grammar 
4. Coding of algorithms in unity for the grammar and its input in the system
5. Graphical creation of the terminal elements of the grammar in Unity
6. Procedural Generation of simple buildings
7. Creation of a more complex grammar (Manual or Inverse Procedural Generation)
8. Procedural Generation of more complex buildings
9. Post processing (occlusion handling)

### Perceptual Study

The perceptual study will take inspiration from a thesis on the Perception of procedurally generated virtual buildings done in 2018 at a KTH and a thesis on Detailed Procedurally Generated Buildings done in Linköping University\.
The idea at the moment is to present to the participants of the study a set of generated buildings and ask them to rank them based on the perceived variety of the buildings and how realistic they look. Further thought must be put on how to measure whether the set of buildings fit into the 'Stockholm building' category.

### Extensions

The most important possible extensions to the project regard the refinement of the grammar to create a more variegated and realistic building and the refinement of the post processing algorithm to deal with issues such as occlusion.

Another possible extensions is to create an inverse procedural generation algorithm, but this task will probabily fall outside the scope of the project.

<h1> Week 2 </h1>

### Grammar
During the second week I started working on understanding how to develop a grammar and I took inspiration of the work done by Karl Gylleus during his thesis on procedurally generated virtual buildings. Gylleus had made use of a split grammar, which uses the rules: split, repeat, decompose and protrude.

 - **split(X,Y,Z)** splits the left shapes into the given shapes on the right.
 - **repeat(X,Y,Z)** works as the split rule but repeats a pattern as many types as there are space.
 - **decompose(XYZ)** hollows out a cube and leaves the surfaces as thin cubes representing the facades. Simulates going down in dimensionality.
 - **protrude(XYZ)** rescales a shape and moves it accordingly to the new size. Used for example to protrude a pillar/balcony from a facade wall.

By starting from his work and developing my own rules, I manged to create quickly a first prototype of the model. A short video of the first prototype can be seen below.

<div class="embed-container">
  <iframe
      src="https://player.vimeo.com/video/542822172"
      width="500"
      height="281"
      frameborder="0"
      webkitallowfullscreen
      mozallowfullscreen
      allowfullscreen>
  </iframe>
</div>

The next step will be to understand if I can easly manage to adapt this shape grammar to the flexibility I need for the buildings. If not, other ways of implementation will be taken in consideration.

**Papers:** Perception of procedurally generated virtual buildings by Karl Gylleus and Procedural Modeling of Buildings by Muller, Wonka et Al.

<h1> Week 3</h1>

I decided I didn't want to re-use the grammar used in Karl Gylleus's work because it didn't give me the possibility to reach the goal I intended to. In his project, Gylleus, mostly cared about 'printing' the elements of the buildings on different cubes and positioning them together in such a way to make it look like they were there. 


My main focus was to create buildings actually made up of GameObject that represent the different elements of the building (doors,windows,walls,..) and that could eventually be modified by hand after the procedural generation.So, during this week I took a step back and decided to start from scratch in creating my language. 


I decided to find a compromise between using a grammar and adding additional coding. My solution has been to define a class for each element of the house specifying its behaviour. Therefore when the elements of the grammar are generated, two functions are called:   

```cs
public void spawnObject ()
```
```cs
public  void adapt(List<Symbol> symbolsChildren)
```
The first function instantiate a certain symbol of the grammar as a gameObject, while the second adapts the children of that Symbol to the gameObject.

The besic algortihm for the generation of the Building is this:

```cs
    void Start()
    {
        grammar = new Grammar();
        grammar.readRules();
        Production prod = grammar.getStartProduction();
        if (prod != null)
        {
            recursive(prod);
        }

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
            }
            else
            {
                child.spawnObject();
                child.adapt();
            }
        }
        prod.father.spawnObject();
        foreach (Symbol element in prod.children)
        {
            element.gameObject.transform.parent = prod.father.gameObject.transform;
        }
        prod.father.adapt(prod.children);
    }
```

After devising the grammar and building the code needed to support it, I started developing the first elements, such as walls, windows, doors, roofs and writing a basic grammar.
This are the production rules that I've built up at the moment, but I'm planning to further implement other elements and to improve and extend already existing ones.

```
_Building -> _GroundFloor _Floor _Floor _Roof
_Floor -> _Cornice _WallList _WallList _WallList _WallList | _WallList _WallList _WallList _WallList
_GroundFloor -> _Cornice _Floor _Door
_WallList -> Wall Wall Wall Wall
_Roof -> Roof | Roof2 | Roof3
_Door -> Door
_Cornice -> Cornice
```

Here you can see a video showing the kind of building that I am able to generate at the moment. Even if the building is still really simple, I do believe that I am on the right track.

<div class="embed-container">
  <iframe
      src="https://player.vimeo.com/video/547188222"
      width="500"
      height="281"
      frameborder="0"
      webkitallowfullscreen
      mozallowfullscreen
      allowfullscreen>
  </iframe>
</div>




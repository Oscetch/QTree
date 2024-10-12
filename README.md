# QTree
A simple quadtree implementation.  

## Installation

Raw:  
`dotnet add package QTree --version 1.0.4`  
MonoGame .net framework 4.7.2:  
`dotnet add package QTree.MonoGame --version 1.0.3`  
MonoGame .net standard 2.1  
`dotnet add package QTree.MonoGame.Standard --version 1.0.3`  
MonoGame .net 8  
`dotnet add package QTree.MonoGame.Common --version 1.0.1`

## Example usage


    // create the quadtree

    // this tree is faster but constrained to the initial bounds 
    var quadTree = new QuadTree<string>(x: 0, y: 0, width: 2000, height: 2000);

    // this tree is a bit slower but has no constrictions in space
    var dynamicTree = new DynamicQuadTree<string>();
    
    // add some objects to the tree
    for(var x = 0; x < 2000; x += 20)
    {
        for(var y = 0; y < 2000; y += 20)
        {
            quadTree.Add(x, y, 10, 10, "whatevs");
            dynamicTree.Add(x, y, 10, 10, "whatevs");
        }
    }
    
    // find some objects
    var objectsInArea = quadTree.FindNode(500, 500, 50, 50);
    var otherObjectsInSameArea = dynamicTree.Add(x, y, 10, 10, "whatevs");
    
    // remove an object
    quadTree.Remove(objectsInArea[0]);
    dynamicTree.Remove(objectsInArea[0]);
    
    // clear the tree
    quadTree.Clear();
    dynamicTree.Clear();

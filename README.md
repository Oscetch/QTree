# QTree
A simple quadtree implementation

Example usage:

    // create the quadtree
    var quadTree = new QuadTree<string>(x: 0, y: 0, width: 2000, height: 2000);
    
    // add some objects to the tree
    for(var x = 0; x < 2000; x += 20)
    {
        for(var y = 0; y < 2000; y += 20)
        {
            quadTree.Add(x, y, 10, 10, "whatevs");
        }
    }
    
    // find some objects
    var objectsInArea = quadTree.FindNode(500, 500, 50, 50);
    
    // remove an object
    quadTree.Remove(objectsInArea[0]);
    
    // clear the tree
    quadTree.Clear();
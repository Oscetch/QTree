# QTree

A simple quadtree implementation.

## Installation

Raw:  
`dotnet add package QTree --version 1.0.5`  
MonoGame .net 8:  
`dotnet add package QTree.MonoGame.Common --version 1.0.2`

## Example usage

### Quad tree

```cs
// create the quadtree:

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
```

### Ray casting

#### MonoGame

Small disclaimer:
In the monogame framework there is already a class called `Ray` which is used with `Vector3`, and `BoundingBox`. To avoid collision, the `Ray` class in this project is called `QTreeRay` inside the `QTree.MonoGame` namespace

```cs
// create a ray with a start point and direction
var searchStartPosition = new Vector2(100, 100);
var direction = new Vector2(.5f, .5f);
var ray = new QTreeRay(searchStartPostion, direction);

// if you want to use an angle in radians instead you can do
var rayFromRadians = new QTreeRay(searchStartPosition, 3.14f);

// and if you want the angle to be between two positions you can do
var otherPosition = new Vector2(200, 200);
var rayBetweenPositions = QTreeRay.BetweenVectors(searchStartPosition, otherPosition);

// search on the tree

qtree.RayCast(ray, (hitObject, intersection) => {
  // if you're looking for a specific object:
  if (hitObject.IsTheObjectYoureSearchingFor) {
    return RaySearchOption.STOP;
  } else {
    return RaySearchOption.CONTINUE;
  }

  // if you're looking for all objects in the path:
  if (Vector2.Distance(ray.Start, intersection) > YOUR_MAX_DISTANCE) {
    return RaySearchOption.STOP;
  } else {
    aListWhereYouPlaceAllFoundObjects.Add(hitObject);
    return RaySearchOption.CONTINUE;
  }

  // if you want to look at everything in the direction you can just continue passing CONTINUE
  return RaySearchOption.CONTINUE;
});
```

#### Not monogame

```cs
// create a ray with a start point and direction
var searchStartPosition = new Point2D(100, 100);
var direction = new PointF2D(.5f, .5f);
var ray = new Ray(searchStartPostion, direction);

// if you want to use an angle in radians instead you can do
var rayFromRadians = new Ray(searchStartPosition, 3.14f);

// and if you want the angle to be between two positions you can do
var otherPosition = new Point2D(200, 200);
var rayBetweenPositions = Ray.BetweenVectors(searchStartPosition, otherPosition);

// search on the tree

qtree.RayCast(ray, (hitObject, intersection) => {
  // if you're looking for a specific object:
  if (hitObject.IsTheObjectYoureSearchingFor) {
    return RaySearchOption.STOP;
  } else {
    return RaySearchOption.CONTINUE;
  }

  // if you're looking for all objects in the path for a set distance:
  if (Vector2.Distance(ray.Start, intersection) > YOUR_MAX_DISTANCE) {
    return RaySearchOption.STOP;
  } else {
    aListWhereYouPlaceAllFoundObjects.Add(hitObject);
    return RaySearchOption.CONTINUE;
  }

  // if you want to look at everything in the direction you can just continue passing CONTINUE
  return RaySearchOption.CONTINUE;
});
```

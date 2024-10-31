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

## Benchmarks

### Adding a point

Benchmark is located [here](QTree.Benchmarking/AddBenchmarks.cs)

| Method              | ItemsInTree | Depth   | SplitLimit |          Mean |        Error |       StdDev |
| ------------------- | ----------- | ------- | ---------- | ------------: | -----------: | -----------: |
| **AddPoint**        | **10000**   | **5**   | **5**      |  **82.64 ns** | **0.162 ns** | **0.151 ns** |
| DynamicTreeAddPoint | 10000       | 5       | 5          |      82.71 ns |     0.380 ns |     0.336 ns |
| **AddPoint**        | **10000**   | **5**   | **50**     |  **64.58 ns** | **0.283 ns** | **0.251 ns** |
| DynamicTreeAddPoint | 10000       | 5       | 50         |      81.18 ns |     0.381 ns |     0.356 ns |
| **AddPoint**        | **10000**   | **5**   | **100**    |  **64.62 ns** | **0.293 ns** | **0.245 ns** |
| DynamicTreeAddPoint | 10000       | 5       | 100        |      81.60 ns |     0.529 ns |     0.495 ns |
| **AddPoint**        | **10000**   | **10**  | **5**      | **107.77 ns** | **0.283 ns** | **0.265 ns** |
| DynamicTreeAddPoint | 10000       | 10      | 5          |     135.14 ns |     0.324 ns |     0.271 ns |
| **AddPoint**        | **10000**   | **10**  | **50**     |  **71.14 ns** | **0.192 ns** | **0.170 ns** |
| DynamicTreeAddPoint | 10000       | 10      | 50         |     127.48 ns |     0.234 ns |     0.219 ns |
| **AddPoint**        | **10000**   | **10**  | **100**    |  **65.68 ns** | **0.194 ns** | **0.172 ns** |
| DynamicTreeAddPoint | 10000       | 10      | 100        |      99.86 ns |     0.289 ns |     0.270 ns |
| **AddPoint**        | **10000**   | **100** | **5**      | **108.04 ns** | **0.323 ns** | **0.302 ns** |
| DynamicTreeAddPoint | 10000       | 100     | 5          |     848.29 ns |     2.068 ns |     1.833 ns |
| **AddPoint**        | **10000**   | **100** | **50**     |  **65.59 ns** | **0.105 ns** | **0.098 ns** |
| DynamicTreeAddPoint | 10000       | 100     | 50         |     127.20 ns |     0.303 ns |     0.268 ns |
| **AddPoint**        | **10000**   | **100** | **100**    |  **65.92 ns** | **0.060 ns** | **0.053 ns** |
| DynamicTreeAddPoint | 10000       | 100     | 100        |      97.02 ns |     0.180 ns |     0.160 ns |

### Finding a point

Benchmark is located [here](QTree.Benchmarking/FindPointBenchmarks.cs)

| Method               | ItemsInTree | Depth   | SplitLimit |          Mean |         Error |        StdDev |
| -------------------- | ----------- | ------- | ---------- | ------------: | ------------: | ------------: |
| **FindPoint**        | **10000**   | **5**   | **5**      | **270.90 ns** |  **5.477 ns** | **12.137 ns** |
| DynamicTreeFindPoint | 10000       | 5       | 5          |  30,223.47 ns |    223.111 ns |    208.698 ns |
| **FindPoint**        | **10000**   | **5**   | **50**     | **450.84 ns** |  **8.862 ns** | **11.831 ns** |
| DynamicTreeFindPoint | 10000       | 5       | 50         |   5,978.30 ns |     35.328 ns |     33.046 ns |
| **FindPoint**        | **10000**   | **5**   | **100**    | **670.52 ns** | **12.310 ns** | **11.515 ns** |
| DynamicTreeFindPoint | 10000       | 5       | 100        |      38.20 ns |      0.815 ns |      1.088 ns |
| **FindPoint**        | **10000**   | **10**  | **5**      | **305.56 ns** |  **6.058 ns** |  **9.953 ns** |
| DynamicTreeFindPoint | 10000       | 10      | 5          |  34,925.07 ns |     70.888 ns |     66.309 ns |
| **FindPoint**        | **10000**   | **10**  | **50**     | **451.99 ns** |  **8.863 ns** | **10.885 ns** |
| DynamicTreeFindPoint | 10000       | 10      | 50         |   5,921.33 ns |     12.147 ns |     10.143 ns |
| **FindPoint**        | **10000**   | **10**  | **100**    | **660.98 ns** | **12.755 ns** | **14.689 ns** |
| DynamicTreeFindPoint | 10000       | 10      | 100        |      37.82 ns |      0.814 ns |      1.030 ns |
| **FindPoint**        | **10000**   | **100** | **5**      | **297.16 ns** |  **5.962 ns** |  **9.796 ns** |
| DynamicTreeFindPoint | 10000       | 100     | 5          |  43,150.67 ns |    145.498 ns |    136.099 ns |
| **FindPoint**        | **10000**   | **100** | **50**     | **436.63 ns** |  **8.777 ns** |  **8.620 ns** |
| DynamicTreeFindPoint | 10000       | 100     | 50         |   5,922.11 ns |     21.402 ns |     20.020 ns |
| **FindPoint**        | **10000**   | **100** | **100**    | **676.08 ns** | **13.486 ns** | **14.430 ns** |
| DynamicTreeFindPoint | 10000       | 100     | 100        |      38.97 ns |      0.833 ns |      1.054 ns |

### Ray cast

Benchmark is located [here](QTree.Benchmarking/RayCastBenchmarks.cs)

| Method                  | ItemsInTree | Depth   | SplitLimit |            Mean |           Error |          StdDev |
| ----------------------- | ----------- | ------- | ---------- | --------------: | --------------: | --------------: |
| **RayCastPoint**        | **10000**   | **5**   | **5**      | **35,265.8 ns** |   **683.35 ns** | **1,022.81 ns** |
| DynamicTreeRayCastPoint | 10000       | 5       | 5          |    172,338.9 ns |     3,366.68 ns |     4,134.58 ns |
| **RayCastPoint**        | **10000**   | **5**   | **50**     | **40,555.5 ns** |   **807.47 ns** | **1,021.19 ns** |
| DynamicTreeRayCastPoint | 10000       | 5       | 50         |    161,928.0 ns |     3,150.83 ns |     3,502.13 ns |
| **RayCastPoint**        | **10000**   | **5**   | **100**    | **41,600.2 ns** |   **786.92 ns** |   **874.66 ns** |
| DynamicTreeRayCastPoint | 10000       | 5       | 100        |        237.9 ns |         4.81 ns |         6.90 ns |
| **RayCastPoint**        | **10000**   | **10**  | **5**      | **36,947.5 ns** |   **719.90 ns** | **1,009.20 ns** |
| DynamicTreeRayCastPoint | 10000       | 10      | 5          |    209,144.6 ns |     4,026.88 ns |     4,475.86 ns |
| **RayCastPoint**        | **10000**   | **10**  | **50**     | **40,364.2 ns** |   **802.20 ns** | **1,014.53 ns** |
| DynamicTreeRayCastPoint | 10000       | 10      | 50         |    157,122.2 ns |     3,139.09 ns |     3,855.08 ns |
| **RayCastPoint**        | **10000**   | **10**  | **100**    | **41,275.1 ns** |   **821.07 ns** | **1,008.34 ns** |
| DynamicTreeRayCastPoint | 10000       | 10      | 100        |        239.5 ns |         4.67 ns |         6.69 ns |
| **RayCastPoint**        | **10000**   | **100** | **5**      | **91,642.0 ns** | **1,781.23 ns** | **2,926.61 ns** |
| DynamicTreeRayCastPoint | 10000       | 100     | 5          |    206,320.2 ns |       652.77 ns |       578.66 ns |
| **RayCastPoint**        | **10000**   | **100** | **50**     | **39,155.4 ns** |   **225.38 ns** |   **210.82 ns** |
| DynamicTreeRayCastPoint | 10000       | 100     | 50         |    151,230.9 ns |       949.32 ns |       792.73 ns |
| **RayCastPoint**        | **10000**   | **100** | **100**    | **41,906.1 ns** |   **831.93 ns** | **1,110.61 ns** |
| DynamicTreeRayCastPoint | 10000       | 100     | 100        |        242.2 ns |         4.91 ns |         7.65 ns |

### Difference from previous version

[Add point](https://html-preview.github.io/?url=https://github.com/Oscetch/QTree/blob/main/BenchmarkResults/diffs/AddDiff.html)  
[Find point](https://html-preview.github.io/?url=https://github.com/Oscetch/QTree/blob/main/BenchmarkResults/diffs/FindDiff.html)  
[Ray cast](https://html-preview.github.io/?url=https://github.com/Oscetch/QTree/blob/main/BenchmarkResults/diffs/RayCastDiff.html)

Find point speed has gone down significantly from the previous version. Add point seems to be a bit faster, and ray cast has gotten slower. Not sure why that is since it is so dependent on the find function.

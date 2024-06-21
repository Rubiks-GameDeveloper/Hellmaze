using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class BacktrackingAlgorithm : Algorithm
{
    public override MazeCreatorCell[,] GenerateMazeWithAlgorithm(int width, int height)
    {
        var maze = PrepareMaze(width, height);
        
        var current = maze[0, 0];
        current.isVisited = true;
        current.DistanceFromStart = 0;

        var stack = new Stack<MazeCreatorCell>();
        do
        {
            var unvisitedNeighbours = new List<MazeCreatorCell>();

            var x = current.X;
            var z = current.Z;

            if (x > 0 && !maze[x - 1, z].isVisited) unvisitedNeighbours.Add(maze[x - 1, z]);
            if (z > 0 && !maze[x, z - 1].isVisited) unvisitedNeighbours.Add(maze[x, z - 1]);
            if (x < width - 2 && !maze[x + 1, z].isVisited) unvisitedNeighbours.Add(maze[x + 1, z]);
            if (z < height - 2 && !maze[x, z + 1].isVisited) unvisitedNeighbours.Add(maze[x, z + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                var selected = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                RemoveWall(current, selected);

                selected.isVisited = true;
                stack.Push(selected);
                current = selected;
                selected.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } 
        while (stack.Count > 0);

        return maze;
    }
    public override void MazeSpawnExit(MazeCreatorCell[,] maze, int width, int height)
    {
        var data = maze[0, 0];

        for (var x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            if (maze[x, height - 2].DistanceFromStart > data.DistanceFromStart) data = maze[x, height - 2];
            if (maze[x, 0].DistanceFromStart > data.DistanceFromStart) data = maze[x, 0];

            maze[x, height - 1].Floor = false;
        }

        for (var z = maze.GetLength(1) - 1; z >= 0; z--)
        {
            if (maze[width - 2, z].DistanceFromStart > data.DistanceFromStart) data = maze[width - 2, z];
            if (maze[0, z].DistanceFromStart > data.DistanceFromStart) data = maze[0, z];

            maze[width - 1, z].Floor = false;
        }

        if (data.X == 0)
        {
            data.WallLeft = false;
            data.Floor = true;

            data.Exit = true;
        }
        else if (data.Z == 0)
        {
            data.WallBottom = false;
            data.Floor = true;

            data.Exit = true;
        }    
        else if (data.X == width - 2)
        {
            maze[data.X + 1, data.Z].WallLeft = false;
            maze[data.X + 1, data.Z].Floor = true;
            maze[data.X + 1, data.Z].Exit = true;
        }    
        else if (data.Z == height - 2)
        {
            maze[data.X, data.Z + 1].WallBottom = false;
            maze[data.X, data.Z + 1].Floor = true;
            maze[data.X, data.Z + 1].Exit = true;
        }
    }
}

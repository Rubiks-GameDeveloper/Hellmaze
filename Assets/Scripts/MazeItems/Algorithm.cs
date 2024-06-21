using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Algorithm
{
    public MazeCreatorCell[,] CreateMaze(int width, int height, int pointCount)
    {
        var maze = GenerateMazeWithAlgorithm(width, height);
        MazeSpawnExit(maze, width, height);
        MazeStarsSpawner(maze, pointCount);
        
        return maze;
    }
    public MazeCreatorCell[,] PrepareMaze(int width, int height)
    {
        var maze = new MazeCreatorCell[width, height];

        for (var x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            for (var z = maze.GetLength(1) - 1; z >= 0; z--)
            {
                maze[x, z] = new MazeCreatorCell { X = x, Z = z};
            }
        }
        for (var x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            maze[x, height - 1].WallLeft = false;

        }
        for (var y = maze.GetLength(1) - 1; y >= 0; y--)
        {
            maze[width - 1, y].WallBottom = false;
        }
        return maze;
    }
    public abstract MazeCreatorCell[,] GenerateMazeWithAlgorithm(int width, int height);
    public void RemoveWall(MazeCreatorCell a, MazeCreatorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Z > b.Z) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
    public void MazeStarsSpawner(MazeCreatorCell[,] maze, int pointCount)
    {
        for (var f = pointCount; f > 0;)
        {
            var x = Random.Range(3, maze.GetLength(0) - 3);
            var y = Random.Range(3, maze.GetLength(1) - 3);
            if (x == 0 || y == 0) continue;
            maze[x, y].Star = true;
            f--;
        }
    }
    public abstract void MazeSpawnExit(MazeCreatorCell[,] maze, int width, int height);
}
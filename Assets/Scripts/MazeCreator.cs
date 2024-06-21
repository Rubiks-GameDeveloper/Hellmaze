using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public class MazeCreatorCell
{
    public int X;
    public int Z;

    public bool WallLeft = true;
    public bool WallBottom = true;
    public bool Floor = true;
    public bool Star = false;
    public bool Exit = false;

    public bool isVisited = false;
    public int DistanceFromStart;
}
//
public class MazeCreator
{
    private int _pointCount;
    private int _width;
    private int _height;
    private int _difficulty;
    
    // private void MazeCreateWithWilsonAlgorithm(MazeCreatorCell[,] maze)
    // {
    //     List<MazeCreatorCell> createdCells = new List<MazeCreatorCell>();
    //     List<MazeCreatorCell> cycleData = new List<MazeCreatorCell>();
    //     
    //     MazeCreatorCell startCell = maze[0, 0];
    //     MazeCreatorCell current = maze[Random.Range(0, _width - 1), Random.Range(0, _height - 1)];
    //     List<MazeCreatorCell> availableCells = new List<MazeCreatorCell>();
    //     MazeCreatorCell[] distinctArray;
    //
    //     for (int m = 0; m < maze.GetLength(0) - 1; m++)
    //     {
    //         for (int k = 0; k < maze.GetLength(1) - 1; k++)
    //         {
    //             availableCells.Add(maze[m, k]);
    //         }
    //     }
    //
    //     cycleData.Add(current);
    //     do
    //     {
    //         bool isCycleEnd = false;
    //         while (!isCycleEnd)
    //         {
    //             cycleData.Add(current);
    //
    //             int X = current.X;
    //             int Z = current.Z;
    //
    //             current.isVisited = true;
    //
    //             List<MazeCreatorCell> UnvisitedNeibours = new List<MazeCreatorCell>();
    //
    //             if (X > 0) UnvisitedNeibours.Add(maze[X - 1, Z]);
    //             if (Z > 0) UnvisitedNeibours.Add(maze[X, Z - 1]);
    //             if (X < _width - 2) UnvisitedNeibours.Add(maze[X + 1, Z]);
    //             if (Z < _height - 2) UnvisitedNeibours.Add(maze[X, Z + 1]);
    //
    //             MazeCreatorCell selected = new MazeCreatorCell();
    //             if (UnvisitedNeibours.Count > 0) selected = UnvisitedNeibours[Random.Range(0, UnvisitedNeibours.Count)];
    //
    //             if (createdCells.Contains(selected) || selected == startCell)
    //             {
    //                 cycleData.Add(selected);
    //                 break;
    //             }
    //             else if (cycleData.Contains(selected))
    //             {
    //                 cycleData.Clear();
    //             }
    //             else if (selected != current)
    //             {
    //                 cycleData.Add(selected);
    //             }
    //             current = selected;
    //         }
    //         distinctArray = cycleData.Distinct().ToArray();
    //         cycleData = new List<MazeCreatorCell>(distinctArray);
    //         distinctArray = createdCells.Distinct().ToArray();
    //         createdCells = new List<MazeCreatorCell>(distinctArray);
    //         createdCells.AddRange(cycleData);
    //         for (int i = 0; i < cycleData.Count; i++)
    //         {
    //             if (availableCells.Count > 0) availableCells.Remove(cycleData[i]);
    //         }
    //         for (int c = 0; c < cycleData.Count; c++)
    //         {
    //             if (c + 1 < cycleData.Count) RemoveWall(cycleData[c], cycleData[c + 1]);
    //         }
    //         cycleData.Clear();
    //
    //         if(availableCells.Count > 0) current = availableCells[Random.Range(0, availableCells.Count - 1)];
    //     }
    //     while (availableCells.Count > 0);
    // }
    private void MazeExitSpawnerWilsonAlgorithm(MazeCreatorCell[,] maze)
    {
        List<MazeCreatorCell> mazeExitVariants = new List<MazeCreatorCell>();
        MazeCreatorCell exitCell;
        for (int x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            maze[x, _height - 1].Floor = false;
            if (x > 11 && x < _width - 1) mazeExitVariants.Add(maze[x, _height - 2]);
        }
        for (int z = maze.GetLength(1) - 1; z >= 0; z--)
        {
            maze[_width - 1, z].Floor = false;
            if (z > 11 && z < _height - 1) mazeExitVariants.Add(maze[_width - 2, z]);
        }
        var d = mazeExitVariants.Distinct().ToArray();
        mazeExitVariants = new List<MazeCreatorCell>(d);

        exitCell = mazeExitVariants[Random.Range(0, mazeExitVariants.Count)];
        exitCell.Exit = true;
    }
    private void MazeFloorsForming(MazeCreatorCell[,] maze)
    {
        for (int x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            maze[x, _height - 1].Floor = false;
        }
        for (int z = maze.GetLength(1) - 1; z >= 0; z--)
        {
            maze[_width - 1, z].Floor = false;
        }
    }
    private void MazeStarsSpawner(MazeCreatorCell[,] maze)
    {
        for (int f = _pointCount; f > 0;)
        {
            int x = Random.Range(3, maze.GetLength(0) - 3);
            int y = Random.Range(3, maze.GetLength(1) - 3);
            if (x != 0 && y != 0)
            {
                maze[x, y].Star = true;
                f--;
            }
        }
    }
}
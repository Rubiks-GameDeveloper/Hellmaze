using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WilsonAlgorithm : Algorithm
{
    public override MazeCreatorCell[,] GenerateMazeWithAlgorithm(int width, int height)
    {
        var maze = PrepareMaze(width, height);
        
        var createdCells = new List<MazeCreatorCell>();
        var cycleData = new List<MazeCreatorCell>();
        
        var startCell = maze[0, 0];
        var current = maze[Random.Range(0, width - 1), Random.Range(0, height - 1)];
        var availableCells = new List<MazeCreatorCell>();

        for (int m = 0; m < maze.GetLength(0) - 1; m++)
        {
            for (int k = 0; k < maze.GetLength(1) - 1; k++)
            {
                availableCells.Add(maze[m, k]);
            }
        }

        cycleData.Add(current);
        do
        {
            while (true)
            {
                cycleData.Add(current);

                var x = current.X;
                var z = current.Z;

                current.isVisited = true;

                var unvisitedNeighbours = new List<MazeCreatorCell>();

                if (x > 0) unvisitedNeighbours.Add(maze[x - 1, z]);
                if (z > 0) unvisitedNeighbours.Add(maze[x, z - 1]);
                if (x < width - 2) unvisitedNeighbours.Add(maze[x + 1, z]);
                if (z < height - 2) unvisitedNeighbours.Add(maze[x, z + 1]);

                var selected = new MazeCreatorCell();
                if (unvisitedNeighbours.Count > 0) selected = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                if (createdCells.Contains(selected) || selected == startCell)
                {
                    cycleData.Add(selected);
                    break;
                }
                if (cycleData.Contains(selected))
                {
                    cycleData.Clear();
                }
                else if (selected != current)
                {
                    cycleData.Add(selected);
                }
                current = selected;
            }
            var distinctArray = cycleData.Distinct().ToArray();
            cycleData = new List<MazeCreatorCell>(distinctArray);
            distinctArray = createdCells.Distinct().ToArray();
            createdCells = new List<MazeCreatorCell>(distinctArray);
            createdCells.AddRange(cycleData);
            foreach (var t in cycleData)
            {
                if (availableCells.Count > 0) availableCells.Remove(t);
            }
            for (var c = 0; c < cycleData.Count; c++)
            {
                if (c + 1 < cycleData.Count) RemoveWall(cycleData[c], cycleData[c + 1]);
            }
            cycleData.Clear();

            if(availableCells.Count > 0) current = availableCells[Random.Range(0, availableCells.Count - 1)];
        }
        while (availableCells.Count > 0);

        return maze;
    }
    public override void MazeSpawnExit(MazeCreatorCell[,] maze, int width, int height)
    {
        var mazeExitVariants = new List<MazeCreatorCell>();
        for (var x = maze.GetLength(0) - 1; x >= 0; x--)
        {
            maze[x, height - 1].Floor = false;
            if (x > width - 5 && x < width - 1) mazeExitVariants.Add(maze[x, height - 2]);
        }
        for (var z = maze.GetLength(1) - 1; z >= 0; z--)
        {
            maze[width - 1, z].Floor = false;
            if (z > height - 5 && z < height - 1) mazeExitVariants.Add(maze[width - 2, z]);
        }
        var d = mazeExitVariants.Distinct().ToArray();
        mazeExitVariants = new List<MazeCreatorCell>(d);

        var exitCell = mazeExitVariants[Random.Range(0, mazeExitVariants.Count)];
        exitCell.Exit = true;
    }
}

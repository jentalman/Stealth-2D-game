using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration
{
    public int _x;
    public int _y;
    public bool _wallActive = true;
}

public class LevelGeneration
{

    private int _width = 10;
    private int _height = 10;
    private int _direction;

    public WallGeneration[,] GenerateLevel()
    {
        WallGeneration[,] level = new WallGeneration[_width, _height];

        for (int x = 0; x < level.GetLength(0); x++)
        {
            for (int y = 0; y < level.GetLength(1); y++)
            {
                level[x, y] = new WallGeneration { _x = x, _y = y };
            }
        }

        GenerateExitPath(level);
        RemoveWalls(level);
        return level;
    }

    private void RemoveWalls(WallGeneration[,] level)
    {
        foreach (var wall in level)
        {

            int randInt = UnityEngine.Random.Range(1, 12);

            if (randInt > 8)
            {
                wall._wallActive = false;
            }

        }
        int deleteCount = 0;

        for (int x = 0; x < level.GetLength(0); x++)
        {
            int randInt = UnityEngine.Random.Range(1, 12);
            
            if (randInt > 8 && deleteCount < 3)
            {

                deleteCount++;

                for (int y = 0; y < level.GetLength(1); y++)
                {
                    level[x, y]._wallActive = false;
                }
            }
        }

        deleteCount = 0;

        for (int y = 0; y < level.GetLength(1); y++)
        {
            int randInt = UnityEngine.Random.Range(1, 12);
            
            if (y == 0 || randInt > 8 && deleteCount < 3)
            {

                deleteCount++;

                for (int x = 0; x < level.GetLength(1); x++)
                {
                    level[x, y]._wallActive = false;
                }
            }
        }
    }

    private void GenerateExitPath(WallGeneration[,] level)
    {
        int currentX = 0;
        int currentY = 0;
        bool stopGenerating = false;

        WallGeneration currentWall = level[currentX, currentY];
        currentWall._wallActive = false;

        _direction = UnityEngine.Random.Range(1, 6);

        do
        {
            if (_direction == 1 || _direction == 2)
            {

                if (currentX + 1 < _width)
                {
                    currentX++;
                    currentWall = level[currentX, currentY];
                    currentWall._wallActive = false;

                    _direction = UnityEngine.Random.Range(1, 6);
                    if (_direction == 3)
                    {
                        _direction = 2;
                    }
                    else if (_direction == 4)
                    {
                        _direction = 5;
                    }
                }
                else
                {
                    _direction = 5;
                }

            }
            else if (_direction == 3 || _direction == 4)
            {
                if (currentX != 0)
                {
                    currentX--;
                    currentWall = level[currentX, currentY];
                    currentWall._wallActive = false;

                    _direction = UnityEngine.Random.Range(3, 6);
                }
                else
                {
                    _direction = 5;
                }

            }
            else if (_direction == 5 )
            {
                
                if (currentY + 1 < _height)
                {
                    currentY++;
                    currentWall = level[currentX, currentY];
                    currentWall._wallActive = false;

                    _direction = UnityEngine.Random.Range(1, 6);
                }
                else if (currentX + 1 < _width)
                {
                    currentX++;
                    currentWall = level[currentX, currentY];
                    currentWall._wallActive = false;
                }

                else
                {
                    stopGenerating = true;
                }
            }

        }
        while (stopGenerating == false);
    }
}

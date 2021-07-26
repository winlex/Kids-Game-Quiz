using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private List<Vector3> _coordinates = new List<Vector3>();
    private float currentY = 0; //сдвиг вниз по строкам матрицы

    public List<Vector3> Coordinates => _coordinates;
    
    public Grid(int countCells)
    {
        for (int i = 0; i < countCells; i++)
        {
            if(_coordinates.Count == 0)
            {
                _coordinates.Add(new Vector3(0, 0, 0));
                continue;
            }
             else
            {
                if (_coordinates[_coordinates.Count - 1].x == 0)
                {
                    _coordinates[_coordinates.Count - 1] = new Vector3(-1, currentY, 0);
                    _coordinates.Add(new Vector3(1, currentY, 0));
                    continue;
                }
                if (_coordinates[_coordinates.Count - 1].x == 1)
                {
                    _coordinates[_coordinates.Count - 2] = new Vector3(-2, currentY, 0);
                    _coordinates[_coordinates.Count - 1] = new Vector3(0, currentY, 0);
                    _coordinates.Add(new Vector3(2, currentY, 0));
                    continue;
                }
                if (_coordinates[_coordinates.Count - 1].x == 2)
                {
                    currentY -= 2;
                    _coordinates.Add(new Vector3(0, currentY, 0));
                    continue;
                }
            }
        }

        for (int i = 0; i < countCells; i++)
            _coordinates[i] = new Vector3(_coordinates[i].x, _coordinates[i].y + countCells / 4, 0);
    }
}

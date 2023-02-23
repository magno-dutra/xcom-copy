using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject 
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;
    private List<Unit> unitList;

    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList) 
        { 
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

}

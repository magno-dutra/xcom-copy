using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<Unit> unitList = new List<Unit>();
    private List<Unit> friendlyUnitList = new List<Unit>();
    private List<Unit> enemyUnitList = new List<Unit>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more then one UnitManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Remove(unit);

        if(unit.IsEnemy())
            enemyUnitList.Remove(unit);
        else
            friendlyUnitList.Remove(unit);
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Add(unit);

        if (unit.IsEnemy())
            enemyUnitList.Add(unit);
        else
            friendlyUnitList.Add(unit);
    }

    public List<Unit> GetUnitList() { return unitList; }

    public List<Unit> GetFriendlyUnitList() { return friendlyUnitList; }

    public List<Unit> GetEnemyUnitList() { return enemyUnitList; }
}

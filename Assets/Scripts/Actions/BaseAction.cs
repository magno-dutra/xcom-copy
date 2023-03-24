using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionComplete;

    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionsList = GetValidActionGridPositionList();
        return validGridPositionsList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive= false;
        onActionComplete();

        OnAnyActionComplete?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit() { return unit; }
    
    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionsList = new List<EnemyAIAction>();

        List<GridPosition> validActionGridPositionList = GetValidActionGridPositionList();

        foreach (GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionsList.Add(enemyAIAction);
        }

        if(enemyAIActionsList.Count > 0)
        {
            enemyAIActionsList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionsList[0];
        }
        else
        {
            // No possible enemy AI actions
            return null;
        }

    }

    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}

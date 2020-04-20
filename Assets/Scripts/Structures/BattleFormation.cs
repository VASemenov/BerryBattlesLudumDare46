using System.Collections.Generic;
using UnityEngine;

namespace Structures
{
    public struct BattleFormation
    {
        private SoldierFormation[] _formation;
        private int _rows;
        private int _columns;

        public BattleFormation(int capacity, int columns)
        {
            _columns = columns;
            _formation = new SoldierFormation[columns];
            _rows = capacity % columns;
        }

        // public void Add(Soldier soldier)
        // {
        //     var targetColumn = GetSoldierColumn(soldier);
        //     if (_formation[targetColumn].Count() >= _rows)
        //     {
        //         var newSoldierFormation = new SoldierFormation();
        //         _formation[targetColumn].Back = new SoldierFormation();
        //         
        //         newSoldierFormation.Front = _formation[targetColumn].
        //         soldier.row = _formation[targetColumn].Count() - 1;
        //     }
        //         
        // }
        
        // public void Remove()

        public int GetSoldierColumn(Soldier soldier)
        {
            return soldier.id % _rows;
        }

        public Vector2 GetPosition(Soldier soldier)
        {
            return new Vector2(GetSoldierColumn(soldier) * 1.5f, 0);
        }
    }
}
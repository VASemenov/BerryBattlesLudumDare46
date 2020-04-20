using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Structures
{
    public struct BattalionTable
    {
        private Dictionary<int, Soldier> _soldiers;
        private int _nextRecruitId;
        private int _quantity;
        private readonly int _capacity;

        public BattalionTable(int capacity)
        {
            _soldiers = new Dictionary<int, Soldier>();
            
            _capacity = capacity;
            _quantity = 0;
            _nextRecruitId = 0;
        }

        public void Add(Soldier soldier)
        {
            if (_quantity >= _capacity) return;
            
            soldier.id = _nextRecruitId;
            soldier.name = "Soldier " + soldier.id;
            
            _soldiers.Add(soldier.id, soldier);
            _quantity++;
            _nextRecruitId = _quantity;
        }

        public void Kill(Soldier soldier)
        {
            _soldiers.Remove(soldier.id);
            _quantity--;
            _nextRecruitId = _quantity;
        }

        public Dictionary<int, Soldier>.ValueCollection GetAll()
        {
            Debug.Log(_soldiers);
            return _soldiers.Values;
        }
    }
}
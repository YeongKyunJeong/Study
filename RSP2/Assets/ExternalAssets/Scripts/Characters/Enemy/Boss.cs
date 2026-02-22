using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Boss : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void Initialize(NPCandEnemyManager _nPCandEnemyManager, bool isBoss = true)
        {
            base.Initialize(_nPCandEnemyManager, true);
        }
        //protected override void SetActionStateMachine()
        //{
        //    base.SetActionStateMachine();
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class RangeAttackingStateForPlayer : BaseAttackStateForPlayer
    {
        Projectile skillProjectile;

        public RangeAttackingStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Enter(int dataKey)
        {
            attackData = attackDataLibrary.RangeAttackDataList[dataKey];
            base.Enter();
            skillProjectile = ProjectileManager.ShootProjectile(combatSystem, attackData.Projectiles[0], player.transform.position, player.transform.forward);

            skillProjectile.EnterEvent += OnProjectileHit;
            // TODO :: Add resource using logic
        }

        public override void Exit()
        {
            skillProjectile.EnterEvent -= OnProjectileHit;
            base.Exit();
        }

        protected virtual void OnProjectileHit(CombatSystem combatSystem, Collider hitCollider)
        {
            if (!CheckTargetFaction(combatSystem)) return;

            Debug.Log($"{combatSystem.name} Hit");
        }

    }
}

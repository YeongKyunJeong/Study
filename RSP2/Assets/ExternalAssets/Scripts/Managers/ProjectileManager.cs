using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ProjectileManager : MonoSingleton<ProjectileManager>
    {
        private ObjectPool objectPool;
        //public List<Projectile> projectileList;
        private HashSet<Projectile> projectiles;

        private void Awake()
        {
            objectPool = GetComponent<ObjectPool>();
            projectiles = new HashSet<Projectile>();
        }

        // Update is called once per frame
        public void CallUpdate()
        {
            //for (int i = 0; i < projectileList.Count; i++)
            //{
            //    projectileList[i].CallUpdate();
            //}

            if (projectiles.Count == 0)
            {
                return;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.CallUpdate();
            }
            
        }
    }
}

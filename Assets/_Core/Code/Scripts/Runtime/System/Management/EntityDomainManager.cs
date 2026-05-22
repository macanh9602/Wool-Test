using System.Collections.Generic;
using UnityEngine;

namespace WoolGame
{
    public sealed class EntityDomainManager : MonoBehaviour
    {
        private readonly List<IEntityDomain> domains = new List<IEntityDomain>();
        private readonly List<IEntityDomain> pendingRemove = new List<IEntityDomain>();

        public void Register(IEntityDomain domain)
        {
            if (domain != null && !domains.Contains(domain))
            {
                domains.Add(domain);
            }
        }

        public void Unregister(IEntityDomain domain)
        {
            if (domain != null && !pendingRemove.Contains(domain))
            {
                pendingRemove.Add(domain);
            }
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            for (var i = 0; i < domains.Count; i++)
            {
                domains[i].Tick(deltaTime);
            }

            FlushPendingRemove();
        }

        private void OnDestroy()
        {
            for (var i = 0; i < domains.Count; i++)
            {
                domains[i].Dispose();
            }

            domains.Clear();
            pendingRemove.Clear();
        }

        private void FlushPendingRemove()
        {
            if (pendingRemove.Count == 0)
            {
                return;
            }

            for (var i = 0; i < pendingRemove.Count; i++)
            {
                domains.Remove(pendingRemove[i]);
            }

            pendingRemove.Clear();
        }
    }
}

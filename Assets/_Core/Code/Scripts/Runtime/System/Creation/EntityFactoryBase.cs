using UnityEngine;

namespace WoolGame
{
    public abstract class EntityFactoryBase<TData, TDomain, TVisual> : MonoBehaviour
        where TData : EntitySpawnData
        where TDomain : EntityDomainBase
        where TVisual : EntityVisualBase<TDomain>
    {
        [SerializeField] private TVisual visualPrefab;
        [SerializeField] private Transform visualParent;

        public TDomain Create(TData data, EntityDomainManager manager)
        {
            var domain = CreateDomain(data);
            var visual = CreateVisual(data);
            ConfigureVisual(visual, domain, data);
            visual.Bind(domain);
            manager.Register(domain);
            domain.OnCreated();
            return domain;
        }

        protected abstract TDomain CreateDomain(TData data);

        protected virtual void ConfigureVisual(TVisual visual, TDomain domain, TData data)
        {
        }

        private TVisual CreateVisual(TData data)
        {
            if (visualPrefab == null)
            {
                var visualObject = new GameObject(typeof(TVisual).Name);
                visualObject.transform.SetPositionAndRotation(data.spawnPosition, Quaternion.identity);
                return visualObject.AddComponent<TVisual>();
            }

            return Instantiate(visualPrefab, data.spawnPosition, Quaternion.identity, visualParent);
        }
    }
}

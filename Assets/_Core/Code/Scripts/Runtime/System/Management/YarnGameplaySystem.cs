using UnityEngine;

namespace WoolGame
{
    public sealed class YarnGameplaySystem : MonoBehaviour
    {
        [SerializeField] private EntityDomainManager domainManager;
        [SerializeField] private YarnSpoolFactory yarnSpoolFactory;
        [SerializeField] private WoolBlockFactory woolBlockFactory;
        [SerializeField] private YarnThreadFactory yarnThreadFactory;
        [SerializeField] private ConveyorFactory conveyorFactory;

        private int nextEntityId = 1;

        private void Awake()
        {
            ResolveDependencies();
        }

        public ConveyorDomain CreateConveyor(ConveyorSpawnData data)
        {
            ResolveDependencies();
            data.entityId = AllocateEntityId(data.entityId);
            return conveyorFactory.Create(data, domainManager);
        }

        public YarnSpoolDomain CreateYarnSpool(YarnSpoolSpawnData data)
        {
            ResolveDependencies();
            data.entityId = AllocateEntityId(data.entityId);
            return yarnSpoolFactory.Create(data, domainManager);
        }

        public WoolBlockDomain CreateWoolBlock(WoolBlockSpawnData data)
        {
            ResolveDependencies();
            data.entityId = AllocateEntityId(data.entityId);
            return woolBlockFactory.Create(data, domainManager);
        }

        public YarnThreadDomain CreateYarnThread(YarnThreadSpawnData data)
        {
            ResolveDependencies();
            data.entityId = AllocateEntityId(data.entityId);
            return yarnThreadFactory.Create(data, domainManager);
        }

        public bool TryClear(YarnSpoolDomain spool, WoolBlockDomain block)
        {
            if (spool == null || block == null || !spool.CanClear(block))
            {
                return false;
            }

            spool.ConsumeCharge();
            block.ApplyCharge();
            return true;
        }

        private int AllocateEntityId(int requestedId)
        {
            if (requestedId > 0)
            {
                nextEntityId = Mathf.Max(nextEntityId, requestedId + 1);
                return requestedId;
            }

            return nextEntityId++;
        }

        private void ResolveDependencies()
        {
            if (domainManager == null)
            {
                domainManager = GetComponent<EntityDomainManager>();
                if (domainManager == null)
                {
                    domainManager = gameObject.AddComponent<EntityDomainManager>();
                }
            }

            if (yarnSpoolFactory == null)
            {
                yarnSpoolFactory = GetComponent<YarnSpoolFactory>();
                if (yarnSpoolFactory == null)
                {
                    yarnSpoolFactory = gameObject.AddComponent<YarnSpoolFactory>();
                }
            }

            if (woolBlockFactory == null)
            {
                woolBlockFactory = GetComponent<WoolBlockFactory>();
                if (woolBlockFactory == null)
                {
                    woolBlockFactory = gameObject.AddComponent<WoolBlockFactory>();
                }
            }

            if (yarnThreadFactory == null)
            {
                yarnThreadFactory = GetComponent<YarnThreadFactory>();
                if (yarnThreadFactory == null)
                {
                    yarnThreadFactory = gameObject.AddComponent<YarnThreadFactory>();
                }
            }

            if (conveyorFactory == null)
            {
                conveyorFactory = GetComponent<ConveyorFactory>();
                if (conveyorFactory == null)
                {
                    conveyorFactory = gameObject.AddComponent<ConveyorFactory>();
                }
            }
        }
    }
}

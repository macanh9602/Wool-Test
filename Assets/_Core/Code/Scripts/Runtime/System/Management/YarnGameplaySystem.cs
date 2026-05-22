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
            if (domainManager == null)
            {
                domainManager = GetComponent<EntityDomainManager>();
            }
        }

        public ConveyorDomain CreateConveyor(ConveyorSpawnData data)
        {
            data.entityId = AllocateEntityId(data.entityId);
            return conveyorFactory.Create(data, domainManager);
        }

        public YarnSpoolDomain CreateYarnSpool(YarnSpoolSpawnData data)
        {
            data.entityId = AllocateEntityId(data.entityId);
            return yarnSpoolFactory.Create(data, domainManager);
        }

        public WoolBlockDomain CreateWoolBlock(WoolBlockSpawnData data)
        {
            data.entityId = AllocateEntityId(data.entityId);
            return woolBlockFactory.Create(data, domainManager);
        }

        public YarnThreadDomain CreateYarnThread(YarnThreadSpawnData data)
        {
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
    }
}

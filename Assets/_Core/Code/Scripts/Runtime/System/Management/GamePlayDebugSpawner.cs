using Dreamteck.Splines;
using UnityEngine;

namespace WoolGame
{
    public sealed class GamePlayDebugSpawner : MonoBehaviour
    {
        [SerializeField] private bool spawnOnStart = true;
        [SerializeField] private YarnGameplaySystem gameplaySystem;
        [SerializeField] private YarnSpoolFactory yarnSpoolFactory;
        [SerializeField] private SplineComputer conveyorSpline;
        [SerializeField] private Color yarnColor = Color.red;
        [SerializeField] private float spoolSpeed = 0.15f;
        [SerializeField] private Vector3 demoCenter = Vector3.zero;
        [SerializeField] private Vector2 demoSize = new Vector2(5f, 3f);

        private void Awake()
        {
            if (gameplaySystem == null)
            {
                gameplaySystem = GetComponent<YarnGameplaySystem>();
                if (gameplaySystem == null)
                {
                    gameplaySystem = gameObject.AddComponent<YarnGameplaySystem>();
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

            if (conveyorSpline == null)
            {
                conveyorSpline = FindExistingConveyorSpline();
            }

            if (conveyorSpline == null)
            {
                conveyorSpline = CreateFallbackConveyorSpline();
            }

            if (yarnSpoolFactory != null)
            {
                yarnSpoolFactory.SetDefaultConveyorSpline(conveyorSpline);
            }
        }

        private void Start()
        {
            if (!spawnOnStart || gameplaySystem == null)
            {
                return;
            }

            SpawnSample();
        }

        [ContextMenu("Spawn Sample")]
        public void SpawnSample()
        {
            if (conveyorSpline == null)
            {
                conveyorSpline = FindExistingConveyorSpline();
            }

            if (conveyorSpline == null)
            {
                conveyorSpline = CreateFallbackConveyorSpline();
                if (yarnSpoolFactory != null)
                {
                    yarnSpoolFactory.SetDefaultConveyorSpline(conveyorSpline);
                }
            }

            gameplaySystem.CreateYarnSpool(new YarnSpoolSpawnData
            {
                yarnColor = yarnColor,
                chargeCount = 3,
                conveyorProgress = 0f,
                conveyorSpeed = spoolSpeed
            });

            gameplaySystem.CreateWoolBlock(new WoolBlockSpawnData
            {
                requiredColor = yarnColor,
                requiredCharge = 1,
                spawnPosition = demoCenter + new Vector3(0f, 0.8f, 0f)
            });
        }

        private SplineComputer FindExistingConveyorSpline()
        {
            return FindFirstObjectByType<SplineComputer>();
        }

        private SplineComputer CreateFallbackConveyorSpline()
        {
            var pathObject = new GameObject("Fallback Conveyor Spline");
            pathObject.transform.SetParent(transform, false);

            var spline = pathObject.AddComponent<SplineComputer>();
            spline.type = Spline.Type.Linear;

            var halfWidth = demoSize.x * 0.5f;
            var halfHeight = demoSize.y * 0.5f;
            var points = new[]
            {
                new SplinePoint(demoCenter + new Vector3(-halfWidth, 0f, -halfHeight)),
                new SplinePoint(demoCenter + new Vector3(halfWidth, 0f, -halfHeight)),
                new SplinePoint(demoCenter + new Vector3(halfWidth, 0f, halfHeight)),
                new SplinePoint(demoCenter + new Vector3(-halfWidth, 0f, halfHeight))
            };

            spline.SetPoints(points, SplineComputer.Space.World);
            spline.Close();
            CreateFallbackConveyorLineVisual(pathObject.transform, points);
            return spline;
        }

        private void CreateFallbackConveyorLineVisual(Transform parent, SplinePoint[] points)
        {
            var lineObject = new GameObject("Fallback Conveyor Line");
            lineObject.transform.SetParent(parent, false);

            var line = lineObject.AddComponent<LineRenderer>();
            line.useWorldSpace = true;
            line.loop = true;
            line.positionCount = points.Length;
            line.startWidth = 0.06f;
            line.endWidth = 0.06f;
            line.startColor = Color.gray;
            line.endColor = Color.gray;
            line.sharedMaterial = new Material(Shader.Find("Sprites/Default"));

            for (var i = 0; i < points.Length; i++)
            {
                line.SetPosition(i, points[i].position);
            }
        }
    }
}

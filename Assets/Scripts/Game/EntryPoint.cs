using Core.Components.Printers;
using Core.Gameplay.Actors.Asteroids;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Actors.Enemies.UFO;
using Core.Gameplay.Actors.Ship;
using Core.Gameplay.Guns;
using Core.Gameplay.Guns.Ammos;
using Core.Gameplay.Guns.Cooldowns;
using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Guns.Projectiles;
using Core.Gameplay.Identity;
using Core.Gameplay.Scores;
using Core.Gameplay.Spawners;
using Core.Gameplay.Spawners.IImpulses;
using Core.Physics;
using Core.Uitls;
using Game.Canvases;
using Game.Config;
using Game.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;
using View.Actors;
using View.Factory;
using View.Ui;
using View.Util;


namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Configs")] [SerializeField] private SpaceShipConfig spaceShipConfig = null!;
        [SerializeField] private UfoConfig ufoConfig = null!;
        [SerializeField] private SpawnAsteroidsConfig spawnAsteroidsConfig;
        [Header("Ship")] [SerializeField] private SpaceShipView spaceShipView = null!;
        [Header("Factories Projectiles")] [SerializeField]
        private ProjectileViewFactory laserViewFactory = null!;
        [SerializeField] private ProjectileViewFactory bulletViewFactory = null!;
        [Header("Factories Enemies")] [SerializeField]
        private PhysicalViewFactory shardsViewFactory = null!;
        [SerializeField] private PhysicalViewFactory asteroidsViewFactory = null!;
        [SerializeField] private PhysicalViewFactory ufoViewFactory = null!;
        [Header("Spawner")] [SerializeField] private float spawnerCooldown = 4;
        private Core.Gameplay.Game game = null!;
        [Header("UI")] [SerializeField] private SpaceShipUi spaceShipUi = null!;
        [Header("Canvases")] [SerializeField] private LoseCanvas loseUi = null!;

        //TODO object pooling
        private readonly ViewPlug<int> scorePlug = new();


        private void Start()
        {
            var boundaries = new Boundaries(new Vector2Int(Screen.width, Screen.height));
            boundaries.ApplyTo(Camera.main.EnsureNotNull());

            var identityPool = new IdentityPool();

            game = new Core.Gameplay.Game(
                new SpaceShip(
                    new Physical(
                        new PhysicalData(
                            boundaries.Center(),
                            0,
                            Vector2.zero,
                            0,
                            spaceShipConfig.PhysicalDrag
                        ),
                        boundaries.Scaled(),
                        spaceShipView.EnsureNotNull().PhysicalView
                    ),
                    new Gun(
                        new Cooldown(spaceShipConfig.Laser.cooldown),
                        new RefillableAmmo(spaceShipConfig.LaserAmmo),
                        new SimpleFactory<IProjectile>(
                            () => new Projectile(
                                identityPool,
                                laserViewFactory.New(),
                                spaceShipConfig.Laser.projectileLifetime,
                                spaceShipConfig.Laser.projectileSpeed
                            )
                        ),
                        spaceShipView.Laser,
                        new Printer()
                    ),
                    new Gun(
                        new Cooldown(spaceShipConfig.Gun.cooldown),
                        new InfinityAmmo(),
                        new SimpleFactory<IProjectile>(
                            () => new Projectile(
                                identityPool,
                                bulletViewFactory.New(),
                                spaceShipConfig.Gun.projectileLifetime,
                                spaceShipConfig.Gun.projectileSpeed
                            )
                        ),
                        spaceShipView.Gun,
                        new Printer()
                    ),
                    new PlayerInput(),
                    new ViewWrap<SpaceShipData>(
                        spaceShipView,
                        spaceShipUi
                    ),
                    spaceShipConfig.EnsureNotNull().SpaceShipParams,
                    identityPool
                ).Cache(out var player),
                new Enemies(
                    new SpawnerWithShards(
                        identityPool,
                        new TimedSpawner<IEnemy>(
                            new RandomFactory<IEnemy>(
                                new AsteroidsFactory(
                                    identityPool,
                                    asteroidsViewFactory.EnsureNotNull(),
                                    boundaries.Scaled(),
                                    new CombinedImpulse(
                                        () => boundaries.RandomPointOnEdge(),
                                        () => spawnAsteroidsConfig.RandomSpeed,
                                        () => spawnAsteroidsConfig.RandomAngle,
                                        () => spawnAsteroidsConfig.RandomAngularSpeed,
                                        spawnAsteroidsConfig.PhysicalDrag
                                    )
                                ),
                                new UfoFactory(
                                    identityPool,
                                    ufoConfig.EnsureNotNull("Ufo Config is null").UfoParams,
                                    ufoViewFactory.EnsureNotNull(),
                                    boundaries.Scaled(),
                                    player,
                                    new CombinedImpulse(
                                        () => boundaries.RandomPointOnEdge(),
                                        () => spawnAsteroidsConfig.RandomSpeed,
                                        () => 0,
                                        () => 0,
                                        ufoConfig.PhysicalDrag
                                    )
                                )
                            ),
                            new Cooldown(
                                spawnerCooldown
                            )
                        ),
                        shardsViewFactory,
                        new CombinedImpulse(
                            () => boundaries.RandomPointOnEdge(),
                            () => spawnAsteroidsConfig.RandomSpeed,
                            () => 0,
                            () => 0,
                            spawnAsteroidsConfig.PhysicalDrag
                        ),
                        spawnAsteroidsConfig.ShardsCount,
                        boundaries.Scaled()
                    ),
                    new Score(scorePlug),
                    identityPool
                ),
                identityPool
            );
            game.GameOver += OnGameOver;
            loseUi.EnsureNotNull().Hide();
            loseUi.RequestNextGame += LoseUiOnRequestNextGame;
        }


        private void LoseUiOnRequestNextGame()
        {
            SceneManager.LoadScene(0);
            loseUi.RequestNextGame -= LoseUiOnRequestNextGame;
        }


        private void OnGameOver()
        {
            enabled = false;
            loseUi.Show(scorePlug.Data().EnsureNotNull());
        }


        private void Update()
        {
            game.Tick(Time.deltaTime);
        }


        private void OnDestroy()
        {
            game.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private string enemyName;
        public string EnemyName => enemyName;

        [SerializeField]
        private Rigidbody2D rigidBody;
        public Rigidbody2D RigidBody => rigidBody;

        [SerializeField]
        private Collider2D ownCollider;
        public Collider2D OwnCollider => ownCollider;

        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeReference]
        [LabelText("Abilities")]
        private List<IAbility> abilities = new();
        public List<IAbility> Abilities => abilities;

        [SerializeReference]
        [ListDrawerSettings(DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
        [FoldoutGroup("Modules")]
        private List<IModule> modules = new();
        public List<IModule> Modules
        {
            get => modules;
            set => modules = value;
        }
        public List<IAbility> UpdatableAbilities { get; set; } = new();
        public List<IAbility> FixedUpdatableAbilities { get; set; } = new();

        private Transform enemyTransform;
        public Transform Transform => enemyTransform;

        private TankImpl tank;

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            enemyTransform = transform;
            health = Modules.GetConcrete<HealthModule, IModule>().Health.GetModifiedValue();
            abilities.ForEach(ability => ability.Initialize(this, this.tank));
            OnDeath += DeathAction;
        }

        private void DeathAction()
        {
            enemyTransform = null;
            tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            Destroy(gameObject);
        }

        public void FixedUpdate()
        {
            UseAbilities(FixedUpdatableAbilities);
        }

        public void Update()
        {
            UseAbilities(UpdatableAbilities);
        }

        private void UseAbilities(List<IAbility> abilities)
        {
            abilities.ForEach(
                (ability) =>
                {
                    if (ability.IsActive)
                    {
                        ability.Use();
                    }
                }
            );
        }

        public void TakeDamage(float damageAmount)
        {
            if (Health <= 0)
            {
                return;
            }
            health -= damageAmount;
            if (Health <= 0.0f)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }

        public T GetConcreteAbility<T>()
            where T : class, IAbility
        {
            return abilities.GetConcrete<T, IAbility>();
        }

        [Button("GetModules")]
        private void SetModules()
        {
            Modules = new();
            abilities.ForEach((ability) => Modules.AddRange(ability.GetModules()));
            Modules.Add(new HealthModule());
            Modules.Add(new DamageModule());
        }
    }
}

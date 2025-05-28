using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : CombatUnit
    {
        private GameManager gameManager;

        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public MoverForPlayer Mover { get; private set; }
        [field: SerializeField] public ForceReceiverForPlayer ForceReceiver { get; private set; }

        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public PlayerScriptableObject SOData { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public StatHandlerForPlayer StatHandler { get; private set; }
        [field: SerializeField] public CombatSystemForPlayer CombatSystem { get; private set; }
        [field: SerializeField] public AttackHitBox AttackHitBox { get; private set; }
        [field: SerializeField] public InteractionHitBox InteractionHitBox { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }


        [field: SerializeField] public Transform MainCameraTransform { get; private set; }

        [field: SerializeField] public Transform WeaponHolder { get; private set; }// TODO:: Make WeaponHolder class and use it to show weapon
        [field: SerializeField] public Weapon CurrentWeapon { get; private set; }

        public Collider AttackHitBoxCollider { get; private set; }
        public ActionStateMachineForPlayer ActionStateMachine { get; private set; }

        public RuntimeDataForPlayer RuntimeData { get; private set; }


        private void Awake()
        {

            RuntimeData = new RuntimeDataForPlayer();
            ActionStateMachine = new ActionStateMachineForPlayer(this);

            if (SOData == null)
            {
                throw new NotImplementedException("Player Scriptable Object Not Assigned");
            }

            if (InputReader == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }
            InputReader.Initialize(this);

            if (Mover == null)
            {
                throw new NotImplementedException("Player Mover Not Assigned");
            }
            Mover.Initialize(this);

            if (ForceReceiver == null)
            {
                throw new NotImplementedException("Player Force Receiver Not Assigned");
            }

            if (Controller == null)
            {
                throw new NotImplementedException("Player Character Controller Not Assigned");
            }

            if (Animator == null)
            {
                throw new NotImplementedException("Player Animator Not Assigned");
            }

            if (StatHandler == null)
            {
                throw new NotImplementedException("Player StatisticsHandler Not Assigned");
            }

            if (CombatSystem == null)
            {
                throw new NotImplementedException("Player CombatSystem Not Assigned");
            }

            if (AttackHitBox == null)
            {
                throw new NotImplementedException("Player AttackHitBox Not Assigned");
            }
            AttackHitBox.Initialize(SOData.AttackDataLibrary.BaseAttackData.TargetLayerMask);
            RuntimeData.AttackPositionModifier = new Vector3(0, AttackHitBox.HitBoxCollider.bounds.center.y, 0);

            if (InteractionHitBox == null)
            {
                throw new NotImplementedException("Player InteractionHitBox Not Assigned");
            }
            InteractionHitBox.Initialize(this);


            if (MainCameraTransform == null)
            {
                MainCameraTransform = Camera.main.transform;
            }
        }

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }


            if (Inventory == null)
            {
                Inventory = GetComponent<Inventory>();
                if (Inventory == null)
                {
                    throw new NotImplementedException("Player Inventory Not Assigned");
                }
            }
            Inventory.Initialize(gameManager);


            StatHandler.Initialize(gameManager.DataManager.TableDataLoader.BaseStatLoaderForPlayer.GetStat());

            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;


            // Temporary weapon equipment

            AddItem(SOData.WeaponDataLibrary.WeaponData[0]);
            AddItem(SOData.WeaponDataLibrary.WeaponData[1]);
            AddItem(SOData.ConsumableDataLibrary.ConsumableData[0], 4);
            //ItemInstance startWeaponInstance = new ItemInstance(SOData.WeaponDataLibrary.WeaponData[0]);
            //EquipItem(startWeaponInstance);

            //ProjectileManager.ShootProjectile(CombatSystem ,SOData.AttackDataLibrary.RangeAttackDataList[0].Projectiles[0], transform.position, transform.forward);
        }

        private void Update()
        {
            ActionStateMachine.CallUpdate();
            ForceReceiver.CallUpdate();
            Mover.CallUpdate();
        }

        private void FixedUpdate()
        {
            ActionStateMachine.CallPhysicsUpdate();
            ForceReceiver.CallPhysicsUpdate();
            Mover.CallPhysicsUpdate();
        }

        public void EquipItem(ItemInstance item)
        {
            WeaponData weaponData = item.ItemData as WeaponData;
            if (weaponData == null) return;

            if (weaponData.EquipPrefab == null) return;

            if (CurrentWeapon)
            {
                CurrentWeapon.ItemInstance.equipped = false;
                Destroy(CurrentWeapon.gameObject);
            }

            GameObject nextWeaponGO = Instantiate(weaponData.EquipPrefab, WeaponHolder);
            CurrentWeapon = nextWeaponGO.GetComponent<Weapon>();
            CurrentWeapon?.Initialize(item);
            item.equipped = true;
        }

        public bool AddItem(ItemData item, int amount = 1)
        {
            return Inventory.AddItem(item, amount);
            // DOTO :: arrange item stack by left
        }

        private void OnHit()
        {
            ActionStateMachine.OnHit();
        }

        private void OnDie()
        {
            ActionStateMachine.OnDie();
            InputReader.enabled = false;
            // TODO :: Add something to do On Dying;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    IInteractable interactable = other.GetComponent<IInteractable>();
        //    if (interactable != null)
        //    {
        //        //floatingTextManager.CreateFloatingText(interactable.GetInteractMsg(), other.transform.position);
        //        interactable?.OnInteract(this);
        //    }
        //}
    }

}
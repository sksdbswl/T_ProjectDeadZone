using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerSO Data { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
     public Rigidbody Rigidbody { get; private set; }
     public Animator Animator { get; private set; }
     public PlayerInput Input { get; private set; }
     public CharacterController Controller { get; private set; }
     
    public void Initialize()
    {
        AnimationData.Initialize();
        StateMachine = new PlayerStateMachine(this);
        StateMachine.ChangeState(StateMachine.IdleState); // 초기 상태 설정
    }

    private void Start()
    {
         Rigidbody = GetComponent<Rigidbody>();
         Animator = GetComponentInChildren<Animator>();
         Input = GetComponent<PlayerInput>();
         Controller = GetComponent<CharacterController>();
         
        Initialize();
    }

    private void Update()
    {
        StateMachine?.Update();
    }
}

// public class Player : MonoBehaviour
// {
//     [field : Header("Animations")]
//     [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
//
//     [field: Header("References")]
//     [field: SerializeField] public PlayerSO Data { get; private set; }
//     public Rigidbody Rigidbody { get; private set; }
//     public Animator Animator { get; private set; }
//     public PlayerInput Input { get; private set; }
//     public CharacterController Controller { get; private set; }
//     public ForceHandler ForceHandler { get; private set; }
//
//     private PlayerStateMachine stateMachine;
//
//     //public HealthSystem HealthSystem { get; private set; }
//
//     //[field : SerializeField] public Weapon CurrentWeapon { get; private set; }
//     //[field: SerializeField] public Transform WeaponJoint { get; private set; }
//
//     //[field: SerializeField] public LayerMask entityLayerMask { get; private set; }
//
//     public bool isDrawAttackGizmo = false;
//     private GizmoDrawer gizmoDrawer;
//     public GizmoDrawer GizmoDrawer
//     {
//         get
//         {
//             if (!isDrawAttackGizmo) return null;
//             if(gizmoDrawer == null)
//                 gizmoDrawer = this.AddComponent<GizmoDrawer>();
//
//             return gizmoDrawer;
//             
//         }
//     }
//
//     //GameManager _gameManager;
//     //public Inventory Inventory { get; private set; }
//
//     //FloatingTextManager floatingTextManager;
//
//     private void Awake()
//     {
//         Initialize();
//     }
//     
//     public void Initialize()
//     {
//         //_gameManager = gameManager;
//       
//
//         AnimationData.Initialize();
//         //Data.AttackData.Initialize();
//         
//         Rigidbody = GetComponent<Rigidbody>();
//         Animator = GetComponentInChildren<Animator>();
//         Input = GetComponent<PlayerInput>();
//         Controller = GetComponent<CharacterController>();
//         
//         stateMachine = new PlayerStateMachine(this);
//
//         //ForceHandler = GetComponent<ForceHandler>();
//         //HealthSystem = GetComponent<HealthSystem>();
//
//         //stateMachine = new PlayerStateMachine(this);
//
//         // if(CurrentWeapon == null)
//         // {
//         //     CurrentWeapon = GetComponentInChildren<Weapon>();
//         //     CurrentWeapon?.Initialize(entityLayerMask);
//         // }
//         //
//         // Inventory = GetComponent<Inventory>();
//         // Inventory?.Initialize(_gameManager);
//
//     }
//
//     private void Start()
//     {
//         // HealthSystem.OnDamage += stateMachine.TakeDamage;
//         // HealthSystem.OnDie += stateMachine.Die;
//
//         Cursor.lockState = CursorLockMode.Locked;
//         stateMachine.ChangeState(stateMachine.IdleState);
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         stateMachine.HandleInput();
//         stateMachine.Update();
//     }
//
//     private void FixedUpdate()
//     {
//         stateMachine.PhysicsUpdate();
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         IInteractable interactable = other.GetComponent<IInteractable>();
//         // if (interactable != null)
//         // {
//         //     floatingTextManager.CreateFloatingText(interactable.GetInteractMsg(), other.transform.position);
//         //     interactable?.OnInteract(this);
//         // }
//     }
//
//     // public bool AddItem(ItemData item, int amount = 1)
//     // {
//     //     return Inventory.AddItem(item, amount);
//     // }
//
//     public void EquipItem(ItemInstance item)
//     {
//         if (item.ItemData.equipPrefab == null)
//             return;
//
//         // if(CurrentWeapon)
//         // {
//         //     CurrentWeapon.ItemInstance.equipped = false;
//         //     Destroy(CurrentWeapon.gameObject);
//         // }
//         //
//         // GameObject go = Instantiate(item.ItemData.equipPrefab, WeaponJoint);
//         // CurrentWeapon = go.GetComponent<Weapon>();
//         // CurrentWeapon?.Initialize(entityLayerMask, item);
//         // item.equipped = true;
//     }
// }

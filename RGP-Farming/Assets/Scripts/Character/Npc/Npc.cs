using UnityEngine;

public class Npc : CharacterManager
{
    private GameObject _renderingObject;
    private Animator _animator;
    
    [SerializeField] private NpcData _npcData;

    public NpcData NpcData
    {
        get => _npcData;
        set => _npcData = value;
    }

    public bool IsBusy;

    public override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        if (_animator == null) _animator = GetComponentInChildren<Animator>();
        
        _renderingObject = transform.GetChild(0).gameObject;
    }

    public override void Update()
    {
        base.Update();
        bool renderNpc = Utility.PointIsVisibleToCamera(transform.position);
        _renderingObject.SetActive(renderNpc);
        _animator.enabled = renderNpc;
    }
}

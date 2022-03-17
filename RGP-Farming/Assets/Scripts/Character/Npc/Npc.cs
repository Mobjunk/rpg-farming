using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        
        _renderingObject = transform.GetChild(0).gameObject;
        
        if (_npcData != null)
        {

            if (_npcData.randomWalking)
            {
                SetAction(new RandomMovementAction(this));
            }
        }
    }

    public override void Update()
    {
        base.Update();
        bool renderNpc = Utility.PointIsVisibleToCamera(transform.position);
        _renderingObject.SetActive(renderNpc);
        _animator.enabled = renderNpc;
    }
}

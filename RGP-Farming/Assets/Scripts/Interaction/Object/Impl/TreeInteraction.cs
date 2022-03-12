using UnityEngine;

public class TreeInteraction : ObjectInteractionManager
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    private HealthManager _healthManager;
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private GameObject _treeTop;

    [SerializeField] private Animator[] _animators = new Animator[2];

    public void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
        _animators[0] = GetComponentInChildren<Animator>();
        if (_treeTop != null)
            _animators[1] = _treeTop.GetComponent<Animator>();
    }

    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        if (itemBarManager.IsWearingCorrectTool(ToolType.AXE))
        {
            if (pCharacterManager.CharacterAction is TreeInteractionAction) return;
            
            pCharacterManager.SetAction(new TreeInteractionAction(pCharacterManager, new Tree(_healthManager, _animators, _treeTop)));
        }
        else _dialogueManager.StartDialogue("Maybe I should be using a different tool.");
    }
}

public class Tree
{
    public HealthManager HealthManager;
    public Animator[] TreeAnimators;
    public GameObject TreeTop;

    public Tree(HealthManager pHealthManager, Animator[] pTreeAnimators, GameObject pTreeTop)
    {
        HealthManager = pHealthManager;
        TreeAnimators = pTreeAnimators;
        TreeTop = pTreeTop;
    }
}

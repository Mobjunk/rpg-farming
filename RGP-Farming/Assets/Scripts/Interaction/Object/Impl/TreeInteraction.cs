using UnityEngine;

public class TreeInteraction : ObjectInteractionManager
{
    private HealthManager healthManager;
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private GameObject _treeTop;

    [SerializeField] private Animator[] _animators = new Animator[2];

    public void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        _animators[0] = GetComponentInChildren<Animator>();
        if (_treeTop != null)
        {
            _animators[1] = _treeTop.GetComponent<Animator>();
        }
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        if (itemBarManager.IsWearingCorrectTool(ToolType.AXE))
        {
            AbstractToolItem tool = (AbstractToolItem) itemBarManager.GetItemSelected();
            healthManager.TakeDamage(tool.toolDamage);
            if (_animators != null)
            {
                if (_treeTop != null) _animators[1].SetBool("treeHit", true);
                else _animators[0].SetBool("hit", true);
            }
            if (healthManager.CurrentHealth < (healthManager.MaxHealth / 2) && _treeTop != null) _animators[1].SetBool("treeFalling", true);
            Debug.Log($"Tree stump took {tool.toolDamage} damage!");
        } else Debug.LogError("Not the correct tool.");
    }
}

using UnityEngine;

public class TreeInteraction : ObjectInteractionManager
{
    private HealthManager healthManager;
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private GameObject _treeTop;

    [SerializeField] private Animator _animator;

    public void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        if (_treeTop != null) _animator = _treeTop.GetComponent<Animator>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        if (itemBarManager.IsWearingCorrectTool(ToolType.AXE))
        {
            AbstractToolItem tool = (AbstractToolItem) itemBarManager.GetItemSelected();
            healthManager.TakeDamage(tool.toolDamage);
            if (_animator != null)
            {
                _animator.SetBool("treeHit", true);
            }
            if (healthManager.CurrentHealth < (healthManager.MaxHealth / 2) && _treeTop != null)
            {
                _animator.SetBool("treeFalling", true);
                //Destroy(_treeTop);
                //_treeTop = null;
            }
            Debug.Log($"Tree stump took {tool.toolDamage} damage!");
        } else Debug.LogError("Not the correct tool.");
    }
}

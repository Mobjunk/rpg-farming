using UnityEngine;

public class ChestOpener : Opener
{
    private Animator animator;
    private float openingTimer;
    private bool animationRunning, isOpened;
    
    //TODO: Find a better way to do this
    private CharacterManager characterManager;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (openingTimer > 0)
        {
            openingTimer -= Time.deltaTime;
        }
        else if(!isOpened && animationRunning)
        {
            base.Open(characterManager);
            characterManager = null;
            isOpened = true;
            animationRunning = false;
        }
    }

    public override void Open(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        isOpened = false;
        animationRunning = true;
        animator.SetBool("opening", true);
        openingTimer = 0.4f;
    }

    public override void Close(CharacterManager characterManager)
    {
        base.Close(characterManager);
        animator.SetBool("opening", false);
    }

    public override void Interact(CharacterManager characterManager)
    {
        Debug.Log("Handle interaction for " + characterManager.gameObject.name);
        base.Interact(characterManager);
    }

    public override void OnInventoryUIClosing()
    {
        base.OnInventoryUIClosing();
        animator.SetBool("opening", false);
    }
}

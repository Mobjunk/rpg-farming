using UnityEngine;

public class CropsTriggerManager : MonoBehaviour
{
    private Animator _animator;
    private CropsGrowManager _cropsGrowManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cropsGrowManager = GetComponent<CropsGrowManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_cropsGrowManager.CurrentCropCycle > 0) _animator.SetBool("shake", true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            CharacterMovementMananger characterMovementMananger = other.gameObject.GetComponent<CharacterMovementMananger>();
            if (!characterMovementMananger.CurrentDirection.Equals(Vector2.zero) && !_animator.GetBool("shake") && _cropsGrowManager.CurrentCropCycle > 0)
                _animator.SetBool("shake", true);
        }
    }
}

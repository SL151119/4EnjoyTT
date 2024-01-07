using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public enum AnimatorState
    {
        Idle,
        Show,
        Hide
    }
    
    [SerializeField] private Animator _animator;

    //It takes longer to access string format than Hash, so we convert it
    private readonly int Show = Animator.StringToHash("Show"); 
    private readonly int Hide = Animator.StringToHash("Hide");

    private AnimatorState _animatorState = AnimatorState.Idle;

    public void PlayAnimation(AnimatorState state)
    {
        if (_animatorState == state)
            return;

        int trigger = state == AnimatorState.Show 
            ? Show
            : Hide;

        _animator.SetTrigger(trigger);

        _animatorState = state;
    }
}

using UnityEngine;

public class GhostInteractuable : MonoBehaviour
{
    private Animator animator;
    CountdownTimer timer;

    private void Start()
    {
        timer = gameObject.AddComponent<CountdownTimer>();
        timer.Duration = 5f;
        timer.AddTimerFinishedListener(delegate { animator.SetBool("GhostAround", true); });
        timer.Run();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CustomTag.Ghost.ToString())){
            animator.SetBool("GhostAround", true);
        }
    }

    private void AnimFinished()
    {
        animator.SetBool("GhostAround", false);
    }
}

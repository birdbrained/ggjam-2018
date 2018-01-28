using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float waitBeforeDestroying;
	[SerializeField]
	private bool useAnimator = false;
	private Animator ani;
	public bool IsDead { get; set; }

	void Start()
	{
		ani = GetComponent<Animator>();
		IsDead = false;
		if (useAnimator && ani != null)
		{
			StartCoroutine(SetTriggerAfterTime());
		}
	}

	private IEnumerator SetTriggerAfterTime()
	{
		yield return new WaitForSeconds(waitBeforeDestroying);
		ani.SetTrigger("die");
	}

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(waitBeforeDestroying);
        Destroy(gameObject);
    }

    void Update()
    {
		if (!useAnimator)
		{
			StartCoroutine(KillOnAnimationEnd());
		}
		if (IsDead)
		{
			Destroy(gameObject);
		}
    }
}

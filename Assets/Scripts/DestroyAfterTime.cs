using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float waitBeforeDestroying;

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(waitBeforeDestroying);
        Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());
    }
}

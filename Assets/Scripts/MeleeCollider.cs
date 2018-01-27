using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeCollider : MonoBehaviour
{
    [SerializeField]
    private List<string> targetTags;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetTags.Contains(other.tag))
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

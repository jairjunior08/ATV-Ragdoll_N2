using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RagdollAnimation : MonoBehaviour
{
    [SerializeField] private Collider ragdollCollider;
    [SerializeField] float RespawnTime = 30f;
    Rigidbody[] ragdollRigidbodies;
    bool isRagdollActive = false;
    // Start is called before the first frame update
    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool isRagdoll)
    {
        isRagdollActive = !isRagdoll;
        ragdollCollider.enabled = isRagdoll;
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = isRagdoll;

            GetComponent<Animator>().enabled = isRagdoll;

            if (isRagdoll) RandomAnimation();
        }

    }

    void RandomAnimation()
    {
        int randomAnimation = UnityEngine.Random.Range(0, 2);
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Run");
        }
    }

        private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Object") && !isRagdollActive)
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackup());
        }
            
                
        }

    private IEnumerator GetBackup()
    {
        yield return new WaitForSeconds(RespawnTime);
        ToggleRagdoll(true);
    }

    // Update is called once per frame
    void Update()
        {

        }
    }



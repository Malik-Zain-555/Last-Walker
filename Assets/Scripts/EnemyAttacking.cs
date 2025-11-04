using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool canAttack = false;
    private Animator animator;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAttack = true;
            StartCoroutine(AttackLoop());
            animator.SetBool("Walk", false);
            Debug.Log("In range (Attack On, Walk Off)");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAttack = false;
            StopCoroutine(AttackLoop());
            animator.SetBool("Walk", true);
            animator.ResetTrigger("Attack");
            Debug.Log("Not in range (Attack off, Walk On)");
        }
    }

    IEnumerator AttackLoop()
    {
        while(canAttack)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            player.GetComponent<PlayerHealth>().TakeDamage(10);
            yield return new WaitForSeconds(2f);
            Debug.Log("Attack after 2 sec");
        }
    }
}

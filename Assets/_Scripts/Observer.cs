using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class Observer : MonoBehaviour
{
    private Transform player;
    private bool isPlayerInRange;
    private GameEnding gameEnding;

    private void Start()
    {
        player = GameObject.Find("JohnLemon").transform;
        gameEnding = GameObject.Find("GameEnding").GetComponent<GameEnding>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.magenta, Time.deltaTime);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawSphere(transform.position, 1f);
    //}
}

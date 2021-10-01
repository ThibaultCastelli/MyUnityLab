using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class CharacterPathFindingMovement : MonoBehaviour
    {
        // speed
        [SerializeField] [Range(0f, 20f)] float speed;
        // distance to stop or change vector3
        [SerializeField] [Range(0f, 10f)] float distanceStop;

        List<Vector3> path = null;

        int currentIndex = 0;

        public void SetTargetPosition(Vector3 targetPos)
        {
            path = PathFinding.Instance.FindPath(transform.position, targetPos);
            currentIndex = 0;

            if (path != null && path.Count > 1)
                path.RemoveAt(0);

            foreach (var p in path)
                Debug.Log(p);
        }

        void HandleMovement()
        {
            if (path != null)
            {
                if (Vector3.Distance(transform.position, path[currentIndex]) > distanceStop)
                {
                    Vector3 moveDir = (path[currentIndex] - transform.position).normalized;
                    transform.position = transform.position + moveDir * Time.deltaTime * speed;
                }
                else
                {
                    currentIndex++;

                    if (currentIndex >= path.Count)
                        StopMoving();
                }
            }
        }

        void StopMoving()
        {
            path = null;
        }

        private void Update()
        {
            HandleMovement();
        }

        // Get an array of vector3

        // Check if the character is at a greater distance than distMin to the next vector3

        // If so continue to go towards it

        // Else index++

        // If no more vector3 stop
    }
}
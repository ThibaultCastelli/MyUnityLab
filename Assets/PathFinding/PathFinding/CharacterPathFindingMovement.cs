using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class CharacterPathFindingMovement : MonoBehaviour
    {
        #region Variables
        // Rigidbody
        Rigidbody rb;

        // speed
        [SerializeField] [Range(0f, 20f)] float speed;
        // distance to stop or change vector3
        [SerializeField] [Range(0f, 10f)] float distanceStop;

        List<Vector3> path = null;
        Vector3 _previousTargetPos;

        int currentIndex = 0;
        #endregion

        #region Starts & Updates
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }

        private void Start()
        {
            PathFinding.Instance.Grid.OnGridValueChanged += ResetTargetPosition;

        }

        private void FixedUpdate()
        {
            HandleMovement();
        }
        #endregion

        #region Functions
        public void SetTargetPosition(Vector3 targetPos)
        {
            _previousTargetPos = targetPos;

            path = PathFinding.Instance.FindPath(transform.position, targetPos);
            currentIndex = 0;

            if (path != null && path.Count > 1)
                path.RemoveAt(0);
        }

        public void ResetTargetPosition(int x, int y)
        {
            if (path != null)
                SetTargetPosition(_previousTargetPos);
        }

        void HandleMovement()
        {
            if (path != null)
            {
                if (Vector3.Distance(transform.position, path[currentIndex]) > distanceStop)
                {
                    Vector3 moveDir = (path[currentIndex] - transform.position).normalized;
                    if (rb != null)
                        rb.position = transform.position + moveDir * Time.fixedDeltaTime * speed;
                    else
                        transform.position = transform.position + moveDir * Time.fixedDeltaTime * speed;
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
        #endregion

    }
}
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float positionX = Mathf.Lerp(transform.position.x, target.position.x, followSpeed * Time.deltaTime);
        
            transform.position = Vector3.right   * positionX +
                                 Vector3.up      * originalPosition.y +
                                 Vector3.forward * originalPosition.z;
        }
    }
      
}

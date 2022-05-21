#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Vector3 movement;

    [SerializeField]
    private float turnSpeed;
    private Rigidbody _rigidbody;
    private Quaternion rotation = Quaternion.identity;

    private AudioSource _audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
#if USING_MOBILE
        float horizontal = Input.GetAxis("Mouse X");    
        float vertical = Input.GetAxis("Mouse Y");    
        if(Input.touchCount > 0) 
        {
            horizontal = Input.touches[0].deltaPosition.x;
            vertical = Input.touches[0].deltaPosition.y;
        }
#else
        float horizontal = Input.GetAxis("Horizontal");    
        float vertical = Input.GetAxis("Vertical");    
#endif

        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        //Know if object is moving
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        //Set animation On/Off
        _animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if(!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else
            _audioSource.Stop();

        //Que movimiento tengo que hacer para ver a donde quiero
        Vector3 desireForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0);
        //Convierto me movimiento deseado en una rotacion
        rotation = Quaternion.LookRotation(desireForward);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}

using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class JoystickObjectMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3;
    [SerializeField] private float runningSpeed = 5;
    [SerializeField] private GameObject groundChecker;
    private AudioSource _audioWalk;
    
    public Joystick joystick;
    private CharacterController _characterController;
    private Vector3 _velocity;
    private GameObject _ground;
    //public Animator Animator { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 90;
        _characterController = GetComponentInChildren<CharacterController>();
        _audioWalk = GetComponent<AudioSource>();

        //Animator = GetComponentInChildren<Animator>();
        //Animator.SetFloat("Transitor", 1);
    }
    
    private void FixedUpdate()
    {
        MovementApplier();
        GravityApplier();
    }

    private void MovementApplier()
    {
        var transformData = transform;
        var moveTrajectory = transformData.right * joystick.Horizontal +
                             transformData.forward * joystick.Vertical;

        if (joystick.Vertical == 0 && joystick.Horizontal == 0)
        {
           //Animator.SetFloat("Transitor", 0, 0.2f, Time.fixedDeltaTime);
        }
        else if (joystick.Vertical > 0.55f)
        {
            //Animator.SetFloat("Transitor", 1, 0.15f, Time.fixedDeltaTime);
            _characterController.Move(moveTrajectory * Time.fixedDeltaTime * runningSpeed);
            
            _audioWalk.pitch = Random.Range(0.5f, 0.6f);
            if (!_audioWalk.isPlaying)
                _audioWalk.Play();
        }
        else if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            //Animator.SetFloat("Transitor", 0.5f * Math.Abs(joystick.Horizontal), 0.15f, Time.fixedDeltaTime);
            _characterController.Move(moveTrajectory * Time.fixedDeltaTime * movementSpeed);
            
            _audioWalk.pitch = Random.Range(0.5f, 0.6f);
            if (!_audioWalk.isPlaying)
                _audioWalk.Play();
        }
    }

    private void GravityApplier()
    {
        if (!IsGrounded())
        {
            _velocity.y -= 9.81f / 2 * Mathf.Sqrt(Time.fixedDeltaTime);
        }
        else
        {
            _velocity.y = 0;
        }
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        var results = new Collider[20];
        Physics.OverlapSphereNonAlloc(groundChecker.transform.position, 0.3f, results);
        foreach (var item in results)
        {
            // if (item != null && item.CompareTag("Ground"))
            // {
            //     transform.SetParent(item.transform);
            //     break;
            // }
        }
        return results.Any(item => item != null && item.CompareTag("Ground"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundChecker.transform.position, 0.3f);
    }
}

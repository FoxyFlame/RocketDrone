using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] float _mainEngineTrust = 400f;
    [SerializeField] float _sideEngineTrust = 100f;
    [SerializeField] AudioClip _mainEngine;
    [SerializeField] AudioClip _leftSideEngine;
    [SerializeField] AudioClip _rightSideEngine;
    [SerializeField] ParticleSystem _leftSideTrustParticle;
    [SerializeField] ParticleSystem _rightSideTrustParticle;
    [SerializeField] ParticleSystem _mainEngineTrustParticle;

    Rigidbody rbEngineLeft;
    Rigidbody rbEngineRight;
    AudioSource _audioSorce;

    void Start()
    {
        rbEngineLeft = transform.GetChild(0).GetComponent<Rigidbody>();
        rbEngineRight = transform.GetChild(1).GetComponent<Rigidbody>();
        _audioSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        TrustActivation();
        RotationTrustActivation();
    }

    void TrustActivation()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartMainTrusting();
        }
        else 
        {
            StopTrusting();
        }
    }

    void RotationTrustActivation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            StartLeftTrusting();
        }
        else
        {
            StopTrusting();
        }

        if (Input.GetKey(KeyCode.A))
        {
            StartRightTrusting();
        }
        else 
        {
            StopTrusting();
        }
    }

    private void StartMainTrusting()
    {
        rbEngineLeft.AddRelativeForce(Vector3.up * _mainEngineTrust * Time.deltaTime);
        rbEngineRight.AddRelativeForce(Vector3.up * _mainEngineTrust * Time.deltaTime);

        if (!_audioSorce.isPlaying)
        {
            _audioSorce.PlayOneShot(_mainEngine);
        }
        if (!_mainEngineTrustParticle.isPlaying)
        {
            _mainEngineTrustParticle.Play();
        }
    }

    private void StopTrusting()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            _audioSorce.Stop();
            _leftSideTrustParticle.Stop();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _audioSorce.Stop();
            _rightSideTrustParticle.Stop();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _audioSorce.Stop();
            _mainEngineTrustParticle.Stop();
        }
    }

    private void StartRightTrusting()
    {
        rbEngineRight.AddRelativeForce(Vector3.up * _sideEngineTrust * Time.deltaTime);
        if (!_audioSorce.isPlaying)
        {
            _audioSorce.PlayOneShot(_rightSideEngine);
        }
        if (!_rightSideTrustParticle.isPlaying)
        {
            _rightSideTrustParticle.Play();
        }
    }

    private void StartLeftTrusting()
    {
        rbEngineLeft.AddRelativeForce(Vector3.up * _sideEngineTrust * Time.deltaTime);
        if (!_audioSorce.isPlaying)
        {
            _audioSorce.PlayOneShot(_leftSideEngine);
        }
        if (!_leftSideTrustParticle.isPlaying)
        {
            _leftSideTrustParticle.Play();
        }
    }
}

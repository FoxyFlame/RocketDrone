using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionMenager : MonoBehaviour
{
    [SerializeField] float _levelLoadDelay = 3f;
    [SerializeField] float _levelRestartDelay = 2f;
    [SerializeField] AudioClip _explosion;
    [SerializeField] AudioClip _winingSound;
    [SerializeField] ParticleSystem _explosionParticle;
    [SerializeField] ParticleSystem _winingParticle;

    AudioSource _audioSource;

    int _maximumHitedWalls = 3;
    bool _alreadyFinishCrash = false;
    bool _collisionIsON = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheatButtons();
    }

    void CheatButtons()
    {
        if (Input.GetKeyDown(KeyCode.C))    //Collision ON / OFF
        {
            _collisionIsON = !_collisionIsON;
        }

        if (Input.GetKeyDown(KeyCode.L))    //Fast load next lvl
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))    //Restart lvl
        {
            ReloadLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_alreadyFinishCrash || !_collisionIsON)
        {
            return;
        } // pomija wszystko poni¿ej do ponownego za³adowania lvl  /  wy³¹cza kolizje

        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("U back on start? Why?");
                break;
            case "Finish":
                Debug.Log("Wow u made it. <3");
                StartFinishSequence();
                break;
            case "Ground":
                Debug.Log("Time for rest?");
                break;
            default :
                Debug.Log("Dont do that");
                _maximumHitedWalls--;
                if (_maximumHitedWalls < 0)
                {
                    _maximumHitedWalls = 3;
                    StartCrashSequence();
                    
                }
                break;
        }
    }

    void StartFinishSequence()
    {
        _alreadyFinishCrash = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_winingSound);
        _winingParticle.Play();
        GetComponent<DroneMovement>().enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
    }

    void StartCrashSequence()
    {
        _alreadyFinishCrash = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_explosion);
        _explosionParticle.Play();
        GetComponent<DroneMovement>().enabled = false;
        Invoke("ReloadLevel", _levelRestartDelay);
    }

    void ReloadLevel()
    {
        int _currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currrentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int _currrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _nextSceneIndex = _currrentSceneIndex + 1;
        
        if(_nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _nextSceneIndex = 0; //Loop levels
        }

        SceneManager.LoadScene(_nextSceneIndex);
    }
}

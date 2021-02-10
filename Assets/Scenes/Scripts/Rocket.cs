using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //State
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    //
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip GoToNextLevel;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem DeathParticles;
    [SerializeField] ParticleSystem GoToNextLevelParticles;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();

        }
    }
    private void RespondToRotateInput()
    {

        rigidBody.freezeRotation = true;
        
        float rotationThisFrame = Time.deltaTime * rcsThrust;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return;}
        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                break;
            case "Finish":
                StartNexLevelSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }

    }

    
    private void StartNexLevelSequence()
    {
        state = State.Transcending;
        GoToNextLevelParticles.Play();
        NextLevelSound();
        Invoke("LoadNextLevel", levelLoadDelay );
    }
    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        DeathParticles.Play();
        DeathSound();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void NextLevelSound()
    {
        audioSource.PlayOneShot(GoToNextLevel);

    }
    private void DeathSound()
    {
        audioSource.PlayOneShot(Death);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}

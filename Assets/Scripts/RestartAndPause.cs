using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartAndPause : MonoBehaviour
{
    [SerializeField] InputActionReference pauseInput;
    [SerializeField] InputActionReference restartInput;

    [SerializeField] InputActionReference caveInput;
    [SerializeField] InputActionReference testInput;
    [SerializeField] InputActionReference CanyonInput;
    [SerializeField] InputActionReference cityInput;


    



    private bool isPaused = false;


    public void Update()
    {
        Restart();

        if(pauseInput.action.WasReleasedThisFrame())
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    public void Restart()
    {
        if(restartInput.action.WasReleasedThisFrame())
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(cityInput.action.WasReleasedThisFrame())
        {
            SceneManager.LoadScene("City");
        }
        if (caveInput.action.WasReleasedThisFrame())
        {
            SceneManager.LoadScene("Cave");
        }
        if (testInput.action.WasReleasedThisFrame())
        {
            SceneManager.LoadScene("TestLevel");
        }
        if (CanyonInput.action.WasReleasedThisFrame())
        {
            SceneManager.LoadScene("Canyon");
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}

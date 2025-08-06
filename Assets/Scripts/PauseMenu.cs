using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia o menu de pausa do jogo, 
/// Detecta a tecla de pausa (ESC), ativa/desativa o menu de pausa e 
/// controla os botões de continuar e sair do jogo.
/// </summary>

public class PauseMenu : MonoBehaviour
{
    [Header("Referências")]
    public GameObject pauseMenuUI; // Canvas ou painel do menu
    public Button resumeButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);

        // Conecta os botões
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Tecla de pausa (ESC)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Saindo do Jogo...");
#else
        Application.Quit();
#endif
    }
}
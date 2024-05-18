using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] GameObject panelStartGame, panelScore, panelTime, shootingBarrels;
    [SerializeField] List<GameObject> textsFinalPanel, starsPanel;
    public GameObject player;
    public int score, numBarrels;

    private float countdownTimer;
    private bool isTimerRunning;

    //! AWAKE ------------------------------------------
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    //! START ------------------------------------------
    void Start()
    {
        player.GetComponent<EquipWeapon>().onEquipWeapon += ActivateStartPanel; // Al equipar un arma habilita el inicio del minijuego
        player.GetComponent<PlayerInteraction>().onStartGame += StartGame; // Al ponernos sobre la marca y pulsar E se inicia el minijuego
    }

    //! UPDATE ------------------------------------------
    void Update()
    {
        StartCountdown();
    }

    private void ActivateStartPanel()
    {
        panelStartGame.SetActive(true);
    }

    // Empezar minijuego ---------------------------
    private void StartGame()
    {
        panelStartGame.SetActive(false);
        panelScore.SetActive(true);
        panelTime.SetActive(true);
        shootingBarrels.SetActive(true);
        StartTimer();
    }

    // Cuenta atras ---------------------------
    private void StartCountdown()
    {
        if (isTimerRunning)
        {
            countdownTimer -= Time.deltaTime;
            var seconds = (int)countdownTimer;
            panelTime.GetComponentInChildren<Text>().text = seconds.ToString();
            if (countdownTimer <= 0)
            {
                countdownTimer = 0;
                isTimerRunning = false;
                FinishGame();
            }
        }
    }

    private void StartTimer()
    {
        countdownTimer = 60f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Puntos -------------------------------------
    public void AddPoints(int points)
    {
        score += points;
        panelScore.GetComponentInChildren<Text>().text = score.ToString();
    }

    // Datos panel final -------------------------------------
    void FinishGame()
    {
        textsFinalPanel[0].SetActive(true);
        textsFinalPanel[1].GetComponent<Text>().text = numBarrels.ToString();
        textsFinalPanel[2].GetComponent<Text>().text = score.ToString();

        if (score > 0 && score <= 3000) // 1 estrella
        {
            starsPanel[0].GetComponent<Toggle>().isOn = true;
        }
        else if (score > 3000 && score <= 7000) // 2 estrellas
        {
            starsPanel[0].GetComponent<Toggle>().isOn = true;
            starsPanel[1].GetComponent<Toggle>().isOn = true;
        }
        else if (score > 7000) // 3 estrellas
        {
            starsPanel[0].GetComponent<Toggle>().isOn = true;
            starsPanel[1].GetComponent<Toggle>().isOn = true;
            starsPanel[2].GetComponent<Toggle>().isOn = true;
        }
        Cursor.lockState = CursorLockMode.None; // Habilito el cursos para poder seleccionar accion
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

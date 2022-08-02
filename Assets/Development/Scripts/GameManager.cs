using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private BandBodyManager bandBodyPlayer1;
    [SerializeField] private BandBodyManager bandBodyPlayer2;
    [SerializeField] private Image winnerBlue;
    [SerializeField] private Image winnerRed;
    [SerializeField] private Image winnerTie;
    [SerializeField] private Text StartText;
    [SerializeField] private List<GameObject> crowdObjects;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioCrowd;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip gameClip;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private int winner;

    
    public bool _changeClip;
    private float _timeForStart;
    public static bool gameOver;
    
    void Start()
    {
        PlayClip(startClip);
        bandBodyPlayer1.bandManager._canMove = false;
        bandBodyPlayer2.bandManager._canMove = false;
        Time.timeScale = 1;
        gameOver = false;
    }

    
    void Update()
    {
        _timeForStart += Time.deltaTime;
        if (_timeForStart < 5)
            TimeReady();
        if (_timeForStart > 5 && !_changeClip)
        {
            _changeClip = true;
            bandBodyPlayer1.bandManager._canMove = true;
            bandBodyPlayer2.bandManager._canMove = true;
        }
        if (_changeClip && audioSource.clip != gameClip)
        {
            PlayClip(gameClip);
            StartText.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (bandBodyPlayer1.BandMembers.Count == 0 || bandBodyPlayer2.BandMembers.Count == 0)
            gameOver = true;
        if (!gameOver) return;
        if (bandBodyPlayer1.BandMembers.Count > bandBodyPlayer2.BandMembers.Count)
            winner = 1;
        else if (bandBodyPlayer2.BandMembers.Count > bandBodyPlayer1.BandMembers.Count)
            winner = 2;
        else
            winner = 3;
        GameOver();
    }

    void TimeReady()
    {
        if (_timeForStart > 1 && _timeForStart < 2)
            StartText.text = "3";
        else if (_timeForStart > 2 && _timeForStart < 3)
            StartText.text = "2";
        else if (_timeForStart > 3 && _timeForStart < 4)
            StartText.text = "1";
        else if(_timeForStart > 4)
            StartText.text = "GO!";
    }
    public void InitializeGame()
    {
        SceneManager.LoadScene("Opening");
    }

    public void ActiveCrowdObject()
    {
        if (crowdObjects.Count == 0)
            return;
        int ind = Random.Range(0, crowdObjects.Count - 1);
        crowdObjects[ind].gameObject.SetActive(true);
        crowdObjects.Remove(crowdObjects[ind]);
    }

    private void GameOver()
    {
        if(!audioCrowd.isPlaying)
            audioCrowd.Play();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        switch (winner)
        {
            case 1:
                winnerBlue.enabled = true;
                break;
            case 2:
                winnerRed.enabled = true;
                break;
            case 3:
                winnerTie.enabled = true;
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            InitializeGame();
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeValue = 90;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameManager;
    private float _activeCrowdTimer;
    private float _crowdCountdown;

    private void Start()
    {
        _activeCrowdTimer = (timeValue - 30)  / 8;
        _crowdCountdown = 0;
    }

    void Update()
    {
        if (!gameManager.GetComponent<GameManager>()._changeClip) return;
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            _crowdCountdown += Time.deltaTime;

            if (_crowdCountdown >= _activeCrowdTimer)
            {
                gameManager.GetComponent<GameManager>().ActiveCrowdObject();
                _crowdCountdown = 0;
            }
        }
        else
            GameManager.gameOver = true;
        DisplayTime(timeValue);
    }

    void DisplayTime(float time)
    {
        if (time < 0)
            time = 0;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}

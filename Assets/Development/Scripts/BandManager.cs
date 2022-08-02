using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BandManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject bandHead;
    [SerializeField] private GameObject bandBody;
    [SerializeField] private int initialBandMusicians;
    [SerializeField] private List<GameObject> musicians;
    [SerializeField] private List<GameObject> musiciansKeys;
    [SerializeField] private float hitCooldown;
    [SerializeField] private float recoveryTime;
    [SerializeField] private float hitAgainDelay;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    
    
    public bool left = true;
    public List<GameObject> Musicians => musicians;

    #endregion

    #region Fields
    private int _clipsIndex;
    private BandBodyMovement _bandBodyMovement;
    private float _coolDownTimer;
    private float _recoveryTimer;
    private float _hitAgainTimer;
    private Color _headInitColor;
    private List<Vector3> _positionsHistory;

    public List<Vector3> PositionsHistory
    {
        get
        {
            var vector3S = _positionsHistory;
            return vector3S;
        }
    }

    public bool _canMove = true;
    public bool CanMove => _canMove;

    private bool _onRecovery;
    public bool OnRecovery => _onRecovery;

    private bool _canGetHitAgain = true;
    public bool CanGetHitAgain => _canGetHitAgain;

    #endregion

    #region Events

    public event Action AddDrummer;
    public event Action AddSax;
    public event Action AddTube;
    public event Action AddBass;
    public event Action GotHit;
    public event Action GotHitWhenVulnerable;
    public event Action ReturnMovement;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        GotHit += setCooldownTimer;
        GotHit += changeTag;
        GotHit += changeHeadColor;
        GotHit += activeFallingAnim;
        ReturnMovement += setRecoveryTimer;
        ReturnMovement += changeTag;
        ReturnMovement += changeHeadColor;
        ReturnMovement += activeGettingUpAnim;

    }
    
    private void OnDestroy()
    {
        GotHit -= changeTag;
        GotHit -= changeHeadColor;
        GotHit -= setCooldownTimer;
        GotHit -= activeFallingAnim;
        ReturnMovement -= changeTag;
        ReturnMovement -= changeHeadColor;
        ReturnMovement -= setRecoveryTimer;
        ReturnMovement -= activeGettingUpAnim;
    }

    private void Start()
    {
        _bandBodyMovement = bandBody.GetComponent<BandBodyMovement>();
        for (int i = 0; i < initialBandMusicians; i++)
        {
            int musicianInd = Random.Range(0, musiciansKeys.Count - 1);
            InvokeAddMusician(musiciansKeys[musicianInd].gameObject.tag);
        }

        _headInitColor = bandHead.GetComponent<SpriteRenderer>().color;
        _positionsHistory = new List<Vector3>();
    }

    private void Update()
    {
        if (_coolDownTimer > 0)
        {
            _coolDownTimer -= Time.deltaTime;
            if (!(_coolDownTimer <= 0)) return;
            InvokeReturnMovement();
            _coolDownTimer = 0;
        }

        if (_recoveryTimer > 0)
        {
            _recoveryTimer -= Time.deltaTime;
            if (_recoveryTimer < 0)
            {
                _onRecovery = false;
                bandHead.GetComponent<Animator>().SetBool("OnRecovery",false);
                _recoveryTimer = 0;
            }
        }

        if (_hitAgainTimer > 0)
        {
            _hitAgainTimer -= Time.deltaTime;
            if (_hitAgainTimer < 0)
            {
                _canGetHitAgain = true;
                _hitAgainTimer = 0;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (bandHead.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            _positionsHistory.Insert(0, bandHead.transform.position);
            _bandBodyMovement.MoveBandBody();
        }
    }

    #endregion

    #region Methods

    private void changeTag()
    {
        if (gameObject.CompareTag("Band"))
        {
            gameObject.tag = "Vulnerable";
        }
        else
        {
            gameObject.tag = "Band";
        }
    }

    private void setCooldownTimer()
    {
        _canMove = false;
        _coolDownTimer = hitCooldown;
    }
    
    private void setRecoveryTimer()
    {
        _recoveryTimer = recoveryTime;
        bandHead.GetComponent<Animator>().SetBool("OnRecovery",true);
        _onRecovery = true;
    }
    
    private void changeHeadColor()
    {
        if( bandHead.GetComponent<SpriteRenderer>().color == _headInitColor)
            bandHead.GetComponent<SpriteRenderer>().color = Color.gray;
        else
            bandHead.GetComponent<SpriteRenderer>().color = _headInitColor;
    }

    private void activeFallingAnim()
    {
        bandHead.GetComponent<Animator>().SetTrigger("Fallen");
        bandHead.GetComponent<AudioSource>().Play();
        foreach (var muisician in bandBody.GetComponent<BandBodyManager>().BandMembers)
        {
            muisician.GetComponent<Animator>().SetTrigger("Fallen");
        }
    }

    private void activeGettingUpAnim()
    {
        bandHead.GetComponent<Animator>().SetTrigger("GetUp");
        foreach (var muisician in bandBody.GetComponent<BandBodyManager>().BandMembers)
        {
            muisician.GetComponent<Animator>().SetTrigger("GetUp");
        }
    }
    
    public void InvokeAddMusician(string tag)
    {
        switch (tag)
        {
            case "Drums":
                _clipsIndex = 0;
                AddDrummer?.Invoke();
                break;
            case "Sax":
                _clipsIndex = 1;
                AddSax?.Invoke();
                break;
            case "Tube":
                _clipsIndex = 2;
                AddTube?.Invoke();
                break;
            case "Bass":
                _clipsIndex = 3;
                AddBass?.Invoke();
                break;
        }
        audioSource.clip = audioClips[_clipsIndex];
        audioSource.Play();
    }

    public void InvokeGotHit()
    {
        GotHit?.Invoke();
    }
    
    public void InvokeGotHitWhenVulnerable()
    {
        _canGetHitAgain = false;
        _hitAgainTimer = hitAgainDelay;
        GotHitWhenVulnerable?.Invoke();
    }

    private void InvokeReturnMovement()
    {
        _canMove = true;
        ReturnMovement?.Invoke();
    }
    #endregion
}
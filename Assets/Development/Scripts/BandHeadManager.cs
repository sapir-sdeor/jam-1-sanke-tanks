using System.Linq;
using UnityEngine;

public class BandHeadManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private BandManager bandManager;
    [SerializeField] private BandBodyManager bandBodyManager;

    #endregion

    #region Fields

    private bool _bandHeadBlue;
    public bool _startMove;

    private void Awake()
    {
        _bandHeadBlue = gameObject.CompareTag("BandHead");
        GetComponent<Animator>().SetBool("BandHeadBlue",_bandHeadBlue);
    }

    public void StartMove()
    {
        _startMove = true;
    }
    
    private readonly string[] _musiciansKeyTags = {"Drums", "Sax", "Tube", "Bass"};
    private readonly string[] _musiciansPlayerTags = {"Drummer", "SaxPlayer", "TubePlayer", "BassPlayer"};

    #endregion

    #region MonoBehaviour
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_startMove)
            return;
        
        if (_musiciansKeyTags.Contains(other.gameObject.tag)) // Head - key
        {
            bandManager.InvokeAddMusician(other.gameObject.tag);
            Destroy(other.gameObject);
        }
        
        if (bandManager.OnRecovery) return;

        if (_musiciansPlayerTags.Contains(other.gameObject.tag)) // Head - body 
        {
            if (other.gameObject == bandBodyManager.BandMembers[0] )
                return;

            if (!other.gameObject.transform.parent.transform.parent.CompareTag("Vulnerable"))
            {
                if (bandManager.CanMove)
                    transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHit();
            }

            else
            {
                if (other.transform.parent.gameObject.GetComponentInParent<BandManager>().CanGetHitAgain)
                    other.transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHitWhenVulnerable();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (bandManager.OnRecovery || !bandManager.CanMove) return;

        if (other.gameObject.CompareTag("Barrier"))
        {
            bandManager.InvokeGotHit();
        }
        
        else if (other.gameObject.CompareTag("BandHead2")) // Head - Head 
        {
            if (other.gameObject.transform.parent.gameObject.CompareTag("Vulnerable")) // if band2 vulnerable
                other.transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHitWhenVulnerable();
            else if (gameObject.transform.parent.gameObject.CompareTag("Vulnerable")) // if band1 vulnerable
                transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHitWhenVulnerable();
            
            else // if both are not vulnerable
            {
                other.transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHit();
                transform.parent.gameObject.GetComponentInParent<BandManager>().InvokeGotHit();
            }
        }
    }

    #endregion

    #region Methods
    
    #endregion
}
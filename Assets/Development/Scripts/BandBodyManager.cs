
using System.Collections.Generic;
using UnityEngine;

public class BandBodyManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] public BandManager bandManager;
    [SerializeField] public GameObject bandHead;
    [SerializeField] private List<GameObject> bandMembers;
    public List<GameObject> BandMembers => bandMembers;

    #endregion

    #region Fields

    private bool _bandHeadBlue;
    private GameObject _drummer;
    private GameObject _bassPlayer;
    private GameObject _tubePlayer;
    private GameObject _saxPlayer;

    #endregion

    private void Awake()
    {
        _bandHeadBlue = bandHead.gameObject.CompareTag("BandHead");
        SaveMusicians();
        bandManager.AddDrummer += AddDrummer;
        bandManager.AddSax += AddSaxPlayer;
        bandManager.AddTube += AddTubePlayer;
        bandManager.AddBass += AddBassPlayer;
        bandManager.GotHitWhenVulnerable += RemoveMusiciansFromList;
    }

    private void OnDestroy()
    {
        bandManager.AddDrummer -= AddDrummer;
        bandManager.AddSax -= AddSaxPlayer;
        bandManager.AddTube -= AddTubePlayer;
        bandManager.AddBass -= AddBassPlayer;
        bandManager.GotHitWhenVulnerable -= RemoveMusiciansFromList;
    }

    private void AddDrummer()
    {
        AddMusician(_drummer);
    }

    private void AddBassPlayer()
    {
        AddMusician(_bassPlayer);
    }

    private void AddTubePlayer()
    {
        AddMusician(_tubePlayer);
    }

    private void AddSaxPlayer()
    {
        AddMusician(_saxPlayer);
    }


    private void SaveMusicians()
    {
        foreach (var musician in bandManager.Musicians)
        {
            if (musician.gameObject.CompareTag("Drummer"))
                _drummer = musician;
            else if (musician.gameObject.CompareTag("TubePlayer"))
                _tubePlayer = musician;
            else if (musician.gameObject.CompareTag("SaxPlayer"))
                _saxPlayer = musician;
            else if (musician.gameObject.CompareTag("BassPlayer"))
                _bassPlayer = musician;
        }
    }

    private void AddMusician(GameObject musician)
    {
        GameObject temp;
        musician.layer = bandHead.layer;
        musician.GetComponent<SpriteRenderer>().flipX = bandHead.GetComponent<SpriteRenderer>().flipX;
        if (bandMembers.Count == 0)
        {
            Vector3 pos = bandHead.gameObject.transform.position;
            temp = Instantiate(musician, pos, transform.rotation, transform);
        }
        else
        {
            Vector3 pos = bandMembers[bandMembers.Count - 1].transform.position;
            temp = Instantiate(musician, pos, transform.rotation, transform);
        }

        bandMembers.Add(temp);
        temp.GetComponent<Animator>().SetBool("BandHeadBlue", _bandHeadBlue);
        temp.GetComponent<Animator>().SetTrigger("Confetty");
        temp.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
    }

    public void RemoveMusiciansFromList()
    {

        GameObject temp = bandMembers[bandMembers.Count - 1];
        temp.GetComponent<Animator>().SetTrigger("MusicianDead");
        Invoke("removeMusician",0.5f);
    }

    private void removeMusician()
    {
        List<GameObject> tempList = new List<GameObject>();
        {
            GameObject temp = bandMembers[bandMembers.Count - 1];
            tempList.Add(temp);
        }
        foreach (var musician in tempList)
        {
            bandMembers.Remove(musician);
            Destroy(musician.gameObject);
        }
    }
}
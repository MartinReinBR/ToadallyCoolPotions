using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public List<AudioClip> NPCClip;

    private Vector3 _centerPos = new Vector3(21f, 10.5f, 0.5f);
    private Vector3 _exitPos = new Vector3(18f, 10.5f, 5.5f);
    private bool _isEntering;
    [SerializeField] private GameObject _ChatBubble;
    private GameObject _NPCBody;
    private Animator _NPCAnimator;
    private CapsuleCollider _NPCCollider; //D added
    [HideInInspector] public bool NPCIsAngry;
    [SerializeField] private AudioClip _angrySound;

    void Start()
    {
        _NPCBody = gameObject.transform.GetChild(0).gameObject;
        _NPCAnimator = _NPCBody.GetComponent<Animator>();
        _NPCCollider = GetComponent<CapsuleCollider>();//D added

        _ChatBubble.SetActive(false);

        GameStats.instance.customerCount++;
        WalkForward();
    }

    void WalkForward()
    {
        _isEntering = true;
        StartCoroutine(MoveOverSeconds(_centerPos, 5f));
    }

    public void WalkAway()
    {
        if(NPCIsAngry)
        {
            TempAudioManager.instance.PlaySoundEffectCustomVolume(_angrySound, 0.5f);
        }

        _ChatBubble.SetActive(false);
        _NPCCollider.enabled = false; //D added
        _isEntering = false;
        StartCoroutine(MoveOverSeconds(_exitPos, 2f));
        NPCSpawner.instance.SpawnNPC();
    }

    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        _NPCAnimator.enabled = true;
        float elapsedTime = 0;
        Vector3 startingPos = gameObject.transform.position;
        while (elapsedTime < seconds)
        {
            gameObject.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.position = end;
        _NPCAnimator.enabled = false;

        if (_isEntering)
        {
            _ChatBubble.SetActive(true);
            TempAudioManager.instance.PlaySoundEffect(NPCClip[Random.Range(0, 4)]);
        }
        else if (!_isEntering)
        {
            Destroy(gameObject);
        }
    }
}

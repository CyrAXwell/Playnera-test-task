using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private MakeupBook _makeupBook;
    [SerializeField] private MakeupBookPage _makeupBookPage;
    [SerializeField] private MakeupManager _makeupManager;
    [SerializeField] private Hand _hand;
    [SerializeField] private Doll _doll;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    
    private void Start()
    {
        _makeupManager.Initialize(_hand);
        _doll.Initialize(_makeupManager);
        _makeupBook.Initialize(_makeupBookPage, _makeupManager);
    }
}

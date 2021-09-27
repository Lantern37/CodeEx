using DG.Tweening;
using Shop.Core;
using Shop.Core.Utils;
using UnityEngine;

namespace Shop
{
    
public class UI : SingletonT<UI>
{
    [SerializeField] private ARParentPlacer m_ArParentPlacer;
    // [SerializeField] private GameObject _sorryPanel;
    [SerializeField] private GameObject _resetBtn;
    [SerializeField] private GameObject _startPanel;
    // [SerializeField] private Animator _321Animator;
    // [SerializeField] private QuestionsPanel _questionsPanel;
    [SerializeField] private GameObject m_ArUIHelper;
    // [SerializeField] private GameObject _speechUI;
    private LableButton[] _lableButtons;

    [SerializeField] private UILable m_UILable;

    [SerializeField] private LableType _labelType;
    private void OnEnable()
    {
        SpawnManager.Instance.ShopSpawned += ARParenSpawned;
        //m_UILable.onSetOptionEvent += OnChoiceLable;
    }

    private void OnDisable()
    {
        SpawnManager.Instance.ShopSpawned -= ARParenSpawned;
        //m_UILable.onSetOptionEvent -= OnChoiceLable;
    }

    
    private   void Start()
    {
        // _lableButtons = _lablesUI.GetComponentsInChildren<LableButton>();
        
        //New UI
        // _startPanel.SetActive(true);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     OnChoiceLable(_labelType);
        // }
    }
    
    public void OnChoiceLable(LableType type)
    {
        Debug.Log("UI OnChoiceLablee is " + type);
        StartScaningPlace();
        ShopManager.Instance.LableType = type;
        SpawnManager.Instance.StartSearchigPlace();
        m_UILable.gameObject.SetActive(false);
        
    }
    
    public void SorryPanel()
    {
        // _sorryPanel.SetActive(true);
        DOVirtual.DelayedCall(2, () =>
        {
            // _321Animator.SetTrigger("Start");
            DOVirtual.DelayedCall(5, () => Quit());
        });

    }

    public void OnStartClick()
    {
        m_UILable.gameObject.SetActive(true);
    }
    
    public  void StartScaningPlace() 
    {
        Debug.Log("StartScaningPlace");

        ShowScanHelpPanel();
    }
    
    public void ShowScanHelpPanel() 
    {
        m_ArUIHelper.SetActive(true);
        // SpawnManager.Instance.StartSpawning();
    }

    public  void ARParenSpawned()
    {
        Debug.Log("StartSession");
        _resetBtn.SetActive(true);
        m_ArUIHelper.SetActive(false);
    }

    public void SpeechPanel(bool active)
    {
        // _speechUI.SetActive(active);
    }
    
   

    //QUESTIONS
    public void QuestionsPanel(bool active)
    {
        // _questionsPanel.gameObject.SetActive(active);
    }

    public void SetQuestion(string question)
    {
        // _questionsPanel.SetDynamicQuestionButton(question);
    }
    
    
    public async void Reset() 
    {
        QuestionsPanel(false);
        SpeechPanel(false);
        _resetBtn.SetActive(false);
        m_ArParentPlacer.Reset();
        
        await UniTaskUtils.Delay(0.5f);
        m_UILable.onSetOptionEvent += OnChoiceLable;
        m_UILable.gameObject.SetActive(true);
        SpawnManager.Instance.Reset();
    }

    //Lables
    public void  LabelsButtonsAnimate()
    {
        foreach (var button in _lableButtons)
        {
            button.transform.DOScale(Vector3.up*0.5f,5 );
        }
    }

    // public void StartLableSequence()
    // {
    //     _lablesUI.GetComponent<TweenSequencer>().StartSequence();
    // }
    public void Quit()
    {
        Debug.Log("Quiiit");
        Application.Quit();
    }
    
}
   
}
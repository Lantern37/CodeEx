using Assets.Scripts.MainWindows;
using DG.Tweening;
using Engenious.Core.WindowsController;
using Shop.Core;
using Shop.Core.Utils;
using UnityEngine;

public class NewUI : SingletonT<NewUI>
{

    [SerializeField] private WindowsManager _windowsManager;
    
    [SerializeField] private GameObject m_LogoPanel;
    [SerializeField] private GameObject m_WelcomePanel;
    [SerializeField] private GameObject m_LablePanel;
    [SerializeField] private ItemPanel m_ItemPanel;
    [SerializeField] private ProductDescriptionWindow _productDescriptionWindow;

    [SerializeField] private GameObject m_SignUpParent;
    [SerializeField] private GameObject m_LoginParent;
    [SerializeField] private GameObject m_MainParent;
    [SerializeField] private GameObject m_CheckoutParent;
    [SerializeField] private GameObject m_SettingsParent;
    [SerializeField] private GameObject m_SupportParent;
    
    public UIState UIState
    {
        get;
        set;
    }

    private void Start()
    {
        //HideAllWindows();

        UIState = UIState.Welcome;
        // m_LogoPanel.SetActive(true);
        // DOVirtual.DelayedCall(1, () =>
        // {
        //     m_WelcomePanel.SetActive(true);
        //     m_LogoPanel.SetActive(false);
        // });
    }

    public void OnLogin()
    {
        UIState = UIState.Login;
        HideAllWindows();
        m_LoginParent.SetActive(true);
        m_LoginParent.GetComponent<NewUIParentControll>().Init(this);
    }
    


    public void OnSignup()
    {
        UIState = UIState.SignUp;
        HideAllWindows();
        m_SignUpParent.SetActive(true);
        m_SignUpParent.GetComponent<NewUIParentControll>().Init(this);
    }
    
    public void Checkout()
    {
        UIState = UIState.Checkout;
        HideAllWindows();
        m_CheckoutParent.SetActive(true);
        m_CheckoutParent.GetComponent<NewUIParentControll>().Init(this);
    }

    public void OnMainPanel()
    {
        UIState = UIState.Main;
        HideAllWindows();
        m_MainParent.SetActive(true);
        m_MainParent.GetComponent<NewUIParentControll>().Init(this);

    }
    
    public void OnLablePanel()
    {
        UIState = UIState.Lable;
        HideAllWindows();
        m_LablePanel.SetActive(true);
    }

    public void OnShowItemPanel(Item itemName)
    {
        // if (UIState == UIState.Main)
        // {
            // m_ItemPanel.gameObject.SetActive(true);
            // m_ItemPanel.MoveUp(1);
            // m_ItemPanel.SetName(itemName);
        //}
        _windowsManager.Show<ProductDescriptionWindow>();
        _productDescriptionWindow.SetData(itemName.ProductInfo.ProductData);
    }

    public void OnHideItemPanel()
    {
        //m_ItemPanel.MoveDown(1);
        //DOVirtual.DelayedCall(0, () => m_ItemPanel.gameObject.SetActive(false));

        if (_productDescriptionWindow.IsShowed && !_productDescriptionWindow.IsClosed)
        {
            _windowsManager.Close<ProductDescriptionWindow>();
        }
    }

    
    public void OnWelcomePanel()
    {
            UIState = UIState.Welcome;
            HideAllWindows();
            m_WelcomePanel.SetActive(true);
    }

    public void OnSupport()
    {
        UIState = UIState.Support;
        HideAllWindows();

        m_SupportParent.SetActive(true);
        m_SupportParent.GetComponent<NewUIParentControll>().Init(this);
    }
    
    public void OnSettings()
    {
        UIState = UIState.Settings;
        HideAllWindows();

        m_SettingsParent.SetActive(true);
        m_SettingsParent.GetComponent<NewUIParentControll>().Init(this);
    }
    public void OnSetState(int int_state)
    {
        UIState state = (UIState) int_state;
        
        switch (state)
        {
            case UIState.Welcome:
                OnWelcomePanel();
                break;
            
            case UIState.SignUp:
                OnSignup();
                break;
            
            case UIState.Login:
                OnLogin();
                break;
                
            case UIState.Main:
                OnMainPanel();
                break;
            
            case UIState.Checkout:
                Checkout();
                break;
            
            case UIState.Settings:
                OnSettings();
                break;
            
            case UIState.Support:
                OnSupport();
                break;
            
            case UIState.Logo:
                OnWelcomePanel();
                break;
            
            case UIState.None:
                HideAllWindows();
                break;
            
            case UIState.Lable:
                OnLablePanel();
                break;
        }
    }



    void HideAllWindows()
    {
        m_WelcomePanel.SetActive(false);
        m_SignUpParent.SetActive(false);
        m_LoginParent.SetActive(false);
        m_MainParent.SetActive(false);
        m_CheckoutParent.SetActive(false);
        m_SettingsParent.SetActive(false);
        m_SupportParent.SetActive(false);
        m_LogoPanel.SetActive(false);
        //m_ItemPanel.gameObject.SetActive(false);
        m_LablePanel.SetActive(false);

        if (_productDescriptionWindow.IsShowed && !_productDescriptionWindow.IsClosed)
        {
            _windowsManager.Close<ProductDescriptionWindow>();
        }
    }
}

public enum UIState
{
    Welcome = 0,
    SignUp,
    Login,
    Main,
    Checkout,
    Settings,
    Support,
    Logo,
    Lable,
    None
}

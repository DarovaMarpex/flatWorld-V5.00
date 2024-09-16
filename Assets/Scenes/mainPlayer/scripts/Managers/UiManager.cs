using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    public static UiManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private CanvasGroup keyBindMenu;

    private GameObject[] keyBindButtons;

    private void Awake()
    {
        keyBindButtons = GameObject.FindGameObjectsWithTag("KeyBind");
    }

    private stat healthStat;
    // Start is called before the first frame update
    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<stat>();

        SetUseable(actionButtons[0],spellBook.MyInstance.GetSpell("FireBall"));
        SetUseable(actionButtons[1], spellBook.MyInstance.GetSpell("IceBall"));
        SetUseable(actionButtons[2], spellBook.MyInstance.GetSpell("NaturePower"));

        //Debug.Log(spellBook.MyInstance.GetSpell("FireBall"));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
    }
    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);

        healthStat.initialize(target.MyHealth.myCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;
        //now updateTimeValue lisening to healthChanged event and work with it
        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemove += new CharacterRemove(HideTargetFrame);
    }

    public void HideTargetFrame(NPC target)
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.myCurrentValue = health;
    }

    private void OpenCloseMenu() 
    {
        // just a statment and else conditions (? 0 : 1 ) - that means if alpha more than zero it becomes 0 else it become 1
        keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
        keyBindMenu.blocksRaycasts = keyBindMenu.blocksRaycasts == true ? false : true; // if   keyBindMenu.blocksRaycasts == true it become false else its true
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keyBindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        // Debug.Log(useable);

        btn.MyUseable = useable;
    }
}

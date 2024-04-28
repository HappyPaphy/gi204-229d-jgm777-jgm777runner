using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Button button_WASD_Button;
    [SerializeField] private Image wasd_UI;
    [SerializeField] private Text settingTextWASD;

    [SerializeField] private Button button_arrow_Button;
    [SerializeField] private Image arrow_UI;
    [SerializeField] private Text settingTextArrow;

    private Color alphaImageFull = new Color(1f, 1f, 1f, 1f);
    private Color alphaImageFade = new Color(1f, 1f, 1f, 0.3f);
    private Color alphaImageHide = new Color(1f, 1f, 1f, 0f);

    private Color alphaTextFull = new Color(0f, 0f, 0f, 1f);
    private Color alphaTextFade = new Color(0f, 0f, 0f, 0.3f);
    private Color alphaTextHide = new Color(0f, 0f, 0f, 0f);


    [SerializeField] PlayerController player;

    public bool isSetTo_WASD_Button;

    public static Setting instance;
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(isSetTo_WASD_Button == null)
        {
            SwitchTo_WASD_Button(true);
        }
        else if(isSetTo_WASD_Button == true)
        {
            SwitchTo_WASD_Button(true);
        }
        else
        {
            SwitchTo_WASD_Button(false);
        }
    }

    public void SwitchTo_WASD_Button(bool b)
    {
        if (b)
        { 
            Debug.Log("Change To WASD");

            wasd_UI.color = alphaImageFull;
            arrow_UI.color = alphaImageFade;

            settingTextWASD.color = alphaTextFull;
            settingTextArrow.color = alphaTextHide;
        }
        else
        {
            Debug.Log("Change To Arrow");

            wasd_UI.color = alphaImageFade;
            arrow_UI.color = alphaImageFull;

            settingTextWASD.color = alphaTextHide;
            settingTextArrow.color = alphaTextFull;
        }

        isSetTo_WASD_Button = b;
        PlayerKeyBindSetting(b);
    }

    void PlayerKeyBindSetting(bool isSetToWASD)
    {
        if (isSetToWASD == true)
        {
            /*player.moveForwardKey = KeyCode.W;
            player.rotateLeftKey = KeyCode.A;
            player.moveBackwardKey = KeyCode.S;
            player.rotateRightKey = KeyCode.D;*/
        }
        else
        {
            /*player.moveForwardKey = KeyCode.UpArrow;
            player.rotateLeftKey = KeyCode.LeftArrow;
            player.moveBackwardKey = KeyCode.DownArrow;
            player.rotateRightKey = KeyCode.RightArrow;*/
        }
    }
}

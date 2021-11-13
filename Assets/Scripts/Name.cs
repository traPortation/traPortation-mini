using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{

    /// <summary>
    /// ���O�̓��͂��󂯕t����(InputField�͎g����)
    /// </summary>

    private InputField inputField;
    public static string resultName;  // ���͂��ꂽ�e�L�X�g

    // Start is called before the first frame update
    void Start()
    {
        inputField = this.gameObject.GetComponent<InputField>();
        InitializeInputField();
        resultName = "";
    }


    public void InitializeInputField()
    {
        inputField.text = "";
    }

    // OnEndEdit�ŌĂяo��
    public void FinishEditName()
    {
        if(inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done && inputField.text != "")
        {
            resultName = inputField.text;
            InitializeInputField();
        }
        // �L�����Z�����͎��A�܂��̓L�[�{�[�h�ł�InputField�ł��Ȃ���������^�b�v�������̏���
        else if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled || inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
        {
            InitializeInputField();
            resultName = "";
        }
        // ���̑�(��O�������o���獢�邽�ߍ��̂Ƃ���L�����Z�����Ɠ�������)
        else
        {
            InitializeInputField();
            resultName = "";
        }

    }


}

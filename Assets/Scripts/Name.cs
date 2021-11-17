using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputName : MonoBehaviour
{

    /// <summary>
    /// ���O�̓��͂��󂯕t����(InputField�͎g����)
    /// </summary>

    private InputField inputField;

    /// <summary>
    /// resultName = ���͂��ꂽ���b�Z�[�W
    /// </summary>
    public string resultName { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.inputField = this.gameObject.GetComponent<InputField>();
        InitializeInputField();
        this.resultName = "";
    }

    public void InitializeInputField()
    {
        this.inputField.text = "";
    }

    // OnEndEdit�ŌĂяo��
    public void FinishEditName()
    {
        switch (this.inputField.touchScreenKeyboard.status)
        {
            case TouchScreenKeyboard.Status.Done:
                if (this.resultName != "")
                {
                    this.resultName = this.inputField.text;
                }
                InitializeInputField();
                break;

            /// <summary>
            /// �L�����Z�����͎��A�܂��̓L�[�{�[�h�ł�InputField�ł��Ȃ���������^�b�v�������̏���
            /// </summary>
            case TouchScreenKeyboard.Status.Canceled:
            case TouchScreenKeyboard.Status.LostFocus:
                InitializeInputField();
                this.resultName = "";
                break;

            /// <summary>
            /// ���̑�(��O�������o���獢�邽�ߍ��̂Ƃ���L�����Z�����Ɠ�������)
            /// </summary>
            default:
                InitializeInputField();
                this.resultName = "";
                break;
        }
    }

}

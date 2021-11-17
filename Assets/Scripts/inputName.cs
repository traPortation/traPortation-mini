using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputName : MonoBehaviour
{

    /// <summary>
    /// 名前の入力を受け付ける(InputFieldは使い回す)
    /// </summary>

    private InputField inputField;

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

    // OnEndEditで呼び出す
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
            /// キャンセル入力時、またはキーボードでもInputFieldでもない部分を誤タップした時の処理
            /// </summary>
            case TouchScreenKeyboard.Status.Canceled:
            case TouchScreenKeyboard.Status.LostFocus:
                InitializeInputField();
                this.resultName = "";
                break;

            /// <summary>
            /// その他(例外処理が出たら困るため今のところキャンセル時と同じ処理)
            /// </summary>
            default:
                InitializeInputField();
                this.resultName = "";
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{

    /// <summary>
    /// 名前の入力を受け付ける(InputFieldは使い回す)
    /// </summary>

    private InputField inputField;
    public static string resultName;  // 入力されたテキスト

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

    // OnEndEditで呼び出す
    public void FinishEditName()
    {
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done && inputField.text != "")
        {
            resultName = inputField.text;
            InitializeInputField();
        }
        // キャンセル入力時、またはキーボードでもInputFieldでもない部分を誤タップした時の処理
        else if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled || inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
        {
            InitializeInputField();
            resultName = "";
        }
        // その他(例外処理が出たら困るため今のところキャンセル時と同じ処理)
        else
        {
            InitializeInputField();
            resultName = "";
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchState;

public class CameraMove : MonoBehaviour
{
    // 参考：https://qiita.com/shell/items/b8d6bd2c4bd42913189e

    TouchManager touchManager;

    /// <summary>
    /// スクロールが始まったかのフラグ
    /// </summary>
    private bool scrollStartFlg = false;

    /// <summary>
    /// スクロールの起点となるタッチポジション
    /// </summary>
    private Vector2 scrollStartPos = new Vector2();

    /// <summary>
    /// スクロール距離の調整
    /// </summary>
    private static float SCROLL_DISTANCE_CORRECTION = 0.8f; 

    /// <summary>
    /// タッチポジション初期化
    /// </summary>
    private Vector2 touchPosition_cam;

    /// <summary>
    /// タッチ位置にあるオブジェクトの判定の初期化
    /// </summary>
    private Collider2D collide2dObj;

    // Use this for initialization
    void Start()
    {
        this.touchManager = new TouchManager();
    }

    // Update is called once per frame
    void Update()
    {
        this.touchManager.update();

        if (this.touchManager.touchFlag)
        {

            touchPosition_cam = Camera.main.ScreenToWorldPoint(touchManager.touchPosition);
            collide2dObj = Physics2D.OverlapPoint(touchPosition_cam);

            if (scrollStartFlg == false && collide2dObj)
            {
                // タッチ位置にオブジェクトがあったらそのオブジェクトを取得する
                // スクロール移動とオブジェクトタッチの処理を区別するために記載
                GameObject obj = collide2dObj.transform.gameObject;
                Debug.Log(obj.name);
            }
            else
            {
                // タッチした場所に何もない場合、スクロールフラグをtrueに
                scrollStartFlg = true;
                if (scrollStartPos.x == 0.0f)
                {
                    // スクロール開始位置を取得
                    scrollStartPos = Camera.main.ScreenToWorldPoint(touchManager.touchPosition);
                }
                else
                {
                    Vector2 touchMovePos = touchPosition_cam;
                    if (scrollStartPos.x != touchMovePos.x)
                    {
                        // 直前のタッチ位置との差を取得する
                        float diffPos_x = SCROLL_DISTANCE_CORRECTION * (touchMovePos.x - scrollStartPos.x);
                        float diffPos_y = SCROLL_DISTANCE_CORRECTION * (touchMovePos.y - scrollStartPos.y);

                        Vector3 pos = this.transform.position;
                        pos.x -= diffPos_x;
                        pos.y -= diffPos_y;

                        this.transform.position = pos;
                        scrollStartPos = touchMovePos;
                    }
                }
            }
        }
        else
        {
            // タッチを離したらフラグを落とし、スクロール開始位置も初期化する 
            scrollStartFlg = false;
            scrollStartPos = new Vector2();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchState
{
    // 参考：https://techblog.gracetory.co.jp/entry/2018/06/04/000000


    /// <summary>
    /// タッチ操作の参照クラス
    /// </summary>

    public class TouchManager
    {
        public bool touchFrag;      // タッチ有無
        public Vector2 touchPosition;   // タッチ座標
        public TouchPhase touchPhase;   // タッチ状態

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="position"></param>
        /// <param name="phase"></param>
        public TouchManager(bool flag = false, Vector2? position = null, TouchPhase phase = TouchPhase.Began)
        {
            this.touchFrag = flag;
            if (position == null)
            {
                this.touchPosition = new Vector2(0, 0);
            }
            else
            {
                this.touchPosition = (Vector2)position;
            }
            this.touchPhase = phase;
        }

        // Update is called once per frame
        public void update()
        {
            this.touchFrag = false;

            // マウス操作
            if (Application.isEditor)
            {
                // 押した瞬間
                if (Input.GetMouseButtonDown(0))
                {
                    this.touchFrag = true;
                    this.touchPhase = TouchPhase.Began;
                }

                // 離した瞬間
                if (Input.GetMouseButtonUp(0))
                {
                    this.touchFrag = true;
                    this.touchPhase = TouchPhase.Ended;
                }

                // 押しっぱなし
                if (Input.GetMouseButton(0))
                {
                    this.touchFrag = true;
                    this.touchPhase = TouchPhase.Moved;
                }

                // 座標取得
                if (this.touchFrag) this.touchPosition = Input.mousePosition;
            }

            // タッチ操作
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    this.touchPosition = touch.position;
                    this.touchPhase = touch.phase;
                    this.touchFrag = true;
                }
            }
        }

        /// <summary>
        /// タッチ状態の取得
        /// </summary>
        
        public TouchManager getTouch()
        {
            return new TouchManager(this.touchFrag, this.touchPosition, this.touchPhase);
        }
    }
}
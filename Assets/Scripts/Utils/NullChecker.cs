using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    static class NullChecker
    {
        /// <summary>
        /// 引数の中にnullがあったら例外を投げる
        /// </summary>
        /// <param name="objs"></param>
        public static void Check(params Object[] objs)
        {
            foreach (var obj in objs)
            {
                if (obj == null) throw new System.NullReferenceException();
            }
        }
    }
}

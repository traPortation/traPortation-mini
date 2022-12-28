using UnityEngine;

#nullable enable

namespace TraPortation
{
    public class MouseIcon
    {
        public readonly GameObject obj;
        SpriteRenderer renderer;
        InputManager manager;

        public MouseIcon(GameObject obj, InputManager manager)
        {
            this.obj = obj;
            this.renderer = obj.GetComponent<SpriteRenderer>();
            this.manager = manager;
        }

        public void SetActive(bool active)
        {
            if (this.obj.activeSelf == active) return;

            this.obj.SetActive(active);

            if (!active)
            {
                this.obj.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
            }
        }

        public void Update()
        {
            if (!this.renderer.enabled) return;

            var pos = this.manager.GetMousePosition();
            this.obj.transform.position = new Vector3(pos.x, pos.y, Const.Z.MouseIcon);
        }

        public void SetAlpha(float alpha)
        {
            var color = this.renderer.color;
            color.a = alpha;
            this.renderer.color = color;
        }
    }
}
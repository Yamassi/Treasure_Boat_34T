using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class ScrollRect_fix : ScrollRect
    {

        override protected void LateUpdate()
        {
            base.LateUpdate();
            if (this.verticalScrollbar)
            {
                this.verticalScrollbar.size = 0.03f;
            }
        }

        override public void Rebuild(CanvasUpdate executing)
        {
            base.Rebuild(executing);
            if (this.verticalScrollbar)
            {
                this.verticalScrollbar.size = 0.03f;
            }
        }
    }
}

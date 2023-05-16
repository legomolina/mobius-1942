using Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI
{
    public interface IInteractable : IUpdatable, IDrawable
    {
        event EventHandler<MouseEventArgs>? MouseDown;
        event EventHandler<MouseEventArgs>? MouseUp;
        event EventHandler<MouseEventArgs>? MouseOver;
        event EventHandler<MouseEventArgs>? MouseOut;

        bool Focus { get; }
        bool Hover { get; }
        int TabIndex { get; set; }

        void SetFocus(bool hasFocus);
    }
}

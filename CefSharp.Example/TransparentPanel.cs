using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CefSharp;

namespace ControlsEx
{
    public class TransparentPanel : Panel
    {
        CefFormsWebBrowser _browserControl = null;

        public TransparentPanel(CefFormsWebBrowser browserControl)
            : base()
        {
            if (browserControl != null)
                _browserControl = browserControl;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (_browserControl != null)
                _browserControl.Invoke(new MethodInvoker(_browserControl.Invalidate));
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //do not allow the background to be painted 
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_browserControl != null)
                _browserControl.InvokeOnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_browserControl != null)
                _browserControl.InvokeOnMouseLeave();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_browserControl != null)
                _browserControl.InvokeOnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_browserControl != null)
                _browserControl.InvokeOnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_browserControl != null)
                _browserControl.InvokeOnMouseUp(e);
        }
    }
}

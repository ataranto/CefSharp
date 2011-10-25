using System;
using System.Windows.Forms;
using CefSharp;

namespace ControlsEx
{
    /// <summary>
    /// very important that this control will be in focus, to receive keyboard events
    /// </summary>
    public class TransparentPanel : Panel
    {
        CefFormsWebBrowser _browserControl = null;

        public TransparentPanel(CefFormsWebBrowser browserControl)
            : base()
        {
            if (browserControl != null)
                _browserControl = browserControl;

            this.TabStop = true;
            this.SetStyle(ControlStyles.Selectable, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                cp.ExStyle |= 0x00000008;
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

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            //System.Diagnostics.Trace.WriteLine("OnLostFocus()");

            this.Focus();
        }
        /*
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            System.Diagnostics.Trace.WriteLine("OnGotFocus()");
        }*/

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        private const int WM_CHAR = 0x102;
        private const int WM_SYSCHAR = 0x106;
        private const int WM_IME_CHAR = 0x286;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            bool keyboardEvent = false;

            switch (m.Msg)
            {
                case WM_CHAR:
                case WM_IME_CHAR:
                case WM_KEYDOWN:
                case WM_KEYUP:
                case WM_SYSCHAR:
                case WM_SYSKEYDOWN:
                case WM_SYSKEYUP: keyboardEvent = true; break;
            }

            if (keyboardEvent && _browserControl != null)
                _browserControl.InvokeSendKeyEvent(m.Msg, m.WParam, m.LParam);

            base.WndProc(ref m);
        }
    }
}

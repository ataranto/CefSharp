using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using ControlsEx;

namespace TestMouseAndKeyboardProxy
{
    public partial class TestEventProxy : Form
    {
        private const string testURL = "https://github.com/chillitom/CefSharp"; // not working with this http://billmill.org/static/canvastutorial/index.html

        private readonly CefFormsWebBrowser browser;
        private readonly TransparentPanel eventProxy;
        
        public TestEventProxy()
        {
            InitializeComponent();

            browser = new CefFormsWebBrowser(testURL, new BrowserSettings());
            browser.Dock = DockStyle.Fill;
            this.toolStripContainer1.ContentPanel.Controls.Add(browser);

            eventProxy = new TransparentPanel(browser);
            eventProxy.Dock = DockStyle.Fill;
            this.toolStripContainer1.ContentPanel.Controls.Add(eventProxy);
        }

        private void TestEventProxy_Shown(object sender, EventArgs e)
        {
            eventProxy.BringToFront();
            eventProxy.Focus();
        }
    }
}

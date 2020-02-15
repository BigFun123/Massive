using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class ChatForm : DToolForm
  {
    public ChatForm()
    {
      InitializeComponent();
      SetTitle("_SOCIAL");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void ChatForm_Shown(object sender, EventArgs e)
    {
      Point p = Main.ClientRect.Location;
      p.Offset(Main.ClientRect.Width - Width, 0);
      Location = p;
      cChatControl1.Setup();
    }

    private void cChatControl1_Load(object sender, EventArgs e)
    {
         
    }

    private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
    {      
      cChatControl1.Close();
      ///Hide();
    }
  }
}

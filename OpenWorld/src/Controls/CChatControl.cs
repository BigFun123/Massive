using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive;
using Massive.Events;
using Massive.Network;

namespace OpenWorld.Controls
{
  public partial class CChatControl : UserControl
  {
    int IncrementID = 0;
    Font BoldFont = new Font("Arial", 8, FontStyle.Bold);
    Font StdFont = new Font("Arial", 8, FontStyle.Regular);
    Dictionary<string, MChatMessage> MessageOwners;
    ListViewGroup RabbleGroup;
    ListViewGroup CommunityGroup;
    ListViewGroup FriendsGroup;
    
    public CChatControl()
    {
      InitializeComponent();

      FriendsList.Groups.Clear();
      MessageOwners = new Dictionary<string, MChatMessage>();

      RabbleGroup = new ListViewGroup();
      RabbleGroup.Header = "Rabble";
      CommunityGroup = new ListViewGroup();
      CommunityGroup.Header = "Community";
      FriendsGroup = new ListViewGroup();
      FriendsGroup.Header = "Friends";

      FriendsList.Groups.Add(RabbleGroup);
      FriendsList.Groups.Add(CommunityGroup);
      FriendsList.Groups.Add(FriendsGroup);
    }

    void UpdateUI()
    {
      FriendsList.Items.Clear();
      foreach( KeyValuePair<string,MChatMessage> m in MessageOwners)
      {
        ListViewItem lvi = new ListViewItem();
        if ( string.IsNullOrEmpty(m.Value.OwnerName))
        {
          //m.OwnerName = "Anon";
          lvi.Text = "Anon";
          lvi.Group = RabbleGroup;
        }
        else
        {
          lvi.Group = CommunityGroup;
          lvi.Text = m.Value.OwnerName;
        }

        lvi.ToolTipText = m.Value.OwnerName + ":" + m.Value.OwnerID + ":" + m.Value.TargetID;
        //TODO check if friends
        lvi.ImageIndex = 2;
        lvi.Tag = m;
        FriendsList.Items.Add(lvi);
      }
    }

    void AddMessage(MChatMessage m)
    {      
      if ( !MessageOwners.ContainsKey(m.OwnerID))
      {
        //MessageOwners.Remove(m.OwnerID);
        MessageOwners.Add(m.OwnerID, m);
      }     

      UpdateUI();
    }

    private void Network_ChatEventHandler(object sender, ChatEvent e)
    { 
      ChatBox.BeginInvoke((MethodInvoker)delegate
      {
        int st = ChatBox.TextLength;
        ChatBox.SelectionFont = BoldFont;
        ChatBox.SelectionBackColor = Color.DarkCyan;              
        ChatBox.AppendText(e.message.OwnerName + ":" );
        
        ChatBox.Select(st, ChatBox.TextLength - st);
        ChatBox.SelectionFont = StdFont;
        ChatBox.DeselectAll();

        st = ChatBox.TextLength;        
        ChatBox.AppendText(e.message.Message + "\r\n");
        ChatBox.Select(st, ChatBox.TextLength-st);
        ChatBox.SelectionBackColor = Color.Black;
        ChatBox.DeselectAll();

        ChatBox.ScrollToCaret();

        AddMessage(e.message);
      });

      if (e.message.MessageID == IncrementID)
      {
        ChatBoxMessage.Invoke((MethodInvoker)delegate
        {
          ChatBoxMessage.Text = "";
          IncrementID++;
          ChatBoxMessage.Focus();
        });
      }
    }

    private void SendButton_Click(object sender, EventArgs e)
    {      
      SendChat(ChatBoxMessage.Text);
    }
    void SendChat( string s)
    {
      s = s.Trim(new char[] { ' ', '\n', '\r' });
      if (string.IsNullOrEmpty(s)) return;
      IncrementID++;
      Globals.Network.ChatRequest("*", s, IncrementID);
    }

    private void ChatBoxMessage_KeyUp(object sender, KeyEventArgs e)
    {
      if ( e.KeyCode == Keys.Return )
      {
        SendChat(ChatBoxMessage.Text);
      }
    }

    private void GiveCake_Click(object sender, EventArgs e)
    {
      SendChat("*Cake*");
    }

    private void ChatBox_VisibleChanged(object sender, EventArgs e)
    {
      if ( Visible == true)
      {
        Globals.Network.ChatEventHandler += Network_ChatEventHandler;
      }
      else
      {
        Globals.Network.ChatEventHandler -= Network_ChatEventHandler;
      }
    }

    public void Setup()
    {
      ChatBoxMessage.Focus();
      ChatBoxMessage.SelectedText = "";
    }

    public void Close()
    {      
      Globals.Network.ChatEventHandler -= Network_ChatEventHandler;
      //base.Dispose();
    }

    private void CChatControl_Load(object sender, EventArgs e)
    {
      ChatBoxMessage.Focus();
      ChatBoxMessage.SelectedText = "";
    }

    private void GotoChatterButton_Click(object sender, EventArgs e)
    {

    }

    private void FriendsList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}

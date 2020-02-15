using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaderPlay
{
  public partial class SettingsControl : UserControl
  {
    public SettingsControl()
    {
      InitializeComponent();
    }

    private void LoadImage1_Click(object sender, EventArgs e)
    {
      if ( openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        Globals.texture.SetTexture1(openFileDialog1.FileName);
        ImageBox1.ImageLocation = openFileDialog1.FileName;
      }
    }

    private void LoadImage2_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        Globals.texture.SetTexture2(openFileDialog1.FileName);
        ImageBox2.ImageLocation = openFileDialog1.FileName;
      }
    }
  }
}

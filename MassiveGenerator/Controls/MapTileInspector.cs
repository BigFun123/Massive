using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassiveGenerator.Controls
{
  public partial class MapTileInspector : UserControl
  {
    MTile Tile;
    public MapTileInspector()
    {
      InitializeComponent();
      Tile = new MTile();
    }

    public void MakeBiome(int x, int y, int zoom)
    {
      //Tile.MakeBiome(x, y, zoom);
    }

    public void Decode(int x, int y, int zoom)
    {
      Tile.Decode(x, y, zoom, treeView1);
    }

    public void Process(int x, int y, int zoom)
    {
     // Tile.Decode(x, y, zoom, treeView1);
      Tile.DecodeGEOJSON(x, y, zoom);
    }

    private void GetJSON_Click(object sender, EventArgs e)
    {
      Tile.DecodeGEOJSON(2257, 2458, 12);
    }
  }
}

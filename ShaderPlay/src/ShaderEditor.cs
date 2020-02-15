using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderPlay
{
  public class ShaderEditor
  {
    public string FragmentPath = "shaders\\fs.glsl";
    FastColoredTextBoxNS.FastColoredTextBox frageditor;
    FastColoredTextBoxNS.FastColoredTextBox vertexeditor;
    MShader _shader;

    TextStyle brownStyle = new TextStyle(Brushes.LightCoral, null, FontStyle.Regular);
    TextStyle blueStyle = new TextStyle(Brushes.CornflowerBlue, null, FontStyle.Regular);
    TextStyle greenStyle = new TextStyle(Brushes.LightGreen, null, FontStyle.Regular);
    TextStyle graystyle = new TextStyle(Brushes.SlateGray, null, FontStyle.Regular);


    public void Setup(FastColoredTextBoxNS.FastColoredTextBox in_fragEditor,
      FastColoredTextBoxNS.FastColoredTextBox in_vertexeditor,
      MShader shader)
    {
      frageditor = in_fragEditor;
      vertexeditor = in_vertexeditor;
      _shader = shader;
      LoadFragment();
      LoadVertexShader();
    }

    public void SetFragShaderEditorStyle(FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      //clear previous highlighting
      e.ChangedRange.ClearStyle(brownStyle);
      //highlight tags
      //vertexeditor.Range.SetStyle(brownStyle, "<[^>]+>");

      frageditor.Range.ClearStyle(graystyle);
      frageditor.Range.SetStyle(graystyle, @"#.*");
      frageditor.Range.SetStyle(graystyle, @"//.*");

      //highlight keywords of GLSL
      frageditor.Range.SetStyle(blueStyle, @"\b(out|in|void|and|eval|else|if|lambda|or|set|lerp|mix|mult|dot|pow|struct|uniform|sampler2D)\b",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      frageditor.Range.SetStyle(greenStyle, @"\b(vec2|vec3|vec4|mat3|mat4|float|int|bool|double)\b",
       System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    public void SetVertexEditorStyle(FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      //clear previous highlighting
      e.ChangedRange.ClearStyle(brownStyle);
      //highlight tags
      //vertexeditor.Range.SetStyle(whitestyle, @"\([^\)]+\)");

      vertexeditor.Range.SetStyle(graystyle, @"#.*");
      vertexeditor.Range.SetStyle(graystyle, @"//.*");

      vertexeditor.Range.SetStyle(brownStyle, @"\b(layout)\b",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      //highlight keywords of GLSL
      vertexeditor.Range.SetStyle(blueStyle, @"\b(out|in|void|and|eval|else|if|lambda|or|set|lerp|mix|mult|dot|pow|struct|uniform|sampler2D)\b",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      vertexeditor.Range.SetStyle(greenStyle, @"\b(vec2|vec3|vec4|mat3|mat4|float|int|bool|double)\b",
  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    void LoadFragment()
    {
      string sShader = File.ReadAllText(_shader.PathToFragmentShader);
      frageditor.Text = sShader;
    }

    void LoadVertexShader()
    {
      string sShader = File.ReadAllText(_shader.PathToVertexShader);
      vertexeditor.Text = sShader;
    }
  }
}

using FastColoredTextBoxNS;
using Massive;
using Massive.Events;
using Massive.GIS;
using Massive.Platform;
using OpenWorld.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.src.Forms
{
  public partial class ShaderEditorForm : DToolForm
  {
    MMaterial CurrentMaterial;
    enum eEditType { VertexShader, FragmentShader, TessellationShader, EvaluationShader, ComputeShader, Functions };
    eEditType EditType = eEditType.VertexShader;

    TextStyle brownStyle = new TextStyle(Brushes.LightCoral, null, FontStyle.Regular);
    TextStyle blueStyle = new TextStyle(Brushes.CornflowerBlue, null, FontStyle.Regular);
    TextStyle greenStyle = new TextStyle(Brushes.LightGreen, null, FontStyle.Regular);
    TextStyle graystyle = new TextStyle(Brushes.SlateGray, null, FontStyle.Regular);

    public ShaderEditorForm()
    {
      InitializeComponent();
      SetTitle("Shader Editor");
      Opacity = 0.9f;
    }

    private void ShaderEditorForm_Load(object sender, EventArgs e)
    {
      ShaderCombo.Items.Clear();
      foreach( MObject mo in MScene.MaterialRoot.Modules)
      {
        if (!(mo is MMaterial)) continue;
        MMaterial m = (MMaterial)mo;
        ShaderCombo.Items.Add(m);
        ShaderCombo.ValueMember = "Name";
      }
    }

    void LoadShader()
    {
      if ( CurrentMaterial == null)
      {
        ResultBox.Text = "No Material Selected";
        return;
      }
      string sData= "";
      switch (EditType)
      {
        case eEditType.VertexShader:
          //sFile = CurrentMaterial.shader.VertexShaderPath;
          sData = CurrentMaterial.shader.VertexShaderCode;
          break;
        case eEditType.FragmentShader:
          //sFile = CurrentMaterial.shader.FragmentShaderPath;
          sData = CurrentMaterial.shader.FragmentShaderCode;
          break;
        case eEditType.TessellationShader:
          //sFile = CurrentMaterial.shader.ControlShaderPath;
          break;
        case eEditType.EvaluationShader:
          //sFile = CurrentMaterial.shader.EvalShaderPath;
          break;
        case eEditType.Functions:
          sData = CurrentMaterial.shader.FragmentFunctionsShaderCode;
          break;
      }

      //string sData = File.ReadAllText(Path.Combine(MFileSystem.ShadersPath, sFile));
      ShaderEditor.Text = sData;

    }

    private void ShaderCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      CurrentMaterial = (MMaterial)ShaderCombo.SelectedItem;
      LoadShader();
    }

    public void SetVertexEditorStyle(FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      //clear previous highlighting
      e.ChangedRange.ClearStyle(brownStyle);
      //highlight tags
      //vertexeditor.Range.SetStyle(whitestyle, @"\([^\)]+\)");

      ShaderEditor.Range.SetStyle(blueStyle, @"#.*");
      ShaderEditor.Range.SetStyle(graystyle, @"//.*");

      ShaderEditor.Range.SetStyle(brownStyle, @"\b(layout)\b",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      //highlight keywords of GLSL
      ShaderEditor.Range.SetStyle(blueStyle, @"\b(out|in|void|and|eval|else|if|lambda|or|set|lerp|mix|mult|dot|pow|struct|uniform|sampler2D)\b",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      ShaderEditor.Range.SetStyle(greenStyle, @"\b(vec2|vec3|vec4|mat3|mat4|float|int|bool|double)\b",
  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    private void ShaderEditor_TextChangedDelayed(object sender, TextChangedEventArgs e)
    {
      SetVertexEditorStyle(e);
      UpdateShader();
    }

    void UpdateShader()
    {
      if (CurrentMaterial == null) return;
      if (EditType == eEditType.VertexShader)
      {
        CurrentMaterial.shader.VertexShaderCode = ShaderEditor.Text;
      }
      if (EditType == eEditType.FragmentShader)
      {
        CurrentMaterial.shader.FragmentShaderCode = ShaderEditor.Text;
      }
      if ( EditType == eEditType.Functions)
      {
        CurrentMaterial.shader.FragmentFunctionsShaderCode = ShaderEditor.Text;
      }
      ResultBox.Text = CurrentMaterial.shader.Recompile();
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
      UpdateShader();
    }

    private void FragmentMode_CheckedChanged(object sender, EventArgs e)
    {
      EditType = eEditType.FragmentShader;
      LoadShader();
    }

    private void VertexMode_CheckedChanged(object sender, EventArgs e)
    {
      EditType = eEditType.VertexShader;
      LoadShader();
    }

    private void ControlMode_CheckedChanged(object sender, EventArgs e)
    {
      EditType = eEditType.TessellationShader;
      LoadShader();
    }

    private void EvalMode_CheckedChanged(object sender, EventArgs e)
    {
      EditType = eEditType.EvaluationShader;
      LoadShader();
    }

    private void FragmentFunctions_CheckedChanged(object sender, EventArgs e)
    {
      EditType = eEditType.Functions;
      LoadShader();
    }

    private void RevertButton_Click(object sender, EventArgs e)
    {
      if (CurrentMaterial == null) return;
      CurrentMaterial.shader.Load(CurrentMaterial.shader.VertexShaderPath, CurrentMaterial.shader.FragmentShaderPath, "", "");
      LoadShader();
    }

    private void TimeOfDay_Scroll(object sender, EventArgs e)
    {
      TimeOfDayLabel.Text = TimeOfDay.Value.ToString();
      MClimate.SetTimeOfDay(TimeOfDay.Value);
      MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID, Globals.Avatar.GetPosition(), Globals.Avatar.GetRotation());
    }
  }
}

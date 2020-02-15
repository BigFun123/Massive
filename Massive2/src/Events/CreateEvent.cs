using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class CreateEvent:EventArgs
  {
    public MSceneObject CreatedObject;
    public CreateEvent(MSceneObject inCreatedObject)
    {
      CreatedObject = inCreatedObject;
    }
  }
}

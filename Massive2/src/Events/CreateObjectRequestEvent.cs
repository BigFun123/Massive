﻿using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class CreateObjectRequestEvent : EventArgs
  {    
    public MServerObject ServerObject;
    public CreateObjectRequestEvent(MServerObject newObject)
    {
      ServerObject = newObject;
    }
  }
}

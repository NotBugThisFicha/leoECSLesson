using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    internal struct HitComponent
    {
        public ECSMonoObject firstCollide;
        public ECSMonoObject secondCollide;
    }
}

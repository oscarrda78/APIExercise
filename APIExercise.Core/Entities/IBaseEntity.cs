﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExercise.Core.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}

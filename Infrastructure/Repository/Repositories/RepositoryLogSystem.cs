﻿using Domain.Interfaces.InterfaceLogSystem;
using Entities.Entities;
using Infrastructure.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryLogSystem : RepositoryGenerics<LogSystem>, ILogSystem
    {
    }
}

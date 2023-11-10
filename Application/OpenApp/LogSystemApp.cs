<<<<<<< HEAD
﻿using ApplicationApp.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿
using ApplicationApp.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceLogSystem;
using Entities.Entities;
using System.Collections.Generic;
>>>>>>> master

namespace ApplicationApp.OpenApp
{
    public class LogSystemApp : ILogSystemApp
    {
<<<<<<< HEAD
        private readonly ILogSystemApp _logSystemApp;   
        public LogSystemApp(ILogSystemApp logSystemApp)
        {
            _logSystemApp = logSystemApp;
=======
        private readonly ILogSystem _ILogSystem;

        public LogSystemApp(ILogSystem ILogSystem)
        {
            _ILogSystem = ILogSystem;
>>>>>>> master
        }

        public async Task Add(LogSystem Object)
        {
<<<<<<< HEAD
            await _logSystemApp.Add(Object);
=======
            await _ILogSystem.Add(Object);
>>>>>>> master
        }

        public async Task Delete(LogSystem Object)
        {
<<<<<<< HEAD
            await _logSystemApp.Delete(Object);
=======
            await _ILogSystem.Delete(Object);
>>>>>>> master
        }

        public async Task<LogSystem> GetEntityById(int Id)
        {
<<<<<<< HEAD
            return await _logSystemApp.GetEntityById(Id); 
=======
            return await _ILogSystem.GetEntityById(Id); 
>>>>>>> master
        }

        public async Task<List<LogSystem>> List()
        {
<<<<<<< HEAD
            return await _logSystemApp.List();
=======
            return await _ILogSystem.List();
>>>>>>> master
        }

        public async Task Update(LogSystem Object)
        {
<<<<<<< HEAD
            await _logSystemApp.Update(Object);
=======
            await _ILogSystem.Update(Object);
>>>>>>> master
        }
    }
}

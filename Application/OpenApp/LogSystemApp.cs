
using ApplicationApp.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceLogSystem;
using Entities.Entities;
using System.Collections.Generic;

namespace ApplicationApp.OpenApp
{
    public class LogSystemApp : ILogSystemApp
    {
        private readonly ILogSystem _ILogSystem;

        public LogSystemApp(ILogSystem ILogSystem)
        {
            _ILogSystem = ILogSystem;
        }

        public async Task Add(LogSystem Object)
        {
            await _ILogSystem.Add(Object);
        }

        public async Task Delete(LogSystem Object)
        {
            await _ILogSystem.Delete(Object);
        }

        public async Task<LogSystem> GetEntityById(int Id)
        {
            return await _ILogSystem.GetEntityById(Id); 
        }

        public async Task<List<LogSystem>> List()
        {
            return await _ILogSystem.List();
        }

        public async Task Update(LogSystem Object)
        {
            await _ILogSystem.Update(Object);
        }
    }
}

using ApplicationApp.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class LogSystemApp : ILogSystemApp
    {
        private readonly ILogSystemApp _logSystemApp;   
        public LogSystemApp(ILogSystemApp logSystemApp)
        {
            _logSystemApp = logSystemApp;
        }

        public async Task Add(LogSystem Object)
        {
            await _logSystemApp.Add(Object);
        }

        public async Task Delete(LogSystem Object)
        {
            await _logSystemApp.Delete(Object);
        }

        public async Task<LogSystem> GetEntityById(int Id)
        {
            return await _logSystemApp.GetEntityById(Id); 
        }

        public async Task<List<LogSystem>> List()
        {
            return await _logSystemApp.List();
        }

        public async Task Update(LogSystem Object)
        {
            await _logSystemApp.Update(Object);
        }
    }
}

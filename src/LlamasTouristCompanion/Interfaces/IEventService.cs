using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAll();
        Event GetById(string id);
        void Add(AddEventViewModel model);
        void Update(Event model);
        void Delete(string id);
    }
}

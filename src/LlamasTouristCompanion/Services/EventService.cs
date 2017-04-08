using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using LlamasTouristCompanion.Repositories;

namespace LlamasTouristCompanion.Services
{
    public class EventService : IEventService
    {

        private readonly IRepository<Event, Guid> _eventRepository;

        public EventService(IRepository<Event, Guid> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Add(AddEventViewModel model)
        {
            _eventRepository.Insert(new Event(model));
        }

        public void Delete(string id)
        {
            _eventRepository.Delete(Guid.Parse(id));
        }

        public Task<List<Event>> GetAll()
        {
            return _eventRepository.GetAll();
        }

        public Event GetById(string id)
        {
            return _eventRepository.GetById(Guid.Parse(id));
        }

        public void Update(Event model)
        {
            _eventRepository.Update(model);
        }
    }
}

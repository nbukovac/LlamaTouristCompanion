using LlamasTouristCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IInfoService
    {
        Task<List<Info>> GetAll();
        Info GetById(string id);
        void Add(Info info);
        void Update(Info info);
        void Delete(string id);


    }
}

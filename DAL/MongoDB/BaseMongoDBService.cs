using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using MongoDbGenericRepository.Models;

using SunmathiTech.HRMS.DAL.MongoDB.Infrasructure;

namespace SunmathiTech.HRMS.DAL.MongoDB
{
    public class BaseMongoDBService
    {
        IBaseMongoDBRepository _baseMongoDBRepository;

        public BaseMongoDBService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HotelManagementSystem"));
            var mongoDbDatabase = client.GetDatabase("hms");
            _baseMongoDBRepository = new BaseMongoDBRepository(mongoDbDatabase);
        }

        public bool Create(Document doc)
        {
            try
            {
                doc.AddedAtUtc = DateTime.UtcNow;
                _baseMongoDBRepository.AddOne<Document>(doc);
                //_baseMongoDBRepository.GetById
                return true;
            }
            catch(Exception ex)
            {
                //Do nothing
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}

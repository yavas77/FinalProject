using Bank.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Api.DbContext
{
    public class MongoDbContext
    {
        public MongoDbContext()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://oyavas:663763@sitemanagement.hp3mm.mongodb.net/SiteManagementDB?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("SiteManagementDB");
        }

    }
}

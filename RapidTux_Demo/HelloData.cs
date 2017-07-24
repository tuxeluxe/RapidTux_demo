using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidTux.ApiClient;
using Newtonsoft.Json.Linq;
using RapidTux.Web.Shared;

namespace RapidTux_Demo
{
    [TestClass]
    public class HelloData
    {
        [TestMethod]
        public void CreatePerson()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            
            var person = client.DataAPI.Create(Person.PersonTypeName).Value; //make sure hello type is run first, to be able to create :)
            person[Person.NavnProperty] = "John Mogensen";
            person[Person.AlderProperty] = 132;
            person[Person.ErBarnProperty] = false;

            var spouse = client.DataAPI.Create(Person.PersonTypeName).Value;
            spouse[Person.NavnProperty] = "Lone Kellerman";
            spouse[Person.ErÆgtefælleProperty] = true;
            ((JArray)person[Person.RelationerProperty]).Add(spouse);

            var resp = client.DataAPI.Save(Person.PersonTypeName, person);
        }

        [TestMethod]
        public void ListData()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.DataAPI.List(Person.PersonTypeName);
        }

        [TestMethod]
        public void ListData_with_limit_skip()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.DataAPI.List(Person.PersonTypeName, 20, 0);
        }

        [TestMethod]
        public void ListData_with_options()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            SearchOptions options = new SearchOptions();
            options.Limit = 20;
            options.Skip = 0;
            options.IsAndCriterias = false;
            options.AddCriteria(new Criteria(Person.NavnProperty, new JValue("Mogensen"), CriteriaTypeEnum.Contains));
            var resp = client.DataAPI.List(Person.PersonTypeName, options);
        }

        [TestMethod]
        public void DeleteData()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.DataAPI.DeleteAll(Person.PersonTypeName);
        }
    }
}

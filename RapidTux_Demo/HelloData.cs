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
        public void ListPersons()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var persons = client.DataAPI.List(Person.PersonTypeName).Value.Data;
        }

        [TestMethod]
        public void SearchPersons()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var search = SearchOptions.Instance.AddCriteria("navn", "Mogensen", CriteriaTypeEnum.Contains);
            var persons = client.DataAPI.List(Person.PersonTypeName, search).Value.Data;
        }

        [TestMethod]
        public void CreatePersons()
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

            var halfdan = client.DataAPI.Create(Person.PersonTypeName).Value;
            halfdan[Person.NavnProperty] = "Halfdan Rasmussen";

            var resp = client.DataAPI.Save(Person.PersonTypeName, person);
            resp = client.DataAPI.Save(Person.PersonTypeName, halfdan);
        }

        [TestMethod]
        public void ListPersons_with_limit_skip()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.DataAPI.List(Person.PersonTypeName, 20, 0);
        }

        [TestMethod]
        public void SearchPersons_with_limit_skip()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            SearchOptions options = SearchOptions.Instance;
            options.Limit = 20;
            options.Skip = 0;
            options.IsAndCriterias = false;
            options.AddCriteria(new Criteria(Person.NavnProperty, new JValue("Mogensen"), CriteriaTypeEnum.Contains));
            var resp = client.DataAPI.List(Person.PersonTypeName, options);
        }

        [TestMethod]
        public void DeletePersons()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.DataAPI.DeleteAll(Person.PersonTypeName);
        }
    }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidTux.ApiClient;
using System.Diagnostics;
using RapidTux.Web.Shared;

namespace RapidTux_Demo
{
    [TestClass]
    public class HelloTypes
    {
        [TestMethod]
        public void ListTypes()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, TestConfig.Username);
            var resp = client.TypeAPI.GetDataTypes();
            string typeNames = string.Join(", ", resp.Value.Types.Select( t => t.Name));
            Debug.WriteLine(typeNames);
        }

        [TestMethod]
        public void CreateType()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, TestConfig.Username);

            //create types
            TypeBuilder tb = new TypeBuilder();
            IDataType person = tb.AddStringProperty(Person.NameProperty, "Preben Fuglekær")
                                   .AddIntegerProperty(Person.AgeProperty, 96)
                                   .AddStringProperty(Person.EmailProperty, "")
                                   .AddBooleanProperty(Person.IsSpouseProperty, false)
                                   .AddBooleanProperty(Person.IsChildProperty, false)
                                   .AddObjectProperty(Person.RelationsProperty, "Person", false, true) //embedded person relations
                                   .ResultWithName(Person.PersonTypeName);
            if(!client.TypeAPI.Exists(person.Name).Value)
                client.TypeAPI.Create(person);

            //get created types
            var resp = client.TypeAPI.GetDataTypes();
            string typeNames = string.Join(", ", resp.Value.Types.Select(t => t.Name));
            foreach(DataType dataType in resp.Value.Types)
                Debug.WriteLine(dataType.Name + Environment.NewLine + dataType.TypeInfo.ToString() + Environment.NewLine);
        }

        [TestMethod]
        public void UpdateType()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, TestConfig.Username);
            if (!client.TypeAPI.Exists(Person.PersonTypeName).Value)
                return;
            var resp = client.TypeAPI.GetDataType(Person.PersonTypeName);
            IDataType personType = resp.Value;
            ITypeBuilder tb = new TypeBuilder(personType.TypeInfo)
                                    .AddArrayOfStringProperty("Pets")
                                    .AddBooleanProperty("WearsOldSwimPants", true);
            //update type Person
            var updateResp = client.TypeAPI.Update(tb.ResultWithName(Person.PersonTypeName));
        }

        [TestMethod]
        public void DeleteType()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, TestConfig.Username);
            client.TypeAPI.Delete(Person.PersonTypeName);
        }
    }
}

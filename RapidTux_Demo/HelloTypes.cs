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
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.TypeAPI.GetDataTypes();
            string typeNames = string.Join(", ", resp.Value.Types.Select( t => t.Name));
            Debug.WriteLine(typeNames);
        }

        [TestMethod]
        public void CreateType()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");

            //create types
            TypeBuilder tb = new TypeBuilder();
            IDataType person = tb.AddStringProperty("navn", "Preben Fuglekær")
                                   .AddIntegerProperty("alder", 96)
                                   .AddStringProperty("email", "")
                                   .AddBooleanProperty("ErÆgtefælle", false)
                                   .AddBooleanProperty("ErBarn", false)
                                   .AddObjectProperty("Relationer", "Person", false, true)
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
        public void DeleteType()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            client.TypeAPI.Delete(Person.PersonTypeName);
        }
    }
}

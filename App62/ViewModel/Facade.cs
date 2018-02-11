using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using App62.Model;
using Newtonsoft.Json;

namespace App62.ViewModel
{
    class Facade
    {
        private static string jsonFileName = "PizzasAsJson.dat";
        private static string xmlFileName = "PizzasAsXml.dat";

        public static async void SavePizzasAsJsonAsync(ObservableCollection<Pizza> pizzas)
        {
            string serializeObject = JsonConvert.SerializeObject(pizzas);
            SerializePizzasFileAsync(serializeObject, jsonFileName);
        }

        public static async Task<List<Pizza>> LoadPizzasFromJsonAsync()
        {
            string pizzasJosonString = await DeSerializePizzasFileAsync(jsonFileName);
            return (List<Pizza>)JsonConvert.DeserializeObject(pizzasJosonString, typeof(List<Pizza>));
        }

        public static async void SavePizzasAsXmlAsync(ObservableCollection<Pizza> pizzas)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(pizzas.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, pizzas);
            SerializePizzasFileAsync(textWriter.ToString(), xmlFileName);
        }

        public static async Task<ObservableCollection<Pizza>> LoadPizzasFromXmlAsync()
        {
            string pizzasXmlString = await DeSerializePizzasFileAsync(xmlFileName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Pizza>));
            return (ObservableCollection<Pizza>)xmlSerializer.Deserialize(new StringReader(pizzasXmlString));
        }


        public static async void SerializePizzasFileAsync(string PizzasString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, PizzasString);
        }

        public static async Task<string> DeSerializePizzasFileAsync(String fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(localFile);
        }
    }
}

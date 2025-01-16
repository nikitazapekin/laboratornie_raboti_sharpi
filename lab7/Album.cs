using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
 
using System.Xml.Serialization;
using System.IO;
using lab7;

[Serializable]
  public  class Album
    {
        public string username;
        public string name;
        public DateTime time;
        public List<NOTE> notes = new List<NOTE>()

            {
       new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
          new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
             new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                   new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                      new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                         new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                            new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                               new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                                  new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
                                     new NOTE { Author="Alex", Title="Test", Date=DateTime.Now},
             
              };

  public  Album()
    {

    }




        public void ReadXmlData(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                throw new FileNotFoundException("XML файл не найден.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Album));
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
            {
                Album album = (Album)serializer.Deserialize(fs);
                notes = album.notes;  
            }
        }

        public void SaveToXml(string xmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Album));
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, this);  
            }
        }


    }
 
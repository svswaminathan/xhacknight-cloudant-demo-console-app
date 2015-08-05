using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudantClient.PCL;

namespace CloudantDemoApp
{
    class Program
    {
        static CloudantClient.PCL.CloudantClient cloudantClient = new CloudantClient.PCL.CloudantClient();
        static string databaseName = "xhacknight_demo";
        static void Main(string[] args)
        {
            CreateDB();
            GetAllDbs();
            GetDB();
            CreateDoc();
            GetAllDocs();

            Console.WriteLine("Press any key to exit ... ");
            Console.ReadKey();
        }

        
        private static void CreateDB()
        {
            Console.WriteLine("Invoking Cloudant Create DB for {0}",databaseName);
            var response = cloudantClient.CreateDB(databaseName);
            Console.WriteLine("Database creation status : {0}",response);
        }
        
        private static void CreateDoc()
        {
            Console.WriteLine("Invoking Cloudant Create Doc for {0}", databaseName);
            var response = cloudantClient.CreateDoc(databaseName);
            Console.WriteLine("Document created : {0}", response);
        }
        
        private static void GetAllDbs()
        {
            Console.WriteLine("Invoking Cloundant All DBs");
            var availableDbs = cloudantClient.GetAllDbs();
            Console.WriteLine("Available DBs are : {0}", availableDbs);
        }

        private static void GetDB()
        {
            Console.WriteLine("Invoking Cloundant Get DB for {0}",databaseName);
            var response = cloudantClient.GetDB(databaseName);
            Console.WriteLine("DB metadata for {0} are {1}", databaseName,response);
        }

        private static void GetAllDocs()
        {
            Console.WriteLine("Invoking Cloundant Get All docs for {0}", databaseName);
            var response = cloudantClient.GetAllDocs(databaseName);
            Console.WriteLine("Documents from {0} are {1}", databaseName, response);
        }
    }
}

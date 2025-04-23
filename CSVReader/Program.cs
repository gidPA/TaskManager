// See https://aka.ms/new-console-template for more information
var csvReader = new CSVReader("startup/LibraryData.csv", new string[]{"BookId", "Title", "Author"});
var libraryData = csvReader.ReadCSVFile();

foreach(var bookData in libraryData){
    Console.WriteLine("Book ID: {0}", bookData["BookId"]);
    Console.WriteLine("Title: {0}", bookData["Title"]);
    Console.WriteLine("Author: {0}", bookData["Author"]);
    Console.WriteLine("");
}
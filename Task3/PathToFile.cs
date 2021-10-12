using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    class PathToFileInfo
    {
        string Path { get; set; }

        public PathToFileInfo(string path = "Not found")
        {
            Path = path;
        }

        //коренева пака це перша папка після диску
        //найвища у ієрархії
        public string GetRootFolder()
        {
            try
            {
                //всі неправильні шляхи не будуть прийматись
                if (!File.Exists(Path))
                    throw new FileNotFoundException("File not found");

                string[] parts = Path.Split('\\');
                if (parts.Length < 3)
                    throw new ArgumentException("There is no root folder ");

                //папка після диску є кореневою
                return parts[1];
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }

        }
        public string GetFileName()
        {
            try
            {
                if (!File.Exists(Path))
                {
                    throw new FileNotFoundException("File not found");
                }
                string[] parts = Path.Split('\\');
                if (parts.Length == 0)
                    throw new ArgumentException("Path is empty");
                //останній елемент це і є файл і його тип
                string[] nameAndType = parts[parts.Length - 1].Split('.');
                if (nameAndType.Length != 2)
                    throw new ArgumentException("Wrong path to the file");

                return nameAndType[0];
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return "Error";
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public override string ToString()
        {
            string res = string.Format($"Path: {Path}\nName of File: {GetFileName()}" +
                $"\nRoot Folder: {GetRootFolder()}");
            return res;
        }
    }
}

using StorageLibrary.NetCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Frontend
{
    [Serializable]
    public class Menu
    {
        ConsoleMethods methods = new ConsoleMethods();

        public void MainMenu()
        {
            if (File.Exists("data.txt"))
            {
                methods.UploadBinaryFile();
            }          
 
            try
            {
                Console.Clear();
                
                int userInput = 0;

                do
                {
                    userInput = DisplayMenu(); 
                    Console.Clear();
                    switch (userInput)
                    {
                        case 1:
                            Console.SetCursorPosition(40, 8);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Clear();
                            methods.AddPackage(ChoosePackageType());
                            
                            break;
                        case 2:
                            Console.SetCursorPosition(40, 9);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            methods.RemovePackage();
                            break;
                        case 3:
                            Console.SetCursorPosition(40, 10);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            methods.MovePackage();
                            break;
                        case 4:
                            Console.SetCursorPosition(25, 11);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            methods.FindLocationOnID();
                            break;
                        case 5:
                            methods.FindPackageOnID();
                            break;
                        case 6:
                            methods.ListAllLocations();
                            break;
                        case 7:
                            methods.FillTenExamples();
                            break;
                        case 8:
                            Console.SetCursorPosition(26, 10);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("Thank you for using | Desirés WareHouse-System 1.0|");
                            Console.SetCursorPosition(34, 12);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("We hope you had a nice day at work!");
                            Console.SetCursorPosition(40, 20);
                            Console.Write("Press");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" \"ENTER\" ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(" to exit ");
                            Console.ReadLine();
                            break;
                        default: 
                            break;
                    }

                } while (userInput != 8); 

                methods.SaveinBinaryFile();
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Wrong input");
            }           
        }
         static int DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(32, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Desirés Warehouse System 1.0 ");
            Console.SetCursorPosition(30, 6);
            Console.WriteLine("----------------------------------------------");
            Console.SetCursorPosition(55, 25);
            Console.WriteLine("System administrator:");
            Console.SetCursorPosition(55, 26);
            Console.WriteLine("Desiré Tillerås");
            Console.SetCursorPosition(55, 27);
            Console.WriteLine("Tel: 070 228 69 84");
            Console.SetCursorPosition(30, 25);
            Console.WriteLine("Address:");
            Console.SetCursorPosition(30, 26);
            Console.WriteLine("Kyrkogatan 27");
            Console.SetCursorPosition(30, 27);
            Console.WriteLine("432 41 Varberg");
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. New Package");
            Console.SetCursorPosition(40, 10);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("2. Remove Package");
            Console.SetCursorPosition(40, 11);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("3. Switch place for Package");
            Console.SetCursorPosition(40, 12);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("4. Find Location for package based on ID");
            Console.SetCursorPosition(40, 13);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("5. Show packageinfo based on ID");
            Console.SetCursorPosition(40, 14);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("6. List all packages in the warehouse");
            Console.SetCursorPosition(40, 15);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("7. Fill warehouse with 10 example packages");
            Console.WriteLine();
            Console.SetCursorPosition(40, 17);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("8. Save to file and Exit");
            Console.SetCursorPosition(60, 20);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|");
            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|   Enter choice: ");

            Console.ForegroundColor = ConsoleColor.White;
            var choice = ReadInt();
            return choice;
          }

        public static ConsoleMethods.PackageType ChoosePackageType()
        {
            int choiceOfPackage = 0;
            
            ConsoleMethods.PackageType packageType = ConsoleMethods.PackageType.none;
            while (packageType == ConsoleMethods.PackageType.none) 
            {
                Console.SetCursorPosition(40, 9);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Blob (Fragile)");
                Console.SetCursorPosition(40, 10);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("2. Cube");
                Console.SetCursorPosition(40, 11);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("3. Cubeiod");
                Console.SetCursorPosition(40, 12);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("4. Sphere");
                Console.SetCursorPosition(40, 13);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("5. Return to Main menu");
                Console.SetCursorPosition(60, 20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|");
                Console.SetCursorPosition(40, 20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|   Enter choice: ");

                Console.ForegroundColor = ConsoleColor.White;
                choiceOfPackage = ReadInt();

                switch (choiceOfPackage)
                {
                    case 1:                         
                        packageType = ConsoleMethods.PackageType.BLOB;
                        break;
                        
                    case 2: 
                        packageType = ConsoleMethods.PackageType.CUBE;
                        break;
                        
                    case 3: 
                        packageType = ConsoleMethods.PackageType.CUBEIOD;
                        break;
                        
                    case 4: 
                        packageType = ConsoleMethods.PackageType.SPHERE;
                        break;
                    case 5:
                        Menu ny = new Menu();
                        ny.MainMenu();
                        break;
                    default:
                        break;
                        
                }                
            } 
            return packageType;

        }
       public static int ReadInt()
        {
            int integer;
            while (!int.TryParse(Console.ReadLine(), out integer))
            {               
                Console.Write("Invalid input. You have to use a number. Try again: ");
            }
            return integer;
        }
    }
}

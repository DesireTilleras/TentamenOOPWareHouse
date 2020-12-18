using System;
using System.Collections.Generic;
using System.Text;
using StorageLibrary.NetCore;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Frontend
{
    [Serializable]
    public class ConsoleMethods
    {

        WareHouse wareHouse = new WareHouse();

        public enum PackageType
        {
            BLOB, CUBE, CUBEIOD, SPHERE, none
        }

        /// <summary>
        /// Saving the object of WareHouse in a BinaryFile.
        /// </summary>
        public void SaveinBinaryFile()
        {
            string filePath = "data.txt";
            IOBinary dataSerializer = new IOBinary();

            dataSerializer.BinarySerialize(wareHouse, filePath);            

        }
        /// <summary>
        /// Uploading the object from a binary file
        /// </summary>
        public void UploadBinaryFile()
        {
            string filePath = "data.txt";
            IOBinary dataSerializer = new IOBinary();
            wareHouse = dataSerializer.BinaryDeserialize(filePath) as WareHouse;
        }

        /// <summary>
        /// Depending on which package the user chose, the method will create a package and then call the StorePackage() Method.
        /// </summary>
        /// <param name="package"></param>
        public void AddPackage(PackageType package)
        {
            Packages newPackage;

            if (package == PackageType.BLOB)
            {
                newPackage = wareHouse.CreateBlob(QuestionOfDescription(), QuestionOfWeight(), QuestionOfSide(), true, QuestionInsuranceValue());
            }
            else if (package == PackageType.CUBE)
            {
                newPackage = wareHouse.CreateCube(QuestionOfDescription(), QuestionOfWeight(), QuestionOfSide(), QuestionOfFragile(), QuestionInsuranceValue());
            }
            else if (package == PackageType.CUBEIOD)
            {
                newPackage = wareHouse.CreateCubeoid(QuestionOfDescription(), QuestionOfWeight(), QuestionOfFragile(), QuestionOfXSide(), QuestionOfYSide(), QuestionOfZSide(), QuestionInsuranceValue());
            }
            else
            {
                newPackage = wareHouse.CreateSphere(QuestionOfDescription(), QuestionOfWeight(), QuestionOfFragile(), QuestionOfRadius(), QuestionInsuranceValue());
            }
            Console.WriteLine($"Your box has ID : {newPackage.ID}");
            StorePackage(newPackage);

        }
        /// <summary>
        /// This method will ask the user if the package is to be stored automatically or manually.
        /// </summary>
        /// <param name="package"></param>
        public void StorePackage(Packages package)
        {
            string choice = "";
            while(choice != "A" || choice != "M")
            {
                Console.WriteLine("Do you want to manually store the package? Or do you want it automatically stored?\n" +
                                "M for Manually, A for Automatically : ");
                choice = Console.ReadLine().ToUpper();
                if (choice == "M")
                {
                    Console.WriteLine("On what level do you want to store the package? You can choose between 0,1 and 2");
                    int level = int.Parse(Console.ReadLine());
                    Console.WriteLine($"On which spot do you want to store the package on level {level}? You can choose between 0-99");
                    int spot = int.Parse(Console.ReadLine());
                    bool canBeStored = wareHouse.TryToStorePackageManually(package, spot, level);
                    if (canBeStored)
                    {
                        Console.WriteLine($"The package is successfully stored at spot : {spot} on floor: {level}");
                        Console.WriteLine($"The package ID is : {package.ID}");                        
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("It cannot be stored here " +
                            "it exceeds either the weight or the maximum dimensions of the warehouse location" +
                            "\n press enter for main menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    }

                }
                if (choice == "A")
                {
                    bool canBeStored = wareHouse.TryToStorePackage(package, out int location, out int floor);
                    if (canBeStored)
                    {
                        Console.WriteLine($"The package is successfully stored at spot : {location} on floor: {floor}");
                        Console.WriteLine($"The package ID is : {package.ID}");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    }
                    else
                    {
                        Console.WriteLine("It cannot be stored here " +
                            "it exceeds either the weight or the maximum dimensions of the warehouse location" +
                            "\n press enter for main menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("You can only choose A or M, press enter");
                    Console.ReadLine();
                    Console.Clear();

                }

            } 
           
        }

        /// <summary>
        /// This method asks the user for a description and returns a string.
        /// </summary>
        /// <returns></returns>
        public string QuestionOfDescription()
        {
            Console.Clear();
            Console.WriteLine("Please enter a short description of whats in the package , for example \"Clothes\": ");
            string description = Console.ReadLine();
            return description;

        }
        /// <summary>
        /// This method asks the user for the weight of the package and returns a double.
        /// </summary>
        /// <returns></returns>
        public double QuestionOfWeight()
        {
            Console.WriteLine("How much does the package weigh? Enter in kg (maximum 1 000 kg)");
            double weight = double.Parse(Console.ReadLine());
            if (weight>1000 || weight < 0)
            {
                Console.WriteLine("The package can only weigh between 0 and 1 000 kg");
            }
            return weight;

        }
        /// <summary>
        /// This method asks the user about the side of the package. Returns an int.
        /// </summary>
        /// <returns></returns>
        public int QuestionOfSide()
        {
            Console.WriteLine("How long is the side of the package? Please enter in whole cm");
            int side = Menu.ReadInt();
            if (side > 300 || side < 0)
            {
                Console.WriteLine("(the side can only be between 0 and 300 cm)");
            }
            return side;
        }
        /// <summary>
        /// This method is only relevant for the package = Sphere. Returns an integer
        /// </summary>
        /// <returns></returns>
        public int QuestionOfRadius()
        {

            Console.WriteLine("How long is the radius of the package? Please enter in whole cm");
            int radius = Menu.ReadInt();
            return radius;

        }
        /// <summary>
        /// Asks the user about XSide, which is only relevant for the Cubeiod. XSide = Width.
        /// </summary>
        /// <returns></returns>
        public int QuestionOfXSide()
        {

            Console.WriteLine("Please enter the width of the package : ");
            int xside = int.Parse(Console.ReadLine());
            return xside;

        }
        /// <summary>
        /// Asks the user about YSide, which is only relevant for the Cubeiod. YSide = Height.
        /// </summary>
        /// <returns></returns>
        public int QuestionOfYSide()
        {

            Console.WriteLine("Please enter the height of the package : ");
            int yside = int.Parse(Console.ReadLine());
            return yside;

        }
        /// <summary>
        /// Asks the user about ZSide, which is only relevant for the Cubeiod. ZSide = Depth.
        /// </summary>
        /// <returns></returns>
        public int QuestionOfZSide()
        {

            Console.WriteLine("Please enter the depth of the package : ");
            int zside = int.Parse(Console.ReadLine());
            return zside;

        }
        /// <summary>
        /// Asks the user if the package is Fragile. Returns a true or false.
        /// </summary>
        /// <returns></returns>
        public bool QuestionOfFragile()
        {
            string answer = "";

            while(answer != "Y"|| answer !="N")
            {
                Console.WriteLine("Is the package fragile? Press Y for Yes and N for No");
                answer = Console.ReadLine().ToUpper();

                if (answer == "Y")
                {
                    return true;                    
                   
                }
                if (answer == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("You can only choose between Y or N, press enter to try again");                   

                }
            }

            return false;
        }
        /// <summary>
        /// Asks the user of the Insurance value for the package, returns a decimal
        /// </summary>
        /// <returns></returns>
        public decimal QuestionInsuranceValue()
        {
            Console.WriteLine("Please enter the insurance value of the package. Enter 0 if it has no value : ");
            decimal value = decimal.Parse(Console.ReadLine());
            return value;

        }
        /// <summary>
        /// This method removes a package, if the package ID exists. It calls the methods QuestionForID(), 
        /// FoundPackageOnID() and RemovePackageOnID()
        /// </summary>
        public void RemovePackage()
        {
            int packageID = QuestionForID();
            Packages copyOfPackage = wareHouse.GetCopy(packageID);
            bool packageIsFound = wareHouse.FoundPackageOnID(packageID, out int placedLocation, out int placedLevel);
            if (packageIsFound)
            {
                wareHouse.RemovePackageOnID(copyOfPackage,packageID);
                Console.WriteLine($"The package is now removed from spot : {placedLocation} on level: {placedLevel}"+
                    "\n press enter for main menu" );
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Cannot find the package, press enter for main menu");
                Console.ReadLine();
                Console.Clear();
            }

        }
        /// <summary>
        /// This method moves a package and calls on the methods : QuestionForID(), FoundPackageOnID(), GetCopy(), TryTotorePackageManually(), RemovePackageOnID()
        /// If the package is found, a copy will be made, then the user gets to chose where the package is to be moved.
        /// If the move succeeds, the old package will be removed.
        /// </summary>
        public void MovePackage()
        {
            Console.WriteLine("Moving package");
            int packageID = QuestionForID();
            bool packageIDIsFound = wareHouse.FoundPackageOnID(packageID, out int placedLocation, out int placedLevel);
            if (packageIDIsFound)
            {
                Packages copyOfPackage = wareHouse.GetCopy(packageID);
                Console.WriteLine($"The package is now stored at spot : {placedLocation} and level :{placedLevel}");
                Console.WriteLine("------ Choose new location-------");
                Console.WriteLine("On what level do you want to store the package? You can choose between 0,1 and 2");
                int level = Menu.ReadInt();
                Console.WriteLine($"On which spot do you want to store the package on level {level}? You can choose between 0-99");
                int spot = Menu.ReadInt();
                if (level > 2 || spot > 99)
                {
                    Console.Clear();
                    Console.WriteLine("You can only enter number between 0-2 for level and 0-99 for spot, press enter to start over");
                    Console.ReadLine();
                    MovePackage();
                }
                else
                {
                    wareHouse.RemovePackageOnID(copyOfPackage, packageID);

                    if (wareHouse.TryToStorePackageManually(copyOfPackage, spot, level))
                    {
                        Console.WriteLine($"The package is now at spot : {spot} and level : {level}");
                    }
                    else
                    {
                        Console.WriteLine("The package can't be stored here, press enter for main menu");
                        wareHouse.TryToStorePackageManually(copyOfPackage, placedLocation, placedLevel);
                    }

                }
            }
            else
            {
                Console.WriteLine("Cannot find the package, press enter for main manu");
            }
            Console.ReadLine();
            Console.Clear();


        }
        /// <summary>
        /// Finds the location for the package based on ID.
        /// </summary>
        public void FindLocationOnID()
        {
            int packageID = QuestionForID();
            bool packageIsFound = wareHouse.FoundPackageOnID(packageID, out int placedLocation, out int placedLevel);
            if (packageIsFound)
            {
                Console.WriteLine($"The package is found at spot :{placedLocation} Level: {placedLevel} " +
                    $"\n Press enter for main menu");
            }
            else
            {
                Console.WriteLine("Can't find the package" +
                    "\nPress enter for main menu");
            }
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// Asks the user for ID. Returns an integer
        /// </summary>
        /// <returns></returns>
        public int QuestionForID()
        {
            Console.Clear();
            Console.Write("Please enter the ID for the package  ");
            Console.Write("(Only numbers) :");
            int id = Menu.ReadInt();
            return id;
        }
        /// <summary>
        /// Fills the array with 10 items and writes them out on the console. Calling the ToString() and TryToStorePackageManually()
        /// </summary>
        public void FillTenExamples()
        {
            Packages examplePackage1 = wareHouse.CreateBlob("Sackosäck", 2.5, 80, true, 200);
            int placedLocation1 = 0;
            int placedLevel = 0;
            wareHouse.TryToStorePackageManually(examplePackage1, placedLocation1, placedLevel);

            Packages examplePackage2 = wareHouse.CreateCube("Datorskärm", 5, 40, false, 1500);
            int placedLocation2 = 0;
            int placedLevel2 = 1;
            wareHouse.TryToStorePackageManually(examplePackage2, placedLocation2, placedLevel2);

            Packages examplePackage3 = wareHouse.CreateCube("Dator", 10, 40, false, 35000);
            int placedLocation3 = 0;
            int placedLevel3 = 1;
            wareHouse.TryToStorePackageManually(examplePackage3, placedLocation3, placedLevel3);

            Packages examplePackage4 = wareHouse.CreateBlob("Klänning", 1, 150, true, 550);
            int placedLocation4 = 2;
            int placedLevel4 = 2;
            wareHouse.TryToStorePackageManually(examplePackage4, placedLocation4, placedLevel4);

            Packages examplePackage5 = wareHouse.CreateCubeoid("Gräsklippare", 75, false, 65, 150, 65, 15000);
            int placedLocation5 = 15;
            int placedLevel5 = 0;
            wareHouse.TryToStorePackageManually(examplePackage5, placedLocation5, placedLevel5);

            Packages examplePackage6 = wareHouse.CreateCubeoid("Häst", 550, false, 200, 160, 65, 1000000);
            int placedLocation6 = 25;
            int placedLevel6 = 2;
            wareHouse.TryToStorePackageManually(examplePackage6, placedLocation6, placedLevel6);

            Packages examplePackage7 = wareHouse.CreateSphere("Badboll", 1, false, 25, 30);
            int placedLocation7 = 99;
            int placedLevel7 = 1;
            wareHouse.TryToStorePackageManually(examplePackage7, placedLocation7, placedLevel7);

            Packages examplePackage8 = wareHouse.CreateSphere("Bowlingklot", 12, false, 15, 25000);
            int placedLocation8 = 82;
            int placedLevel8 = 0;
            wareHouse.TryToStorePackageManually(examplePackage8, placedLocation8, placedLevel8);

            Packages examplePackage9 = wareHouse.CreateBlob("Vas", 5, 30, true, 1500);
            int placedLocation9 = 43;
            int placedLevel9 = 1;
            wareHouse.TryToStorePackageManually(examplePackage9, placedLocation9, placedLevel9);

            Packages examplePackage10 = wareHouse.CreateCubeoid("Motorcross", 100, false, 150, 80, 40, 75000);
            int placedLocation10 = 33;
            int placedLevel10 = 2;
            wareHouse.TryToStorePackageManually(examplePackage10, placedLocation10, placedLevel10);

            List<Packages> listOfExamples = new List<Packages>
            {
                examplePackage1, examplePackage2, examplePackage3, examplePackage4, examplePackage5, examplePackage6, examplePackage7, examplePackage8, examplePackage9, examplePackage10

            };

            foreach (var example in listOfExamples)
            {
                Console.WriteLine(example.ToString());
            }

            Console.ReadLine();
            Console.Clear();

        }
        /// <summary>
        /// Listing all packages in the warehouse
        /// </summary>
        public void ListAllLocations()
        {

            for (int i = 0; i < wareHouse.NumberOfLocations; i++)
            {
                for (int j = 0; j < wareHouse.NumberOfFloors; j++)
                {
                    Console.Write($"Location : {i}, Level : {j}");

                    foreach (Packages package in wareHouse[i, j])
                    {
                        Console.Write($"  Package ID : {package.ID} Description : {package.Description}");
                    }
                    Console.WriteLine();

                }

            }
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// Finding packages based on ID. Calling the methods QuestionForID(), FoundPackageOnID(), GetCopy()
        /// </summary>
        public void FindPackageOnID()
        {
            Console.WriteLine("Printing info on certain package");
            int packageID = QuestionForID();
            bool packageIDIsFound = wareHouse.FoundPackageOnID(packageID, out int placedLocation, out int placedLevel);
            if (packageIDIsFound)
            {
                Console.WriteLine();
                Console.WriteLine($"Package is located on spot: {placedLocation} and level : {placedLevel}");
                Packages copyOfPackage = wareHouse.GetCopy(packageID);
                Console.WriteLine(copyOfPackage.ToString());
            }
            else
            {
                Console.WriteLine("Cannot find the package");
            }
            Console.ReadLine();
            Console.Clear();

        }
    }
}

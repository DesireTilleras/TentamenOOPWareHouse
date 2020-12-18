﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StorageLibrary
{
    [Serializable]
    public class WareHouse
    {
        private int currentIDCount = 1;

        private WareHouseLocation[,] WareHouseLocations;
        private readonly int numberOfLocations = 100;
        private readonly int numberOfFloors = 3;

        public int NumberOfLocations { get => numberOfLocations; }
        public int NumberOfFloors { get => numberOfFloors; }



        /// <summary>
        /// Creates a constructor that creates an array from the class WareHouseLocations, is this class, there is a list representing each spot in the warehouse
        /// </summary>
        public WareHouse()
        {
            WareHouseLocations = new WareHouseLocation[numberOfLocations, numberOfFloors];

            for (int i = 0; i < WareHouseLocations.GetLength(0); i++)
            {
                for (int j = 0; j < WareHouseLocations.GetLength(1); j++)
                {
                    WareHouseLocations[i, j] = new WareHouseLocation();
                }
            }
        }
        /// <summary>
        /// An indexer to be able to use foreach when printing the list of the warehouse locations.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public WareHouseLocation this[int index, int index2]
        {
            get
            {
                var temp = WareHouseLocations[index, index2];
                return temp;
            }

        }
        public Blob CreateBlob(string description, double weight, int side, bool isFragile, decimal insuranceValue)
        {
            return new Blob(currentIDCount++, description, weight, isFragile, side, insuranceValue);
        }
        public Cube CreateCube(string description, double weight, int side, bool isFragile, decimal insuranceValue)
        {
            return new Cube(currentIDCount++, description, weight, side, isFragile, insuranceValue);
        }
        public Cubeiod CreateCubeoid(string description, double weight, bool isFragile, int xSide, int ySide, int zSide, decimal insuranceValue)
        {
            return new Cubeiod(currentIDCount++, description, weight, isFragile, xSide, ySide, zSide, insuranceValue);
        }
        public Sphere CreateSphere(string description, double weight, bool isFragile, int radius, decimal insuranceValue)
        {
            return new Sphere(currentIDCount++, description, weight, isFragile, radius, insuranceValue);
        }
        /// <summary>
        /// Automatically storing the package in the list. Calling the method TryToCheckInPackage()
        /// </summary>
        /// <param name="package"></param>
        /// <param name="placedLocation"></param>
        /// <param name="placedFloor"></param>
        /// <returns></returns>
        public bool TryToStorePackage(Packages package, out int placedLocation, out int placedFloor)
        {
            for (int i = 0; i < WareHouseLocations.GetLength(0); i++)
            {
                for (int j = 0; j < WareHouseLocations.GetLength(1); j++)
                {
                    if (IsRoomInStack(j))
                    {
                        if (WareHouseLocations[i, j].TryToCheckInPackage(package))
                        {

                            placedLocation = i;
                            placedFloor = j;
                            return true;


                        }
                    }

                }
            }
            placedLocation = 0;
            placedFloor = 0;
            return false;
        }
        /// <summary>
        /// If it is possible to store the package on the spot and level that the user chose, then this method will return true.
        /// Calling the TryToCheckInPackage() method
        /// </summary>
        /// <param name="package"></param>
        /// <param name="placedLocation"></param>
        /// <param name="placedFloor"></param>
        /// <returns></returns>
        public bool TryToStorePackageManually(Packages package, int placedLocation, int placedFloor)
        {
            if (WareHouseLocations[placedLocation, placedFloor].TryToCheckInPackage(package))
            {
                if (IsRoomInStack(placedFloor))
                {
                    return true;
                }
                
            }
            return false;

        }

        /// <summary>
        /// This method will return true if the spot contains the ID the user chose. Calls the method ifSpotContainsID()
        /// </summary>
        /// <param name="id"></param>
        /// <param name="placedLocation"></param>
        /// <param name="placedLevel"></param>
        /// <returns></returns>
        public bool FoundPackageOnID(int id, out int placedLocation, out int placedLevel)
        {
            for (int i = 0; i < WareHouseLocations.GetLength(0); i++)
            {
                for (int j = 0; j < WareHouseLocations.GetLength(1); j++)
                {
                    if (WareHouseLocations[i, j].ifSpotcontainsID(id))
                    {
                        placedLocation = i;
                        placedLevel = j;
                        return true;
                    }
                }
            }
            placedLocation = 0;
            placedLevel = 0;
            return false;
        }
        /// <summary>
        /// This will return true if it was possible to remove the package based on ID.
        /// Calls the method TryToRemovePackage() method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePackageOnID(int id)
        {
            for (int i = 0; i < WareHouseLocations.GetLength(0); i++)
            {
                for (int j = 0; j < WareHouseLocations.GetLength(1); j++)
                {
                    if (WareHouseLocations[i, j].TryToRemovePackage(id))
                    {

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This will create a copy of the package and return it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Packages GetCopy(int id)
        {
            for (int i = 0; i < WareHouseLocations.GetLength(0); i++)
            {
                for (int j = 0; j < WareHouseLocations.GetLength(1); j++)
                {
                    if (WareHouseLocations[i, j].ifSpotcontainsID(id))
                    {
                        Packages copy = WareHouseLocations[i, j].GetCopyOfPackage(id);
                        return copy;
                    }

                }
            }
            return null;
        }

        /// <summary>
        /// Checks if there is room in the stack.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsRoomInStack(int level)
        {
            List<double> listWeightSpots = new List<double>();
            double weight = 0;
            double weightInStack = 0;
            for (int i = 0; i < WareHouseLocations.GetLength(0); i++) //100 spots
            {
                for (int j = level; j < WareHouseLocations.GetLength(1); j++) //3 levels
                {
                    // I save the weight from the spot in "weight"
                    weight = WareHouseLocations[i, j].weightInSpot();
                    
                    listWeightSpots.Add(weight);
                }

                for (int k = 0; k < listWeightSpots.Count; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        weightInStack += listWeightSpots[k];
                    }
                    if (weightInStack < 2000)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public IEnumerator Enumerator()
        {
            return WareHouseLocations.GetEnumerator();
        }

    }
}

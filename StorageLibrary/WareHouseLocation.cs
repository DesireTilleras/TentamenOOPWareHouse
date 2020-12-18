using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace StorageLibrary
{
    [Serializable]
    public class WareHouseLocation : IEnumerable
    {
        List<Packages> listOfPackages = new List<Packages>();


        internal List<Packages> ListOfPackages { get => listOfPackages; }

        private double currentVolume;
        private double currentWeight;
        private bool spotHasFragilePackage = false;

        private double currentWeightInWarehouse;

        double MaxWeight = 1000;
        int MaxHeight = 200;
        int MaxWidth = 300;
        int MaxDepth = 200;

        int MaxVolume { get; }
        /// <summary>
        /// A constructor for the parameters of the location in the warehouse. 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        /// <param name="maxWeight"></param>
        public WareHouseLocation()
        {
            this.MaxHeight = 200;
            this.MaxDepth = 200;
            this.MaxWidth = 300;
            this.MaxWeight = 1000;
            this.MaxVolume = (200 * 300 * 200) / 1000000;
        }
        /// <summary>
        /// Checks if package can be checked in, if it can, it will check it in the list and then return true.
        /// </summary>
        /// <returns></returns>
        internal bool TryToCheckInPackage(Packages package)
        {
            // If the list contains a fragile package, it shuld return false. No other package can be added in the list.
            // If the incoming package is Fragile = It should add the package in the list if the currentVolume is 0

            if (package.IsFragile == true)
            {
                if (currentVolume == 0)
                {
                    if (currentVolume + package.Volume > MaxVolume)
                    {
                        return false;
                    }
                    // If the total weight of 1000 kg is exceeded in the list, the package cannot be stored in this location.
                    if (currentWeight + package.Weight > MaxWeight)
                    {
                        return false;
                    }
                    if (package.longestSide > MaxWidth)
                    {
                        return false;
                    }
                    else
                    {
                        if (currentWeightInWarehouse + package.Weight < 175000)
                        {
                            listOfPackages.Add(package);
                            currentWeightInWarehouse += package.Weight;
                            currentVolume += package.Volume;
                            currentWeight += package.Weight;
                            spotHasFragilePackage = true;
                            return true;
                        }


                    }
                }
                else
                {
                    return false;
                }
            }
            // If the list contains a Fragile package = the incoming package can't be stored here
            if (spotHasFragilePackage)
            {
                return false;
            }
            // If the current volume + new package volume exceeds MaxVolume, it cannot be stored in this location.
            if (currentVolume + package.Volume > MaxVolume)
            {
                return false;
            }
            // If the total weight of 1000 kg is exceeded, the package cannot be stored in this location.
            if (currentWeight + package.Weight > MaxWeight)
            {
                return false;
            }
            // If the longest side of the package exceed the max width (which is the max dimension of the spot = 300 cm), the incoming package can't be stored here
            // Max length = 200, max depth = 200            
            if (package.longestSide > MaxWidth)
            {
                return false;
            }
            else
            {
                if (currentWeightInWarehouse + package.Weight < 175000)
                {
                    listOfPackages.Add(package);
                    currentWeightInWarehouse += package.volume;
                    currentVolume += package.Volume;
                    currentWeight += package.Weight;
                    return true;
                }
            }
            return false;

        }
        /// <summary>
        /// Checks if the list contains the ID. Returns true or false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool ifSpotcontainsID(int id)
        {
            for (int i = 0; i < listOfPackages.Count; i++)
            {
                if (listOfPackages[i].ID == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the total weight of the warehouse location
        /// </summary>
        /// <returns></returns>
        internal double weightInSpot()
        {
            double totalWeightInSpot = 0;
            for (int i = 0; i < listOfPackages.Count; i++)
            {
                totalWeightInSpot += listOfPackages[i].Weight;
                
            }
            return totalWeightInSpot;
        }
        /// <summary>
        /// If it is possible to remove package, it will return true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool TryToRemovePackage(int id)
        {
            for (int i = 0; i < listOfPackages.Count; i++)
            {
                if (listOfPackages[i].ID == id)
                {
                    currentVolume -= listOfPackages[i].volume;
                    currentWeight -= listOfPackages[i].volume;
                    currentWeightInWarehouse -= listOfPackages[i].Weight;
                    listOfPackages.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Returns a copy of a specific package
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal Packages GetCopyOfPackage(int id)
        {
            for (int i = 0; i < listOfPackages.Count; i++)
            {
                if (listOfPackages[i].ID == id)
                {
                    Packages copyOfBox = listOfPackages[i];
                    return copyOfBox;
                }
            }
            return null;
        }
        /// <summary>
        /// Method for returning the list of packages, for cloning
        /// </summary>
        /// <returns></returns>
        public List<Packages> Content()
        {
            return listOfPackages;
        }
        /// <summary>
        /// Method for cloning the WareHouseLocations
        /// </summary>
        /// <returns>Returns a clone of the current WarehouseLocation</returns>
        internal WareHouseLocation Clone()
        {
            WareHouseLocation clone = new WareHouseLocation();
            List<Packages> clonedPackage = Content();
            foreach (Packages package in clonedPackage)
            {
                clone.TryToCheckInPackage(package);
            }
            return clone;
        }




        public IEnumerator Enumerator()
        {
            return listOfPackages.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return listOfPackages.GetEnumerator();
        }
    }
}

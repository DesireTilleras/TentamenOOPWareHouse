using System;
using System.Collections.Generic;
using System.Text;

namespace StorageLibrary
{
    [Serializable]
    public abstract class Packages : I3DObjectStorage
    {
        /// <summary>
        /// Instead of calling it MaxDimension I chose to call it "Longest side". It was esier for me to comprehend.
        /// </summary>

        private int id;
        private string description;
        private double weight;
        internal double volume;
        internal double area;
        internal bool isFragile;
        internal int longestSide;
       
        public int ID { get => id;}
        public string Description { get => description; }
        public double Weight { get => weight;}
        public double Volume { get => volume; }
        public double Area { get => area; }
        public bool IsFragile { get => isFragile; }
        public int LongestSide { get => longestSide;}
        public decimal InsuranceValue { get; set; }
        

        protected Packages(int id, string description, double weight, bool isFragile)
        {
            this.id = id;
            this.description = description;
            this.weight = weight;
            this.isFragile = isFragile;
        }
    
        public abstract override string ToString();      


    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StorageLibrary
{
    // Alla properties utom InsuranceValue sätts när de skapas och är sedan read only.
    // Volume och Area beräknas ut de dimensioner som anges de olika lådorna.
    // Sphere och Blob beräknas som Cube vad gäller Volym och Area.
    // Cube = Side
    // Cubeiod = XSide = bredd, YSide = höjd och ZSide = djup
    // Sphere = Radius
    // Blob = Side
    // Blobs är alltid Fragile
    [Serializable]
    public class Cubeiod : Packages
    {
        public int XSide { get; } // Width
        public int YSide { get; } // Height
        public int ZSide { get; } // Depth
        public Cubeiod(int id, string description, double weight, bool isFragile, int xSide, int ySide, int zSide, decimal insuranceValue) : base(id, description, weight, isFragile)
        {

            List<int> ListOfSides = new List<int> { xSide, ySide, zSide }.OrderBy(x => x).ToList();

            this.XSide = xSide;
            this.YSide = ySide;
            this.ZSide = zSide;
            this.area = Math.Round((double)(ListOfSides[1]) * (double)(ListOfSides[2])/ 10000.00,2);
            this.volume = Math.Round((double)XSide * (double)ZSide * (double)YSide / 1000000.00,2);            
            this.longestSide = ListOfSides[2]; 
            this.InsuranceValue = insuranceValue;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packagetype: Cubeiod");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Weight: {Weight} kg");
            sb.AppendLine("Fragile: " + (IsFragile ? "Yes" : "No"));
            sb.AppendLine($"Width: {XSide} Height : {YSide} Depth: {ZSide} cm");
            sb.AppendLine($"Area: {Area} square m");
            sb.AppendLine($"Volume: {Volume} cubic m");
            sb.AppendLine($"Longest side: {LongestSide} cm");
            sb.AppendLine($"Insurance value: {InsuranceValue}");
            return sb.ToString();
        }


    }
}

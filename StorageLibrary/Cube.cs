using System;
using System.Collections.Generic;
using System.Text;

namespace StorageLibrary
{
    // Alla properties utom InsuranceValue sätts när de skapas och är sedan read only.
    // Volume och Area beräknas ut de dimensioner som anges de olika lådorna.
    // Sphere och Blob beräknas som Cube vad gäller Volym och Area.
    // Cube = Side
    // Cubeiod = XSide, YSide och ZSide
    // Sphere = Radius
    // Blob = Side
    // Blobs är alltid Fragile
    [Serializable]
    public class Cube: Packages
    {
        public int Side { get;}
        public Cube(int id, string description, double weight, int side, bool isFragile, decimal insuranceValue) : base(id, description, weight, isFragile)
        {
            
            this.Side = side;
            this.area = Math.Round((double)side * (double)side / 10000.00,2);
            this.volume = Math.Round((double)side * (double)side * (double)side / 1000000.00,2);
            this.longestSide = side;  
            this.InsuranceValue = insuranceValue;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packagetype: Cube");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Weight: {Weight} kg");
            sb.AppendLine("Fragile: " + (IsFragile ? "Yes" : "No"));
            sb.AppendLine($"Side: {Side} cm");
            sb.AppendLine($"Area: {Area} square m");
            sb.AppendLine($"Volume: {Volume} cubic m");
            sb.AppendLine($"Max Dimension: {LongestSide} cm");
            sb.AppendLine($"Insurance value: {InsuranceValue}");
            return sb.ToString();
        }
    }
}

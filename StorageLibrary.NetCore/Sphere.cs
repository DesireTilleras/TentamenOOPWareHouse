using System;
using System.Collections.Generic;
using System.Text;

namespace StorageLibrary.NetCore
{
    [Serializable]
    public class Sphere : Packages
    {
        public int Radius { get; }
        public Sphere(int id, string description, double weight, bool isFragile, int radius, decimal insuranceValue) : base(id, description, weight, isFragile)
        {
            this.Radius = radius;
            this.area = Math.Round((double)(radius * 2) * (double)(radius * 2) / 10000.00, 2);
            this.volume = Math.Round((double)(radius * 2) * (double)(radius * 2) * (double)(radius * 2) / 1000000.00, 2);
            this.longestSide = radius * 2;
            this.InsuranceValue = insuranceValue;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packagetype: Sphere");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Weight: {Weight} kg");
            sb.AppendLine("Fragile: " + (IsFragile ? "Yes" : "No"));
            sb.AppendLine($"Side: {Radius * 2} cm");
            sb.AppendLine($"Area: {Area} square cm");
            sb.AppendLine($"Volume: {Volume} cubic m");
            sb.AppendLine($"Longest side: {LongestSide} cm");
            sb.AppendLine($"Insurance value: {InsuranceValue}");
            return sb.ToString();
        }
    }

}

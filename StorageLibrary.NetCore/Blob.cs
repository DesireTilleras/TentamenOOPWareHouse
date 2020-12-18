using System;
using System.Collections.Generic;
using System.Text;

namespace StorageLibrary.NetCore
{

    [Serializable]
    public class Blob : Packages
    {
        public int Side { get; }
        public Blob(int id, string description, double weight, bool isFragile, int side, decimal insuranceValue) : base(id, description, weight, isFragile)
        {
            this.Side = side;
            this.area = Math.Round((double)side * (double)side / 10000.00, 2);
            this.volume = Math.Round((double)side * (double)side * (double)side / 1000000.00, 2);
            this.longestSide = side;
            this.isFragile = true;
            this.InsuranceValue = insuranceValue;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packagetype: Blob");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Weight: {Weight} kg");
            sb.AppendLine("Fragile: " + (IsFragile ? "Yes" : "No"));
            sb.AppendLine($"Side: {Side} cm");
            sb.AppendLine($"Area: {Area} square cm");
            sb.AppendLine($"Volume: {Volume} cubic m");
            sb.AppendLine($"Longest side: {LongestSide} cm");
            sb.AppendLine($"Insurance value: {InsuranceValue}");
            return sb.ToString();
        }
    }
}

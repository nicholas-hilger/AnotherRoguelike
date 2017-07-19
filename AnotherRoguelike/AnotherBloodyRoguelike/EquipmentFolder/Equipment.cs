using AnotherRoguelike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Core
{
    public class Equipment : IEquipment
    {
        public int Attack { get; set; }
        public int AttChance { get; set; }
        public int Awareness { get; set; }
        public int Defense { get; set; }
        public int DefChance { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }

        protected bool Equals(Equipment other)
        {
            return Attack == other.Attack && AttChance == other.AttChance && Awareness == other.Awareness && Defense == other.Defense && DefChance == other.DefChance && Speed == other.Speed && string.Equals(Name,other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Equipment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Attack;
                hashCode = (hashCode * 397) ^ AttChance;
                hashCode = (hashCode * 397) ^ Awareness;
                hashCode = (hashCode * 397) ^ Defense;
                hashCode = (hashCode * 397) ^ DefChance;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Speed;
                return hashCode;
            }
        }

        public static bool operator ==(Equipment left, Equipment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Equipment left, Equipment right)
        {
            return !Equals(left, right);
        }
    }
}

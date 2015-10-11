﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;

namespace DFWV
{
    //
    // Summary:
    //     Represents an ordered pair of integer x- and y-coordinates that defines a point
    //     in a two-dimensional plane.
    public struct Point3
    {
        public static readonly Point3 Empty = new Point3(0, 0, 0);

        public Point3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool IsEmpty
        {
            get { return X == 0 && Y == 0 && Z == 0; }
        }


        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }


        public override bool Equals(object obj)
        {
            if (!(obj is Point3))
                return false;
            return ((Point3) obj) == this;
        }


        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash*23 + X.GetHashCode();
                hash = hash*23 + Y.GetHashCode();
                hash = hash*23 + Z.GetHashCode();
                return hash;
            }
        }


        public override string ToString()
        {
            return string.Format("{0},{1},{2}", X, Y, Z);
        }


        public static bool operator ==(Point3 left, Point3 right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }


        public static bool operator !=(Point3 left, Point3 right)
        {
            return !(left == right);
        }
    }
}
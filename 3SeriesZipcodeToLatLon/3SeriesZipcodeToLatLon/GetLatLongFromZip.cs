/* 3 series US and Canada  Lat and Long and GMT offset from zipcode library
 * Written by Tim Gray Crestron Technical Institute
 * Copyright 2024
 * 
 * One thing you will note is that this is Identical to the 4 series code with a single exception
 * we use a SimplSharpString for the method input where 4 series does not have that limitation.
 * we then convert it into a standard string for use in our library.
 * 
 * Reflection of an embedded resource is identical, NOTE: VS2008 does not like seeing the resource the first time
 * I actually had to type the whole path for Assembly once, then VS2008 understood what I wanted.
 * 
 * You will get spoiled with a modern IDE,  Resharper in VS2008 helps a LOT to make it less of a slog to code in
 * 
 */
using System;
using System.Linq;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.Reflection;

namespace _3SeriesZipcodeToLatLon
{
    public class GetLatLongFromZip
    {
        public short GMTOffset;
        public short latDeg;
        public short latMin;
        public ushort latNorth;
        public short lonDeg;
        public ushort lonEast;
        public short lonMin;

        public void GetLocation(SimplSharpString zip)
        {
            string z = zip.ToString().Trim(); // Convert the 3 series S+ strings to a standard string

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName =
                assembly.GetManifestResourceNames().Single(str => str.EndsWith("UsCanadaLatLonGMTOffset.csv"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    double gmtoff = double.Parse(values[1]);
                    double lat = double.Parse(values[2]);
                    double lon = double.Parse(values[3]);

                    if (values[0].Contains(z))
                    {
                        GMTOffset = (short) gmtoff;

                        /*
                         *  Crestron Astronomical clock wants deg sec with east and north  the following converts
                         *  that decimal coordinates to old school deg,min,sec with also detection of
                         *  north and east.
                         */

                        // Convert to DegMin
                        DMS DLat = ConvertDegMin(lat);
                        latDeg = (short) DLat.Degrees;
                        latMin = (short) DLat.Minutes;
                        if (latDeg < 0)
                        {
                            latNorth = 0;
                            latDeg = (short) (latDeg*-1);
                        }
                        else
                        {
                            latNorth = 1;
                        }
                        DMS DLon = ConvertDegMin(lon);
                        lonDeg = (short) DLon.Degrees;
                        lonMin = (short) DLon.Minutes;
                        if (lonDeg < 0)
                        {
                            lonEast = 0;
                            lonDeg = (short) (lonDeg*-1); // get rid of the negative
                        }
                        else
                        {
                            lonEast = 1;
                        }

                        break;
                    }
                }
            }
        }

        private DMS ConvertDegMin(double decdeg)
        {
            var sec = (int) Math.Round(decdeg*3600);
            int deg = sec/3600;
            sec = Math.Abs(sec%3600);
            int min = sec/60;
            sec %= 60;

            return new DMS {Degrees = deg, Minutes = min, Seconds = sec};
        }

        private class DMS
        {
            public int Degrees;
            public int Minutes;
            public int Seconds;
        }
    }
}
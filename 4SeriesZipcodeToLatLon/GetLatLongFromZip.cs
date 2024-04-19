/* Library to find Lat and Long with GMT offset from a postal code.
 * Written by Tim Gray Crestron Technical Institute
 *
 * Copyright 2024
 *
 * Works for US and Canada short postal codes only.
 *
 */



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;

namespace _4SeriesZipcodeToLatLon
{
    public class GetLatLongFromZip
    {
        public short latDeg;
        public short lonDeg;
        public short latMin;
        public short lonMin;
        public ushort latNorth;
        public ushort lonEast;
        public short GMTOffset;

        /// <summary>
        /// Will look up the lat and long from a lookup table
        /// If the data is found Valid will go true and the Latitude and Longitude properties will contain the data
        /// If the zipcode is not found Latitude and Longitude will = 0d
        /// </summary>
        /// <param name="zip"></param>
        public void GetLocation(string zip)  //SimplSharpString is not needed on 4 series.
        {
            // using an embedded resource file we look up the zipcode to get our lat and long and gmt offset (non DST)
            // this reads so fast that it does not matter that this is blocking and it is only needed to be done once.

            string z = zip.Trim();

            //Lets reflect in the embedded csv resource and parse it

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("UsCanadaLatLonGMTOffset.csv"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');
                    double gmtoff = double.Parse(values[1]);
                    double lat = double.Parse(values[2]);
                    double lon = double.Parse(values[3]);

                    if (values[0].Contains(z))
                    {
                        GMTOffset = (short)gmtoff;

                        /*
                         *  Crestron Astronomical clock wants deg sec with east and north  the following converts
                         *  that decimal coordinates to old school deg,min,sec with also detection of
                         *  north and east.
                         */

                        // Convert to DegMin
                        DMS DLat = ConvertDegMin(lat);
                        latDeg = (short)DLat.Degrees;
                        latMin = (short)DLat.Minutes;
                        if (latDeg < 0)
                        {
                            latNorth = 0;
                            latDeg = (short)(latDeg * -1);
                        }
                        else
                        {
                            latNorth = 1;
                        }
                        DMS DLon = ConvertDegMin(lon);
                        lonDeg = (short)DLon.Degrees;
                        lonMin = (short)DLon.Minutes;
                        if (lonDeg < 0)
                        {
                            lonEast = 0;
                            lonDeg = (short)(lonDeg * -1); // get rid of the negative
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
            var sec = (int)Math.Round(decdeg * 3600);
            var deg = sec / 3600;
            sec = Math.Abs(sec % 3600);
            var min = sec / 60;
            sec %= 60;

            return new DMS { Degrees = deg, Minutes = min, Seconds = sec };
        }
        private class DMS
        {
            public int Degrees;
            public int Minutes;
            public int Seconds;

        }
    }
}

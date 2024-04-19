namespace _3SeriesZipcodeToLatLon;
        // class declarations
         class GetLatLongFromZip;
     class GetLatLongFromZip 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION GetLocation ( SIMPLSHARPSTRING zip );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_INTEGER GMTOffset;
        SIGNED_INTEGER latDeg;
        SIGNED_INTEGER latMin;
        INTEGER latNorth;
        SIGNED_INTEGER lonDeg;
        INTEGER lonEast;
        SIGNED_INTEGER lonMin;

        // class properties
    };


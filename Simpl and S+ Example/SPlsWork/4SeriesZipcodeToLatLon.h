namespace _4SeriesZipcodeToLatLon;
        // class declarations
         class GetLatLongFromZip;
     class GetLatLongFromZip 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION GetLocation ( STRING zip );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_INTEGER latDeg;
        SIGNED_INTEGER lonDeg;
        SIGNED_INTEGER latMin;
        SIGNED_INTEGER lonMin;
        INTEGER latNorth;
        INTEGER lonEast;
        SIGNED_INTEGER GMTOffset;

        // class properties
    };


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;
using _4SeriesZipcodeToLatLon;

namespace UserModule_ZIP2LATLONG
{
    public class UserModuleClass_ZIP2LATLONG : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalOutput LOCATIONEAST;
        Crestron.Logos.SplusObjects.DigitalOutput LOCATIONNORTH;
        Crestron.Logos.SplusObjects.AnalogOutput LATITUDEDEGREES;
        Crestron.Logos.SplusObjects.AnalogOutput LATITUDEMINUTES;
        Crestron.Logos.SplusObjects.AnalogOutput LONGITUDEDEGREES;
        Crestron.Logos.SplusObjects.AnalogOutput LONGITUDEMINUTES;
        Crestron.Logos.SplusObjects.AnalogOutput GMTOFFSET;
        StringParameter ZIPCODE;
        _4SeriesZipcodeToLatLon.GetLatLongFromZip LOCATION;
        public override object FunctionMain (  object __obj__ ) 
            { 
            try
            {
                SplusExecutionContext __context__ = SplusFunctionMainStartCode();
                
                __context__.SourceCodeLine = 44;
                WaitForInitializationComplete ( ) ; 
                __context__.SourceCodeLine = 45;
                LOCATION . GetLocation ( ZIPCODE  .ToString()) ; 
                __context__.SourceCodeLine = 47;
                LOCATIONEAST  .Value = (ushort) ( LOCATION.lonEast ) ; 
                __context__.SourceCodeLine = 48;
                LOCATIONNORTH  .Value = (ushort) ( LOCATION.latNorth ) ; 
                __context__.SourceCodeLine = 49;
                LATITUDEDEGREES  .Value = (ushort) ( LOCATION.latDeg ) ; 
                __context__.SourceCodeLine = 50;
                LATITUDEMINUTES  .Value = (ushort) ( LOCATION.latMin ) ; 
                __context__.SourceCodeLine = 51;
                LONGITUDEDEGREES  .Value = (ushort) ( LOCATION.lonDeg ) ; 
                __context__.SourceCodeLine = 52;
                LONGITUDEMINUTES  .Value = (ushort) ( LOCATION.lonMin ) ; 
                __context__.SourceCodeLine = 53;
                GMTOFFSET  .Value = (ushort) ( LOCATION.GMTOffset ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            return __obj__;
            }
            
        
        public override void LogosSplusInitialize()
        {
            SocketInfo __socketinfo__ = new SocketInfo( 1, this );
            InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
            _SplusNVRAM = new SplusNVRAM( this );
            
            LOCATIONEAST = new Crestron.Logos.SplusObjects.DigitalOutput( LOCATIONEAST__DigitalOutput__, this );
            m_DigitalOutputList.Add( LOCATIONEAST__DigitalOutput__, LOCATIONEAST );
            
            LOCATIONNORTH = new Crestron.Logos.SplusObjects.DigitalOutput( LOCATIONNORTH__DigitalOutput__, this );
            m_DigitalOutputList.Add( LOCATIONNORTH__DigitalOutput__, LOCATIONNORTH );
            
            LATITUDEDEGREES = new Crestron.Logos.SplusObjects.AnalogOutput( LATITUDEDEGREES__AnalogSerialOutput__, this );
            m_AnalogOutputList.Add( LATITUDEDEGREES__AnalogSerialOutput__, LATITUDEDEGREES );
            
            LATITUDEMINUTES = new Crestron.Logos.SplusObjects.AnalogOutput( LATITUDEMINUTES__AnalogSerialOutput__, this );
            m_AnalogOutputList.Add( LATITUDEMINUTES__AnalogSerialOutput__, LATITUDEMINUTES );
            
            LONGITUDEDEGREES = new Crestron.Logos.SplusObjects.AnalogOutput( LONGITUDEDEGREES__AnalogSerialOutput__, this );
            m_AnalogOutputList.Add( LONGITUDEDEGREES__AnalogSerialOutput__, LONGITUDEDEGREES );
            
            LONGITUDEMINUTES = new Crestron.Logos.SplusObjects.AnalogOutput( LONGITUDEMINUTES__AnalogSerialOutput__, this );
            m_AnalogOutputList.Add( LONGITUDEMINUTES__AnalogSerialOutput__, LONGITUDEMINUTES );
            
            GMTOFFSET = new Crestron.Logos.SplusObjects.AnalogOutput( GMTOFFSET__AnalogSerialOutput__, this );
            m_AnalogOutputList.Add( GMTOFFSET__AnalogSerialOutput__, GMTOFFSET );
            
            ZIPCODE = new StringParameter( ZIPCODE__Parameter__, this );
            m_ParameterList.Add( ZIPCODE__Parameter__, ZIPCODE );
            
            
            
            _SplusNVRAM.PopulateCustomAttributeList( true );
            
            NVRAM = _SplusNVRAM;
            
        }
        
        public override void LogosSimplSharpInitialize()
        {
            LOCATION  = new _4SeriesZipcodeToLatLon.GetLatLongFromZip();
            
            
        }
        
        public UserModuleClass_ZIP2LATLONG ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
        
        
        
        
        const uint LOCATIONEAST__DigitalOutput__ = 0;
        const uint LOCATIONNORTH__DigitalOutput__ = 1;
        const uint LATITUDEDEGREES__AnalogSerialOutput__ = 0;
        const uint LATITUDEMINUTES__AnalogSerialOutput__ = 1;
        const uint LONGITUDEDEGREES__AnalogSerialOutput__ = 2;
        const uint LONGITUDEMINUTES__AnalogSerialOutput__ = 3;
        const uint GMTOFFSET__AnalogSerialOutput__ = 4;
        const uint ZIPCODE__Parameter__ = 10;
        
        [SplusStructAttribute(-1, true, false)]
        public class SplusNVRAM : SplusStructureBase
        {
        
            public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
            
            
        }
        
        SplusNVRAM _SplusNVRAM = null;
        
        public class __CEvent__ : CEvent
        {
            public __CEvent__() {}
            public void Close() { base.Close(); }
            public int Reset() { return base.Reset() ? 1 : 0; }
            public int Set() { return base.Set() ? 1 : 0; }
            public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
        }
        public class __CMutex__ : CMutex
        {
            public __CMutex__() {}
            public void Close() { base.Close(); }
            public void ReleaseMutex() { base.ReleaseMutex(); }
            public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
        }
         public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
    }
    
    
}

using System;
using System.Collections.Generic;

namespace Client.Rust
{
    public static class RustTables
    {
        #region Tables 
        public static readonly List<Tuple<double, double>> AssaultRifle = new()
        {
                        new (1.390706, -2.003941), 
                        new (1.176434, -3.844176), 
                        new (3.387171, -5.516686), 
                        new (5.087049, -7.017456), 
                        new (5.094189, -8.342467), 
                        new (4.426013, -9.487704), 
                        new (3.250455, -10.44915), 
                        new (1.73545, -11.22279), 
                        new (0.04893398, -11.8046), 
                        new (-1.641158, -12.19056), 
                        new (-3.166891, -12.58713), 
                        new (-4.360331, -13.32077), 
                        new (-5.053545, -14.32128), 
                        new (-5.090651, -15.51103), 
                        new (-4.489915, -16.81242), 
                        new (-3.382552, -18.14783), 
                        new (-1.899585, -19.43966), 
                        new (-0.1720295, -20.61031), 
                        new (1.669086, -21.58213), 
                        new (3.492748, -22.27755),
                        new (5.16793, -22.61893),
                        new (6.563614, -22.81778),
                        new (7.548776, -23.37389),
                        new (7.992399, -24.21139),
                        new (7.512226, -25.23734),
                        new (6.063792, -26.35886),
                        new (4.117367, -27.48302),
                        new (2.143932, -28.51692),
                        new (0.6144824, -29.36766)
        };
        
        public static readonly List<Tuple<double, double>> Custom = new()
        {
                        new (0.6512542, -1.305408), 
                        new (0.9681615, -2.599905), 
                        new (0.9872047, -3.859258), 
                        new (0.6951124, -5.05923), 
                        new (0.2062594, -6.175588), 
                        new (-0.3338249, -7.184096),
                        new (-0.7796098, -8.060521), 
                        new (-0.9855663, -8.812342), 
                        new (-0.8372459, -9.496586), 
                        new (-0.4148501, -10.11968), 
                        new (0.1267298, -10.68622), 
                        new (0.6324611, -11.20081), 
                        new (0.9473124, -11.66807), 
                        new (0.9353167, -12.09258), 
                        new (0.6385964, -12.47896), 
                        new (0.1786009, -12.83181), 
                        new (-0.3247314, -13.15574), 
                        new (-0.7514643, -13.45534), 
                        new (-0.9816588, -13.73522), 
                        new (-0.9354943, -13.99999), 
                        new (-0.714118, -14.25425), 
                        new (-0.4193012, -14.5026), 
                        new (-0.1487077, -14.74965)
        };
        
        public static readonly List<Tuple<double, double>> Lr300 = new()
        {
                        new (0.09836517, -1.004928), 
                        new (0.3469534, -2.248523), 
                        new (0.7512205, -3.575346), 
                        new (1.326888, -4.829963), 
                        new (1.958069, -5.858609),
                        new (2.527623, -6.687347), 
                        new (2.918412, -7.399671), 
                        new (3.007762, -8.005643), 
                        new (2.641919, -8.515327), 
                        new (1.950645, -8.938788), 
                        new (1.144313, -9.286088), 
                        new (0.4332969, -9.567291), 
                        new (0.02797037, -9.793953), 
                        new (0.04550591, -9.992137), 
                        new (0.2685102, -10.17017), 
                        new (0.6408804, -10.33037), 
                        new (1.127565, -10.47505), 
                        new (1.693516, -10.60654), 
                        new (2.303682, -10.72716), 
                        new (2.923013, -10.83923), 
                        new (3.516459, -10.94506), 
                        new (4.04897, -11.04699), 
                        new (4.485496, -11.14732), 
                        new (4.790986, -11.24838),
                        new (4.92656, -11.35249), 
                        new (4.387823, -11.46197), 
                        new (3.16274, -11.57914), 
                        new (1.714368, -11.70632), 
                        new (0.5057687, -11.84584)
        };
        
        public static readonly List<Tuple<double, double>> M249 = new() //STANDING
        {
                        new (0.0, -2.75), 
                        new (0.0, -5.5),
                        new (0.0, -8.25), 
                        new (0.0, -11.0), 
                        new (0.0, -13.75), 
                        new (0.0, -16.5), 
                        new (0.0, -19.25), 
                        new (0.0, -22.0),
                        new (0.0, -24.75), 
                        new (0.0, -27.5), 
                        new (0.0, -30.25), 
                        new (0.0, -33.0), 
                        new (0.0, -35.75), 
                        new (0.0, -38.5), 
                        new (0.0, -41.25),
                        new (0.0, -44.0), 
                        new (0.0, -46.75), 
                        new (0.0, -49.5), 
                        new (0.0, -52.25), 
                        new (0.0, -55.0), 
                        new (0.0, -57.75), 
                        new (0.0, -60.5), 
                        new (0.0, -63.25), 
                        new (0.0, -66.0), 
                        new (0.0, -68.75), 
                        new (0.0, -71.5), 
                        new (0.0, -74.25), 
                        new (0.0, -77.0), 
                        new (0.0, -79.75), 
                        new (0.0, -82.5), 
                        new (0.0, -85.25), 
                        new (0.0, -88.0), 
                        new (0.0, -90.75), 
                        new (0.0, -93.5), 
                        new (0.0, -96.25), 
                        new (0.0, -99.0), 
                        new (0.0, -101.75), 
                        new (0.0, -104.5), 
                        new (0.0, -107.25), 
                        new (0.0, -110.0), 
                        new (0.0, -112.75),
                        new (0.0, -115.5), 
                        new (0.0, -118.25), 
                        new (0.0, -121.0), 
                        new (0.0, -123.75), 
                        new (0.0, -126.5), 
                        new (0.0, -129.25), 
                        new (0.0, -132.0), 
                        new (0.0, -134.75), 
                        new (0.0, -137.5), 
                        new (0.0, -140.25), 
                        new (0.0, -143.0), 
                        new (0.0, -145.75), 
                        new (0.0, -148.5), 
                        new (0.0, -151.25),
                        new (0.0, -154.0), 
                        new (0.0, -156.75), 
                        new (0.0, -159.5), 
                        new (0.0, -162.25), 
                        new (0.0, -165.0), 
                        new (0.0, -167.75), 
                        new (0.0, -170.5), 
                        new (0.0, -173.25), 
                        new (0.0, -176.0), 
                        new (0.0, -178.75),
                        new (0.0, -181.5), 
                        new (0.0, -184.25),
                        new (0.0, -187.0), 
                        new (0.0, -189.75), 
                        new (0.0, -192.5), 
                        new (0.0, -195.25), 
                        new (0.0, -198.0), 
                        new (0.0, -200.75),
                        new (0.0, -203.5), 
                        new (0.0, -206.25), 
                        new (0.0, -209.0), 
                        new (0.0, -211.75), 
                        new (0.0, -214.5), 
                        new (0.0, -217.25), 
                        new (0.0, -220.0), 
                        new (0.0, -222.75), 
                        new (0.0, -225.5), 
                        new (0.0, -228.25), 
                        new (0.0, -231.0), 
                        new (0.0, -233.75),
                        new (0.0, -236.5), 
                        new (0.0, -239.25), 
                        new (0.0, -242.0), 
                        new (0.0, -244.75), 
                        new (0.0, -247.5), 
                        new (0.0, -250.25), 
                        new (0.0, -253.0), 
                        new (0.0, -255.75), 
                        new (0.0, -258.5), 
                        new (0.0, -261.25), 
                        new (0.0, -264.0), 
                        new (0.0, -266.75), 
                        new (0.0, -269.5), 
                        new (0.0, -272.25), 
                        new (0.0, -275.0), 
                        new (0.0, -277.75)
        };
        
        public static readonly List<Tuple<double, double>> Mp5 = new()
        {
                        new (0, -0.8688382), 
                        new (0, -2.042219), 
                        new (-2.992364e-14, -3.370441), 
                        new (-0.5103882, -4.703804), 
                        new (-1.687297, -5.892606), 
                        new (-2.999344, -6.787148), 
                        new (-3.915147, -7.311441), 
                        new (-3.948318, -7.742482), 
                        new (-3.224567, -8.127406), 
                        new (-2.228431, -8.468721), 
                        new (-1.438722, -8.768936), 
                        new (-1.288914, -9.03056), 
                        new (-1.598686, -9.2561), 
                        new (-2.154637, -9.448063), 
                        new (-2.826861, -9.60896), 
                        new (-3.485454, -9.741299), 
                        new (-4.000507, -9.847586), 
                        new (-4.242117, -9.930332), 
                        new (-4.184897, -9.992043), 
                        new (-3.969568, -10.03523), 
                        new (-3.629241, -10.0624),
                        new (-3.194572, -10.07606),
                        new (-2.696223, -10.07872), 
                        new (-2.16485, -10.07288), 
                        new (-1.631112, -10.06106), 
                        new (-1.125667, -10.04577), 
                        new (-0.6791761, -10.02951), 
                        new (-0.3222946, -10.01479), 
                        new (-0.08568263, -10.00412)
        };
        
        public static readonly List<Tuple<double, double>> Thompson = new()
        {
                        new (0.7399524, -1.565956), 
                        new (1.011324, -3.109221), 
                        new (0.8437103, -4.587918), 
                        new (0.3127854, -5.960169), 
                        new (-0.3338249, -7.184096), 
                        new (-0.8446444, -8.217823), 
                        new (-0.9689822, -9.093672), 
                        new (-0.6067921, -9.877484), 
                        new (0.01632042, -10.57721), 
                        new (0.6324611, -11.20081), 
                        new (0.9737339, -11.75624), 
                        new (0.8438975, -12.25145), 
                        new (0.3745165, -12.6944), 
                        new (-0.2263549, -13.09305), 
                        new (-0.7514643, -13.45534), 
                        new (-0.9935587, -13.78924), 
                        new (-0.862007, -14.1027), 
                        new (-0.5397906, -14.40368),
                        new (-0.1962007, -14.70013)
        };
        #endregion
    }
}
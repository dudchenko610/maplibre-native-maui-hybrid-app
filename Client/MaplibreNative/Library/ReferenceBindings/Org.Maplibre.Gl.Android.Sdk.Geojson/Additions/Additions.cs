namespace Mapbox.Geojson
{
    public partial class PointAsCoordinatesTypeAdapter
    {
        public override unsafe Java.Lang.Object Read(global::GoogleGson.Stream.JsonReader @in)
        {
            return ReadBase(@in) as Mapbox.Geojson.Point;
        }

        public override unsafe void Write(GoogleGson.Stream.JsonWriter @out, Java.Lang.Object value)
        {
            Write(@out, value as Mapbox.Geojson.Point);
        }
    }
}

namespace Mapbox.Geojson.Gson
{
    public partial class BoundingBoxTypeAdapter
    {
        public override unsafe Java.Lang.Object Read(GoogleGson.Stream.JsonReader @in)
        {
            return ReadBase(@in) as Mapbox.Geojson.BoundingBox;
        }

        public override unsafe void Write(GoogleGson.Stream.JsonWriter @out, Java.Lang.Object value)
        {
            Write(@out, value as Mapbox.Geojson.BoundingBox);
        }
    }

    //public partial class CoordinateTypeAdapter
    //{
    //    public override unsafe Java.Lang.Object Read(GoogleGson.Stream.JsonReader @in)
    //    {
    //        return ReadBase(@in) as Android.Runtime.JavaList<Java.Lang.Double>;
    //    }

    //    public override unsafe void Write(GoogleGson.Stream.JsonWriter @out, Java.Lang.Object value)
    //    {
    //        Write(@out, value as Android.Runtime.JavaList<Java.Lang.Double>);
    //    }
    //}

    //public partial class GeometryTypeAdapter
    //{
    //    public override unsafe Java.Lang.Object Read(GoogleGson.Stream.JsonReader @in)
    //    {
    //        return ReadBase(@in) as Android.Runtime.JavaList<Mapbox.Geojson.IGeometry>;
    //    }

    //    public override unsafe void Write(GoogleGson.Stream.JsonWriter @out, Java.Lang.Object value)
    //    {
    //        Write(@out, value as Android.Runtime.JavaList<Mapbox.Geojson.IGeometry>);
    //    }
    //}
}
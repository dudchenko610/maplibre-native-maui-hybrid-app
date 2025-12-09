using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using CoreLocation;
using Foundation;
using MapLibre;
using ObjCRuntime;

using CoreGraphics;
using OpenGLES;
using UIKit;

using CoreAnimation;
using CoreVideo;

namespace MapLibre.Native.iOS;

[Native]
public enum MLNAnnotationViewDragState : ulong
{
	None = 0,
	Starting,
	Dragging,
	Canceling,
	Ending
}

[Native]
public enum MLNErrorCode : long
{
	Unknown = -1,
	NotFound = 1,
	BadServerResponse = 2,
	ConnectionFailed = 3,
	ParseStyleFailed = 4,
	LoadStyleFailed = 5,
	SnapshotFailed = 6,
	SourceIsInUseCannotRemove = 7,
	SourceIdentifierMismatch = 8,
	ModifyingOfflineStorageFailed = 9,
	SourceCannotBeRemovedFromStyle = 10,
	RenderingError = 11
}

[Flags]
[Native]
public enum MLNMapDebugMaskOptions : ulong
{
	TileBoundariesMask = 1uL << 1,
	TileInfoMask = 1uL << 2,
	TimestampsMask = 1uL << 3,
	CollisionBoxesMask = 1uL << 4,
	OverdrawVisualizationMask = 1uL << 5
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNTransition
{
	public double duration;

	public double delay;
}

// Many of these inline C helpers are not exported from the simulator slice.
// Disable P/Invoke declarations to avoid undefined symbol link errors.
#if false
static class CFunctions
{
	// NSString * _Nonnull MLNStringFromMLNTransition (MLNTransition transition) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern NSString MLNStringFromMLNTransition (MLNTransition transition);

	// MLNTransition MLNTransitionMake (NSTimeInterval duration, NSTimeInterval delay) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNTransition MLNTransitionMake (double duration, double delay);

	// MLNCoordinateSpan MLNCoordinateSpanMake (CLLocationDegrees latitudeDelta, CLLocationDegrees longitudeDelta) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateSpan MLNCoordinateSpanMake (double latitudeDelta, double longitudeDelta);

	// MLNMapPoint MLNMapPointMake (CGFloat x, CGFloat y, CGFloat zoomLevel) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNMapPoint MLNMapPointMake (nfloat x, nfloat y, nfloat zoomLevel);

	// BOOL MLNCoordinateSpanEqualToCoordinateSpan (MLNCoordinateSpan span1, MLNCoordinateSpan span2) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern bool MLNCoordinateSpanEqualToCoordinateSpan (MLNCoordinateSpan span1, MLNCoordinateSpan span2);

	// MLNCoordinateBounds MLNCoordinateBoundsMake (CLLocationCoordinate2D sw, CLLocationCoordinate2D ne) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateBounds MLNCoordinateBoundsMake (CLLocationCoordinate2D sw, CLLocationCoordinate2D ne);

	// MLNCoordinateQuad MLNCoordinateQuadMake (CLLocationCoordinate2D topLeft, CLLocationCoordinate2D bottomLeft, CLLocationCoordinate2D bottomRight, CLLocationCoordinate2D topRight) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateQuad MLNCoordinateQuadMake (CLLocationCoordinate2D topLeft, CLLocationCoordinate2D bottomLeft, CLLocationCoordinate2D bottomRight, CLLocationCoordinate2D topRight);

	// MLNCoordinateQuad MLNCoordinateQuadFromCoordinateBounds (MLNCoordinateBounds bounds) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateQuad MLNCoordinateQuadFromCoordinateBounds (MLNCoordinateBounds bounds);

	// BOOL MLNCoordinateBoundsEqualToCoordinateBounds (MLNCoordinateBounds bounds1, MLNCoordinateBounds bounds2) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern bool MLNCoordinateBoundsEqualToCoordinateBounds (MLNCoordinateBounds bounds1, MLNCoordinateBounds bounds2);

	// BOOL MLNCoordinateBoundsIntersectsCoordinateBounds (MLNCoordinateBounds bounds1, MLNCoordinateBounds bounds2) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern bool MLNCoordinateBoundsIntersectsCoordinateBounds (MLNCoordinateBounds bounds1, MLNCoordinateBounds bounds2);

	// BOOL MLNCoordinateInCoordinateBounds (CLLocationCoordinate2D coordinate, MLNCoordinateBounds bounds) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern bool MLNCoordinateInCoordinateBounds (CLLocationCoordinate2D coordinate, MLNCoordinateBounds bounds);

	// MLNCoordinateSpan MLNCoordinateBoundsGetCoordinateSpan (MLNCoordinateBounds bounds) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateSpan MLNCoordinateBoundsGetCoordinateSpan (MLNCoordinateBounds bounds);

	// MLNCoordinateBounds MLNCoordinateBoundsOffset (MLNCoordinateBounds bounds, MLNCoordinateSpan offset) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNCoordinateBounds MLNCoordinateBoundsOffset (MLNCoordinateBounds bounds, MLNCoordinateSpan offset);

	// BOOL MLNCoordinateBoundsIsEmpty (MLNCoordinateBounds bounds) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern bool MLNCoordinateBoundsIsEmpty (MLNCoordinateBounds bounds);

	// NSString * _Nonnull MLNStringFromCoordinateBounds (MLNCoordinateBounds bounds) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern NSString MLNStringFromCoordinateBounds (MLNCoordinateBounds bounds);

	// NSString * _Nonnull MLNStringFromCoordinateQuad (MLNCoordinateQuad quad) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern NSString MLNStringFromCoordinateQuad (MLNCoordinateQuad quad);

	// CGFloat MLNRadiansFromDegrees (CLLocationDegrees degrees) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern nfloat MLNRadiansFromDegrees (double degrees);

	// CLLocationDegrees MLNDegreesFromRadians (CGFloat radians) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern double MLNDegreesFromRadians (nfloat radians);

	// extern MLNMapPoint MLNMapPointForCoordinate (CLLocationCoordinate2D coordinate, double zoomLevel) __attribute__((visibility("default")));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNMapPoint MLNMapPointForCoordinate (CLLocationCoordinate2D coordinate, double zoomLevel);

	// extern CLLocationDistance MLNAltitudeForZoomLevel (double zoomLevel, CGFloat pitch, CLLocationDegrees latitude, CGSize size) __attribute__((visibility("default")));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern double MLNAltitudeForZoomLevel (double zoomLevel, nfloat pitch, double latitude, CGSize size);

	// extern double MLNZoomLevelForAltitude (CLLocationDistance altitude, CGFloat pitch, CLLocationDegrees latitude, CGSize size) __attribute__((visibility("default")));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern double MLNZoomLevelForAltitude (double altitude, nfloat pitch, double latitude, CGSize size);

	// extern CGFloat MLNEffectiveScaleFactorForView (id viewOrNil);
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern nfloat MLNEffectiveScaleFactorForView (NSObject viewOrNil);

	// MLNSphericalPosition MLNSphericalPositionMake (CGFloat radial, CLLocationDirection azimuthal, CLLocationDirection polar) __attribute__((always_inline));
	[DllImport ("__Internal")]
	// [Verify (PlatformInvoke)]
	static extern MLNSphericalPosition MLNSphericalPositionMake (nfloat radial, double azimuthal, double polar);
}
#endif

[Native]
public enum MLNOrnamentVisibility : long
{
	Adaptive,
	Hidden,
	Visible
}

[Native]
public enum MLNAttributionInfoStyle : ulong
{
	Short = 1,
	Medium,
	Long
}

[Flags]
[Native]
public enum MLNCameraChangeReason : ulong
{
	None = 0x0,
	Programmatic = 1uL << 0,
	ResetNorth = 1uL << 1,
	GesturePan = 1uL << 2,
	GesturePinch = 1uL << 3,
	GestureRotate = 1uL << 4,
	GestureZoomIn = 1uL << 5,
	GestureZoomOut = 1uL << 6,
	GestureOneFingerZoom = 1uL << 7,
	GestureTilt = 1uL << 8,
	TransitionCancelled = 1uL << 16
}

[Native]
public enum MLNCirclePitchAlignment : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNCircleScaleAlignment : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNCircleTranslationAnchor : ulong
{
	Map,
	Viewport
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNCoordinateSpan
{
	public double latitudeDelta;

	public double longitudeDelta;
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNMapPoint
{
	public nfloat x;

	public nfloat y;

	public nfloat zoomLevel;
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNMatrix4
{
	public double m00;

	public double m01;

	public double m02;

	public double m03;

	public double m10;

	public double m11;

	public double m12;

	public double m13;

	public double m20;

	public double m21;

	public double m22;

	public double m23;

	public double m30;

	public double m31;

	public double m32;

	public double m33;
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNCoordinateBounds
{
	public CLLocationCoordinate2D sw;

	public CLLocationCoordinate2D ne;
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNCoordinateQuad
{
	public CLLocationCoordinate2D topLeft;

	public CLLocationCoordinate2D bottomLeft;

	public CLLocationCoordinate2D bottomRight;

	public CLLocationCoordinate2D topRight;
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNStyleLayerDrawingContext
{
	public CGSize size;

	public CLLocationCoordinate2D centerCoordinate;

	public double zoomLevel;

	public double direction;

	public nfloat pitch;

	public nfloat fieldOfView;

	public MLNMatrix4 projectionMatrix;
}

[Native]
public enum MLNFillExtrusionTranslationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNFillTranslationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNHillshadeIlluminationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNLightAnchor : ulong
{
	Map,
	Viewport
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNSphericalPosition
{
	public nfloat radial;

	public double azimuthal;

	public double polar;
}

[Native]
public enum MLNLineCap : ulong
{
	Butt,
	Round,
	Square
}

[Native]
public enum MLNLineJoin : ulong
{
	Bevel,
	Round,
	Miter
}

[Native]
public enum MLNLineTranslationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNLoggingLevel : long
{
	None = 0,
	Fault,
	Error,
	Warning,
	Info,
	Debug,
	Verbose
}

[Native]
public enum MLNWellKnownTileServer : ulong
{
	Tiler,
	Libre,
	box
}

[Native]
public enum MLNAnnotationVerticalAlignment : ulong
{
	Center = 0,
	Top,
	Bottom
}

[Native]
public enum MLNOrnamentPosition : ulong
{
	TopLeft = 0,
	TopRight,
	BottomLeft,
	BottomRight
}

[Native]
public enum MLNUserTrackingMode : ulong
{
	None = 0,
	Follow,
	FollowWithHeading,
	FollowWithCourse
}

[Native]
public enum MLNPanScrollingMode : ulong
{
	Horizontal = 0,
	Vertical,
	Default
}

[Native]
public enum MLNTileOperation : long
{
	RequestedFromCache,
	RequestedFromNetwork,
	LoadFromNetwork,
	LoadFromCache,
	StartParse,
	EndParse,
	Error,
	Cancelled,
	NullOp
}

[Native]
public enum MLNOfflinePackState : long
{
	Unknown = 0,
	Inactive = 1,
	Active = 2,
	Complete = 3,
	Invalid = 4
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNOfflinePackProgress
{
	public ulong countOfResourcesCompleted;

	public ulong countOfBytesCompleted;

	public ulong countOfTilesCompleted;

	public ulong countOfTileBytesCompleted;

	public ulong countOfResourcesExpected;

	public ulong maximumResourcesExpected;
}

[Native]
public enum MLNResourceKind : ulong
{
	Unknown,
	Style,
	Source,
	Tile,
	Glyphs,
	SpriteImage,
	SpriteJSON,
	Image
}

[Native]
public enum MLNTileCoordinateSystem : ulong
{
	Xyz = 0,
	Tms
}

[Native]
public enum MLNDEMEncoding : ulong
{
	Mapbox = 0,
	Terrarium = 1
}

[Native]
public enum MLNRasterResamplingMode : ulong
{
	Linear,
	Nearest
}

[Native]
public enum MLNIconAnchor : ulong
{
	Center,
	Left,
	Right,
	Top,
	Bottom,
	TopLeft,
	TopRight,
	BottomLeft,
	BottomRight
}

[Native]
public enum MLNIconPitchAlignment : ulong
{
	Map,
	Viewport,
	Auto
}

[Native]
public enum MLNIconRotationAlignment : ulong
{
	Map,
	Viewport,
	Auto
}

[Native]
public enum MLNIconTextFit : ulong
{
	None,
	Width,
	Height,
	Both
}

[Native]
public enum MLNSymbolPlacement : ulong
{
	Point,
	Line,
	LineCenter
}

[Native]
public enum MLNSymbolZOrder : ulong
{
	Auto,
	ViewportY,
	Source
}

[Native]
public enum MLNTextAnchor : ulong
{
	Center,
	Left,
	Right,
	Top,
	Bottom,
	TopLeft,
	TopRight,
	BottomLeft,
	BottomRight
}

[Native]
public enum MLNTextJustification : ulong
{
	Auto,
	Left,
	Center,
	Right
}

[Native]
public enum MLNTextPitchAlignment : ulong
{
	Map,
	Viewport,
	Auto
}

[Native]
public enum MLNTextRotationAlignment : ulong
{
	Map,
	Viewport,
	Auto
}

[Native]
public enum MLNTextTransform : ulong
{
	None,
	Uppercase,
	Lowercase
}

[Native]
public enum MLNTextWritingMode : ulong
{
	Horizontal,
	Vertical
}

[Native]
public enum MLNIconTranslationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNTextTranslationAnchor : ulong
{
	Map,
	Viewport
}

[Native]
public enum MLNVectorTileSourceEncoding : ulong
{
	apbox = 0,
	Lt = 1
}

public enum MLNPluginLayerPropertyType : uint
{
	Unknown,
	SingleFloat,
	Color
}

public enum MLNPluginLayerTileKind : uint
{
	Geometry,
	Raster,
	RasterDEM,
	NotRequired
}

[StructLayout (LayoutKind.Sequential)]
public struct MLNPluginLayerDrawingContext
{
	public CGSize size;

	public CLLocationCoordinate2D centerCoordinate;

	public double zoomLevel;

	public double direction;

	public nfloat pitch;

	public nfloat fieldOfView;

	public MLNMatrix4 projectionMatrix;

	public MLNMatrix4 nearClippedProjMatrix;
}

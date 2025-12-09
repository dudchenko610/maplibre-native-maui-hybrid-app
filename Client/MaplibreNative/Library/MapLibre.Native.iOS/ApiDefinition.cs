using System;
using CoreFoundation;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapLibre;
using ObjCRuntime;
using OpenGLES;
using UIKit;

using CoreAnimation;
using CoreVideo;
using Metal;

namespace MapLibre.Native.iOS;

[Protocol]
interface IMLNFeature : INativeObject, IDisposable
{
}

[Protocol]
interface IMLNOverlay : INativeObject, IDisposable
{
}

[Protocol]
interface IMLNAnnotation : INativeObject, IDisposable
{
}

[Protocol]
interface IMLNCluster : IMLNFeature
{
}

[Protocol]
interface IMLNOfflineRegion : INativeObject, IDisposable
{
}

[Protocol]
interface IMLNStylable : INativeObject, IDisposable
{
}


// @interface MLNActionJournalOptions : NSObject
[BaseType (typeof(NSObject))]
interface MLNActionJournalOptions
{
	// @property (nonatomic) BOOL enabled;
	[Export ("enabled")]
	bool Enabled { get; set; }

	// @property (nonatomic) NSString * _Nonnull path;
	[Export ("path")]
	string Path { get; set; }

	// @property (nonatomic) NSInteger logFileSize;
	[Export ("logFileSize")]
	nint LogFileSize { get; set; }

	// @property (nonatomic) NSInteger logFileCount;
	[Export ("logFileCount")]
	nint LogFileCount { get; set; }

	// @property (nonatomic) NSInteger renderingStatsReportInterval;
	[Export ("renderingStatsReportInterval")]
	nint RenderingStatsReportInterval { get; set; }
}

// @protocol MLNAnnotation <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface MLNAnnotation
{
	// @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
	[Abstract]
	[Export ("coordinate")]
	CLLocationCoordinate2D Coordinate { get; }

	// @optional @property (readonly, copy, nonatomic) NSString * _Nullable title;
	[NullAllowed, Export ("title")]
	string Title { get; }

	// @optional @property (readonly, copy, nonatomic) NSString * _Nullable subtitle;
	[NullAllowed, Export ("subtitle")]
	string Subtitle { get; }
}

// @interface MLNAnnotationImage : NSObject <NSSecureCoding>
[BaseType (typeof(NSObject))]
interface MLNAnnotationImage : INSSecureCoding
{
	// +(instancetype _Nonnull)annotationImageWithImage:(UIImage * _Nonnull)image reuseIdentifier:(NSString * _Nonnull)reuseIdentifier;
	[Static]
	[Export ("annotationImageWithImage:reuseIdentifier:")]
	MLNAnnotationImage AnnotationImageWithImage (UIImage image, string reuseIdentifier);

	// @property (nonatomic, strong) UIImage * _Nullable image;
	[NullAllowed, Export ("image", ArgumentSemantic.Strong)]
	UIImage Image { get; set; }

	// @property (readonly, nonatomic) NSString * _Nonnull reuseIdentifier;
	[Export ("reuseIdentifier")]
	string ReuseIdentifier { get; }

	// @property (getter = isEnabled, nonatomic) BOOL enabled;
	[Export ("enabled")]
	bool Enabled { [Bind ("isEnabled")] get; set; }
}

// @interface MLNAnnotationView : UIView <NSSecureCoding>
[BaseType (typeof(UIView))]
interface MLNAnnotationView : INSSecureCoding
{
	// -(instancetype _Nonnull)initWithReuseIdentifier:(NSString * _Nullable)reuseIdentifier;
	[Export ("initWithReuseIdentifier:")]
	NativeHandle Constructor ([NullAllowed] string reuseIdentifier);

	// -(instancetype _Nonnull)initWithAnnotation:(id<MLNAnnotation> _Nullable)annotation reuseIdentifier:(NSString * _Nullable)reuseIdentifier;
	[Export ("initWithAnnotation:reuseIdentifier:")]
	NativeHandle Constructor ([NullAllowed] MLNAnnotation annotation, [NullAllowed] string reuseIdentifier);

	// -(void)prepareForReuse;
	[Export ("prepareForReuse")]
	void PrepareForReuse ();

	// @property (nonatomic) id<MLNAnnotation> _Nullable annotation;
	[NullAllowed, Export ("annotation", ArgumentSemantic.Assign)]
	MLNAnnotation Annotation { get; set; }

	// @property (readonly, nonatomic) NSString * _Nullable reuseIdentifier;
	[NullAllowed, Export ("reuseIdentifier")]
	string ReuseIdentifier { get; }

	// @property (nonatomic) CGVector centerOffset;
	[Export ("centerOffset", ArgumentSemantic.Assign)]
	CGVector CenterOffset { get; set; }

	// @property (assign, nonatomic) BOOL scalesWithViewingDistance;
	[Export ("scalesWithViewingDistance")]
	bool ScalesWithViewingDistance { get; set; }

	// @property (assign, nonatomic) BOOL rotatesToMatchCamera;
	[Export ("rotatesToMatchCamera")]
	bool RotatesToMatchCamera { get; set; }

	// @property (getter = isSelected, assign, nonatomic) BOOL selected;
	[Export ("selected")]
	bool Selected { [Bind ("isSelected")] get; set; }

	// -(void)setSelected:(BOOL)selected animated:(BOOL)animated;
	[Export ("setSelected:animated:")]
	void SetSelected (bool selected, bool animated);

	// @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
	[Export ("enabled")]
	bool Enabled { [Bind ("isEnabled")] get; set; }

	// @property (getter = isDraggable, assign, nonatomic) BOOL draggable;
	[Export ("draggable")]
	bool Draggable { [Bind ("isDraggable")] get; set; }

	// @property (readonly, nonatomic) MLNAnnotationViewDragState dragState;
	[Export ("dragState")]
	MLNAnnotationViewDragState DragState { get; }

	// -(void)setDragState:(MLNAnnotationViewDragState)dragState animated:(BOOL)animated __attribute__((objc_requires_super));
	[Export ("setDragState:animated:")]
	[RequiresSuper]
	void SetDragState (MLNAnnotationViewDragState dragState, bool animated);
}

[Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNAttributedExpressionKey _Nonnull MLNFontNamesAttribute __attribute__((visibility("default")));
	[Field ("MLNFontNamesAttribute", "__Internal")]
	NSString MLNFontNamesAttribute { get; }

	// extern const MLNAttributedExpressionKey _Nonnull MLNFontScaleAttribute __attribute__((visibility("default")));
	[Field ("MLNFontScaleAttribute", "__Internal")]
	NSString MLNFontScaleAttribute { get; }

	// extern const MLNAttributedExpressionKey _Nonnull MLNFontColorAttribute __attribute__((visibility("default")));
	[Field ("MLNFontColorAttribute", "__Internal")]
	NSString MLNFontColorAttribute { get; }
}

// @interface MLNAttributedExpression : NSObject
[BaseType (typeof(NSObject))]
interface MLNAttributedExpression
{
	// @property (nonatomic, strong) NSExpression * _Nonnull expression;
	[Export ("expression", ArgumentSemantic.Strong)]
	NSExpression Expression { get; set; }

	// @property (readonly, nonatomic, strong) NSDictionary<MLNAttributedExpressionKey,NSExpression *> * _Nonnull attributes;
	[Export ("attributes", ArgumentSemantic.Strong)]
	NSDictionary<NSString, NSExpression> Attributes { get; }

	// -(instancetype _Nonnull)initWithExpression:(NSExpression * _Nonnull)expression;
	[Export ("initWithExpression:")]
	NativeHandle Constructor (NSExpression expression);

	// -(instancetype _Nonnull)initWithExpression:(NSExpression * _Nonnull)expression attributes:(NSDictionary<MLNAttributedExpressionKey,NSExpression *> * _Nonnull)attrs;
	[Export ("initWithExpression:attributes:")]
	NativeHandle Constructor (NSExpression expression, NSDictionary<NSString, NSExpression> attrs);

	// +(instancetype _Nonnull)attributedExpression:(NSExpression * _Nonnull)expression fontNames:(NSArray<NSString *> * _Nullable)fontNames fontScale:(NSNumber * _Nullable)fontScale;
	[Static]
	[Export ("attributedExpression:fontNames:fontScale:")]
	MLNAttributedExpression AttributedExpression (NSExpression expression, [NullAllowed] string[] fontNames, [NullAllowed] NSNumber fontScale);

	// +(instancetype _Nonnull)attributedExpression:(NSExpression * _Nonnull)expression attributes:(NSDictionary<MLNAttributedExpressionKey,NSExpression *> * _Nonnull)attrs;
	[Static]
	[Export ("attributedExpression:attributes:")]
	MLNAttributedExpression AttributedExpression (NSExpression expression, NSDictionary<NSString, NSExpression> attrs);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExceptionName _Nonnull MLNAbstractClassException __attribute__((visibility("default")));
	[Field ("MLNAbstractClassException", "__Internal")]
	NSString MLNAbstractClassException { get; }

	// extern const NSErrorDomain _Nonnull MLNErrorDomain __attribute__((visibility("default")));
	[Field ("MLNErrorDomain", "__Internal")]
	NSString MLNErrorDomain { get; }
}

// @interface MLNAttributionInfo : NSObject
[BaseType (typeof(NSObject))]
interface MLNAttributionInfo
{
	// -(instancetype _Nonnull)initWithTitle:(NSAttributedString * _Nonnull)title URL:(NSUrl * _Nullable)URL;
	[Export ("initWithTitle:URL:")]
	NativeHandle Constructor (NSAttributedString title, [NullAllowed] NSUrl URL);

	// @property (nonatomic) NSAttributedString * _Nonnull title;
	[Export ("title", ArgumentSemantic.Assign)]
	NSAttributedString Title { get; set; }

	// @property (nonatomic) NSUrl * _Nullable URL;
	[NullAllowed, Export ("URL", ArgumentSemantic.Assign)]
	NSUrl URL { get; set; }

	// @property (getter = isFeedbackLink, nonatomic) BOOL feedbackLink;
	[Export ("feedbackLink")]
	bool FeedbackLink { [Bind ("isFeedbackLink")] get; set; }

	// -(NSAttributedString * _Nonnull)titleWithStyle:(MLNAttributionInfoStyle)style;
	[Export ("titleWithStyle:")]
	NSAttributedString TitleWithStyle (MLNAttributionInfoStyle style);
}

// @interface MLNBackendResource : NSObject
// [BaseType (typeof(NSObject))]
// interface MLNBackendResource
// {
// }

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExceptionName _Nonnull MLNInvalidStyleLayerException __attribute__((visibility("default")));
	[Field ("MLNInvalidStyleLayerException", "__Internal")]
	NSString MLNInvalidStyleLayerException { get; }
}

// @interface MLNStyleLayer : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface MLNStyleLayer
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull identifier;
	[Export ("identifier")]
	string Identifier { get; }

	// @property (getter = isVisible, assign, nonatomic) BOOL visible;
	[Export ("visible")]
	bool Visible { [Bind ("isVisible")] get; set; }

	// @property (assign, nonatomic) float maximumZoomLevel;
	[Export ("maximumZoomLevel")]
	float MaximumZoomLevel { get; set; }

	// @property (assign, nonatomic) float minimumZoomLevel;
	[Export ("minimumZoomLevel")]
	float MinimumZoomLevel { get; set; }
}

// @interface MLNBackgroundStyleLayer : MLNStyleLayer
[BaseType (typeof(MLNStyleLayer))]
interface MLNBackgroundStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("initWithIdentifier:")]
	NativeHandle Constructor (string identifier);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified backgroundColor;
	[NullAllowed, Export ("backgroundColor", ArgumentSemantic.Assign)]
	NSExpression BackgroundColor { get; set; }

	// @property (nonatomic) MLNTransition backgroundColorTransition;
	[Export ("backgroundColorTransition", ArgumentSemantic.Assign)]
	MLNTransition BackgroundColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified backgroundOpacity;
	[NullAllowed, Export ("backgroundOpacity", ArgumentSemantic.Assign)]
	NSExpression BackgroundOpacity { get; set; }

	// @property (nonatomic) MLNTransition backgroundOpacityTransition;
	[Export ("backgroundOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition BackgroundOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified backgroundPattern;
	[NullAllowed, Export ("backgroundPattern", ArgumentSemantic.Assign)]
	NSExpression BackgroundPattern { get; set; }

	// @property (nonatomic) MLNTransition backgroundPatternTransition;
	[Export ("backgroundPatternTransition", ArgumentSemantic.Assign)]
	MLNTransition BackgroundPatternTransition { get; set; }
}

// @protocol MLNCalloutView <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface MLNCalloutView
{
	// @required @property (nonatomic, strong) id<MLNAnnotation> _Nonnull representedObject;
	[Abstract]
	[Export ("representedObject", ArgumentSemantic.Strong)]
	MLNAnnotation RepresentedObject { get; set; }

	// @required @property (nonatomic, strong) UIView * _Nonnull leftAccessoryView;
	[Abstract]
	[Export ("leftAccessoryView", ArgumentSemantic.Strong)]
	UIView LeftAccessoryView { get; set; }

	// @required @property (nonatomic, strong) UIView * _Nonnull rightAccessoryView;
	[Abstract]
	[Export ("rightAccessoryView", ArgumentSemantic.Strong)]
	UIView RightAccessoryView { get; set; }

	[Wrap ("WeakDelegate"), Abstract]
	[NullAllowed]
	MLNCalloutViewDelegate Delegate { get; set; }

	// @required @property (nonatomic, weak) id<MLNCalloutViewDelegate> _Nullable delegate;
	[Abstract]
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @required -(void)presentCalloutFromRect:(CGRect)rect inView:(UIView * _Nonnull)view constrainedToRect:(CGRect)constrainedRect animated:(BOOL)animated;
	[Abstract]
	[Export ("presentCalloutFromRect:inView:constrainedToRect:animated:")]
	void PresentCalloutFromRect (CGRect rect, UIView view, CGRect constrainedRect, bool animated);

	// @required -(void)dismissCalloutAnimated:(BOOL)animated;
	[Abstract]
	[Export ("dismissCalloutAnimated:")]
	void DismissCalloutAnimated (bool animated);

	// @optional -(UIEdgeInsets)marginInsetsHintForPresentationFromRect:(CGRect)rect __attribute__((swift_name("marginInsetsHintForPresentation(from:)")));
	[Export ("marginInsetsHintForPresentationFromRect:")]
	UIEdgeInsets MarginInsetsHintForPresentationFromRect (CGRect rect);

	// @optional @property (readonly, getter = isAnchoredToAnnotation, assign, nonatomic) BOOL anchoredToAnnotation;
	[Export ("anchoredToAnnotation")]
	bool AnchoredToAnnotation { [Bind ("isAnchoredToAnnotation")] get; }

	// @optional @property (readonly, assign, nonatomic) BOOL dismissesAutomatically;
	[Export ("dismissesAutomatically")]
	bool DismissesAutomatically { get; }
}

// @protocol MLNCalloutViewDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNCalloutViewDelegate
{
	// @optional -(BOOL)calloutViewShouldHighlight:(UIView<MLNCalloutView> * _Nonnull)calloutView;
	[Export ("calloutViewShouldHighlight:")]
	bool CalloutViewShouldHighlight (MLNCalloutView calloutView);

	// @optional -(void)calloutViewTapped:(UIView<MLNCalloutView> * _Nonnull)calloutView;
	[Export ("calloutViewTapped:")]
	void CalloutViewTapped (MLNCalloutView calloutView);

	// @optional -(void)calloutViewWillAppear:(UIView<MLNCalloutView> * _Nonnull)calloutView;
	[Export ("calloutViewWillAppear:")]
	void CalloutViewWillAppear (MLNCalloutView calloutView);

	// @optional -(void)calloutViewDidAppear:(UIView<MLNCalloutView> * _Nonnull)calloutView;
	[Export ("calloutViewDidAppear:")]
	void CalloutViewDidAppear (MLNCalloutView calloutView);
}

// @interface MLNForegroundStyleLayer : MLNStyleLayer
[BaseType (typeof(MLNStyleLayer))]
[DisableDefaultCtor]
interface MLNForegroundStyleLayer
{
	// @property (readonly, nonatomic) NSString * _Nullable sourceIdentifier;
	[NullAllowed, Export ("sourceIdentifier")]
	string SourceIdentifier { get; }
}

// @interface MLNVectorStyleLayer : MLNForegroundStyleLayer
[BaseType (typeof(MLNForegroundStyleLayer))]
interface MLNVectorStyleLayer
{
	// @property (nonatomic) NSString * _Nullable sourceLayerIdentifier;
	[NullAllowed, Export ("sourceLayerIdentifier")]
	string SourceLayerIdentifier { get; set; }

	// @property (nonatomic) NSPredicate * _Nullable predicate;
	[NullAllowed, Export ("predicate", ArgumentSemantic.Assign)]
	NSPredicate Predicate { get; set; }
}

// @interface MLNCircleStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNCircleStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleSortKey;
	[NullAllowed, Export ("circleSortKey", ArgumentSemantic.Assign)]
	NSExpression CircleSortKey { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleBlur;
	[NullAllowed, Export ("circleBlur", ArgumentSemantic.Assign)]
	NSExpression CircleBlur { get; set; }

	// @property (nonatomic) MLNTransition circleBlurTransition;
	[Export ("circleBlurTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleBlurTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleColor;
	[NullAllowed, Export ("circleColor", ArgumentSemantic.Assign)]
	NSExpression CircleColor { get; set; }

	// @property (nonatomic) MLNTransition circleColorTransition;
	[Export ("circleColorTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleOpacity;
	[NullAllowed, Export ("circleOpacity", ArgumentSemantic.Assign)]
	NSExpression CircleOpacity { get; set; }

	// @property (nonatomic) MLNTransition circleOpacityTransition;
	[Export ("circleOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circlePitchAlignment;
	[NullAllowed, Export ("circlePitchAlignment", ArgumentSemantic.Assign)]
	NSExpression CirclePitchAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleRadius;
	[NullAllowed, Export ("circleRadius", ArgumentSemantic.Assign)]
	NSExpression CircleRadius { get; set; }

	// @property (nonatomic) MLNTransition circleRadiusTransition;
	[Export ("circleRadiusTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleRadiusTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleScaleAlignment;
	[NullAllowed, Export ("circleScaleAlignment", ArgumentSemantic.Assign)]
	NSExpression CircleScaleAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleStrokeColor;
	[NullAllowed, Export ("circleStrokeColor", ArgumentSemantic.Assign)]
	NSExpression CircleStrokeColor { get; set; }

	// @property (nonatomic) MLNTransition circleStrokeColorTransition;
	[Export ("circleStrokeColorTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleStrokeColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleStrokeOpacity;
	[NullAllowed, Export ("circleStrokeOpacity", ArgumentSemantic.Assign)]
	NSExpression CircleStrokeOpacity { get; set; }

	// @property (nonatomic) MLNTransition circleStrokeOpacityTransition;
	[Export ("circleStrokeOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleStrokeOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleStrokeWidth;
	[NullAllowed, Export ("circleStrokeWidth", ArgumentSemantic.Assign)]
	NSExpression CircleStrokeWidth { get; set; }

	// @property (nonatomic) MLNTransition circleStrokeWidthTransition;
	[Export ("circleStrokeWidthTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleStrokeWidthTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleTranslation;
	[NullAllowed, Export ("circleTranslation", ArgumentSemantic.Assign)]
	NSExpression CircleTranslation { get; set; }

	// @property (nonatomic) MLNTransition circleTranslationTransition;
	[Export ("circleTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition CircleTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified circleTranslationAnchor;
	[NullAllowed, Export ("circleTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression CircleTranslationAnchor { get; set; }
}

// @interface MLNCircleStyleLayerAdditions (NSValue)
// [Category]
// [BaseType (typeof(NSValue))]
// interface NSValue_MLNCircleStyleLayerAdditions
// {
// 	[Static]
// 	[Export ("valueWithMLNCirclePitchAlignment:")]
// 	NSValue ValueWithMLNCirclePitchAlignment (MLNCirclePitchAlignment circlePitchAlignment);
// 	[Export ("MLNCirclePitchAlignmentValue")]
// 	MLNCirclePitchAlignment MLNCirclePitchAlignmentValue { get; }
// 	[Static]
// 	[Export ("valueWithMLNCircleScaleAlignment:")]
// 	NSValue ValueWithMLNCircleScaleAlignment (MLNCircleScaleAlignment circleScaleAlignment);
// 	[Export ("MLNCircleScaleAlignmentValue")]
// 	MLNCircleScaleAlignment MLNCircleScaleAlignmentValue { get; }
// 	[Static]
// 	[Export ("valueWithMLNCircleTranslationAnchor:")]
// 	NSValue ValueWithMLNCircleTranslationAnchor (MLNCircleTranslationAnchor circleTranslationAnchor);
// 	[Export ("MLNCircleTranslationAnchorValue")]
// 	MLNCircleTranslationAnchor MLNCircleTranslationAnchorValue { get; }
// }

// @interface MLNClockDirectionFormatter : NSFormatter
[BaseType (typeof(NSFormatter))]
interface MLNClockDirectionFormatter
{
	// @property (nonatomic) NSFormattingUnitStyle unitStyle;
	[Export ("unitStyle", ArgumentSemantic.Assign)]
	NSFormattingUnitStyle UnitStyle { get; set; }

	// -(NSString * _Nonnull)stringFromDirection:(CLLocationDirection)direction;
	[Export ("stringFromDirection:")]
	string StringFromDirection (double direction);

	// -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
	[Export ("getObjectValue:forString:errorDescription:")]
	bool GetObjectValue ([NullAllowed] out NSObject obj, string @string, [NullAllowed] out string error);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const NSUInteger MLNClusterIdentifierInvalid __attribute__((visibility("default")));
	[Field ("MLNClusterIdentifierInvalid", "__Internal")]
	nuint MLNClusterIdentifierInvalid { get; }
}

// @protocol MLNCluster <MLNFeature>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
interface MLNCluster : IMLNFeature
{
	// @required @property (readonly, nonatomic) NSUInteger clusterIdentifier;
	[Abstract]
	[Export ("clusterIdentifier")]
	nuint ClusterIdentifier { get; }

	// @required @property (readonly, nonatomic) NSUInteger clusterPointCount;
	[Abstract]
	[Export ("clusterPointCount")]
	nuint ClusterPointCount { get; }
}

// @interface MLNCompassButton : UIImageView
[BaseType (typeof(UIImageView))]
interface MLNCompassButton
{
	// @property (assign, nonatomic) MLNOrnamentVisibility compassVisibility;
	[Export ("compassVisibility", ArgumentSemantic.Assign)]
	MLNOrnamentVisibility CompassVisibility { get; set; }
}

// @interface MLNCompassDirectionFormatter : NSFormatter
[BaseType (typeof(NSFormatter))]
interface MLNCompassDirectionFormatter
{
	// @property (nonatomic) NSFormattingUnitStyle unitStyle;
	[Export ("unitStyle", ArgumentSemantic.Assign)]
	NSFormattingUnitStyle UnitStyle { get; set; }

	// -(NSString * _Nonnull)stringFromDirection:(CLLocationDirection)direction;
	[Export ("stringFromDirection:")]
	string StringFromDirection (double direction);

	// -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
	[Export ("getObjectValue:forString:errorDescription:")]
	bool GetObjectValue ([NullAllowed] out NSObject obj, string @string, [NullAllowed] out string error);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNCoordinateSpan MLNCoordinateSpanZero __attribute__((visibility("default")));
	// [Field ("MLNCoordinateSpanZero", "__Internal")]
	// MLNCoordinateSpan MLNCoordinateSpanZero { get; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExceptionName _Nonnull MLNInvalidStyleSourceException __attribute__((visibility("default")));
	[Field ("MLNInvalidStyleSourceException", "__Internal")]
	NSString MLNInvalidStyleSourceException { get; }
}

// @interface MLNSource : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface MLNSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("initWithIdentifier:")]
	NativeHandle Constructor (string identifier);

	// @property (copy, nonatomic) NSString * _Nonnull identifier;
	[Export ("identifier")]
	string Identifier { get; set; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionClustered __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionClustered", "__Internal")]
	NSString MLNShapeSourceOptionClustered { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionClusterRadius __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionClusterRadius", "__Internal")]
	NSString MLNShapeSourceOptionClusterRadius { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionClusterMinPoints __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionClusterMinPoints", "__Internal")]
	NSString MLNShapeSourceOptionClusterMinPoints { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionClusterProperties __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionClusterProperties", "__Internal")]
	NSString MLNShapeSourceOptionClusterProperties { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionMaximumZoomLevelForClustering __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionMaximumZoomLevelForClustering", "__Internal")]
	NSString MLNShapeSourceOptionMaximumZoomLevelForClustering { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionMinimumZoomLevel __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionMinimumZoomLevel", "__Internal")]
	NSString MLNShapeSourceOptionMinimumZoomLevel { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionMaximumZoomLevel __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionMaximumZoomLevel", "__Internal")]
	NSString MLNShapeSourceOptionMaximumZoomLevel { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionBuffer __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionBuffer", "__Internal")]
	NSString MLNShapeSourceOptionBuffer { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionSimplificationTolerance __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionSimplificationTolerance", "__Internal")]
	NSString MLNShapeSourceOptionSimplificationTolerance { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionLineDistanceMetrics __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionLineDistanceMetrics", "__Internal")]
	NSString MLNShapeSourceOptionLineDistanceMetrics { get; }
}

// @interface MLNShapeSource : MLNSource
[BaseType (typeof(MLNSource))]
interface MLNShapeSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier URL:(NSUrl * _Nonnull)url options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:URL:options:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier shape:(MLNShape * _Nullable)shape options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:shape:options:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, [NullAllowed] MLNShape shape, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier features:(NSArray<MLNShape<MLNFeature> *> * _Nonnull)features options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options;
	[Export ("initWithIdentifier:features:options:")]
	NativeHandle Constructor (string identifier, MLNFeature[] features, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier shapes:(NSArray<MLNShape *> * _Nonnull)shapes options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options;
	[Export ("initWithIdentifier:shapes:options:")]
	NativeHandle Constructor (string identifier, MLNShape[] shapes, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// @property (copy, nonatomic) MLNShape * _Nullable shape;
	[NullAllowed, Export ("shape", ArgumentSemantic.Copy)]
	MLNShape Shape { get; set; }

	// @property (copy, nonatomic) NSUrl * _Nullable URL;
	[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
	NSUrl URL { get; set; }

	// -(NSArray<id<MLNFeature>> * _Nonnull)featuresMatchingPredicate:(NSPredicate * _Nullable)predicate;
	[Export ("featuresMatchingPredicate:")]
	IMLNFeature[] FeaturesMatchingPredicate ([NullAllowed] NSPredicate predicate);

	// -(NSArray<id<MLNFeature>> * _Nonnull)leavesOfCluster:(MLNPointFeatureCluster * _Nonnull)cluster offset:(NSUInteger)offset limit:(NSUInteger)limit;
	[Export ("leavesOfCluster:offset:limit:")]
	IMLNFeature[] LeavesOfCluster (MLNPointFeatureCluster cluster, nuint offset, nuint limit);

	// -(NSArray<id<MLNFeature>> * _Nonnull)childrenOfCluster:(MLNPointFeatureCluster * _Nonnull)cluster;
	[Export ("childrenOfCluster:")]
	IMLNFeature[] ChildrenOfCluster (MLNPointFeatureCluster cluster);

	// -(double)zoomLevelForExpandingCluster:(MLNPointFeatureCluster * _Nonnull)cluster;
	[Export ("zoomLevelForExpandingCluster:")]
	double ZoomLevelForExpandingCluster (MLNPointFeatureCluster cluster);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionWrapsCoordinates __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionWrapsCoordinates", "__Internal")]
	NSString MLNShapeSourceOptionWrapsCoordinates { get; }

	// extern const MLNShapeSourceOption _Nonnull MLNShapeSourceOptionClipsCoordinates __attribute__((visibility("default")));
	[Field ("MLNShapeSourceOptionClipsCoordinates", "__Internal")]
	NSString MLNShapeSourceOptionClipsCoordinates { get; }

	// extern const MLNExceptionName _Nonnull MLNInvalidDatasourceException __attribute__((visibility("default")));
	[Field ("MLNInvalidDatasourceException", "__Internal")]
	NSString MLNInvalidDatasourceException { get; }
}

// @protocol MLNComputedShapeSourceDataSource <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNComputedShapeSourceDataSource
{
	// @optional -(NSArray<MLNShape<MLNFeature> *> * _Nonnull)featuresInTileAtX:(NSUInteger)x y:(NSUInteger)y zoomLevel:(NSUInteger)zoomLevel;
	[Export ("featuresInTileAtX:y:zoomLevel:")]
	MLNFeature[] FeaturesInTileAtX (nuint x, nuint y, nuint zoomLevel);

	// @optional -(NSArray<MLNShape<MLNFeature> *> * _Nonnull)featuresInCoordinateBounds:(MLNCoordinateBounds)bounds zoomLevel:(NSUInteger)zoomLevel;
	[Export ("featuresInCoordinateBounds:zoomLevel:")]
	MLNFeature[] FeaturesInCoordinateBounds (MLNCoordinateBounds bounds, nuint zoomLevel);
}

// @interface MLNComputedShapeSource : MLNSource
[BaseType (typeof(MLNSource))]
interface MLNComputedShapeSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:options:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier dataSource:(id<MLNComputedShapeSourceDataSource> _Nonnull)dataSource options:(NSDictionary<MLNShapeSourceOption,id> * _Nullable)options;
	[Export ("initWithIdentifier:dataSource:options:")]
	NativeHandle Constructor (string identifier, MLNComputedShapeSourceDataSource dataSource, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(void)invalidateBounds:(MLNCoordinateBounds)bounds;
	[Export ("invalidateBounds:")]
	void InvalidateBounds (MLNCoordinateBounds bounds);

	// -(void)invalidateTileAtX:(NSUInteger)x y:(NSUInteger)y zoomLevel:(NSUInteger)zoomLevel;
	[Export ("invalidateTileAtX:y:zoomLevel:")]
	void InvalidateTileAtX (nuint x, nuint y, nuint zoomLevel);

	// -(void)setFeatures:(NSArray<MLNShape<MLNFeature> *> * _Nonnull)features inTileAtX:(NSUInteger)x y:(NSUInteger)y zoomLevel:(NSUInteger)zoomLevel;
	[Export ("setFeatures:inTileAtX:y:zoomLevel:")]
	void SetFeatures (MLNFeature[] features, nuint x, nuint y, nuint zoomLevel);

	[Wrap ("WeakDataSource")]
	[NullAllowed]
	MLNComputedShapeSourceDataSource DataSource { get; set; }

	// @property (nonatomic, weak) id<MLNComputedShapeSourceDataSource> _Nullable dataSource;
	[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
	NSObject WeakDataSource { get; set; }

	// @property (readonly, nonatomic) NSOperationQueue * _Nonnull requestQueue;
	[Export ("requestQueue")]
	NSOperationQueue RequestQueue { get; }
}

// @interface MLNCoordinateFormatter : NSFormatter
[BaseType (typeof(NSFormatter))]
interface MLNCoordinateFormatter
{
	// @property (nonatomic) BOOL allowsMinutes;
	[Export ("allowsMinutes")]
	bool AllowsMinutes { get; set; }

	// @property (nonatomic) BOOL allowsSeconds;
	[Export ("allowsSeconds")]
	bool AllowsSeconds { get; set; }

	// @property (nonatomic) NSFormattingUnitStyle unitStyle;
	[Export ("unitStyle", ArgumentSemantic.Assign)]
	NSFormattingUnitStyle UnitStyle { get; set; }

	// -(NSString * _Nonnull)stringFromCoordinate:(CLLocationCoordinate2D)coordinate;
	[Export ("stringFromCoordinate:")]
	string StringFromCoordinate (CLLocationCoordinate2D coordinate);

	// -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
	[Export ("getObjectValue:forString:errorDescription:")]
	bool GetObjectValue ([NullAllowed] out NSObject obj, string @string, [NullAllowed] out string error);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
}

// @interface MLNCustomDrawableStyleLayer : MLNStyleLayer
[BaseType (typeof(MLNStyleLayer))]
interface MLNCustomDrawableStyleLayer
{
}

// @interface MLNCustomStyleLayer : MLNStyleLayer
[BaseType (typeof(MLNStyleLayer))]
interface MLNCustomStyleLayer
{
	// @property (readonly, nonatomic, weak) MLNStyle * _Nullable style;
	[NullAllowed, Export ("style", ArgumentSemantic.Weak)]
	MLNStyle Style { get; }

	// @property (readonly, nonatomic) EAGLContext * _Nonnull context;
	[Export ("context")]
	EAGLContext Context { get; }

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("initWithIdentifier:")]
	NativeHandle Constructor (string identifier);

	// -(void)didMoveToMapView:(MLNMapView * _Nonnull)mapView;
	[Export ("didMoveToMapView:")]
	void DidMoveToMapView (MLNMapView mapView);

	// -(void)willMoveFromMapView:(MLNMapView * _Nonnull)mapView;
	[Export ("willMoveFromMapView:")]
	void WillMoveFromMapView (MLNMapView mapView);

	// -(void)drawInMapView:(MLNMapView * _Nonnull)mapView withContext:(MLNStyleLayerDrawingContext)context;
	[Export ("drawInMapView:withContext:")]
	void DrawInMapView (MLNMapView mapView, MLNStyleLayerDrawingContext context);

	// -(void)setNeedsDisplay;
	[Export ("setNeedsDisplay")]
	void SetNeedsDisplay ();
}

// @interface MLNDefaultStyle : NSObject
[BaseType (typeof(NSObject))]
interface MLNDefaultStyle
{
	// @property (retain, nonatomic) NSUrl * _Nonnull url;
	[Export ("url", ArgumentSemantic.Retain)]
	NSUrl Url { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull name;
	[Export ("name", ArgumentSemantic.Retain)]
	string Name { get; set; }

	// @property (nonatomic) int version;
	[Export ("version")]
	int Version { get; set; }
}

// @interface MLNDistanceFormatter : NSLengthFormatter
[BaseType (typeof(NSLengthFormatter))]
interface MLNDistanceFormatter
{
	// -(NSString * _Nonnull)stringFromDistance:(CLLocationDistance)distance;
	[Export ("stringFromDistance:")]
	string StringFromDistance (double distance);
}

// @interface MLNShape : NSObject <MLNAnnotation, NSSecureCoding>
[BaseType (typeof(NSObject))]
interface MLNShape : IMLNAnnotation, INSSecureCoding
{
	// +(MLNShape * _Nullable)shapeWithData:(NSData * _Nonnull)data encoding:(NSStringEncoding)encoding error:(NSError * _Nullable * _Nullable)outError;
	[Static]
	[Export ("shapeWithData:encoding:error:")]
	[return: NullAllowed]
	MLNShape ShapeWithData (NSData data, nuint encoding, [NullAllowed] out NSError outError);

	// @property (copy, nonatomic) NSString * _Nullable title;
	[NullAllowed, Export ("title")]
	string Title { get; set; }

	// @property (copy, nonatomic) NSString * _Nullable subtitle;
	[NullAllowed, Export ("subtitle")]
	string Subtitle { get; set; }

	// -(NSData * _Nonnull)geoJSONDataUsingEncoding:(NSStringEncoding)encoding;
	[Export ("geoJSONDataUsingEncoding:")]
	NSData GeoJSONDataUsingEncoding (nuint encoding);
}

// @interface MLNPointAnnotation : MLNShape
[BaseType (typeof(MLNShape))]
interface MLNPointAnnotation
{
	// @property (assign, nonatomic) CLLocationCoordinate2D coordinate;
	[Export ("coordinate", ArgumentSemantic.Assign)]
	CLLocationCoordinate2D Coordinate { get; set; }
}

// @protocol MLNOverlay <MLNAnnotation>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNOverlay : IMLNAnnotation
{
	// @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
	[Abstract]
	[Export ("coordinate")]
	CLLocationCoordinate2D Coordinate { get; }

	// @required @property (readonly, nonatomic) MLNCoordinateBounds overlayBounds;
	[Abstract]
	[Export ("overlayBounds")]
	MLNCoordinateBounds OverlayBounds { get; }

	// @required -(BOOL)intersectsOverlayBounds:(MLNCoordinateBounds)overlayBounds;
	[Abstract]
	[Export ("intersectsOverlayBounds:")]
	bool IntersectsOverlayBounds (MLNCoordinateBounds overlayBounds);
}

// @interface MLNPointCollection : MLNShape <MLNOverlay>
[BaseType (typeof(MLNShape))]
interface MLNPointCollection : IMLNOverlay
{
	// +(instancetype)pointCollectionWithCoordinates:(const CLLocationCoordinate2D *)coords count:(NSUInteger)count;
	[Static]
	[Export ("pointCollectionWithCoordinates:count:")]
	unsafe MLNPointCollection PointCollectionWithCoordinates (CLLocationCoordinate2D* coords, nuint count);

	// @property (readonly, nonatomic) CLLocationCoordinate2D * coordinates __attribute__((objc_returns_inner_pointer));
	[Export ("coordinates")]
	unsafe IntPtr Coordinates { get; }

	// @property (readonly, nonatomic) NSUInteger pointCount;
	[Export ("pointCount")]
	nuint PointCount { get; }

	// -(void)getCoordinates:(CLLocationCoordinate2D *)coords range:(NSRange)range;
	[Export ("getCoordinates:range:")]
	unsafe void GetCoordinates (CLLocationCoordinate2D* coords, NSRange range);
}

// @interface MLNMultiPoint : MLNShape
[BaseType (typeof(MLNShape))]
interface MLNMultiPoint
{
	// @property (readonly, nonatomic) NS_RETURNS_INNER_POINTER CLLocationCoordinate2D * coordinates __attribute__((objc_returns_inner_pointer));
	[Export ("coordinates")]
	unsafe IntPtr Coordinates { get; }

	// @property (readonly, nonatomic) NSUInteger pointCount;
	[Export ("pointCount")]
	nuint PointCount { get; }

	// -(void)getCoordinates:(CLLocationCoordinate2D * _Nonnull)coords range:(NSRange)range;
	[Export ("getCoordinates:range:")]
	unsafe void GetCoordinates (CLLocationCoordinate2D* coords, NSRange range);

	// -(void)setCoordinates:(CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
	[Export ("setCoordinates:count:")]
	unsafe void SetCoordinates (CLLocationCoordinate2D* coords, nuint count);

	// -(void)insertCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count atIndex:(NSUInteger)index;
	[Export ("insertCoordinates:count:atIndex:")]
	unsafe void InsertCoordinates (CLLocationCoordinate2D* coords, nuint count, nuint index);

	// -(void)appendCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
	[Export ("appendCoordinates:count:")]
	unsafe void AppendCoordinates (CLLocationCoordinate2D* coords, nuint count);

	// -(void)replaceCoordinatesInRange:(NSRange)range withCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords;
	[Export ("replaceCoordinatesInRange:withCoordinates:")]
	unsafe void ReplaceCoordinatesInRange (NSRange range, CLLocationCoordinate2D* coords);

	// -(void)replaceCoordinatesInRange:(NSRange)range withCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
	[Export ("replaceCoordinatesInRange:withCoordinates:count:")]
	unsafe void ReplaceCoordinatesInRange (NSRange range, CLLocationCoordinate2D* coords, nuint count);

	// -(void)removeCoordinatesInRange:(NSRange)range;
	[Export ("removeCoordinatesInRange:")]
	void RemoveCoordinatesInRange (NSRange range);
}

// @interface MLNPolygon : MLNMultiPoint <MLNOverlay>
[BaseType (typeof(MLNMultiPoint))]
interface MLNPolygon : IMLNOverlay
{
	// @property (readonly, nonatomic) NSArray<MLNPolygon *> * _Nullable interiorPolygons;
	[NullAllowed, Export ("interiorPolygons")]
	MLNPolygon[] InteriorPolygons { get; }

	// +(instancetype _Nonnull)polygonWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
	[Static]
	[Export ("polygonWithCoordinates:count:")]
	unsafe MLNPolygon PolygonWithCoordinates (CLLocationCoordinate2D* coords, nuint count);

	// +(instancetype _Nonnull)polygonWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count interiorPolygons:(NSArray<MLNPolygon *> * _Nullable)interiorPolygons;
	[Static]
	[Export ("polygonWithCoordinates:count:interiorPolygons:")]
	unsafe MLNPolygon PolygonWithCoordinates (CLLocationCoordinate2D* coords, nuint count, [NullAllowed] MLNPolygon[] interiorPolygons);
}

// @interface MLNMultiPolygon : MLNShape <MLNOverlay>
[BaseType (typeof(MLNShape))]
interface MLNMultiPolygon : IMLNOverlay
{
	// @property (readonly, copy, nonatomic) NSArray<MLNPolygon *> * _Nonnull polygons;
	[Export ("polygons", ArgumentSemantic.Copy)]
	MLNPolygon[] Polygons { get; }

	// +(instancetype _Nonnull)multiPolygonWithPolygons:(NSArray<MLNPolygon *> * _Nonnull)polygons;
	[Static]
	[Export ("multiPolygonWithPolygons:")]
	MLNMultiPolygon MultiPolygonWithPolygons (MLNPolygon[] polygons);
}

// @interface MLNPolyline : MLNMultiPoint <MLNOverlay>
[BaseType (typeof(MLNMultiPoint))]
interface MLNPolyline : IMLNOverlay
{
	// +(instancetype _Nonnull)polylineWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
	[Static]
	[Export ("polylineWithCoordinates:count:")]
	unsafe MLNPolyline PolylineWithCoordinates (CLLocationCoordinate2D* coords, nuint count);
}

// @interface MLNMultiPolyline : MLNShape <MLNOverlay>
[BaseType (typeof(MLNShape))]
interface MLNMultiPolyline : IMLNOverlay
{
	// @property (readonly, copy, nonatomic) NSArray<MLNPolyline *> * _Nonnull polylines;
	[Export ("polylines", ArgumentSemantic.Copy)]
	MLNPolyline[] Polylines { get; }

	// +(instancetype _Nonnull)multiPolylineWithPolylines:(NSArray<MLNPolyline *> * _Nonnull)polylines;
	[Static]
	[Export ("multiPolylineWithPolylines:")]
	MLNMultiPolyline MultiPolylineWithPolylines (MLNPolyline[] polylines);
}

// @interface MLNShapeCollection : MLNShape
[BaseType (typeof(MLNShape))]
interface MLNShapeCollection
{
	// @property (readonly, copy, nonatomic) NSArray<MLNShape *> * _Nonnull shapes;
	[Export ("shapes", ArgumentSemantic.Copy)]
	MLNShape[] Shapes { get; }

	// +(instancetype _Nonnull)shapeCollectionWithShapes:(NSArray<MLNShape *> * _Nonnull)shapes;
	[Static]
	[Export ("shapeCollectionWithShapes:")]
	MLNShapeCollection ShapeCollectionWithShapes (MLNShape[] shapes);
}

// @protocol MLNFeature <MLNAnnotation>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNFeature : IMLNAnnotation
{
	// @required @property (copy, nonatomic) id _Nullable identifier;
	[Abstract]
	[NullAllowed, Export ("identifier", ArgumentSemantic.Copy)]
	NSObject Identifier { get; set; }

	// @required @property (copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull attributes;
	[Abstract]
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; set; }

	// @required -(id _Nullable)attributeForKey:(NSString * _Nonnull)key;
	[Abstract]
	[Export ("attributeForKey:")]
	[return: NullAllowed]
	NSObject AttributeForKey (string key);

	// @required -(NSDictionary<NSString *,id> * _Nonnull)geoJSONDictionary;
	[Abstract]
	[Export ("geoJSONDictionary")]
	// [Verify (MethodToProperty)]
	NSDictionary<NSString, NSObject> GeoJSONDictionary { get; }
}

// @interface MLNEmptyFeature : MLNShape <MLNFeature>
[BaseType (typeof(MLNShape))]
interface MLNEmptyFeature : IMLNFeature
{
}

// @interface MLNPointFeature : MLNPointAnnotation <MLNFeature>
[BaseType (typeof(MLNPointAnnotation))]
interface MLNPointFeature : IMLNFeature
{
}

// @interface MLNPointFeatureCluster : MLNPointFeature <MLNCluster>
[BaseType (typeof(MLNPointFeature))]
interface MLNPointFeatureCluster : IMLNCluster
{
}

// @interface MLNPolylineFeature : MLNPolyline <MLNFeature>
[BaseType (typeof(MLNPolyline))]
interface MLNPolylineFeature : IMLNFeature
{
}

// @interface MLNPolygonFeature : MLNPolygon <MLNFeature>
[BaseType (typeof(MLNPolygon))]
interface MLNPolygonFeature : IMLNFeature
{
}

// @interface MLNPointCollectionFeature : MLNPointCollection <MLNFeature>
[BaseType (typeof(MLNPointCollection))]
interface MLNPointCollectionFeature : IMLNFeature
{
}

// @interface MLNMultiPolylineFeature : MLNMultiPolyline <MLNFeature>
[BaseType (typeof(MLNMultiPolyline))]
interface MLNMultiPolylineFeature : IMLNFeature
{
}

// @interface MLNMultiPolygonFeature : MLNMultiPolygon <MLNFeature>
[BaseType (typeof(MLNMultiPolygon))]
interface MLNMultiPolygonFeature : IMLNFeature
{
}

// @interface MLNShapeCollectionFeature : MLNShapeCollection <MLNFeature>
[BaseType (typeof(MLNShapeCollection))]
interface MLNShapeCollectionFeature : IMLNFeature
{
	// @property (readonly, copy, nonatomic) NSArray<MLNShape<MLNFeature> *> * _Nonnull shapes;
	[Export ("shapes", ArgumentSemantic.Copy)]
	MLNFeature[] Shapes { get; }

	// +(instancetype _Nonnull)shapeCollectionWithShapes:(NSArray<MLNShape<MLNFeature> *> * _Nonnull)shapes;
	[Static]
	[Export ("shapeCollectionWithShapes:")]
	MLNShapeCollectionFeature ShapeCollectionWithShapes (MLNFeature[] shapes);
}

// @interface MLNFillExtrusionStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNFillExtrusionStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionBase;
	[NullAllowed, Export ("fillExtrusionBase", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionBase { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionBaseTransition;
	[Export ("fillExtrusionBaseTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionBaseTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionColor;
	[NullAllowed, Export ("fillExtrusionColor", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionColor { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionColorTransition;
	[Export ("fillExtrusionColorTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionHasVerticalGradient;
	[NullAllowed, Export ("fillExtrusionHasVerticalGradient", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionHasVerticalGradient { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionHeight;
	[NullAllowed, Export ("fillExtrusionHeight", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionHeight { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionHeightTransition;
	[Export ("fillExtrusionHeightTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionHeightTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionOpacity;
	[NullAllowed, Export ("fillExtrusionOpacity", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionOpacity { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionOpacityTransition;
	[Export ("fillExtrusionOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionPattern;
	[NullAllowed, Export ("fillExtrusionPattern", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionPattern { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionPatternTransition;
	[Export ("fillExtrusionPatternTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionPatternTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionTranslation;
	[NullAllowed, Export ("fillExtrusionTranslation", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionTranslation { get; set; }

	// @property (nonatomic) MLNTransition fillExtrusionTranslationTransition;
	[Export ("fillExtrusionTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition FillExtrusionTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillExtrusionTranslationAnchor;
	[NullAllowed, Export ("fillExtrusionTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression FillExtrusionTranslationAnchor { get; set; }
}

// @interface MLNFillExtrusionStyleLayerAdditions (NSValue)
// [Category]
// [BaseType (typeof(NSValue))]
// interface NSValue_MLNFillExtrusionStyleLayerAdditions
// {
// 	[Static]
// 	[Export ("valueWithMLNFillExtrusionTranslationAnchor:")]
// 	NSValue ValueWithMLNFillExtrusionTranslationAnchor (MLNFillExtrusionTranslationAnchor fillExtrusionTranslationAnchor);
// 	[Export ("MLNFillExtrusionTranslationAnchorValue")]
// 	MLNFillExtrusionTranslationAnchor MLNFillExtrusionTranslationAnchorValue { get; }
// }

// @interface MLNFillStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNFillStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillSortKey;
	[NullAllowed, Export ("fillSortKey", ArgumentSemantic.Assign)]
	NSExpression FillSortKey { get; set; }

	// @property (getter = isFillAntialiased, nonatomic, null_resettable) NSExpression * _Null_unspecified fillAntialiased;
	[NullAllowed, Export ("fillAntialiased", ArgumentSemantic.Assign)]
	NSExpression FillAntialiased { [Bind ("isFillAntialiased")] get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillColor;
	[NullAllowed, Export ("fillColor", ArgumentSemantic.Assign)]
	NSExpression FillColor { get; set; }

	// @property (nonatomic) MLNTransition fillColorTransition;
	[Export ("fillColorTransition", ArgumentSemantic.Assign)]
	MLNTransition FillColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillOpacity;
	[NullAllowed, Export ("fillOpacity", ArgumentSemantic.Assign)]
	NSExpression FillOpacity { get; set; }

	// @property (nonatomic) MLNTransition fillOpacityTransition;
	[Export ("fillOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition FillOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillOutlineColor;
	[NullAllowed, Export ("fillOutlineColor", ArgumentSemantic.Assign)]
	NSExpression FillOutlineColor { get; set; }

	// @property (nonatomic) MLNTransition fillOutlineColorTransition;
	[Export ("fillOutlineColorTransition", ArgumentSemantic.Assign)]
	MLNTransition FillOutlineColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillPattern;
	[NullAllowed, Export ("fillPattern", ArgumentSemantic.Assign)]
	NSExpression FillPattern { get; set; }

	// @property (nonatomic) MLNTransition fillPatternTransition;
	[Export ("fillPatternTransition", ArgumentSemantic.Assign)]
	MLNTransition FillPatternTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillTranslation;
	[NullAllowed, Export ("fillTranslation", ArgumentSemantic.Assign)]
	NSExpression FillTranslation { get; set; }

	// @property (nonatomic) MLNTransition fillTranslationTransition;
	[Export ("fillTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition FillTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified fillTranslationAnchor;
	[NullAllowed, Export ("fillTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression FillTranslationAnchor { get; set; }
}

// @interface MLNFillStyleLayerAdditions (NSValue)
// [Category]
// [BaseType (typeof(NSValue))]
// interface NSValue_MLNFillStyleLayerAdditions
// {
// 	[Static]
// 	[Export ("valueWithMLNFillTranslationAnchor:")]
// 	NSValue ValueWithMLNFillTranslationAnchor (MLNFillTranslationAnchor fillTranslationAnchor);
// 	[Export ("MLNFillTranslationAnchorValue")]
// 	MLNFillTranslationAnchor MLNFillTranslationAnchorValue { get; }
// }

// @interface MLNHeatmapStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNHeatmapStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified heatmapColor;
	[NullAllowed, Export ("heatmapColor", ArgumentSemantic.Assign)]
	NSExpression HeatmapColor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified heatmapIntensity;
	[NullAllowed, Export ("heatmapIntensity", ArgumentSemantic.Assign)]
	NSExpression HeatmapIntensity { get; set; }

	// @property (nonatomic) MLNTransition heatmapIntensityTransition;
	[Export ("heatmapIntensityTransition", ArgumentSemantic.Assign)]
	MLNTransition HeatmapIntensityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified heatmapOpacity;
	[NullAllowed, Export ("heatmapOpacity", ArgumentSemantic.Assign)]
	NSExpression HeatmapOpacity { get; set; }

	// @property (nonatomic) MLNTransition heatmapOpacityTransition;
	[Export ("heatmapOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition HeatmapOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified heatmapRadius;
	[NullAllowed, Export ("heatmapRadius", ArgumentSemantic.Assign)]
	NSExpression HeatmapRadius { get; set; }

	// @property (nonatomic) MLNTransition heatmapRadiusTransition;
	[Export ("heatmapRadiusTransition", ArgumentSemantic.Assign)]
	MLNTransition HeatmapRadiusTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified heatmapWeight;
	[NullAllowed, Export ("heatmapWeight", ArgumentSemantic.Assign)]
	NSExpression HeatmapWeight { get; set; }
}

// @interface MLNHillshadeStyleLayer : MLNForegroundStyleLayer
[BaseType (typeof(MLNForegroundStyleLayer))]
interface MLNHillshadeStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeAccentColor;
	[NullAllowed, Export ("hillshadeAccentColor", ArgumentSemantic.Assign)]
	NSExpression HillshadeAccentColor { get; set; }

	// @property (nonatomic) MLNTransition hillshadeAccentColorTransition;
	[Export ("hillshadeAccentColorTransition", ArgumentSemantic.Assign)]
	MLNTransition HillshadeAccentColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeExaggeration;
	[NullAllowed, Export ("hillshadeExaggeration", ArgumentSemantic.Assign)]
	NSExpression HillshadeExaggeration { get; set; }

	// @property (nonatomic) MLNTransition hillshadeExaggerationTransition;
	[Export ("hillshadeExaggerationTransition", ArgumentSemantic.Assign)]
	MLNTransition HillshadeExaggerationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeHighlightColor;
	[NullAllowed, Export ("hillshadeHighlightColor", ArgumentSemantic.Assign)]
	NSExpression HillshadeHighlightColor { get; set; }

	// @property (nonatomic) MLNTransition hillshadeHighlightColorTransition;
	[Export ("hillshadeHighlightColorTransition", ArgumentSemantic.Assign)]
	MLNTransition HillshadeHighlightColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeIlluminationAnchor;
	[NullAllowed, Export ("hillshadeIlluminationAnchor", ArgumentSemantic.Assign)]
	NSExpression HillshadeIlluminationAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeIlluminationDirection;
	[NullAllowed, Export ("hillshadeIlluminationDirection", ArgumentSemantic.Assign)]
	NSExpression HillshadeIlluminationDirection { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified hillshadeShadowColor;
	[NullAllowed, Export ("hillshadeShadowColor", ArgumentSemantic.Assign)]
	NSExpression HillshadeShadowColor { get; set; }

	// @property (nonatomic) MLNTransition hillshadeShadowColorTransition;
	[Export ("hillshadeShadowColorTransition", ArgumentSemantic.Assign)]
	MLNTransition HillshadeShadowColorTransition { get; set; }
}

// @interface MLNHillshadeStyleLayerAdditions (NSValue)
// [Category]
// [BaseType (typeof(NSValue))]
// interface NSValue_MLNHillshadeStyleLayerAdditions
// {
// 	[Static]
// 	[Export ("valueWithMLNHillshadeIlluminationAnchor:")]
// 	NSValue ValueWithMLNHillshadeIlluminationAnchor (MLNHillshadeIlluminationAnchor hillshadeIlluminationAnchor);
// 	[Export ("MLNHillshadeIlluminationAnchorValue")]
// 	MLNHillshadeIlluminationAnchor MLNHillshadeIlluminationAnchorValue { get; }
// }

// @interface MLNImageSource : MLNSource
[BaseType (typeof(MLNSource))]
interface MLNImageSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier coordinateQuad:(MLNCoordinateQuad)coordinateQuad URL:(NSUrl * _Nonnull)url;
	[Export ("initWithIdentifier:coordinateQuad:URL:")]
	NativeHandle Constructor (string identifier, MLNCoordinateQuad coordinateQuad, NSUrl url);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier coordinateQuad:(MLNCoordinateQuad)coordinateQuad image:(UIImage * _Nonnull)image;
	[Export ("initWithIdentifier:coordinateQuad:image:")]
	NativeHandle Constructor (string identifier, MLNCoordinateQuad coordinateQuad, UIImage image);

	// @property (copy, nonatomic) NSUrl * _Nullable URL;
	[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
	NSUrl URL { get; set; }

	// @property (retain, nonatomic) UIImage * _Nullable image;
	[NullAllowed, Export ("image", ArgumentSemantic.Retain)]
	UIImage Image { get; set; }

	// @property (nonatomic) MLNCoordinateQuad coordinates;
	[Export ("coordinates", ArgumentSemantic.Assign)]
	MLNCoordinateQuad Coordinates { get; set; }
}

// @interface MLNLight : NSObject
[BaseType (typeof(NSObject))]
interface MLNLight
{
	// @property (nonatomic) NSExpression * _Nonnull anchor;
	[Export ("anchor", ArgumentSemantic.Assign)]
	NSExpression Anchor { get; set; }

	// @property (nonatomic) NSExpression * _Nonnull position;
	[Export ("position", ArgumentSemantic.Assign)]
	NSExpression Position { get; set; }

	// @property (nonatomic) MLNTransition positionTransition;
	[Export ("positionTransition", ArgumentSemantic.Assign)]
	MLNTransition PositionTransition { get; set; }

	// @property (nonatomic) NSExpression * _Nonnull color;
	[Export ("color", ArgumentSemantic.Assign)]
	NSExpression Color { get; set; }

	// @property (nonatomic) MLNTransition colorTransition;
	[Export ("colorTransition", ArgumentSemantic.Assign)]
	MLNTransition ColorTransition { get; set; }

	// @property (nonatomic) NSExpression * _Nonnull intensity;
	[Export ("intensity", ArgumentSemantic.Assign)]
	NSExpression Intensity { get; set; }

	// @property (nonatomic) MLNTransition intensityTransition;
	[Export ("intensityTransition", ArgumentSemantic.Assign)]
	MLNTransition IntensityTransition { get; set; }
}

// @interface MLNLineStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNLineStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineCap;
	[NullAllowed, Export ("lineCap", ArgumentSemantic.Assign)]
	NSExpression LineCap { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineJoin;
	[NullAllowed, Export ("lineJoin", ArgumentSemantic.Assign)]
	NSExpression LineJoin { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineMiterLimit;
	[NullAllowed, Export ("lineMiterLimit", ArgumentSemantic.Assign)]
	NSExpression LineMiterLimit { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineRoundLimit;
	[NullAllowed, Export ("lineRoundLimit", ArgumentSemantic.Assign)]
	NSExpression LineRoundLimit { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineSortKey;
	[NullAllowed, Export ("lineSortKey", ArgumentSemantic.Assign)]
	NSExpression LineSortKey { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineBlur;
	[NullAllowed, Export ("lineBlur", ArgumentSemantic.Assign)]
	NSExpression LineBlur { get; set; }

	// @property (nonatomic) MLNTransition lineBlurTransition;
	[Export ("lineBlurTransition", ArgumentSemantic.Assign)]
	MLNTransition LineBlurTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineColor;
	[NullAllowed, Export ("lineColor", ArgumentSemantic.Assign)]
	NSExpression LineColor { get; set; }

	// @property (nonatomic) MLNTransition lineColorTransition;
	[Export ("lineColorTransition", ArgumentSemantic.Assign)]
	MLNTransition LineColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineDashPattern;
	[NullAllowed, Export ("lineDashPattern", ArgumentSemantic.Assign)]
	NSExpression LineDashPattern { get; set; }

	// @property (nonatomic) MLNTransition lineDashPatternTransition;
	[Export ("lineDashPatternTransition", ArgumentSemantic.Assign)]
	MLNTransition LineDashPatternTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineGapWidth;
	[NullAllowed, Export ("lineGapWidth", ArgumentSemantic.Assign)]
	NSExpression LineGapWidth { get; set; }

	// @property (nonatomic) MLNTransition lineGapWidthTransition;
	[Export ("lineGapWidthTransition", ArgumentSemantic.Assign)]
	MLNTransition LineGapWidthTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineGradient;
	[NullAllowed, Export ("lineGradient", ArgumentSemantic.Assign)]
	NSExpression LineGradient { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineOffset;
	[NullAllowed, Export ("lineOffset", ArgumentSemantic.Assign)]
	NSExpression LineOffset { get; set; }

	// @property (nonatomic) MLNTransition lineOffsetTransition;
	[Export ("lineOffsetTransition", ArgumentSemantic.Assign)]
	MLNTransition LineOffsetTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineOpacity;
	[NullAllowed, Export ("lineOpacity", ArgumentSemantic.Assign)]
	NSExpression LineOpacity { get; set; }

	// @property (nonatomic) MLNTransition lineOpacityTransition;
	[Export ("lineOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition LineOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified linePattern;
	[NullAllowed, Export ("linePattern", ArgumentSemantic.Assign)]
	NSExpression LinePattern { get; set; }

	// @property (nonatomic) MLNTransition linePatternTransition;
	[Export ("linePatternTransition", ArgumentSemantic.Assign)]
	MLNTransition LinePatternTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineTranslation;
	[NullAllowed, Export ("lineTranslation", ArgumentSemantic.Assign)]
	NSExpression LineTranslation { get; set; }

	// @property (nonatomic) MLNTransition lineTranslationTransition;
	[Export ("lineTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition LineTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineTranslationAnchor;
	[NullAllowed, Export ("lineTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression LineTranslationAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified lineWidth;
	[NullAllowed, Export ("lineWidth", ArgumentSemantic.Assign)]
	NSExpression LineWidth { get; set; }

	// @property (nonatomic) MLNTransition lineWidthTransition;
	[Export ("lineWidthTransition", ArgumentSemantic.Assign)]
	MLNTransition LineWidthTransition { get; set; }
}

// @interface MLNLineStyleLayerAdditions (NSValue)
// [Category]
// [BaseType (typeof(NSValue))]
// interface NSValue_MLNLineStyleLayerAdditions
// {
// 	[Static]
// 	[Export ("valueWithMLNLineCap:")]
// 	NSValue ValueWithMLNLineCap (MLNLineCap lineCap);
// 	[Export ("MLNLineCapValue")]
// 	MLNLineCap MLNLineCapValue { get; }
// 	[Static]
// 	[Export ("valueWithMLNLineJoin:")]
// 	NSValue ValueWithMLNLineJoin (MLNLineJoin lineJoin);
// 	[Export ("MLNLineJoinValue")]
// 	MLNLineJoin MLNLineJoinValue { get; }
// 	[Static]
// 	[Export ("valueWithMLNLineTranslationAnchor:")]
// 	NSValue ValueWithMLNLineTranslationAnchor (MLNLineTranslationAnchor lineTranslationAnchor);
// 	[Export ("MLNLineTranslationAnchorValue")]
// 	MLNLineTranslationAnchor MLNLineTranslationAnchorValue { get; }
// }

// @protocol MLNLocationManager <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface MLNLocationManager
{
	// @optional -(CLLocationDistance)distanceFilter;
	// @optional -(void)setDistanceFilter:(CLLocationDistance)distanceFilter;
	[Export ("distanceFilter")]
	// [Verify (MethodToProperty)]
	double DistanceFilter { get; set; }

	// @optional -(CLLocationAccuracy)desiredAccuracy;
	[Export ("desiredAccuracy")]
	double DesiredAccuracy ();

	// @optional -(void)setDesiredAccuracy:(CLLocationAccuracy)desiredAccuracy;
	[Export ("setDesiredAccuracy:")]
	void SetDesiredAccuracy (double desiredAccuracy);

	// @optional -(CLAccuracyAuthorization)accuracyAuthorization __attribute__((availability(ios, introduced=14)));
	// [iOS (14,0)]
	[Export ("accuracyAuthorization")]
	CLAccuracyAuthorization AccuracyAuthorization ();

	// @optional -(CLActivityType)activityType;
	[Export ("activityType")]
	CLActivityType ActivityType ();

	// @optional -(void)setActivityType:(CLActivityType)activityType;
	[Export ("setActivityType:")]
	void SetActivityType (CLActivityType activityType);

	// @optional -(void)requestTemporaryFullAccuracyAuthorizationWithPurposeKey:(NSString * _Nonnull)purposeKey __attribute__((availability(ios, introduced=14)));
	// [iOS (14,0)]
	[Export ("requestTemporaryFullAccuracyAuthorizationWithPurposeKey:")]
	void RequestTemporaryFullAccuracyAuthorizationWithPurposeKey (string purposeKey);

	[Wrap ("WeakDelegate"), Abstract]
	[NullAllowed]
	MLNLocationManagerDelegate Delegate { get; set; }

	// @required @property (nonatomic, weak) id<MLNLocationManagerDelegate> _Nullable delegate;
	[Abstract]
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @required @property (readonly, nonatomic) CLAuthorizationStatus authorizationStatus;
	[Abstract]
	[Export ("authorizationStatus")]
	CLAuthorizationStatus AuthorizationStatus { get; }

	// @required -(void)requestAlwaysAuthorization;
	[Abstract]
	[Export ("requestAlwaysAuthorization")]
	void RequestAlwaysAuthorization ();

	// @required -(void)requestWhenInUseAuthorization;
	[Abstract]
	[Export ("requestWhenInUseAuthorization")]
	void RequestWhenInUseAuthorization ();

	// @required -(void)startUpdatingLocation;
	[Abstract]
	[Export ("startUpdatingLocation")]
	void StartUpdatingLocation ();

	// @required -(void)stopUpdatingLocation;
	[Abstract]
	[Export ("stopUpdatingLocation")]
	void StopUpdatingLocation ();

	// @required @property (nonatomic) CLDeviceOrientation headingOrientation;
	[Abstract]
	[Export ("headingOrientation", ArgumentSemantic.Assign)]
	CLDeviceOrientation HeadingOrientation { get; set; }

	// @required -(void)startUpdatingHeading;
	[Abstract]
	[Export ("startUpdatingHeading")]
	void StartUpdatingHeading ();

	// @required -(void)stopUpdatingHeading;
	[Abstract]
	[Export ("stopUpdatingHeading")]
	void StopUpdatingHeading ();

	// @required -(void)dismissHeadingCalibrationDisplay;
	[Abstract]
	[Export ("dismissHeadingCalibrationDisplay")]
	void DismissHeadingCalibrationDisplay ();
}

// @protocol MLNLocationManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNLocationManagerDelegate
{
	// @required -(void)locationManager:(id<MLNLocationManager> _Nonnull)manager didUpdateLocations:(NSArray<CLLocation *> * _Nonnull)locations;
	[Abstract]
	[Export ("locationManager:didUpdateLocations:")]
	void LocationManager (MLNLocationManager manager, CLLocation[] locations);

	// @required -(void)locationManager:(id<MLNLocationManager> _Nonnull)manager didUpdateHeading:(CLHeading * _Nonnull)newHeading;
	[Abstract]
	[Export ("locationManager:didUpdateHeading:")]
	void LocationManager (MLNLocationManager manager, CLHeading newHeading);

	// @required -(BOOL)locationManagerShouldDisplayHeadingCalibration:(id<MLNLocationManager> _Nonnull)manager;
	[Abstract]
	[Export ("locationManagerShouldDisplayHeadingCalibration:")]
	bool LocationManagerShouldDisplayHeadingCalibration (MLNLocationManager manager);

	// @required -(void)locationManager:(id<MLNLocationManager> _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
	[Abstract]
	[Export ("locationManager:didFailWithError:")]
	void LocationManager (MLNLocationManager manager, NSError error);

	// @required -(void)locationManagerDidChangeAuthorization:(id<MLNLocationManager> _Nonnull)manager;
	[Abstract]
	[Export ("locationManagerDidChangeAuthorization:")]
	void LocationManagerDidChangeAuthorization (MLNLocationManager manager);
}

// typedef void (^MLNLoggingBlockHandler)(MLNLoggingLevel, NSString * _Nonnull, NSUInteger, NSString * _Nonnull);
delegate void MLNLoggingBlockHandler (MLNLoggingLevel arg0, string arg1, nuint arg2, string arg3);

// @interface MLNLoggingConfiguration : NSObject
[BaseType (typeof(NSObject))]
interface MLNLoggingConfiguration
{
	// @property (copy, nonatomic, null_resettable) MLNLoggingBlockHandler _Null_unspecified handler;
	[NullAllowed, Export ("handler", ArgumentSemantic.Copy)]
	MLNLoggingBlockHandler Handler { get; set; }

	// @property (assign, nonatomic) MLNLoggingLevel loggingLevel;
	[Export ("loggingLevel", ArgumentSemantic.Assign)]
	MLNLoggingLevel LoggingLevel { get; set; }

	// @property (readonly, nonatomic, class) MLNLoggingConfiguration * _Nonnull sharedConfiguration;
	[Static]
	[Export ("sharedConfiguration")]
	MLNLoggingConfiguration SharedConfiguration { get; }
}

// @interface MLNMapCamera : NSObject <NSSecureCoding, NSCopying>
[BaseType (typeof(NSObject))]
interface MLNMapCamera : INSSecureCoding, INSCopying
{
	// @property (nonatomic) CLLocationCoordinate2D centerCoordinate;
	[Export ("centerCoordinate", ArgumentSemantic.Assign)]
	CLLocationCoordinate2D CenterCoordinate { get; set; }

	// @property (nonatomic) CLLocationDirection heading;
	[Export ("heading")]
	double Heading { get; set; }

	// @property (nonatomic) CGFloat pitch;
	[Export ("pitch")]
	nfloat Pitch { get; set; }

	// @property (nonatomic) CLLocationDistance altitude;
	[Export ("altitude")]
	double Altitude { get; set; }

	// @property (nonatomic) CLLocationDistance viewingDistance;
	[Export ("viewingDistance")]
	double ViewingDistance { get; set; }

	// +(instancetype _Nonnull)camera;
	[Static]
	[Export ("camera")]
	MLNMapCamera Camera ();

	// +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate fromEyeCoordinate:(CLLocationCoordinate2D)eyeCoordinate eyeAltitude:(CLLocationDistance)eyeAltitude;
	[Static]
	[Export ("cameraLookingAtCenterCoordinate:fromEyeCoordinate:eyeAltitude:")]
	MLNMapCamera CameraLookingAtCenterCoordinate (CLLocationCoordinate2D centerCoordinate, CLLocationCoordinate2D eyeCoordinate, double eyeAltitude);

	// +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate acrossDistance:(CLLocationDistance)distance pitch:(CGFloat)pitch heading:(CLLocationDirection)heading;
	[Static]
	[Export ("cameraLookingAtCenterCoordinate:acrossDistance:pitch:heading:")]
	MLNMapCamera CameraLookingAtCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double distance, nfloat pitch, double heading);

	// +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate altitude:(CLLocationDistance)altitude pitch:(CGFloat)pitch heading:(CLLocationDirection)heading;
	[Static]
	[Export ("cameraLookingAtCenterCoordinate:altitude:pitch:heading:")]
	MLNMapCamera CameraLookingAtCenterCoordinateAltitudePitchHeading (CLLocationCoordinate2D centerCoordinate, double altitude, nfloat pitch, double heading);

	// +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate fromDistance:(CLLocationDistance)distance pitch:(CGFloat)pitch heading:(CLLocationDirection)heading __attribute__((deprecated("Use -cameraLookingAtCenterCoordinate:acrossDistance:pitch:heading: or -cameraLookingAtCenterCoordinate:altitude:pitch:heading:.")));
	[Static]
	[Export ("cameraLookingAtCenterCoordinate:fromDistance:pitch:heading:")]
	MLNMapCamera CameraLookingAtCenterCoordinateFromDistancePitchHeading (CLLocationCoordinate2D centerCoordinate, double distance, nfloat pitch, double heading);

	// -(BOOL)isEqualToMapCamera:(MLNMapCamera * _Nonnull)otherCamera;
	[Export ("isEqualToMapCamera:")]
	bool IsEqualToMapCamera (MLNMapCamera otherCamera);
}

// @interface MLNMapOptions : NSObject
[BaseType (typeof(NSObject))]
interface MLNMapOptions
{
	// @property (nonatomic) NSUrl * _Nullable styleURL;
	[NullAllowed, Export ("styleURL", ArgumentSemantic.Assign)]
	NSUrl StyleURL { get; set; }

	// @property (nonatomic) NSString * _Nullable styleJSON;
	[NullAllowed, Export ("styleJSON")]
	string StyleJSON { get; set; }

	// @property (nonatomic) MLNActionJournalOptions * _Nonnull actionJournalOptions;
	[Export ("actionJournalOptions", ArgumentSemantic.Assign)]
	MLNActionJournalOptions ActionJournalOptions { get; set; }

	// @property NSArray * _Nonnull pluginLayers;
	[Export ("pluginLayers", ArgumentSemantic.Assign)]
	// [Verify (StronglyTypedNSArray)]
	NSObject[] PluginLayers { get; set; }
}

// @interface MLNTileServerOptions : NSObject
[BaseType (typeof(NSObject))]
interface MLNTileServerOptions
{
	// @property (retain, nonatomic) NSString * _Nonnull baseURL;
	[Export ("baseURL", ArgumentSemantic.Retain)]
	string BaseURL { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull uriSchemeAlias;
	[Export ("uriSchemeAlias", ArgumentSemantic.Retain)]
	string UriSchemeAlias { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull sourceTemplate;
	[Export ("sourceTemplate", ArgumentSemantic.Retain)]
	string SourceTemplate { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull sourceDomainName;
	[Export ("sourceDomainName", ArgumentSemantic.Retain)]
	string SourceDomainName { get; set; }

	// @property (retain, nonatomic) NSString * _Nullable sourceVersionPrefix;
	[NullAllowed, Export ("sourceVersionPrefix", ArgumentSemantic.Retain)]
	string SourceVersionPrefix { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull styleTemplate;
	[Export ("styleTemplate", ArgumentSemantic.Retain)]
	string StyleTemplate { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull styleDomainName;
	[Export ("styleDomainName", ArgumentSemantic.Retain)]
	string StyleDomainName { get; set; }

	// @property (retain, nonatomic) NSString * _Nullable styleVersionPrefix;
	[NullAllowed, Export ("styleVersionPrefix", ArgumentSemantic.Retain)]
	string StyleVersionPrefix { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull spritesTemplate;
	[Export ("spritesTemplate", ArgumentSemantic.Retain)]
	string SpritesTemplate { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull spritesDomainName;
	[Export ("spritesDomainName", ArgumentSemantic.Retain)]
	string SpritesDomainName { get; set; }

	// @property (retain, nonatomic) NSString * _Nullable spritesVersionPrefix;
	[NullAllowed, Export ("spritesVersionPrefix", ArgumentSemantic.Retain)]
	string SpritesVersionPrefix { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull glyphsTemplate;
	[Export ("glyphsTemplate", ArgumentSemantic.Retain)]
	string GlyphsTemplate { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull glyphsDomainName;
	[Export ("glyphsDomainName", ArgumentSemantic.Retain)]
	string GlyphsDomainName { get; set; }

	// @property (retain, nonatomic) NSString * _Nullable glyphsVersionPrefix;
	[NullAllowed, Export ("glyphsVersionPrefix", ArgumentSemantic.Retain)]
	string GlyphsVersionPrefix { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull tileTemplate;
	[Export ("tileTemplate", ArgumentSemantic.Retain)]
	string TileTemplate { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull tileDomainName;
	[Export ("tileDomainName", ArgumentSemantic.Retain)]
	string TileDomainName { get; set; }

	// @property (retain, nonatomic) NSString * _Nullable tileVersionPrefix;
	[NullAllowed, Export ("tileVersionPrefix", ArgumentSemantic.Retain)]
	string TileVersionPrefix { get; set; }

	// @property (retain, nonatomic) NSString * _Nonnull apiKeyParameterName;
	[Export ("apiKeyParameterName", ArgumentSemantic.Retain)]
	string ApiKeyParameterName { get; set; }

	// @property (retain, nonatomic) NSArray<MLNDefaultStyle *> * _Nonnull defaultStyles;
	[Export ("defaultStyles", ArgumentSemantic.Retain)]
	MLNDefaultStyle[] DefaultStyles { get; set; }

	// @property (retain, nonatomic) MLNDefaultStyle * _Nonnull defaultStyle;
	[Export ("defaultStyle", ArgumentSemantic.Retain)]
	MLNDefaultStyle DefaultStyle { get; set; }
}

// @interface MLNSettings : NSObject
[BaseType (typeof(NSObject))]
interface MLNSettings
{
	// @property (copy, class) MLNTileServerOptions * _Nullable tileServerOptions;
	[Static]
	[NullAllowed, Export ("tileServerOptions", ArgumentSemantic.Copy)]
	MLNTileServerOptions TileServerOptions { get; set; }

	// @property (copy, class) NSString * _Nullable apiKey;
	[Static]
	[NullAllowed, Export ("apiKey")]
	string ApiKey { get; set; }

	// +(void)useWellKnownTileServer:(MLNWellKnownTileServer)tileServer;
	[Static]
	[Export ("useWellKnownTileServer:")]
	void UseWellKnownTileServer (MLNWellKnownTileServer tileServer);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExceptionName _Nonnull MLNInvalidStyleURLException __attribute__((visibility("default")));
	[Field ("MLNInvalidStyleURLException", "__Internal")]
	NSString MLNInvalidStyleURLException { get; }

	// extern const MLNExceptionName _Nonnull MLNRedundantLayerException __attribute__((visibility("default")));
	[Field ("MLNRedundantLayerException", "__Internal")]
	NSString MLNRedundantLayerException { get; }

	// extern const MLNExceptionName _Nonnull MLNRedundantLayerIdentifierException __attribute__((visibility("default")));
	[Field ("MLNRedundantLayerIdentifierException", "__Internal")]
	NSString MLNRedundantLayerIdentifierException { get; }

	// extern const MLNExceptionName _Nonnull MLNRedundantSourceException __attribute__((visibility("default")));
	[Field ("MLNRedundantSourceException", "__Internal")]
	NSString MLNRedundantSourceException { get; }

	// extern const MLNExceptionName _Nonnull MLNRedundantSourceIdentifierException __attribute__((visibility("default")));
	[Field ("MLNRedundantSourceIdentifierException", "__Internal")]
	NSString MLNRedundantSourceIdentifierException { get; }
}

// @interface MLNStyle : NSObject
[BaseType (typeof(NSObject))]
interface MLNStyle
{
	// +(NSArray<MLNDefaultStyle *> * _Nonnull)predefinedStyles;
	[Static]
	[Export ("predefinedStyles")]
	// [Verify (MethodToProperty)]
	MLNDefaultStyle[] PredefinedStyles { get; }

	// +(MLNDefaultStyle * _Nonnull)defaultStyle;
	[Static]
	[Export ("defaultStyle")]
	// [Verify (MethodToProperty)]
	MLNDefaultStyle DefaultStyle { get; }

	// +(NSUrl * _Nullable)defaultStyleURL;
	[Static]
	[NullAllowed, Export ("defaultStyleURL")]
	// [Verify (MethodToProperty)]
	NSUrl DefaultStyleURL { get; }

	// +(MLNDefaultStyle * _Nullable)predefinedStyle:(NSString * _Nonnull)withStyleName;
	[Static]
	[Export ("predefinedStyle:")]
	[return: NullAllowed]
	MLNDefaultStyle PredefinedStyle (string withStyleName);

	// @property (readonly, copy) NSString * _Nullable name;
	[NullAllowed, Export ("name")]
	string Name { get; }

	// @property (copy, nonatomic) NSString * _Nonnull styleJSON;
	[Export ("styleJSON")]
	string StyleJSON { get; set; }

	// @property (nonatomic, strong) NSSet<__kindof MLNSource *> * _Nonnull sources;
	[Export ("sources", ArgumentSemantic.Strong)]
	// NSSet<MLNSource> Sources { get; set; }
	NSSet Sources { get; set; }

	// @property (nonatomic) MLNTransition transition;
	[Export ("transition", ArgumentSemantic.Assign)]
	MLNTransition Transition { get; set; }

	// @property (assign, nonatomic) BOOL performsPlacementTransitions;
	[Export ("performsPlacementTransitions")]
	bool PerformsPlacementTransitions { get; set; }

	// -(MLNSource * _Nullable)sourceWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("sourceWithIdentifier:")]
	[return: NullAllowed]
	MLNSource SourceWithIdentifier (string identifier);

	// -(void)addSource:(MLNSource * _Nonnull)source;
	[Export ("addSource:")]
	void AddSource (MLNSource source);

	// -(void)removeSource:(MLNSource * _Nonnull)source;
	[Export ("removeSource:")]
	void RemoveSource (MLNSource source);

	// -(BOOL)removeSource:(MLNSource * _Nonnull)source error:(NSError * _Nullable * _Nullable)outError;
	[Export ("removeSource:error:")]
	bool RemoveSource (MLNSource source, [NullAllowed] out NSError outError);

	// @property (nonatomic, strong) NSArray<__kindof MLNStyleLayer *> * _Nonnull layers;
	[Export ("layers", ArgumentSemantic.Strong)]
	MLNStyleLayer[] Layers { get; set; }

	// -(MLNStyleLayer * _Nullable)layerWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("layerWithIdentifier:")]
	[return: NullAllowed]
	MLNStyleLayer LayerWithIdentifier (string identifier);

	// -(void)addLayer:(MLNStyleLayer * _Nonnull)layer;
	[Export ("addLayer:")]
	void AddLayer (MLNStyleLayer layer);

	// -(void)insertLayer:(MLNStyleLayer * _Nonnull)layer atIndex:(NSUInteger)index;
	[Export ("insertLayer:atIndex:")]
	void InsertLayer (MLNStyleLayer layer, nuint index);

	// -(void)insertLayer:(MLNStyleLayer * _Nonnull)layer belowLayer:(MLNStyleLayer * _Nonnull)sibling;
	[Export ("insertLayer:belowLayer:")]
	void InsertLayer (MLNStyleLayer layer, MLNStyleLayer sibling);

	// -(void)insertLayer:(MLNStyleLayer * _Nonnull)layer aboveLayer:(MLNStyleLayer * _Nonnull)sibling;
	[Export ("insertLayer:aboveLayer:")]
	void InsertLayerAboveLayer (MLNStyleLayer layer, MLNStyleLayer sibling);

	// -(void)removeLayer:(MLNStyleLayer * _Nonnull)layer;
	[Export ("removeLayer:")]
	void RemoveLayer (MLNStyleLayer layer);

	// -(UIImage * _Nullable)imageForName:(NSString * _Nonnull)name;
	[Export ("imageForName:")]
	[return: NullAllowed]
	UIImage ImageForName (string name);

	// -(void)setImage:(UIImage * _Nonnull)image forName:(NSString * _Nonnull)name;
	[Export ("setImage:forName:")]
	void SetImage (UIImage image, string name);

	// -(void)removeImageForName:(NSString * _Nonnull)name;
	[Export ("removeImageForName:")]
	void RemoveImageForName (string name);

	// @property (nonatomic, strong) MLNLight * _Nonnull light;
	[Export ("light", ArgumentSemantic.Strong)]
	MLNLight Light { get; set; }

	// -(void)localizeLabelsIntoLocale:(NSLocale * _Nullable)locale;
	[Export ("localizeLabelsIntoLocale:")]
	void LocalizeLabelsIntoLocale ([NullAllowed] NSLocale locale);
}

// @protocol MLNStylable <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface MLNStylable
{
	// @required @property (readonly, nonatomic) MLNStyle * _Nullable style;
	[Abstract]
	[NullAllowed, Export ("style")]
	MLNStyle Style { get; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNMapViewDecelerationRate MLNMapViewDecelerationRateNormal __attribute__((visibility("default")));
	[Field ("MLNMapViewDecelerationRateNormal", "__Internal")]
	double MLNMapViewDecelerationRateNormal { get; }

	// extern const MLNMapViewDecelerationRate MLNMapViewDecelerationRateFast __attribute__((visibility("default")));
	[Field ("MLNMapViewDecelerationRateFast", "__Internal")]
	double MLNMapViewDecelerationRateFast { get; }

	// extern const MLNMapViewDecelerationRate MLNMapViewDecelerationRateImmediate __attribute__((visibility("default")));
	[Field ("MLNMapViewDecelerationRateImmediate", "__Internal")]
	double MLNMapViewDecelerationRateImmediate { get; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNMapViewPreferredFramesPerSecond MLNMapViewPreferredFramesPerSecondDefault __attribute__((visibility("default")));
	[Field ("MLNMapViewPreferredFramesPerSecondDefault", "__Internal")]
	nint MLNMapViewPreferredFramesPerSecondDefault { get; }

	// extern const MLNMapViewPreferredFramesPerSecond MLNMapViewPreferredFramesPerSecondLowPower __attribute__((visibility("default")));
	[Field ("MLNMapViewPreferredFramesPerSecondLowPower", "__Internal")]
	nint MLNMapViewPreferredFramesPerSecondLowPower { get; }

	// extern const MLNMapViewPreferredFramesPerSecond MLNMapViewPreferredFramesPerSecondMaximum __attribute__((visibility("default")));
	[Field ("MLNMapViewPreferredFramesPerSecondMaximum", "__Internal")]
	nint MLNMapViewPreferredFramesPerSecondMaximum { get; }

	// extern const MLNExceptionName _Nonnull MLNMissingLocationServicesUsageDescriptionException __attribute__((visibility("default")));
	[Field ("MLNMissingLocationServicesUsageDescriptionException", "__Internal")]
	NSString MLNMissingLocationServicesUsageDescriptionException { get; }

	// extern const MLNExceptionName _Nonnull MLNUserLocationAnnotationTypeException __attribute__((visibility("default")));
	[Field ("MLNUserLocationAnnotationTypeException", "__Internal")]
	NSString MLNUserLocationAnnotationTypeException { get; }
}

// @interface MLNMapView : UIView <MLNStylable>
[BaseType (typeof(UIView))]
interface MLNMapView : IMLNStylable
{
	// -(instancetype _Nonnull)initWithFrame:(CGRect)frame;
	[Export ("initWithFrame:")]
	NativeHandle Constructor (CGRect frame);

	// -(instancetype _Nonnull)initWithFrame:(CGRect)frame styleURL:(NSUrl * _Nullable)styleURL;
	[Export ("initWithFrame:styleURL:")]
	NativeHandle Constructor (CGRect frame, [NullAllowed] NSUrl styleURL);

	// -(instancetype _Nonnull)initWithFrame:(CGRect)frame styleJSON:(NSString * _Nonnull)styleJSON;
	[Export ("initWithFrame:styleJSON:")]
	NativeHandle Constructor (CGRect frame, string styleJSON);

	// -(instancetype _Nonnull)initWithFrame:(CGRect)frame options:(MLNMapOptions * _Nonnull)options;
	[Export ("initWithFrame:options:")]
	NativeHandle Constructor (CGRect frame, MLNMapOptions options);

	[Wrap ("WeakDelegate")]
	[NullAllowed]
	MLNMapViewDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<MLNMapViewDelegate> _Nullable delegate __attribute__((iboutlet));
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic) MLNStyle * _Nullable style;
	[NullAllowed, Export ("style")]
	MLNStyle Style { get; }

	// @property (nonatomic, null_resettable) NSUrl * _Null_unspecified styleURL;
	[NullAllowed, Export ("styleURL", ArgumentSemantic.Assign)]
	NSUrl StyleURL { get; set; }

	// @property (copy, nonatomic) NSString * _Nonnull styleJSON;
	[Export ("styleJSON")]
	string StyleJSON { get; set; }

	// -(void)reloadStyle:(id _Nullable)sender __attribute__((ibaction));
	[Export ("reloadStyle:")]
	void ReloadStyle ([NullAllowed] NSObject sender);

	// @property (assign) BOOL automaticallyAdjustsContentInset;
	[Export ("automaticallyAdjustsContentInset")]
	bool AutomaticallyAdjustsContentInset { get; set; }

	// @property (assign, nonatomic) BOOL showsScale;
	[Export ("showsScale")]
	bool ShowsScale { get; set; }

	// @property (readonly, nonatomic) MLNScaleBar * _Nonnull scaleBar;
	// [Export ("scaleBar")]
	// MLNScaleBar ScaleBar { get; }

	// @property (assign, nonatomic) BOOL scaleBarShouldShowDarkStyles;
	[Export ("scaleBarShouldShowDarkStyles")]
	bool ScaleBarShouldShowDarkStyles { get; set; }

	// @property (assign, nonatomic) BOOL scaleBarUsesMetricSystem;
	[Export ("scaleBarUsesMetricSystem")]
	bool ScaleBarUsesMetricSystem { get; set; }

	// @property (assign, nonatomic) MLNOrnamentPosition scaleBarPosition;
	[Export ("scaleBarPosition", ArgumentSemantic.Assign)]
	MLNOrnamentPosition ScaleBarPosition { get; set; }

	// @property (assign, nonatomic) CGPoint scaleBarMargins;
	[Export ("scaleBarMargins", ArgumentSemantic.Assign)]
	CGPoint ScaleBarMargins { get; set; }

	// @property (assign, nonatomic) BOOL showsCompassView;
	[Export ("showsCompassView")]
	bool ShowsCompassView { get; set; }

	// @property (readonly, nonatomic) MLNCompassButton * _Nonnull compassView;
	[Export ("compassView")]
	MLNCompassButton CompassView { get; }

	// @property (assign, nonatomic) MLNOrnamentPosition compassViewPosition;
	[Export ("compassViewPosition", ArgumentSemantic.Assign)]
	MLNOrnamentPosition CompassViewPosition { get; set; }

	// @property (assign, nonatomic) CGPoint compassViewMargins;
	[Export ("compassViewMargins", ArgumentSemantic.Assign)]
	CGPoint CompassViewMargins { get; set; }

	// @property (assign, nonatomic) BOOL showsLogoView;
	[Export ("showsLogoView")]
	bool ShowsLogoView { get; set; }

	// @property (readonly, nonatomic) UIImageView * _Nonnull logoView;
	[Export ("logoView")]
	UIImageView LogoView { get; }

	// @property (assign, nonatomic) MLNOrnamentPosition logoViewPosition;
	[Export ("logoViewPosition", ArgumentSemantic.Assign)]
	MLNOrnamentPosition LogoViewPosition { get; set; }

	// @property (assign, nonatomic) CGPoint logoViewMargins;
	[Export ("logoViewMargins", ArgumentSemantic.Assign)]
	CGPoint LogoViewMargins { get; set; }

	// @property (assign, nonatomic) BOOL showsAttributionButton;
	[Export ("showsAttributionButton")]
	bool ShowsAttributionButton { get; set; }

	// @property (readonly, nonatomic) UIButton * _Nonnull attributionButton;
	[Export ("attributionButton")]
	UIButton AttributionButton { get; }

	// @property (assign, nonatomic) MLNOrnamentPosition attributionButtonPosition;
	[Export ("attributionButtonPosition", ArgumentSemantic.Assign)]
	MLNOrnamentPosition AttributionButtonPosition { get; set; }

	// @property (assign, nonatomic) CGPoint attributionButtonMargins;
	[Export ("attributionButtonMargins", ArgumentSemantic.Assign)]
	CGPoint AttributionButtonMargins { get; set; }

	// -(void)showAttribution:(id _Nonnull)sender __attribute__((ibaction));
	[Export ("showAttribution:")]
	void ShowAttribution (NSObject sender);

	// @property (assign, nonatomic) MLNMapViewPreferredFramesPerSecond preferredFramesPerSecond;
	[Export ("preferredFramesPerSecond")]
	nint PreferredFramesPerSecond { get; set; }

	// @property (assign, nonatomic) BOOL prefetchesTiles;
	[Export ("prefetchesTiles")]
	bool PrefetchesTiles { get; set; }

	// @property (assign, nonatomic) BOOL tileCacheEnabled;
	[Export ("tileCacheEnabled")]
	bool TileCacheEnabled { get; set; }

	// @property (assign, nonatomic) double tileLodMinRadius;
	[Export ("tileLodMinRadius")]
	double TileLodMinRadius { get; set; }

	// @property (assign, nonatomic) double tileLodScale;
	[Export ("tileLodScale")]
	double TileLodScale { get; set; }

	// @property (assign, nonatomic) double tileLodPitchThreshold;
	[Export ("tileLodPitchThreshold")]
	double TileLodPitchThreshold { get; set; }

	// @property (assign, nonatomic) double tileLodZoomShift;
	[Export ("tileLodZoomShift")]
	double TileLodZoomShift { get; set; }

	// @property (assign, nonatomic) UIEdgeInsets frustumOffset;
	[Export ("frustumOffset", ArgumentSemantic.Assign)]
	UIEdgeInsets FrustumOffset { get; set; }

	// -(void)disableLocationManager;
	[Export ("disableLocationManager")]
	void DisableLocationManager ();

	// @property (nonatomic, null_resettable) id<MLNLocationManager> _Null_unspecified locationManager;
	[NullAllowed, Export ("locationManager", ArgumentSemantic.Assign)]
	MLNLocationManager LocationManager { get; set; }

	// @property (assign, nonatomic) BOOL showsUserLocation;
	[Export ("showsUserLocation")]
	bool ShowsUserLocation { get; set; }

	// @property (assign, nonatomic) BOOL dynamicNavigationCameraAnimationDuration;
	[Export ("dynamicNavigationCameraAnimationDuration")]
	bool DynamicNavigationCameraAnimationDuration { get; set; }

	// @property (assign, nonatomic) BOOL shouldRequestAuthorizationToUseLocationServices;
	[Export ("shouldRequestAuthorizationToUseLocationServices")]
	bool ShouldRequestAuthorizationToUseLocationServices { get; set; }

	// @property (readonly, getter = isUserLocationVisible, assign, nonatomic) BOOL userLocationVisible;
	[Export ("userLocationVisible")]
	bool UserLocationVisible { [Bind ("isUserLocationVisible")] get; }

	// @property (readonly, nonatomic) MLNUserLocation * _Nullable userLocation;
	[NullAllowed, Export ("userLocation")]
	MLNUserLocation UserLocation { get; }

	// @property (assign, nonatomic) MLNUserTrackingMode userTrackingMode;
	[Export ("userTrackingMode", ArgumentSemantic.Assign)]
	MLNUserTrackingMode UserTrackingMode { get; set; }

	// -(void)setUserTrackingMode:(MLNUserTrackingMode)mode animated:(BOOL)animated __attribute__((deprecated("Use `-setUserTrackingMode:animated:completionHandler:` instead.")));
	[Export ("setUserTrackingMode:animated:")]
	void SetUserTrackingMode (MLNUserTrackingMode mode, bool animated);

	// -(void)setUserTrackingMode:(MLNUserTrackingMode)mode animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setUserTrackingMode:animated:completionHandler:")]
	void SetUserTrackingMode (MLNUserTrackingMode mode, bool animated, [NullAllowed] Action completion);

	// @property (assign, nonatomic) MLNAnnotationVerticalAlignment userLocationVerticalAlignment __attribute__((deprecated("Use ``MLNMapViewDelegate/mapViewUserLocationAnchorPoint:`` instead.")));
	[Export ("userLocationVerticalAlignment", ArgumentSemantic.Assign)]
	MLNAnnotationVerticalAlignment UserLocationVerticalAlignment { get; set; }

	// -(void)setUserLocationVerticalAlignment:(MLNAnnotationVerticalAlignment)alignment animated:(BOOL)animated __attribute__((deprecated("Use ``MLNMapViewDelegate/mapViewUserLocationAnchorPoint:`` instead.")));
	[Export ("setUserLocationVerticalAlignment:animated:")]
	void SetUserLocationVerticalAlignment (MLNAnnotationVerticalAlignment alignment, bool animated);

	// -(void)updateUserLocationAnnotationView;
	[Export ("updateUserLocationAnnotationView")]
	void UpdateUserLocationAnnotationView ();

	// -(void)updateUserLocationAnnotationViewAnimatedWithDuration:(NSTimeInterval)duration;
	[Export ("updateUserLocationAnnotationViewAnimatedWithDuration:")]
	void UpdateUserLocationAnnotationViewAnimatedWithDuration (double duration);

	// @property (assign, nonatomic) BOOL showsUserHeadingIndicator;
	[Export ("showsUserHeadingIndicator")]
	bool ShowsUserHeadingIndicator { get; set; }

	// @property (assign, nonatomic) BOOL displayHeadingCalibration;
	[Export ("displayHeadingCalibration")]
	bool DisplayHeadingCalibration { get; set; }

	// @property (assign, nonatomic) CLLocationCoordinate2D targetCoordinate;
	[Export ("targetCoordinate", ArgumentSemantic.Assign)]
	CLLocationCoordinate2D TargetCoordinate { get; set; }

	// -(void)setTargetCoordinate:(CLLocationCoordinate2D)targetCoordinate animated:(BOOL)animated __attribute__((deprecated("Use `-setTargetCoordinate:animated:completionHandler:` instead.")));
	[Export ("setTargetCoordinate:animated:")]
	void SetTargetCoordinate (CLLocationCoordinate2D targetCoordinate, bool animated);

	// -(void)setTargetCoordinate:(CLLocationCoordinate2D)targetCoordinate animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setTargetCoordinate:animated:completionHandler:")]
	void SetTargetCoordinate (CLLocationCoordinate2D targetCoordinate, bool animated, [NullAllowed] Action completion);

	// @property (getter = isZoomEnabled, nonatomic) BOOL zoomEnabled;
	[Export ("zoomEnabled")]
	bool ZoomEnabled { [Bind ("isZoomEnabled")] get; set; }

	// @property (getter = isQuickZoomReversed, nonatomic) BOOL quickZoomReversed;
	[Export ("quickZoomReversed")]
	bool QuickZoomReversed { [Bind ("isQuickZoomReversed")] get; set; }

	// @property (getter = isScrollEnabled, nonatomic) BOOL scrollEnabled;
	[Export ("scrollEnabled")]
	bool ScrollEnabled { [Bind ("isScrollEnabled")] get; set; }

	// @property (assign, nonatomic) MLNPanScrollingMode panScrollingMode;
	[Export ("panScrollingMode", ArgumentSemantic.Assign)]
	MLNPanScrollingMode PanScrollingMode { get; set; }

	// @property (getter = isRotateEnabled, nonatomic) BOOL rotateEnabled;
	[Export ("rotateEnabled")]
	bool RotateEnabled { [Bind ("isRotateEnabled")] get; set; }

	// @property (nonatomic) CGFloat toleranceForSnappingToNorth;
	[Export ("toleranceForSnappingToNorth")]
	nfloat ToleranceForSnappingToNorth { get; set; }

	// @property (getter = isPitchEnabled, nonatomic) BOOL pitchEnabled;
	[Export ("pitchEnabled")]
	bool PitchEnabled { [Bind ("isPitchEnabled")] get; set; }

	// @property (nonatomic) BOOL anchorRotateOrZoomGesturesToCenterCoordinate;
	[Export ("anchorRotateOrZoomGesturesToCenterCoordinate")]
	bool AnchorRotateOrZoomGesturesToCenterCoordinate { get; set; }

	// @property (getter = isHapticFeedbackEnabled, nonatomic) BOOL hapticFeedbackEnabled;
	[Export ("hapticFeedbackEnabled")]
	bool HapticFeedbackEnabled { [Bind ("isHapticFeedbackEnabled")] get; set; }

	// @property (nonatomic) CGFloat decelerationRate;
	[Export ("decelerationRate")]
	nfloat DecelerationRate { get; set; }

	// @property (nonatomic) CLLocationCoordinate2D centerCoordinate;
	[Export ("centerCoordinate", ArgumentSemantic.Assign)]
	CLLocationCoordinate2D CenterCoordinate { get; set; }

	// -(void)setCenterCoordinate:(CLLocationCoordinate2D)coordinate animated:(BOOL)animated;
	[Export ("setCenterCoordinate:animated:")]
	void SetCenterCoordinate (CLLocationCoordinate2D coordinate, bool animated);

	// -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel animated:(BOOL)animated;
	[Export ("setCenterCoordinate:zoomLevel:animated:")]
	void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, bool animated);

	// -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated;
	[Export ("setCenterCoordinate:zoomLevel:direction:animated:")]
	void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated);

	// -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setCenterCoordinate:zoomLevel:direction:animated:completionHandler:")]
	void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated, [NullAllowed] Action completion);

	// @property (nonatomic) double zoomLevel;
	[Export ("zoomLevel")]
	double ZoomLevel { get; set; }

	// -(void)setZoomLevel:(double)zoomLevel animated:(BOOL)animated;
	[Export ("setZoomLevel:animated:")]
	void SetZoomLevel (double zoomLevel, bool animated);

	// @property (nonatomic) double minimumZoomLevel;
	[Export ("minimumZoomLevel")]
	double MinimumZoomLevel { get; set; }

	// @property (nonatomic) double maximumZoomLevel;
	[Export ("maximumZoomLevel")]
	double MaximumZoomLevel { get; set; }

	// @property (nonatomic) MLNCoordinateBounds maximumScreenBounds;
	[Export ("maximumScreenBounds", ArgumentSemantic.Assign)]
	MLNCoordinateBounds MaximumScreenBounds { get; set; }

	// @property (nonatomic) CLLocationDirection direction;
	[Export ("direction")]
	double Direction { get; set; }

	// -(void)setDirection:(CLLocationDirection)direction animated:(BOOL)animated;
	[Export ("setDirection:animated:")]
	void SetDirection (double direction, bool animated);

	// @property (nonatomic) CGFloat minimumPitch;
	[Export ("minimumPitch")]
	nfloat MinimumPitch { get; set; }

	// @property (nonatomic) CGFloat maximumPitch;
	[Export ("maximumPitch")]
	nfloat MaximumPitch { get; set; }

	// -(void)resetNorth __attribute__((ibaction));
	[Export ("resetNorth")]
	void ResetNorth ();

	// -(void)resetPosition __attribute__((ibaction));
	[Export ("resetPosition")]
	void ResetPosition ();

	// @property (nonatomic) MLNCoordinateBounds visibleCoordinateBounds;
	[Export ("visibleCoordinateBounds", ArgumentSemantic.Assign)]
	MLNCoordinateBounds VisibleCoordinateBounds { get; set; }

	// -(void)setVisibleCoordinateBounds:(MLNCoordinateBounds)bounds animated:(BOOL)animated;
	[Export ("setVisibleCoordinateBounds:animated:")]
	void SetVisibleCoordinateBounds (MLNCoordinateBounds bounds, bool animated);

	// -(void)setVisibleCoordinateBounds:(MLNCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated __attribute__((deprecated("Use `-setVisibleCoordinateBounds:edgePadding:animated:completionHandler:` instead.")));
	[Export ("setVisibleCoordinateBounds:edgePadding:animated:")]
	void SetVisibleCoordinateBounds (MLNCoordinateBounds bounds, UIEdgeInsets insets, bool animated);

	// -(void)setVisibleCoordinateBounds:(MLNCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setVisibleCoordinateBounds:edgePadding:animated:completionHandler:")]
	void SetVisibleCoordinateBounds (MLNCoordinateBounds bounds, UIEdgeInsets insets, bool animated, [NullAllowed] Action completion);

	// -(void)setVisibleCoordinates:(const CLLocationCoordinate2D * _Nonnull)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
	[Export ("setVisibleCoordinates:count:edgePadding:animated:")]
	unsafe void SetVisibleCoordinates (CLLocationCoordinate2D* coordinates, nuint count, UIEdgeInsets insets, bool animated);

	// -(void)setVisibleCoordinates:(const CLLocationCoordinate2D * _Nonnull)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets direction:(CLLocationDirection)direction duration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setVisibleCoordinates:count:edgePadding:direction:duration:animationTimingFunction:completionHandler:")]
	unsafe void SetVisibleCoordinates (CLLocationCoordinate2D* coordinates, nuint count, UIEdgeInsets insets, double direction, double duration, [NullAllowed] CAMediaTimingFunction function, [NullAllowed] Action completion);

	// -(void)showAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations animated:(BOOL)animated;
	[Export ("showAnnotations:animated:")]
	void ShowAnnotations (MLNAnnotation[] annotations, bool animated);

	// -(void)showAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated __attribute__((deprecated("Use `-showAnnotations:edgePadding:animated:completionHandler:` instead.")));
	[Export ("showAnnotations:edgePadding:animated:")]
	void ShowAnnotations (MLNAnnotation[] annotations, UIEdgeInsets insets, bool animated);

	// -(void)showAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("showAnnotations:edgePadding:animated:completionHandler:")]
	void ShowAnnotations (MLNAnnotation[] annotations, UIEdgeInsets insets, bool animated, [NullAllowed] Action completion);

	// @property (copy, nonatomic) MLNMapCamera * _Nonnull camera;
	[Export ("camera", ArgumentSemantic.Copy)]
	MLNMapCamera Camera { get; set; }

	// -(void)setCamera:(MLNMapCamera * _Nonnull)camera animated:(BOOL)animated;
	[Export ("setCamera:animated:")]
	void SetCamera (MLNMapCamera camera, bool animated);

	// -(void)setCamera:(MLNMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function;
	[Export ("setCamera:withDuration:animationTimingFunction:")]
	void SetCamera (MLNMapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function);

	// -(void)setCamera:(MLNMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setCamera:withDuration:animationTimingFunction:completionHandler:")]
	void SetCamera (MLNMapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function, [NullAllowed] Action completion);

	// -(void)setCamera:(MLNMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function edgePadding:(UIEdgeInsets)edgePadding completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setCamera:withDuration:animationTimingFunction:edgePadding:completionHandler:")]
	void SetCamera (MLNMapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function, UIEdgeInsets edgePadding, [NullAllowed] Action completion);

	// -(void)flyToCamera:(MLNMapCamera * _Nonnull)camera completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("flyToCamera:completionHandler:")]
	void FlyToCamera (MLNMapCamera camera, [NullAllowed] Action completion);

	// -(void)flyToCamera:(MLNMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("flyToCamera:withDuration:completionHandler:")]
	void FlyToCamera (MLNMapCamera camera, double duration, [NullAllowed] Action completion);

	// -(void)flyToCamera:(MLNMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration peakAltitude:(CLLocationDistance)peakAltitude completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("flyToCamera:withDuration:peakAltitude:completionHandler:")]
	void FlyToCamera (MLNMapCamera camera, double duration, double peakAltitude, [NullAllowed] Action completion);

	// -(void)flyToCamera:(MLNMapCamera * _Nonnull)camera edgePadding:(UIEdgeInsets)insets withDuration:(NSTimeInterval)duration completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("flyToCamera:edgePadding:withDuration:completionHandler:")]
	void FlyToCamera (MLNMapCamera camera, UIEdgeInsets insets, double duration, [NullAllowed] Action completion);

	// -(MLNMapCamera * _Nonnull)cameraThatFitsCoordinateBounds:(MLNCoordinateBounds)bounds;
	[Export ("cameraThatFitsCoordinateBounds:")]
	MLNMapCamera CameraThatFitsCoordinateBounds (MLNCoordinateBounds bounds);

	// -(MLNMapCamera * _Nonnull)cameraThatFitsCoordinateBounds:(MLNCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets;
	[Export ("cameraThatFitsCoordinateBounds:edgePadding:")]
	MLNMapCamera CameraThatFitsCoordinateBounds (MLNCoordinateBounds bounds, UIEdgeInsets insets);

	// -(MLNMapCamera * _Nonnull)camera:(MLNMapCamera * _Nonnull)camera fittingCoordinateBounds:(MLNCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets;
	[Export ("camera:fittingCoordinateBounds:edgePadding:")]
	MLNMapCamera CameraFittingCoordinateBoundsEdgePadding (MLNMapCamera camera, MLNCoordinateBounds bounds, UIEdgeInsets insets);

	// -(MLNMapCamera * _Nonnull)camera:(MLNMapCamera * _Nonnull)camera fittingShape:(MLNShape * _Nonnull)shape edgePadding:(UIEdgeInsets)insets;
	[Export ("camera:fittingShape:edgePadding:")]
	MLNMapCamera CameraFittingShapeEdgePadding (MLNMapCamera camera, MLNShape shape, UIEdgeInsets insets);

	// -(MLNMapCamera * _Nonnull)cameraThatFitsShape:(MLNShape * _Nonnull)shape direction:(CLLocationDirection)direction edgePadding:(UIEdgeInsets)insets;
	[Export ("cameraThatFitsShape:direction:edgePadding:")]
	MLNMapCamera CameraThatFitsShape (MLNShape shape, double direction, UIEdgeInsets insets);

	// -(CGPoint)anchorPointForGesture:(UIGestureRecognizer * _Nonnull)gesture;
	[Export ("anchorPointForGesture:")]
	CGPoint AnchorPointForGesture (UIGestureRecognizer gesture);

	// @property (assign, nonatomic) UIEdgeInsets contentInset;
	[Export ("contentInset", ArgumentSemantic.Assign)]
	UIEdgeInsets ContentInset { get; set; }

	// @property (readonly, nonatomic) UIEdgeInsets cameraEdgeInsets;
	[Export ("cameraEdgeInsets")]
	UIEdgeInsets CameraEdgeInsets { get; }

	// -(void)setContentInset:(UIEdgeInsets)contentInset animated:(BOOL)animated __attribute__((deprecated("Use `-setContentInset:animated:completionHandler:` instead.")));
	[Export ("setContentInset:animated:")]
	void SetContentInset (UIEdgeInsets contentInset, bool animated);

	// -(void)setContentInset:(UIEdgeInsets)contentInset animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("setContentInset:animated:completionHandler:")]
	void SetContentInset (UIEdgeInsets contentInset, bool animated, [NullAllowed] Action completion);

	// -(CLLocationCoordinate2D)convertPoint:(CGPoint)point toCoordinateFromView:(UIView * _Nullable)view;
	[Export ("convertPoint:toCoordinateFromView:")]
	CLLocationCoordinate2D ConvertPoint (CGPoint point, [NullAllowed] UIView view);

	// -(CGPoint)convertCoordinate:(CLLocationCoordinate2D)coordinate toPointToView:(UIView * _Nullable)view;
	[Export ("convertCoordinate:toPointToView:")]
	CGPoint ConvertCoordinate (CLLocationCoordinate2D coordinate, [NullAllowed] UIView view);

	// -(MLNCoordinateBounds)convertRect:(CGRect)rect toCoordinateBoundsFromView:(UIView * _Nullable)view;
	[Export ("convertRect:toCoordinateBoundsFromView:")]
	MLNCoordinateBounds ConvertRect (CGRect rect, [NullAllowed] UIView view);

	// -(CGRect)convertCoordinateBounds:(MLNCoordinateBounds)bounds toRectToView:(UIView * _Nullable)view;
	[Export ("convertCoordinateBounds:toRectToView:")]
	CGRect ConvertCoordinateBounds (MLNCoordinateBounds bounds, [NullAllowed] UIView view);

	// -(CLLocationDistance)metersPerPointAtLatitude:(CLLocationDegrees)latitude;
	[Export ("metersPerPointAtLatitude:")]
	double MetersPerPointAtLatitude (double latitude);

	// -(MLNMapProjection * _Nonnull)mapProjection;
	[Export ("mapProjection")]
	// [Verify (MethodToProperty)]
	MLNMapProjection MapProjection { get; }

	// @property (readonly, nonatomic) NSArray<id<MLNAnnotation>> * _Nullable annotations;
	[NullAllowed, Export ("annotations")]
	MLNAnnotation[] Annotations { get; }

	// -(void)addAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("addAnnotation:")]
	void AddAnnotation (MLNAnnotation annotation);

	// -(void)addAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations;
	[Export ("addAnnotations:")]
	void AddAnnotations (MLNAnnotation[] annotations);

	// -(void)removeAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("removeAnnotation:")]
	void RemoveAnnotation (MLNAnnotation annotation);

	// -(void)removeAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations;
	[Export ("removeAnnotations:")]
	void RemoveAnnotations (MLNAnnotation[] annotations);

	// -(MLNAnnotationView * _Nullable)viewForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("viewForAnnotation:")]
	[return: NullAllowed]
	MLNAnnotationView ViewForAnnotation (MLNAnnotation annotation);

	// -(__kindof MLNAnnotationImage * _Nullable)dequeueReusableAnnotationImageWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("dequeueReusableAnnotationImageWithIdentifier:")]
	[return: NullAllowed]
	MLNAnnotationImage DequeueReusableAnnotationImageWithIdentifier (string identifier);

	// -(__kindof MLNAnnotationView * _Nullable)dequeueReusableAnnotationViewWithIdentifier:(NSString * _Nonnull)identifier;
	[Export ("dequeueReusableAnnotationViewWithIdentifier:")]
	[return: NullAllowed]
	MLNAnnotationView DequeueReusableAnnotationViewWithIdentifier (string identifier);

	// @property (readonly, nonatomic) NSArray<id<MLNAnnotation>> * _Nullable visibleAnnotations;
	[NullAllowed, Export ("visibleAnnotations")]
	MLNAnnotation[] VisibleAnnotations { get; }

	// -(NSArray<id<MLNAnnotation>> * _Nullable)visibleAnnotationsInRect:(CGRect)rect;
	[Export ("visibleAnnotationsInRect:")]
	[return: NullAllowed]
	MLNAnnotation[] VisibleAnnotationsInRect (CGRect rect);

	// @property (copy, nonatomic) NSArray<id<MLNAnnotation>> * _Nonnull selectedAnnotations;
	[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
	MLNAnnotation[] SelectedAnnotations { get; set; }

	// -(void)selectAnnotation:(id<MLNAnnotation> _Nonnull)annotation animated:(BOOL)animated __attribute__((deprecated("Use `-selectAnnotation:animated:completionHandler:` instead.")));
	[Export ("selectAnnotation:animated:")]
	void SelectAnnotation (MLNAnnotation annotation, bool animated);

	// -(void)selectAnnotation:(id<MLNAnnotation> _Nonnull)annotation animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("selectAnnotation:animated:completionHandler:")]
	void SelectAnnotation (MLNAnnotation annotation, bool animated, [NullAllowed] Action completion);

	// -(void)selectAnnotation:(id<MLNAnnotation> _Nonnull)annotation moveIntoView:(BOOL)moveIntoView animateSelection:(BOOL)animateSelection completionHandler:(void (^ _Nullable)(void))completion;
	[Export ("selectAnnotation:moveIntoView:animateSelection:completionHandler:")]
	void SelectAnnotation (MLNAnnotation annotation, bool moveIntoView, bool animateSelection, [NullAllowed] Action completion);

	// -(void)deselectAnnotation:(id<MLNAnnotation> _Nullable)annotation animated:(BOOL)animated;
	[Export ("deselectAnnotation:animated:")]
	void DeselectAnnotation ([NullAllowed] MLNAnnotation annotation, bool animated);

	// @property (readonly, nonatomic) NSArray<id<MLNOverlay>> * _Nonnull overlays;
	[Export ("overlays")]
	MLNOverlay[] Overlays { get; }

	// -(void)addOverlay:(id<MLNOverlay> _Nonnull)overlay;
	[Export ("addOverlay:")]
	void AddOverlay (MLNOverlay overlay);

	// -(void)addOverlays:(NSArray<id<MLNOverlay>> * _Nonnull)overlays;
	[Export ("addOverlays:")]
	void AddOverlays (MLNOverlay[] overlays);

	// -(void)removeOverlay:(id<MLNOverlay> _Nonnull)overlay;
	[Export ("removeOverlay:")]
	void RemoveOverlay (MLNOverlay overlay);

	// -(void)removeOverlays:(NSArray<id<MLNOverlay>> * _Nonnull)overlays;
	[Export ("removeOverlays:")]
	void RemoveOverlays (MLNOverlay[] overlays);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point __attribute__((swift_name("visibleFeatures(at:)")));
	[Export ("visibleFeaturesAtPoint:")]
	IMLNFeature[] VisibleFeaturesAtPoint (CGPoint point);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers __attribute__((swift_name("visibleFeatures(at:styleLayerIdentifiers:)")));
	[Export ("visibleFeaturesAtPoint:inStyleLayersWithIdentifiers:")]
	IMLNFeature[] VisibleFeaturesAtPoint (CGPoint point, [NullAllowed] NSSet<NSString> styleLayerIdentifiers);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate __attribute__((swift_name("visibleFeatures(at:styleLayerIdentifiers:predicate:)")));
	[Export ("visibleFeaturesAtPoint:inStyleLayersWithIdentifiers:predicate:")]
	IMLNFeature[] VisibleFeaturesAtPoint (CGPoint point, [NullAllowed] NSSet<NSString> styleLayerIdentifiers, [NullAllowed] NSPredicate predicate);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect __attribute__((swift_name("visibleFeatures(in:)")));
	[Export ("visibleFeaturesInRect:")]
	IMLNFeature[] VisibleFeaturesInRect (CGRect rect);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers __attribute__((swift_name("visibleFeatures(in:styleLayerIdentifiers:)")));
	[Export ("visibleFeaturesInRect:inStyleLayersWithIdentifiers:")]
	IMLNFeature[] VisibleFeaturesInRect (CGRect rect, [NullAllowed] NSSet<NSString> styleLayerIdentifiers);

	// -(NSArray<id<MLNFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate __attribute__((swift_name("visibleFeatures(in:styleLayerIdentifiers:predicate:)")));
	[Export ("visibleFeaturesInRect:inStyleLayersWithIdentifiers:predicate:")]
	IMLNFeature[] VisibleFeaturesInRect (CGRect rect, [NullAllowed] NSSet<NSString> styleLayerIdentifiers, [NullAllowed] NSPredicate predicate);

	// @property (nonatomic) MLNMapDebugMaskOptions debugMask;
	[Export ("debugMask", ArgumentSemantic.Assign)]
	MLNMapDebugMaskOptions DebugMask { get; set; }

	// -(BOOL)isRenderingStatsViewEnabled;
	[Export ("isRenderingStatsViewEnabled")]
	// [Verify (MethodToProperty)]
	bool IsRenderingStatsViewEnabled { get; }

	// -(void)enableRenderingStatsView:(BOOL)value;
	[Export ("enableRenderingStatsView:")]
	void EnableRenderingStatsView (bool value);

	// -(NSArray<NSString *> * _Nonnull)getActionJournalLogFiles;
	[Export ("getActionJournalLogFiles")]
	// [Verify (MethodToProperty)]
	string[] ActionJournalLogFiles { get; }

	// -(NSArray<NSString *> * _Nonnull)getActionJournalLog;
	[Export ("getActionJournalLog")]
	// [Verify (MethodToProperty)]
	string[] ActionJournalLog { get; }

	// -(void)clearActionJournalLog;
	[Export ("clearActionJournalLog")]
	void ClearActionJournalLog ();

	// -(MLNBackendResource * _Nonnull)backendResource;
	// [Export ("backendResource")]
	// // [Verify (MethodToProperty)]
	// MLNBackendResource BackendResource { get; }

	// -(void)triggerRepaint;
	[Export ("triggerRepaint")]
	void TriggerRepaint ();

	// -(void)addPluginLayerType:(Class _Nonnull)pluginLayerClass;
	[Export ("addPluginLayerType:")]
	void AddPluginLayerType (Class pluginLayerClass);
}

// @interface MLNMapProjection : NSObject
[BaseType (typeof(NSObject))]
interface MLNMapProjection
{
	// -(instancetype _Nonnull)initWithMapView:(MLNMapView * _Nonnull)mapView;
	[Export ("initWithMapView:")]
	NativeHandle Constructor (MLNMapView mapView);

	// @property (readonly, copy) MLNMapCamera * _Nonnull camera;
	[Export ("camera", ArgumentSemantic.Copy)]
	MLNMapCamera Camera { get; }

	// -(void)setCamera:(MLNMapCamera * _Nonnull)camera withEdgeInsets:(UIEdgeInsets)insets;
	[Export ("setCamera:withEdgeInsets:")]
	void SetCamera (MLNMapCamera camera, UIEdgeInsets insets);

	// -(void)setVisibleCoordinateBounds:(MLNCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets;
	[Export ("setVisibleCoordinateBounds:edgePadding:")]
	void SetVisibleCoordinateBounds (MLNCoordinateBounds bounds, UIEdgeInsets insets);

	// -(CLLocationCoordinate2D)convertPoint:(CGPoint)point;
	[Export ("convertPoint:")]
	CLLocationCoordinate2D ConvertPoint (CGPoint point);

	// -(CGPoint)convertCoordinate:(CLLocationCoordinate2D)coordinate;
	[Export ("convertCoordinate:")]
	CGPoint ConvertCoordinate (CLLocationCoordinate2D coordinate);

	// @property (readonly) CLLocationDistance metersPerPoint;
	[Export ("metersPerPoint")]
	double MetersPerPoint { get; }
}

// @interface MLNMapSnapshotOverlay : NSObject
[BaseType (typeof(NSObject))]
interface MLNMapSnapshotOverlay
{
	// @property (readonly, nonatomic) CGContextRef _Nonnull context;
	[Export ("context")]
	CGContext Context { get; }

	// -(CGPoint)pointForCoordinate:(CLLocationCoordinate2D)coordinate;
	[Export ("pointForCoordinate:")]
	CGPoint PointForCoordinate (CLLocationCoordinate2D coordinate);

	// -(CLLocationCoordinate2D)coordinateForPoint:(CGPoint)point;
	[Export ("coordinateForPoint:")]
	CLLocationCoordinate2D CoordinateForPoint (CGPoint point);
}

// typedef void (^MLNMapSnapshotOverlayHandler)(MLNMapSnapshotOverlay * _Nonnull);
delegate void MLNMapSnapshotOverlayHandler (MLNMapSnapshotOverlay arg0);

// @interface MLNMapSnapshotOptions : NSObject <NSCopying>
[BaseType (typeof(NSObject))]
interface MLNMapSnapshotOptions : INSCopying
{
	// -(instancetype _Nonnull)initWithStyleURL:(NSUrl * _Nullable)styleURL camera:(MLNMapCamera * _Nonnull)camera size:(CGSize)size;
	[Export ("initWithStyleURL:camera:size:")]
	NativeHandle Constructor ([NullAllowed] NSUrl styleURL, MLNMapCamera camera, CGSize size);

	// @property (readwrite, nonatomic) BOOL showsLogo;
	[Export ("showsLogo")]
	bool ShowsLogo { get; set; }

	// @property (readwrite, nonatomic) BOOL showsAttribution;
	[Export ("showsAttribution")]
	bool ShowsAttribution { get; set; }

	// @property (readonly, nonatomic) NSUrl * _Nonnull styleURL;
	[Export ("styleURL")]
	NSUrl StyleURL { get; }

	// @property (nonatomic) double zoomLevel;
	[Export ("zoomLevel")]
	double ZoomLevel { get; set; }

	// @property (nonatomic) MLNMapCamera * _Nonnull camera;
	[Export ("camera", ArgumentSemantic.Assign)]
	MLNMapCamera Camera { get; set; }

	// @property (nonatomic) MLNCoordinateBounds coordinateBounds;
	[Export ("coordinateBounds", ArgumentSemantic.Assign)]
	MLNCoordinateBounds CoordinateBounds { get; set; }

	// @property (readonly, nonatomic) CGSize size;
	[Export ("size")]
	CGSize Size { get; }

	// @property (nonatomic) CGFloat scale;
	[Export ("scale")]
	nfloat Scale { get; set; }
}

// @interface MLNMapSnapshot : NSObject
[BaseType (typeof(NSObject))]
interface MLNMapSnapshot
{
	// -(CGPoint)pointForCoordinate:(CLLocationCoordinate2D)coordinate;
	[Export ("pointForCoordinate:")]
	CGPoint PointForCoordinate (CLLocationCoordinate2D coordinate);

	// -(CLLocationCoordinate2D)coordinateForPoint:(CGPoint)point;
	[Export ("coordinateForPoint:")]
	CLLocationCoordinate2D CoordinateForPoint (CGPoint point);

	// @property (readonly, nonatomic) UIImage * _Nonnull image;
	[Export ("image")]
	UIImage Image { get; }
}

// typedef void (^MLNMapSnapshotCompletionHandler)(MLNMapSnapshot * _Nullable, NSError * _Nullable);
delegate void MLNMapSnapshotCompletionHandler ([NullAllowed] MLNMapSnapshot arg0, [NullAllowed] NSError arg1);

// @interface MLNMapSnapshotter : NSObject <MLNStylable>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface MLNMapSnapshotter : IMLNStylable
{
	// -(instancetype _Nonnull)initWithOptions:(MLNMapSnapshotOptions * _Nonnull)options __attribute__((objc_designated_initializer));
	[Export ("initWithOptions:")]
	[DesignatedInitializer]
	NativeHandle Constructor (MLNMapSnapshotOptions options);

	// -(void)addAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("addAnnotation:")]
	void AddAnnotation (MLNAnnotation annotation);

	// -(void)addAnnotations:(NSArray<id<MLNAnnotation>> * _Nonnull)annotations;
	[Export ("addAnnotations:")]
	void AddAnnotations (MLNAnnotation[] annotations);

	// -(void)startWithCompletionHandler:(MLNMapSnapshotCompletionHandler _Nonnull)completionHandler;
	[Export ("startWithCompletionHandler:")]
	void StartWithCompletionHandler (MLNMapSnapshotCompletionHandler completionHandler);

	// -(void)startWithQueue:(dispatch_queue_t _Nonnull)queue completionHandler:(MLNMapSnapshotCompletionHandler _Nonnull)completionHandler;
	[Export ("startWithQueue:completionHandler:")]
	void StartWithQueue (DispatchQueue queue, MLNMapSnapshotCompletionHandler completionHandler);

	// -(void)startWithOverlayHandler:(MLNMapSnapshotOverlayHandler _Nonnull)overlayHandler completionHandler:(MLNMapSnapshotCompletionHandler _Nonnull)completionHandler;
	[Export ("startWithOverlayHandler:completionHandler:")]
	void StartWithOverlayHandler (MLNMapSnapshotOverlayHandler overlayHandler, MLNMapSnapshotCompletionHandler completionHandler);

	// -(void)cancel;
	[Export ("cancel")]
	void Cancel ();

	// @property (nonatomic) MLNMapSnapshotOptions * _Nonnull options;
	[Export ("options", ArgumentSemantic.Assign)]
	MLNMapSnapshotOptions Options { get; set; }

	// @property (readonly, getter = isLoading, nonatomic) BOOL loading;
	[Export ("loading")]
	bool Loading { [Bind ("isLoading")] get; }

	[Wrap ("WeakDelegate")]
	[NullAllowed]
	MLNMapSnapshotterDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<MLNMapSnapshotterDelegate> _Nullable delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic) MLNStyle * _Nullable style;
	[NullAllowed, Export ("style")]
	MLNStyle Style { get; }
}

// @protocol MLNMapSnapshotterDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNMapSnapshotterDelegate
{
	// @optional -(void)mapSnapshotterDidFail:(MLNMapSnapshotter * _Nonnull)snapshotter withError:(NSError * _Nonnull)error;
	[Export ("mapSnapshotterDidFail:withError:")]
	void MapSnapshotterDidFail (MLNMapSnapshotter snapshotter, NSError error);

	// @optional -(void)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter didFinishLoadingStyle:(MLNStyle * _Nonnull)style;
	[Export ("mapSnapshotter:didFinishLoadingStyle:")]
	void MapSnapshotter (MLNMapSnapshotter snapshotter, MLNStyle style);

	// @optional -(void)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter didFailLoadingImageNamed:(NSString * _Nonnull)name;
	[Export ("mapSnapshotter:didFailLoadingImageNamed:")]
	void MapSnapshotter (MLNMapSnapshotter snapshotter, string name);

	// @optional -(MLNAnnotationImage * _Nullable)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter imageForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapSnapshotter:imageForAnnotation:")]
	[return: NullAllowed]
	MLNAnnotationImage MapSnapshotter (MLNMapSnapshotter snapshotter, MLNAnnotation annotation);

	// @optional -(CGFloat)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter alphaForShapeAnnotation:(MLNShape * _Nonnull)annotation;
	[Export ("mapSnapshotter:alphaForShapeAnnotation:")]
	nfloat MapSnapshotter (MLNMapSnapshotter snapshotter, MLNShape annotation);

	// @optional -(UIColor * _Nonnull)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter strokeColorForShapeAnnotation:(MLNShape * _Nonnull)annotation;
	[Export ("mapSnapshotter:strokeColorForShapeAnnotation:")]
	UIColor MapSnapshotterStrokeColorForShapeAnnotation (MLNMapSnapshotter snapshotter, MLNShape annotation);

	// @optional -(UIColor * _Nonnull)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter fillColorForPolygonAnnotation:(MLNPolygon * _Nonnull)annotation;
	[Export ("mapSnapshotter:fillColorForPolygonAnnotation:")]
	UIColor MapSnapshotter (MLNMapSnapshotter snapshotter, MLNPolygon annotation);

	// @optional -(CGFloat)mapSnapshotter:(MLNMapSnapshotter * _Nonnull)snapshotter lineWidthForPolylineAnnotation:(MLNPolyline * _Nonnull)annotation;
	[Export ("mapSnapshotter:lineWidthForPolylineAnnotation:")]
	nfloat MapSnapshotter (MLNMapSnapshotter snapshotter, MLNPolyline annotation);
}

// @interface IBAdditions (MLNMapView)
// [Category]
// [BaseType (typeof(MLNMapView))]
// interface MLNMapView_IBAdditions
// {
// 	[Export ("latitude")]
// 	double Latitude { get; set; }
// 	[Export ("longitude")]
// 	double Longitude { get; set; }
// 	[Export ("zoomLevel")]
// 	double ZoomLevel { get; set; }
// 	[Export ("minimumZoomLevel")]
// 	double MinimumZoomLevel { get; set; }
// 	[Export ("maximumZoomLevel")]
// 	double MaximumZoomLevel { get; set; }
// 	[Export ("allowsZooming")]
// 	bool AllowsZooming { get; set; }
// 	[Export ("allowsScrolling")]
// 	bool AllowsScrolling { get; set; }
// 	[Export ("allowsRotating")]
// 	bool AllowsRotating { get; set; }
// 	[Export ("allowsTilting")]
// 	bool AllowsTilting { get; set; }
// 	[Export ("showsUserLocation")]
// 	bool ShowsUserLocation { get; set; }
// 	[Export ("showsHeading")]
// 	bool ShowsHeading { get; set; }
// 	[Export ("showsScale")]
// 	bool ShowsScale { get; set; }
// }

// @interface MLNRenderingStats : NSObject
[BaseType (typeof(NSObject))]
interface MLNRenderingStats
{
	// @property (readonly) double encodingTime;
	[Export ("encodingTime")]
	double EncodingTime { get; }

	// @property (readonly) double renderingTime;
	[Export ("renderingTime")]
	double RenderingTime { get; }

	// @property (readonly) int numFrames;
	[Export ("numFrames")]
	int NumFrames { get; }

	// @property (readonly) int numDrawCalls;
	[Export ("numDrawCalls")]
	int NumDrawCalls { get; }

	// @property (readonly) int totalDrawCalls;
	[Export ("totalDrawCalls")]
	int TotalDrawCalls { get; }

	// @property (readonly) int numCreatedTextures;
	[Export ("numCreatedTextures")]
	int NumCreatedTextures { get; }

	// @property (readonly) int numActiveTextures;
	[Export ("numActiveTextures")]
	int NumActiveTextures { get; }

	// @property (readonly) int numTextureBindings;
	[Export ("numTextureBindings")]
	int NumTextureBindings { get; }

	// @property (readonly) int numTextureUpdates;
	[Export ("numTextureUpdates")]
	int NumTextureUpdates { get; }

	// @property (readonly) unsigned long textureUpdateBytes;
	[Export ("textureUpdateBytes")]
	nuint TextureUpdateBytes { get; }

	// @property (readonly) unsigned long totalBuffers;
	[Export ("totalBuffers")]
	nuint TotalBuffers { get; }

	// @property (readonly) unsigned long totalBufferObjs;
	[Export ("totalBufferObjs")]
	nuint TotalBufferObjs { get; }

	// @property (readonly) unsigned long bufferUpdates;
	[Export ("bufferUpdates")]
	nuint BufferUpdates { get; }

	// @property (readonly) unsigned long bufferObjUpdates;
	[Export ("bufferObjUpdates")]
	nuint BufferObjUpdates { get; }

	// @property (readonly) unsigned long bufferUpdateBytes;
	[Export ("bufferUpdateBytes")]
	nuint BufferUpdateBytes { get; }

	// @property (readonly) int numBuffers;
	[Export ("numBuffers")]
	int NumBuffers { get; }

	// @property (readonly) int numFrameBuffers;
	[Export ("numFrameBuffers")]
	int NumFrameBuffers { get; }

	// @property (readonly) int numIndexBuffers;
	[Export ("numIndexBuffers")]
	int NumIndexBuffers { get; }

	// @property (readonly) unsigned long indexUpdateBytes;
	[Export ("indexUpdateBytes")]
	nuint IndexUpdateBytes { get; }

	// @property (readonly) int numVertexBuffers;
	[Export ("numVertexBuffers")]
	int NumVertexBuffers { get; }

	// @property (readonly) unsigned long vertexUpdateBytes;
	[Export ("vertexUpdateBytes")]
	nuint VertexUpdateBytes { get; }

	// @property (readonly) int numUniformBuffers;
	[Export ("numUniformBuffers")]
	int NumUniformBuffers { get; }

	// @property (readonly) int numUniformUpdates;
	[Export ("numUniformUpdates")]
	int NumUniformUpdates { get; }

	// @property (readonly) unsigned long uniformUpdateBytes;
	[Export ("uniformUpdateBytes")]
	nuint UniformUpdateBytes { get; }

	// @property (readonly) int memTextures;
	[Export ("memTextures")]
	int MemTextures { get; }

	// @property (readonly) int memBuffers;
	[Export ("memBuffers")]
	int MemBuffers { get; }

	// @property (readonly) int memIndexBuffers;
	[Export ("memIndexBuffers")]
	int MemIndexBuffers { get; }

	// @property (readonly) int memVertexBuffers;
	[Export ("memVertexBuffers")]
	int MemVertexBuffers { get; }

	// @property (readonly) int memUniformBuffers;
	[Export ("memUniformBuffers")]
	int MemUniformBuffers { get; }

	// @property (readonly) int stencilClears;
	[Export ("stencilClears")]
	int StencilClears { get; }

	// @property (readonly) int stencilUpdates;
	[Export ("stencilUpdates")]
	int StencilUpdates { get; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
// extern double MapboxVersionNumber __attribute__((visibility("default")));
// [Field ("MapboxVersionNumber", "__Internal")]
// double MapboxVersionNumber { get; }

	// extern const unsigned char[] MapboxVersionString __attribute__((visibility("default")));
	// [Field ("MapboxVersionString", "__Internal")]
	// byte[] MapboxVersionString { get; }
}

// @interface MLNNetworkResponse : NSObject
[BaseType (typeof(NSObject))]
interface MLNNetworkResponse
{
	// @property (retain) NSError * _Nullable error;
	[NullAllowed, Export ("error", ArgumentSemantic.Retain)]
	NSError Error { get; set; }

	// @property (retain) NSData * _Nullable data;
	[NullAllowed, Export ("data", ArgumentSemantic.Retain)]
	NSData Data { get; set; }

	// @property (retain) NSUrlResponse * _Nullable response;
	[NullAllowed, Export ("response", ArgumentSemantic.Retain)]
	NSUrlResponse Response { get; set; }

	// +(MLNNetworkResponse * _Nonnull)responseWithData:(NSData * _Nonnull)data urlResponse:(NSUrlResponse * _Nonnull)response error:(NSError * _Nonnull)error;
	[Static]
	[Export ("responseWithData:urlResponse:error:")]
	MLNNetworkResponse ResponseWithData (NSData data, NSUrlResponse response, NSError error);
}

// @protocol MLNNetworkConfigurationDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNNetworkConfigurationDelegate
{
	// @optional -(NSUrlSession * _Nonnull)sessionForNetworkConfiguration:(MLNNetworkConfiguration * _Nonnull)configuration;
	[Export ("sessionForNetworkConfiguration:")]
	NSUrlSession SessionForNetworkConfiguration (MLNNetworkConfiguration configuration);

	// @optional -(NSMutableUrlRequest * _Nonnull)willSendRequest:(NSMutableUrlRequest * _Nonnull)request;
	[Export ("willSendRequest:")]
	NSMutableUrlRequest WillSendRequest (NSMutableUrlRequest request);

	// @optional -(MLNNetworkResponse * _Nonnull)didReceiveResponse:(MLNNetworkResponse * _Nonnull)response;
	[Export ("didReceiveResponse:")]
	MLNNetworkResponse DidReceiveResponse (MLNNetworkResponse response);
}

// @interface MLNNetworkConfiguration : NSObject
[BaseType (typeof(NSObject))]
interface MLNNetworkConfiguration
{
	[Wrap ("WeakDelegate")]
	[NullAllowed]
	MLNNetworkConfigurationDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<MLNNetworkConfigurationDelegate> _Nullable delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic, class) MLNNetworkConfiguration * _Nonnull sharedManager;
	[Static]
	[Export ("sharedManager")]
	MLNNetworkConfiguration SharedManager { get; }

	// @property (atomic, strong, null_resettable) NSUrlSessionConfiguration * _Null_unspecified sessionConfiguration;
	[NullAllowed, Export ("sessionConfiguration", ArgumentSemantic.Strong)]
	NSUrlSessionConfiguration SessionConfiguration { get; set; }
}

// @protocol MLNOfflineRegion <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface MLNOfflineRegion
{
	// @required @property (readonly, nonatomic) NSUrl * _Nonnull styleURL;
	[Abstract]
	[Export ("styleURL")]
	NSUrl StyleURL { get; }

	// @required @property (nonatomic) BOOL includesIdeographicGlyphs;
	[Abstract]
	[Export ("includesIdeographicGlyphs")]
	bool IncludesIdeographicGlyphs { get; set; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExceptionName _Nonnull MLNInvalidOfflinePackException __attribute__((visibility("default")));
	[Field ("MLNInvalidOfflinePackException", "__Internal")]
	NSString MLNInvalidOfflinePackException { get; }
}

// @interface MLNOfflinePack : NSObject
[BaseType (typeof(NSObject))]
interface MLNOfflinePack
{
	// @property (readonly, nonatomic) id<MLNOfflineRegion> _Nonnull region;
	[Export ("region")]
	MLNOfflineRegion Region { get; }

	// @property (readonly, nonatomic) NSData * _Nonnull context;
	[Export ("context")]
	NSData Context { get; }

	// -(void)setContext:(NSData * _Nonnull)context completionHandler:(void (^ _Nullable)(NSError * _Nullable))completion;
	[Export ("setContext:completionHandler:")]
	void SetContext (NSData context, [NullAllowed] Action<NSError> completion);

	// @property (readonly, nonatomic) MLNOfflinePackState state;
	[Export ("state")]
	MLNOfflinePackState State { get; }

	// @property (readonly, nonatomic) MLNOfflinePackProgress progress;
	[Export ("progress")]
	MLNOfflinePackProgress Progress { get; }

	// -(void)resume;
	[Export ("resume")]
	void Resume ();

	// -(void)suspend;
	[Export ("suspend")]
	void Suspend ();

	// -(void)requestProgress;
	[Export ("requestProgress")]
	void RequestProgress ();
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const NSNotificationName _Nonnull MLNOfflinePackProgressChangedNotification __attribute__((visibility("default")));
	[Field ("MLNOfflinePackProgressChangedNotification", "__Internal")]
	NSString MLNOfflinePackProgressChangedNotification { get; }

	// extern const NSNotificationName _Nonnull MLNOfflinePackErrorNotification __attribute__((visibility("default")));
	[Field ("MLNOfflinePackErrorNotification", "__Internal")]
	NSString MLNOfflinePackErrorNotification { get; }

	// extern const NSNotificationName _Nonnull MLNOfflinePackMaximumMapboxTilesReachedNotification __attribute__((visibility("default")));
	[Field ("MLNOfflinePackMaximumMapboxTilesReachedNotification", "__Internal")]
	NSString MLNOfflinePackMaximumMapboxTilesReachedNotification { get; }

	// extern const MLNOfflinePackUserInfoKey _Nonnull MLNOfflinePackUserInfoKeyState __attribute__((visibility("default")));
	[Field ("MLNOfflinePackUserInfoKeyState", "__Internal")]
	NSString MLNOfflinePackUserInfoKeyState { get; }

	// extern const MLNOfflinePackUserInfoKey _Nonnull MLNOfflinePackUserInfoKeyProgress __attribute__((visibility("default")));
	[Field ("MLNOfflinePackUserInfoKeyProgress", "__Internal")]
	NSString MLNOfflinePackUserInfoKeyProgress { get; }

	// extern const MLNOfflinePackUserInfoKey _Nonnull MLNOfflinePackUserInfoKeyError __attribute__((visibility("default")));
	[Field ("MLNOfflinePackUserInfoKeyError", "__Internal")]
	NSString MLNOfflinePackUserInfoKeyError { get; }

	// extern const MLNOfflinePackUserInfoKey _Nonnull MLNOfflinePackUserInfoKeyMaximumCount __attribute__((visibility("default")));
	[Field ("MLNOfflinePackUserInfoKeyMaximumCount", "__Internal")]
	NSString MLNOfflinePackUserInfoKeyMaximumCount { get; }

	// extern const MLNExceptionName _Nonnull MLNUnsupportedRegionTypeException __attribute__((visibility("default")));
	[Field ("MLNUnsupportedRegionTypeException", "__Internal")]
	NSString MLNUnsupportedRegionTypeException { get; }
}

// typedef void (^MLNOfflinePackAdditionCompletionHandler)(MLNOfflinePack * _Nullable, NSError * _Nullable);
delegate void MLNOfflinePackAdditionCompletionHandler ([NullAllowed] MLNOfflinePack arg0, [NullAllowed] NSError arg1);

// typedef void (^MLNOfflinePackRemovalCompletionHandler)(NSError * _Nullable);
delegate void MLNOfflinePackRemovalCompletionHandler ([NullAllowed] NSError arg0);

// typedef void (^MLNBatchedOfflinePackAdditionCompletionHandler)(NSUrl * _Nonnull, NSArray<MLNOfflinePack *> * _Nullable, NSError * _Nullable);
delegate void MLNBatchedOfflinePackAdditionCompletionHandler (NSUrl arg0, [NullAllowed] MLNOfflinePack[] arg1, [NullAllowed] NSError arg2);

// typedef void (^MLNOfflinePreloadDataCompletionHandler)(NSUrl * _Nonnull, NSError * _Nullable);
delegate void MLNOfflinePreloadDataCompletionHandler (NSUrl arg0, [NullAllowed] NSError arg1);

// @interface MLNOfflineStorage : NSObject
[BaseType (typeof(NSObject))]
interface MLNOfflineStorage
{
	// @property (readonly, nonatomic, class) MLNOfflineStorage * _Nonnull sharedOfflineStorage;
	[Static]
	[Export ("sharedOfflineStorage")]
	MLNOfflineStorage SharedOfflineStorage { get; }

	[Wrap ("WeakDelegate")]
	[NullAllowed]
	MLNOfflineStorageDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<MLNOfflineStorageDelegate> _Nullable delegate __attribute__((iboutlet));
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull databasePath;
	[Export ("databasePath")]
	string DatabasePath { get; }

	// @property (readonly, copy, nonatomic) NSUrl * _Nonnull databaseURL;
	[Export ("databaseURL", ArgumentSemantic.Copy)]
	NSUrl DatabaseURL { get; }

	// -(void)addContentsOfFile:(NSString * _Nonnull)filePath withCompletionHandler:(MLNBatchedOfflinePackAdditionCompletionHandler _Nullable)completion;
	[Export ("addContentsOfFile:withCompletionHandler:")]
	void AddContentsOfFile (string filePath, [NullAllowed] MLNBatchedOfflinePackAdditionCompletionHandler completion);

	// -(void)addContentsOfURL:(NSUrl * _Nonnull)fileURL withCompletionHandler:(MLNBatchedOfflinePackAdditionCompletionHandler _Nullable)completion;
	[Export ("addContentsOfURL:withCompletionHandler:")]
	void AddContentsOfURL (NSUrl fileURL, [NullAllowed] MLNBatchedOfflinePackAdditionCompletionHandler completion);

	// @property (readonly, nonatomic, strong) NSArray<MLNOfflinePack *> * _Nullable packs;
	[NullAllowed, Export ("packs", ArgumentSemantic.Strong)]
	MLNOfflinePack[] Packs { get; }

	// -(void)addPackForRegion:(id<MLNOfflineRegion> _Nonnull)region withContext:(NSData * _Nonnull)context completionHandler:(MLNOfflinePackAdditionCompletionHandler _Nullable)completion;
	[Export ("addPackForRegion:withContext:completionHandler:")]
	void AddPackForRegion (MLNOfflineRegion region, NSData context, [NullAllowed] MLNOfflinePackAdditionCompletionHandler completion);

	// -(void)removePack:(MLNOfflinePack * _Nonnull)pack withCompletionHandler:(MLNOfflinePackRemovalCompletionHandler _Nullable)completion;
	[Export ("removePack:withCompletionHandler:")]
	void RemovePack (MLNOfflinePack pack, [NullAllowed] MLNOfflinePackRemovalCompletionHandler completion);

	// -(void)invalidatePack:(MLNOfflinePack * _Nonnull)pack withCompletionHandler:(void (^ _Nonnull)(NSError * _Nullable))completion;
	[Export ("invalidatePack:withCompletionHandler:")]
	void InvalidatePack (MLNOfflinePack pack, Action<NSError> completion);

	// -(void)reloadPacks;
	[Export ("reloadPacks")]
	void ReloadPacks ();

	// -(void)setMaximumAllowedMapboxTiles:(uint64_t)maximumCount;
	[Export ("setMaximumAllowedMapboxTiles:")]
	void SetMaximumAllowedMapboxTiles (ulong maximumCount);

	// @property (readonly, nonatomic) unsigned long long countOfBytesCompleted;
	[Export ("countOfBytesCompleted")]
	ulong CountOfBytesCompleted { get; }

	// -(void)setMaximumAmbientCacheSize:(NSUInteger)cacheSize withCompletionHandler:(void (^ _Nonnull)(NSError * _Nullable))completion;
	[Export ("setMaximumAmbientCacheSize:withCompletionHandler:")]
	void SetMaximumAmbientCacheSize (nuint cacheSize, Action<NSError> completion);

	// -(void)invalidateAmbientCacheWithCompletionHandler:(void (^ _Nonnull)(NSError * _Nullable))completion;
	[Export ("invalidateAmbientCacheWithCompletionHandler:")]
	void InvalidateAmbientCacheWithCompletionHandler (Action<NSError> completion);

	// -(void)clearAmbientCacheWithCompletionHandler:(void (^ _Nonnull)(NSError * _Nullable))completion;
	[Export ("clearAmbientCacheWithCompletionHandler:")]
	void ClearAmbientCacheWithCompletionHandler (Action<NSError> completion);

	// -(void)resetDatabaseWithCompletionHandler:(void (^ _Nonnull)(NSError * _Nullable))completion;
	[Export ("resetDatabaseWithCompletionHandler:")]
	void ResetDatabaseWithCompletionHandler (Action<NSError> completion);

	// -(void)preloadData:(NSData * _Nonnull)data forURL:(NSUrl * _Nonnull)url modificationDate:(NSDate * _Nullable)modified expirationDate:(NSDate * _Nullable)expires eTag:(NSString * _Nullable)eTag mustRevalidate:(BOOL)mustRevalidate __attribute__((swift_name("preload(_:for:modifiedOn:expiresOn:eTag:mustRevalidate:)")));
	[Export ("preloadData:forURL:modificationDate:expirationDate:eTag:mustRevalidate:")]
	void PreloadData (NSData data, NSUrl url, [NullAllowed] NSDate modified, [NullAllowed] NSDate expires, [NullAllowed] string eTag, bool mustRevalidate);

	// -(void)putResourceWithUrl:(NSUrl * _Nonnull)url data:(NSData * _Nonnull)data modified:(NSDate * _Nullable)modified expires:(NSDate * _Nullable)expires etag:(NSString * _Nullable)etag mustRevalidate:(BOOL)mustRevalidate __attribute__((deprecated("", "-preloadData:forURL:modificationDate:expirationDate:eTag:mustRevalidate:")));
	[Export ("putResourceWithUrl:data:modified:expires:etag:mustRevalidate:")]
	void PutResourceWithUrl (NSUrl url, NSData data, [NullAllowed] NSDate modified, [NullAllowed] NSDate expires, [NullAllowed] string etag, bool mustRevalidate);

	// -(void)preloadData:(NSData * _Nonnull)data forURL:(NSUrl * _Nonnull)url modificationDate:(NSDate * _Nullable)modified expirationDate:(NSDate * _Nullable)expires eTag:(NSString * _Nullable)eTag mustRevalidate:(BOOL)mustRevalidate completionHandler:(MLNOfflinePreloadDataCompletionHandler _Nullable)completion;
	[Export ("preloadData:forURL:modificationDate:expirationDate:eTag:mustRevalidate:completionHandler:")]
	void PreloadData (NSData data, NSUrl url, [NullAllowed] NSDate modified, [NullAllowed] NSDate expires, [NullAllowed] string eTag, bool mustRevalidate, [NullAllowed] MLNOfflinePreloadDataCompletionHandler completion);
}

// @protocol MLNOfflineStorageDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNOfflineStorageDelegate
{
	// @required -(NSUrl * _Nonnull)offlineStorage:(MLNOfflineStorage * _Nonnull)storage URLForResourceOfKind:(MLNResourceKind)kind withURL:(NSUrl * _Nonnull)url;
	[Abstract]
	[Export ("offlineStorage:URLForResourceOfKind:withURL:")]
	NSUrl URLForResourceOfKind (MLNOfflineStorage storage, MLNResourceKind kind, NSUrl url);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionMinimumZoomLevel __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionMinimumZoomLevel", "__Internal")]
	NSString MLNTileSourceOptionMinimumZoomLevel { get; }

	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionMaximumZoomLevel __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionMaximumZoomLevel", "__Internal")]
	NSString MLNTileSourceOptionMaximumZoomLevel { get; }

	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionCoordinateBounds __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionCoordinateBounds", "__Internal")]
	NSString MLNTileSourceOptionCoordinateBounds { get; }

	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionAttributionHTMLString __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionAttributionHTMLString", "__Internal")]
	NSString MLNTileSourceOptionAttributionHTMLString { get; }

	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionAttributionInfos __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionAttributionInfos", "__Internal")]
	NSString MLNTileSourceOptionAttributionInfos { get; }

	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionTileCoordinateSystem __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionTileCoordinateSystem", "__Internal")]
	NSString MLNTileSourceOptionTileCoordinateSystem { get; }
}

// @interface MLNTileSource : MLNSource
[BaseType (typeof(MLNSource))]
interface MLNTileSource
{
	// @property (readonly, copy, nonatomic) NSUrl * _Nullable configurationURL;
	[NullAllowed, Export ("configurationURL", ArgumentSemantic.Copy)]
	NSUrl ConfigurationURL { get; }

	// @property (readonly, copy, nonatomic) NSArray<MLNAttributionInfo *> * _Nonnull attributionInfos;
	[Export ("attributionInfos", ArgumentSemantic.Copy)]
	MLNAttributionInfo[] AttributionInfos { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable attributionHTMLString;
	[NullAllowed, Export ("attributionHTMLString")]
	string AttributionHTMLString { get; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNTileSourceOption _Nonnull MLNTileSourceOptionTileSize __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionTileSize", "__Internal")]
	NSString MLNTileSourceOptionTileSize { get; }
}

// @interface MLNRasterTileSource : MLNTileSource
[BaseType (typeof(MLNTileSource))]
interface MLNRasterTileSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSUrl * _Nonnull)configurationURL;
	[Export ("initWithIdentifier:configurationURL:")]
	NativeHandle Constructor (string identifier, NSUrl configurationURL);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSUrl * _Nonnull)configurationURL tileSize:(CGFloat)tileSize __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:configurationURL:tileSize:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, NSUrl configurationURL, nfloat tileSize);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier tileURLTemplates:(NSArray<NSString *> * _Nonnull)tileURLTemplates options:(NSDictionary<MLNTileSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:tileURLTemplates:options:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, string[] tileURLTemplates, [NullAllowed] NSDictionary<NSString, NSObject> options);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNTileSourceOption MLNTileSourceOptionDEMEncoding __attribute__((visibility("default")));
	[Field ("MLNTileSourceOptionDEMEncoding", "__Internal")]
	NSString MLNTileSourceOptionDEMEncoding { get; }
}

// @interface MLNRasterDEMSource : MLNRasterTileSource
[BaseType (typeof(MLNRasterTileSource))]
interface MLNRasterDEMSource
{
}

// @interface MLNRasterStyleLayer : MLNForegroundStyleLayer
[BaseType (typeof(MLNForegroundStyleLayer))]
interface MLNRasterStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified maximumRasterBrightness;
	[NullAllowed, Export ("maximumRasterBrightness", ArgumentSemantic.Assign)]
	NSExpression MaximumRasterBrightness { get; set; }

	// @property (nonatomic) MLNTransition maximumRasterBrightnessTransition;
	[Export ("maximumRasterBrightnessTransition", ArgumentSemantic.Assign)]
	MLNTransition MaximumRasterBrightnessTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified minimumRasterBrightness;
	[NullAllowed, Export ("minimumRasterBrightness", ArgumentSemantic.Assign)]
	NSExpression MinimumRasterBrightness { get; set; }

	// @property (nonatomic) MLNTransition minimumRasterBrightnessTransition;
	[Export ("minimumRasterBrightnessTransition", ArgumentSemantic.Assign)]
	MLNTransition MinimumRasterBrightnessTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterContrast;
	[NullAllowed, Export ("rasterContrast", ArgumentSemantic.Assign)]
	NSExpression RasterContrast { get; set; }

	// @property (nonatomic) MLNTransition rasterContrastTransition;
	[Export ("rasterContrastTransition", ArgumentSemantic.Assign)]
	MLNTransition RasterContrastTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterFadeDuration;
	[NullAllowed, Export ("rasterFadeDuration", ArgumentSemantic.Assign)]
	NSExpression RasterFadeDuration { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterHueRotation;
	[NullAllowed, Export ("rasterHueRotation", ArgumentSemantic.Assign)]
	NSExpression RasterHueRotation { get; set; }

	// @property (nonatomic) MLNTransition rasterHueRotationTransition;
	[Export ("rasterHueRotationTransition", ArgumentSemantic.Assign)]
	MLNTransition RasterHueRotationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterOpacity;
	[NullAllowed, Export ("rasterOpacity", ArgumentSemantic.Assign)]
	NSExpression RasterOpacity { get; set; }

	// @property (nonatomic) MLNTransition rasterOpacityTransition;
	[Export ("rasterOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition RasterOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterResamplingMode;
	[NullAllowed, Export ("rasterResamplingMode", ArgumentSemantic.Assign)]
	NSExpression RasterResamplingMode { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified rasterSaturation;
	[NullAllowed, Export ("rasterSaturation", ArgumentSemantic.Assign)]
	NSExpression RasterSaturation { get; set; }

	// @property (nonatomic) MLNTransition rasterSaturationTransition;
	[Export ("rasterSaturationTransition", ArgumentSemantic.Assign)]
	MLNTransition RasterSaturationTransition { get; set; }
}

// @interface MLNRasterStyleLayerAdditions (NSValue)
#if false
[Category]
[BaseType (typeof(NSValue))]
interface NSValue_MLNRasterStyleLayerAdditions
{
	// +(instancetype _Nonnull)valueWithMLNRasterResamplingMode:(MLNRasterResamplingMode)rasterResamplingMode;
	[Static]
	[Export ("valueWithMLNRasterResamplingMode:")]
	NSValue ValueWithMLNRasterResamplingMode (MLNRasterResamplingMode rasterResamplingMode);

	// @property (readonly) MLNRasterResamplingMode MLNRasterResamplingModeValue;
	[Export ("MLNRasterResamplingModeValue")]
	MLNRasterResamplingMode MLNRasterResamplingModeValue { get; }
}
#endif

// @interface MLNShapeOfflineRegion : NSObject <MLNOfflineRegion, NSSecureCoding, NSCopying>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface MLNShapeOfflineRegion : IMLNOfflineRegion, INSSecureCoding, INSCopying
{
	// @property (readonly, nonatomic) MLNShape * _Nonnull shape;
	[Export ("shape")]
	MLNShape Shape { get; }

	// @property (readonly, nonatomic) double minimumZoomLevel;
	[Export ("minimumZoomLevel")]
	double MinimumZoomLevel { get; }

	// @property (readonly, nonatomic) double maximumZoomLevel;
	[Export ("maximumZoomLevel")]
	double MaximumZoomLevel { get; }

	// -(instancetype _Nonnull)initWithStyleURL:(NSUrl * _Nullable)styleURL shape:(MLNShape * _Nonnull)shape fromZoomLevel:(double)minimumZoomLevel toZoomLevel:(double)maximumZoomLevel __attribute__((objc_designated_initializer));
	[Export ("initWithStyleURL:shape:fromZoomLevel:toZoomLevel:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] NSUrl styleURL, MLNShape shape, double minimumZoomLevel, double maximumZoomLevel);
}

// @interface MLNSymbolStyleLayer : MLNVectorStyleLayer
[BaseType (typeof(MLNVectorStyleLayer))]
interface MLNSymbolStyleLayer
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MLNSource * _Nonnull)source;
	[Export ("initWithIdentifier:source:")]
	NativeHandle Constructor (string identifier, MLNSource source);

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconAllowsOverlap;
	[NullAllowed, Export ("iconAllowsOverlap", ArgumentSemantic.Assign)]
	NSExpression IconAllowsOverlap { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconAnchor;
	[NullAllowed, Export ("iconAnchor", ArgumentSemantic.Assign)]
	NSExpression IconAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconIgnoresPlacement;
	[NullAllowed, Export ("iconIgnoresPlacement", ArgumentSemantic.Assign)]
	NSExpression IconIgnoresPlacement { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconImageName;
	[NullAllowed, Export ("iconImageName", ArgumentSemantic.Assign)]
	NSExpression IconImageName { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconOffset;
	[NullAllowed, Export ("iconOffset", ArgumentSemantic.Assign)]
	NSExpression IconOffset { get; set; }

	// @property (getter = isIconOptional, nonatomic, null_resettable) NSExpression * _Null_unspecified iconOptional;
	[NullAllowed, Export ("iconOptional", ArgumentSemantic.Assign)]
	NSExpression IconOptional { [Bind ("isIconOptional")] get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconPadding;
	[NullAllowed, Export ("iconPadding", ArgumentSemantic.Assign)]
	NSExpression IconPadding { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconPitchAlignment;
	[NullAllowed, Export ("iconPitchAlignment", ArgumentSemantic.Assign)]
	NSExpression IconPitchAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconRotation;
	[NullAllowed, Export ("iconRotation", ArgumentSemantic.Assign)]
	NSExpression IconRotation { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconRotationAlignment;
	[NullAllowed, Export ("iconRotationAlignment", ArgumentSemantic.Assign)]
	NSExpression IconRotationAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconScale;
	[NullAllowed, Export ("iconScale", ArgumentSemantic.Assign)]
	NSExpression IconScale { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconTextFit;
	[NullAllowed, Export ("iconTextFit", ArgumentSemantic.Assign)]
	NSExpression IconTextFit { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconTextFitPadding;
	[NullAllowed, Export ("iconTextFitPadding", ArgumentSemantic.Assign)]
	NSExpression IconTextFitPadding { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified keepsIconUpright;
	[NullAllowed, Export ("keepsIconUpright", ArgumentSemantic.Assign)]
	NSExpression KeepsIconUpright { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified keepsTextUpright;
	[NullAllowed, Export ("keepsTextUpright", ArgumentSemantic.Assign)]
	NSExpression KeepsTextUpright { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified maximumTextAngle;
	[NullAllowed, Export ("maximumTextAngle", ArgumentSemantic.Assign)]
	NSExpression MaximumTextAngle { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified maximumTextWidth;
	[NullAllowed, Export ("maximumTextWidth", ArgumentSemantic.Assign)]
	NSExpression MaximumTextWidth { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolAvoidsEdges;
	[NullAllowed, Export ("symbolAvoidsEdges", ArgumentSemantic.Assign)]
	NSExpression SymbolAvoidsEdges { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolPlacement;
	[NullAllowed, Export ("symbolPlacement", ArgumentSemantic.Assign)]
	NSExpression SymbolPlacement { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolScreenSpace;
	[NullAllowed, Export ("symbolScreenSpace", ArgumentSemantic.Assign)]
	NSExpression SymbolScreenSpace { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolSortKey;
	[NullAllowed, Export ("symbolSortKey", ArgumentSemantic.Assign)]
	NSExpression SymbolSortKey { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolSpacing;
	[NullAllowed, Export ("symbolSpacing", ArgumentSemantic.Assign)]
	NSExpression SymbolSpacing { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified symbolZOrder;
	[NullAllowed, Export ("symbolZOrder", ArgumentSemantic.Assign)]
	NSExpression SymbolZOrder { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified text;
	[NullAllowed, Export ("text", ArgumentSemantic.Assign)]
	NSExpression Text { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textAllowsOverlap;
	[NullAllowed, Export ("textAllowsOverlap", ArgumentSemantic.Assign)]
	NSExpression TextAllowsOverlap { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textAnchor;
	[NullAllowed, Export ("textAnchor", ArgumentSemantic.Assign)]
	NSExpression TextAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textFontNames;
	[NullAllowed, Export ("textFontNames", ArgumentSemantic.Assign)]
	NSExpression TextFontNames { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textFontSize;
	[NullAllowed, Export ("textFontSize", ArgumentSemantic.Assign)]
	NSExpression TextFontSize { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textIgnoresPlacement;
	[NullAllowed, Export ("textIgnoresPlacement", ArgumentSemantic.Assign)]
	NSExpression TextIgnoresPlacement { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textJustification;
	[NullAllowed, Export ("textJustification", ArgumentSemantic.Assign)]
	NSExpression TextJustification { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textLetterSpacing;
	[NullAllowed, Export ("textLetterSpacing", ArgumentSemantic.Assign)]
	NSExpression TextLetterSpacing { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textLineHeight;
	[NullAllowed, Export ("textLineHeight", ArgumentSemantic.Assign)]
	NSExpression TextLineHeight { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textOffset;
	[NullAllowed, Export ("textOffset", ArgumentSemantic.Assign)]
	NSExpression TextOffset { get; set; }

	// @property (getter = isTextOptional, nonatomic, null_resettable) NSExpression * _Null_unspecified textOptional;
	[NullAllowed, Export ("textOptional", ArgumentSemantic.Assign)]
	NSExpression TextOptional { [Bind ("isTextOptional")] get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textPadding;
	[NullAllowed, Export ("textPadding", ArgumentSemantic.Assign)]
	NSExpression TextPadding { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textPitchAlignment;
	[NullAllowed, Export ("textPitchAlignment", ArgumentSemantic.Assign)]
	NSExpression TextPitchAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textRadialOffset;
	[NullAllowed, Export ("textRadialOffset", ArgumentSemantic.Assign)]
	NSExpression TextRadialOffset { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textRotation;
	[NullAllowed, Export ("textRotation", ArgumentSemantic.Assign)]
	NSExpression TextRotation { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textRotationAlignment;
	[NullAllowed, Export ("textRotationAlignment", ArgumentSemantic.Assign)]
	NSExpression TextRotationAlignment { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textTransform;
	[NullAllowed, Export ("textTransform", ArgumentSemantic.Assign)]
	NSExpression TextTransform { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textVariableAnchor;
	[NullAllowed, Export ("textVariableAnchor", ArgumentSemantic.Assign)]
	NSExpression TextVariableAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textVariableAnchorOffset;
	[NullAllowed, Export ("textVariableAnchorOffset", ArgumentSemantic.Assign)]
	NSExpression TextVariableAnchorOffset { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textWritingModes;
	[NullAllowed, Export ("textWritingModes", ArgumentSemantic.Assign)]
	NSExpression TextWritingModes { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconColor;
	[NullAllowed, Export ("iconColor", ArgumentSemantic.Assign)]
	NSExpression IconColor { get; set; }

	// @property (nonatomic) MLNTransition iconColorTransition;
	[Export ("iconColorTransition", ArgumentSemantic.Assign)]
	MLNTransition IconColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconHaloBlur;
	[NullAllowed, Export ("iconHaloBlur", ArgumentSemantic.Assign)]
	NSExpression IconHaloBlur { get; set; }

	// @property (nonatomic) MLNTransition iconHaloBlurTransition;
	[Export ("iconHaloBlurTransition", ArgumentSemantic.Assign)]
	MLNTransition IconHaloBlurTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconHaloColor;
	[NullAllowed, Export ("iconHaloColor", ArgumentSemantic.Assign)]
	NSExpression IconHaloColor { get; set; }

	// @property (nonatomic) MLNTransition iconHaloColorTransition;
	[Export ("iconHaloColorTransition", ArgumentSemantic.Assign)]
	MLNTransition IconHaloColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconHaloWidth;
	[NullAllowed, Export ("iconHaloWidth", ArgumentSemantic.Assign)]
	NSExpression IconHaloWidth { get; set; }

	// @property (nonatomic) MLNTransition iconHaloWidthTransition;
	[Export ("iconHaloWidthTransition", ArgumentSemantic.Assign)]
	MLNTransition IconHaloWidthTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconOpacity;
	[NullAllowed, Export ("iconOpacity", ArgumentSemantic.Assign)]
	NSExpression IconOpacity { get; set; }

	// @property (nonatomic) MLNTransition iconOpacityTransition;
	[Export ("iconOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition IconOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconTranslation;
	[NullAllowed, Export ("iconTranslation", ArgumentSemantic.Assign)]
	NSExpression IconTranslation { get; set; }

	// @property (nonatomic) MLNTransition iconTranslationTransition;
	[Export ("iconTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition IconTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified iconTranslationAnchor;
	[NullAllowed, Export ("iconTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression IconTranslationAnchor { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textColor;
	[NullAllowed, Export ("textColor", ArgumentSemantic.Assign)]
	NSExpression TextColor { get; set; }

	// @property (nonatomic) MLNTransition textColorTransition;
	[Export ("textColorTransition", ArgumentSemantic.Assign)]
	MLNTransition TextColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textHaloBlur;
	[NullAllowed, Export ("textHaloBlur", ArgumentSemantic.Assign)]
	NSExpression TextHaloBlur { get; set; }

	// @property (nonatomic) MLNTransition textHaloBlurTransition;
	[Export ("textHaloBlurTransition", ArgumentSemantic.Assign)]
	MLNTransition TextHaloBlurTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textHaloColor;
	[NullAllowed, Export ("textHaloColor", ArgumentSemantic.Assign)]
	NSExpression TextHaloColor { get; set; }

	// @property (nonatomic) MLNTransition textHaloColorTransition;
	[Export ("textHaloColorTransition", ArgumentSemantic.Assign)]
	MLNTransition TextHaloColorTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textHaloWidth;
	[NullAllowed, Export ("textHaloWidth", ArgumentSemantic.Assign)]
	NSExpression TextHaloWidth { get; set; }

	// @property (nonatomic) MLNTransition textHaloWidthTransition;
	[Export ("textHaloWidthTransition", ArgumentSemantic.Assign)]
	MLNTransition TextHaloWidthTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textOpacity;
	[NullAllowed, Export ("textOpacity", ArgumentSemantic.Assign)]
	NSExpression TextOpacity { get; set; }

	// @property (nonatomic) MLNTransition textOpacityTransition;
	[Export ("textOpacityTransition", ArgumentSemantic.Assign)]
	MLNTransition TextOpacityTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textTranslation;
	[NullAllowed, Export ("textTranslation", ArgumentSemantic.Assign)]
	NSExpression TextTranslation { get; set; }

	// @property (nonatomic) MLNTransition textTranslationTransition;
	[Export ("textTranslationTransition", ArgumentSemantic.Assign)]
	MLNTransition TextTranslationTransition { get; set; }

	// @property (nonatomic, null_resettable) NSExpression * _Null_unspecified textTranslationAnchor;
	[NullAllowed, Export ("textTranslationAnchor", ArgumentSemantic.Assign)]
	NSExpression TextTranslationAnchor { get; set; }
}

// @interface MLNSymbolStyleLayerAdditions (NSValue)
#if false
[Category]
[BaseType (typeof(NSValue))]
interface NSValue_MLNSymbolStyleLayerAdditions
{
	// +(instancetype _Nonnull)valueWithMLNIconAnchor:(MLNIconAnchor)iconAnchor;
	[Static]
	[Export ("valueWithMLNIconAnchor:")]
	NSValue ValueWithMLNIconAnchor (MLNIconAnchor iconAnchor);

	// @property (readonly) MLNIconAnchor MLNIconAnchorValue;
	[Export ("MLNIconAnchorValue")]
	MLNIconAnchor MLNIconAnchorValue { get; }

	// +(instancetype _Nonnull)valueWithMLNIconPitchAlignment:(MLNIconPitchAlignment)iconPitchAlignment;
	[Static]
	[Export ("valueWithMLNIconPitchAlignment:")]
	NSValue ValueWithMLNIconPitchAlignment (MLNIconPitchAlignment iconPitchAlignment);

	// @property (readonly) MLNIconPitchAlignment MLNIconPitchAlignmentValue;
	[Export ("MLNIconPitchAlignmentValue")]
	MLNIconPitchAlignment MLNIconPitchAlignmentValue { get; }

	// +(instancetype _Nonnull)valueWithMLNIconRotationAlignment:(MLNIconRotationAlignment)iconRotationAlignment;
	[Static]
	[Export ("valueWithMLNIconRotationAlignment:")]
	NSValue ValueWithMLNIconRotationAlignment (MLNIconRotationAlignment iconRotationAlignment);

	// @property (readonly) MLNIconRotationAlignment MLNIconRotationAlignmentValue;
	[Export ("MLNIconRotationAlignmentValue")]
	MLNIconRotationAlignment MLNIconRotationAlignmentValue { get; }

	// +(instancetype _Nonnull)valueWithMLNIconTextFit:(MLNIconTextFit)iconTextFit;
	[Static]
	[Export ("valueWithMLNIconTextFit:")]
	NSValue ValueWithMLNIconTextFit (MLNIconTextFit iconTextFit);

	// @property (readonly) MLNIconTextFit MLNIconTextFitValue;
	[Export ("MLNIconTextFitValue")]
	MLNIconTextFit MLNIconTextFitValue { get; }

	// +(instancetype _Nonnull)valueWithMLNSymbolPlacement:(MLNSymbolPlacement)symbolPlacement;
	[Static]
	[Export ("valueWithMLNSymbolPlacement:")]
	NSValue ValueWithMLNSymbolPlacement (MLNSymbolPlacement symbolPlacement);

	// @property (readonly) MLNSymbolPlacement MLNSymbolPlacementValue;
	[Export ("MLNSymbolPlacementValue")]
	MLNSymbolPlacement MLNSymbolPlacementValue { get; }

	// +(instancetype _Nonnull)valueWithMLNSymbolZOrder:(MLNSymbolZOrder)symbolZOrder;
	[Static]
	[Export ("valueWithMLNSymbolZOrder:")]
	NSValue ValueWithMLNSymbolZOrder (MLNSymbolZOrder symbolZOrder);

	// @property (readonly) MLNSymbolZOrder MLNSymbolZOrderValue;
	[Export ("MLNSymbolZOrderValue")]
	MLNSymbolZOrder MLNSymbolZOrderValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextAnchor:(MLNTextAnchor)textAnchor;
	[Static]
	[Export ("valueWithMLNTextAnchor:")]
	NSValue ValueWithMLNTextAnchor (MLNTextAnchor textAnchor);

	// @property (readonly) MLNTextAnchor MLNTextAnchorValue;
	[Export ("MLNTextAnchorValue")]
	MLNTextAnchor MLNTextAnchorValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextJustification:(MLNTextJustification)textJustification;
	[Static]
	[Export ("valueWithMLNTextJustification:")]
	NSValue ValueWithMLNTextJustification (MLNTextJustification textJustification);

	// @property (readonly) MLNTextJustification MLNTextJustificationValue;
	[Export ("MLNTextJustificationValue")]
	MLNTextJustification MLNTextJustificationValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextPitchAlignment:(MLNTextPitchAlignment)textPitchAlignment;
	[Static]
	[Export ("valueWithMLNTextPitchAlignment:")]
	NSValue ValueWithMLNTextPitchAlignment (MLNTextPitchAlignment textPitchAlignment);

	// @property (readonly) MLNTextPitchAlignment MLNTextPitchAlignmentValue;
	[Export ("MLNTextPitchAlignmentValue")]
	MLNTextPitchAlignment MLNTextPitchAlignmentValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextRotationAlignment:(MLNTextRotationAlignment)textRotationAlignment;
	[Static]
	[Export ("valueWithMLNTextRotationAlignment:")]
	NSValue ValueWithMLNTextRotationAlignment (MLNTextRotationAlignment textRotationAlignment);

	// @property (readonly) MLNTextRotationAlignment MLNTextRotationAlignmentValue;
	[Export ("MLNTextRotationAlignmentValue")]
	MLNTextRotationAlignment MLNTextRotationAlignmentValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextTransform:(MLNTextTransform)textTransform;
	[Static]
	[Export ("valueWithMLNTextTransform:")]
	NSValue ValueWithMLNTextTransform (MLNTextTransform textTransform);

	// @property (readonly) MLNTextTransform MLNTextTransformValue;
	[Export ("MLNTextTransformValue")]
	MLNTextTransform MLNTextTransformValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextWritingMode:(MLNTextWritingMode)textWritingModes;
	[Static]
	[Export ("valueWithMLNTextWritingMode:")]
	NSValue ValueWithMLNTextWritingMode (MLNTextWritingMode textWritingModes);

	// @property (readonly) MLNTextWritingMode MLNTextWritingModeValue;
	[Export ("MLNTextWritingModeValue")]
	MLNTextWritingMode MLNTextWritingModeValue { get; }

	// +(instancetype _Nonnull)valueWithMLNIconTranslationAnchor:(MLNIconTranslationAnchor)iconTranslationAnchor;
	[Static]
	[Export ("valueWithMLNIconTranslationAnchor:")]
	NSValue ValueWithMLNIconTranslationAnchor (MLNIconTranslationAnchor iconTranslationAnchor);

	// @property (readonly) MLNIconTranslationAnchor MLNIconTranslationAnchorValue;
	[Export ("MLNIconTranslationAnchorValue")]
	MLNIconTranslationAnchor MLNIconTranslationAnchorValue { get; }

	// +(instancetype _Nonnull)valueWithMLNTextTranslationAnchor:(MLNTextTranslationAnchor)textTranslationAnchor;
	[Static]
	[Export ("valueWithMLNTextTranslationAnchor:")]
	NSValue ValueWithMLNTextTranslationAnchor (MLNTextTranslationAnchor textTranslationAnchor);

	// @property (readonly) MLNTextTranslationAnchor MLNTextTranslationAnchorValue;
	[Export ("MLNTextTranslationAnchorValue")]
	MLNTextTranslationAnchor MLNTextTranslationAnchorValue { get; }
}
#endif

// @interface MLNTilePyramidOfflineRegion : NSObject <MLNOfflineRegion, NSSecureCoding, NSCopying>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface MLNTilePyramidOfflineRegion : IMLNOfflineRegion, INSSecureCoding, INSCopying
{
	// @property (readonly, nonatomic) MLNCoordinateBounds bounds;
	[Export ("bounds")]
	MLNCoordinateBounds Bounds { get; }

	// @property (readonly, nonatomic) double minimumZoomLevel;
	[Export ("minimumZoomLevel")]
	double MinimumZoomLevel { get; }

	// @property (readonly, nonatomic) double maximumZoomLevel;
	[Export ("maximumZoomLevel")]
	double MaximumZoomLevel { get; }

	// -(instancetype _Nonnull)initWithStyleURL:(NSUrl * _Nullable)styleURL bounds:(MLNCoordinateBounds)bounds fromZoomLevel:(double)minimumZoomLevel toZoomLevel:(double)maximumZoomLevel __attribute__((objc_designated_initializer));
	[Export ("initWithStyleURL:bounds:fromZoomLevel:toZoomLevel:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] NSUrl styleURL, MLNCoordinateBounds bounds, double minimumZoomLevel, double maximumZoomLevel);
}

// @interface MLNUserLocation : NSObject <MLNAnnotation, NSSecureCoding>
[BaseType (typeof(NSObject))]
interface MLNUserLocation : IMLNAnnotation, INSSecureCoding
{
	// @property (readonly, nonatomic) CLLocation * _Nullable location;
	[NullAllowed, Export ("location")]
	CLLocation Location { get; }

	// @property (readonly, getter = isUpdating, nonatomic) BOOL updating;
	[Export ("updating")]
	bool Updating { [Bind ("isUpdating")] get; }

	// @property (readonly, nonatomic) CLHeading * _Nullable heading;
	[NullAllowed, Export ("heading")]
	CLHeading Heading { get; }

	// @property (copy, nonatomic) NSString * _Nonnull title;
	[Export ("title")]
	string Title { get; set; }

	// @property (copy, nonatomic) NSString * _Nullable subtitle;
	[NullAllowed, Export ("subtitle")]
	string Subtitle { get; set; }
}

// @interface MLNUserLocationAnnotationView : MLNAnnotationView
[BaseType (typeof(MLNAnnotationView))]
interface MLNUserLocationAnnotationView
{
	// @property (readonly, nonatomic, weak) MLNMapView * _Nullable mapView;
	[NullAllowed, Export ("mapView", ArgumentSemantic.Weak)]
	MLNMapView MapView { get; }

	// @property (readonly, nonatomic, weak) MLNUserLocation * _Nullable userLocation;
	[NullAllowed, Export ("userLocation", ArgumentSemantic.Weak)]
	MLNUserLocation UserLocation { get; }

	// @property (readonly, nonatomic, weak) CALayer * _Nullable hitTestLayer;
	[NullAllowed, Export ("hitTestLayer", ArgumentSemantic.Weak)]
	CALayer HitTestLayer { get; }

	// -(void)update;
	[Export ("update")]
	void Update ();
}

// @interface MLNUserLocationAnnotationViewStyle : NSObject
[BaseType (typeof(NSObject))]
interface MLNUserLocationAnnotationViewStyle
{
	// @property (nonatomic) UIColor * _Nonnull puckFillColor;
	[Export ("puckFillColor", ArgumentSemantic.Assign)]
	UIColor PuckFillColor { get; set; }

	// @property (nonatomic) UIColor * _Nonnull puckShadowColor;
	[Export ("puckShadowColor", ArgumentSemantic.Assign)]
	UIColor PuckShadowColor { get; set; }

	// @property (assign, nonatomic) CGFloat puckShadowOpacity;
	[Export ("puckShadowOpacity")]
	nfloat PuckShadowOpacity { get; set; }

	// @property (nonatomic) UIColor * _Nonnull puckArrowFillColor;
	[Export ("puckArrowFillColor", ArgumentSemantic.Assign)]
	UIColor PuckArrowFillColor { get; set; }

	// @property (nonatomic) UIColor * _Nonnull haloFillColor;
	[Export ("haloFillColor", ArgumentSemantic.Assign)]
	UIColor HaloFillColor { get; set; }

	// @property (nonatomic) API_AVAILABLE(ios(14)) UIColor * approximateHaloFillColor __attribute__((availability(ios, introduced=14)));
	// [iOS (14, 0)]
	[Export ("approximateHaloFillColor", ArgumentSemantic.Assign)]
	UIColor ApproximateHaloFillColor { get; set; }

	// @property (nonatomic) API_AVAILABLE(ios(14)) UIColor * approximateHaloBorderColor __attribute__((availability(ios, introduced=14)));
	// [iOS (14, 0)]
	[Export ("approximateHaloBorderColor", ArgumentSemantic.Assign)]
	UIColor ApproximateHaloBorderColor { get; set; }

	// @property (assign, nonatomic) CGFloat approximateHaloBorderWidth __attribute__((availability(ios, introduced=14)));
	// [iOS (14, 0)]
	[Export ("approximateHaloBorderWidth")]
	nfloat ApproximateHaloBorderWidth { get; set; }

	// @property (assign, nonatomic) CGFloat approximateHaloOpacity __attribute__((availability(ios, introduced=14)));
	// [iOS (14, 0)]
	[Export ("approximateHaloOpacity")]
	nfloat ApproximateHaloOpacity { get; set; }
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNTileSourceOption _Nonnull MLNVectorTileSourceOptionEncoding __attribute__((visibility("default")));
	[Field ("MLNVectorTileSourceOptionEncoding", "__Internal")]
	NSString MLNVectorTileSourceOptionEncoding { get; }
}

// @interface MLNVectorTileSource : MLNTileSource
[BaseType (typeof(MLNTileSource))]
interface MLNVectorTileSource
{
	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSUrl * _Nonnull)configurationURL __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:configurationURL:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, NSUrl configurationURL);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURLString:(NSString * _Nonnull)configurationURLString __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:configurationURLString:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, string configurationURLString);

	// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier tileURLTemplates:(NSArray<NSString *> * _Nonnull)tileURLTemplates options:(NSDictionary<MLNTileSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
	[Export ("initWithIdentifier:tileURLTemplates:options:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string identifier, string[] tileURLTemplates, [NullAllowed] NSDictionary<NSString, NSObject> options);

	// -(NSArray<id<MLNFeature>> * _Nonnull)featuresInSourceLayersWithIdentifiers:(NSSet<NSString *> * _Nonnull)sourceLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate __attribute__((swift_name("features(sourceLayerIdentifiers:predicate:)")));
	[Export ("featuresInSourceLayersWithIdentifiers:predicate:")]
	IMLNFeature[] FeaturesInSourceLayersWithIdentifiers (NSSet<NSString> sourceLayerIdentifiers, [NullAllowed] NSPredicate predicate);
}

// [Static]
// [Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern const MLNExpressionInterpolationMode _Nonnull MLNExpressionInterpolationModeLinear __attribute__((visibility("default")));
	[Field ("MLNExpressionInterpolationModeLinear", "__Internal")]
	NSString MLNExpressionInterpolationModeLinear { get; }

	// extern const MLNExpressionInterpolationMode _Nonnull MLNExpressionInterpolationModeExponential __attribute__((visibility("default")));
	[Field ("MLNExpressionInterpolationModeExponential", "__Internal")]
	NSString MLNExpressionInterpolationModeExponential { get; }

	// extern const MLNExpressionInterpolationMode _Nonnull MLNExpressionInterpolationModeCubicBezier __attribute__((visibility("default")));
	[Field ("MLNExpressionInterpolationModeCubicBezier", "__Internal")]
	NSString MLNExpressionInterpolationModeCubicBezier { get; }
}

// @interface MLNAdditions (NSExpression)
#if false
[Category]
[BaseType (typeof(NSExpression))]
interface NSExpression_MLNAdditions
{
	// @property (readonly, nonatomic, class) NSExpression * _Nonnull zoomLevelVariableExpression;
	[Static]
	[Export ("zoomLevelVariableExpression")]
	NSExpression ZoomLevelVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull heatmapDensityVariableExpression;
	[Static]
	[Export ("heatmapDensityVariableExpression")]
	NSExpression HeatmapDensityVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull lineProgressVariableExpression;
	[Static]
	[Export ("lineProgressVariableExpression")]
	NSExpression LineProgressVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull geometryTypeVariableExpression;
	[Static]
	[Export ("geometryTypeVariableExpression")]
	NSExpression GeometryTypeVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull featureIdentifierVariableExpression;
	[Static]
	[Export ("featureIdentifierVariableExpression")]
	NSExpression FeatureIdentifierVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull featureAccumulatedVariableExpression;
	[Static]
	[Export ("featureAccumulatedVariableExpression")]
	NSExpression FeatureAccumulatedVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull featureAttributesVariableExpression;
	[Static]
	[Export ("featureAttributesVariableExpression")]
	NSExpression FeatureAttributesVariableExpression { get; }

	// @property (readonly, nonatomic, class) NSExpression * _Nonnull featurePropertiesVariableExpression __attribute__((deprecated("", "featureAttributesVariableExpression")));
	[Static]
	[Export ("featurePropertiesVariableExpression")]
	NSExpression FeaturePropertiesVariableExpression { get; }

	// +(instancetype _Nonnull)mgl_expressionForConditional:(NSPredicate * _Nonnull)conditionPredicate trueExpression:(NSExpression * _Nonnull)trueExpression falseExpresssion:(NSExpression * _Nonnull)falseExpression __attribute__((swift_name("init(forMLNConditional:trueExpression:falseExpression:)")));
	[Static]
	[Export ("mgl_expressionForConditional:trueExpression:falseExpresssion:")]
	NSExpression Mgl_expressionForConditional (NSPredicate conditionPredicate, NSExpression trueExpression, NSExpression falseExpression);

	// +(instancetype _Nonnull)mgl_expressionForSteppingExpression:(NSExpression * _Nonnull)steppingExpression fromExpression:(NSExpression * _Nonnull)minimumExpression stops:(NSExpression * _Nonnull)stops __attribute__((swift_name("init(forMLNStepping:from:stops:)")));
	[Static]
	[Export ("mgl_expressionForSteppingExpression:fromExpression:stops:")]
	NSExpression Mgl_expressionForSteppingExpression (NSExpression steppingExpression, NSExpression minimumExpression, NSExpression stops);

	// +(instancetype _Nonnull)mgl_expressionForInterpolatingExpression:(NSExpression * _Nonnull)inputExpression withCurveType:(MLNExpressionInterpolationMode _Nonnull)curveType parameters:(NSExpression * _Nullable)parameters stops:(NSExpression * _Nonnull)stops __attribute__((swift_name("init(forMLNInterpolating:curveType:parameters:stops:)")));
	[Static]
	[Export ("mgl_expressionForInterpolatingExpression:withCurveType:parameters:stops:")]
	NSExpression Mgl_expressionForInterpolatingExpression (NSExpression inputExpression, string curveType, [NullAllowed] NSExpression parameters, NSExpression stops);

	// +(instancetype _Nonnull)mgl_expressionForMatchingExpression:(NSExpression * _Nonnull)inputExpression inDictionary:(NSDictionary<NSExpression *,NSExpression *> * _Nonnull)matchedExpressions defaultExpression:(NSExpression * _Nonnull)defaultExpression __attribute__((swift_name("init(forMLNMatchingKey:in:default:)")));
	[Static]
	[Export ("mgl_expressionForMatchingExpression:inDictionary:defaultExpression:")]
	NSExpression Mgl_expressionForMatchingExpression (NSExpression inputExpression, NSDictionary<NSExpression, NSExpression> matchedExpressions, NSExpression defaultExpression);

	// +(instancetype _Nonnull)mgl_expressionForAttributedExpressions:(NSArray<NSExpression *> * _Nonnull)attributedExpressions __attribute__((swift_name("init(forAttributedExpressions:)")));
	[Static]
	[Export ("mgl_expressionForAttributedExpressions:")]
	NSExpression Mgl_expressionForAttributedExpressions (NSExpression[] attributedExpressions);

	// -(instancetype _Nonnull)mgl_expressionByAppendingExpression:(NSExpression * _Nonnull)expression __attribute__((swift_name("mgl_appending(_:)")));
	[Export ("mgl_expressionByAppendingExpression:")]
	NSExpression Mgl_expressionByAppendingExpression (NSExpression expression);

	// +(instancetype _Nonnull)expressionWithMLNJSONObject:(id _Nonnull)object __attribute__((swift_name("init(mglJSONObject:)")));
	[Static]
	[Export ("expressionWithMLNJSONObject:")]
	NSExpression ExpressionWithMLNJSONObject (NSObject @object);

	// @property (readonly, nonatomic) id _Nonnull mgl_jsonExpressionObject;
	[Export ("mgl_jsonExpressionObject")]
	NSObject Mgl_jsonExpressionObject { get; }

	// -(NSExpression * _Nonnull)mgl_expressionLocalizedIntoLocale:(NSLocale * _Nullable)locale;
	[Export ("mgl_expressionLocalizedIntoLocale:")]
	NSExpression Mgl_expressionLocalizedIntoLocale ([NullAllowed] NSLocale locale);
}

// @interface MLNAdditions (NSPredicate)
[Category]
[BaseType (typeof(NSPredicate))]
interface NSPredicate_MLNAdditions
{
	// +(instancetype _Nonnull)predicateWithMLNJSONObject:(id _Nonnull)object __attribute__((swift_name("init(mglJSONObject:)")));
	[Static]
	[Export ("predicateWithMLNJSONObject:")]
	NSPredicate PredicateWithMLNJSONObject (NSObject @object);

	// @property (readonly, nonatomic) id _Nonnull mgl_jsonExpressionObject;
	[Export ("mgl_jsonExpressionObject")]
	NSObject Mgl_jsonExpressionObject { get; }
}

// @interface MLNAdditions (NSValue)
[Category]
[BaseType (typeof(NSValue))]
interface NSValue_MLNAdditions
{
	// +(instancetype _Nonnull)valueWithMLNCoordinate:(CLLocationCoordinate2D)coordinate;
	[Static]
	[Export ("valueWithMLNCoordinate:")]
	NSValue ValueWithMLNCoordinate (CLLocationCoordinate2D coordinate);

	// @property (readonly) CLLocationCoordinate2D MLNCoordinateValue;
	[Export ("MLNCoordinateValue")]
	CLLocationCoordinate2D MLNCoordinateValue { get; }

	// +(instancetype _Nonnull)valueWithMLNMapPoint:(MLNMapPoint)point;
	[Static]
	[Export ("valueWithMLNMapPoint:")]
	NSValue ValueWithMLNMapPoint (MLNMapPoint point);

	// @property (readonly) MLNMapPoint MLNMapPointValue;
	[Export ("MLNMapPointValue")]
	MLNMapPoint MLNMapPointValue { get; }

	// +(instancetype _Nonnull)valueWithMLNCoordinateSpan:(MLNCoordinateSpan)span;
	[Static]
	[Export ("valueWithMLNCoordinateSpan:")]
	NSValue ValueWithMLNCoordinateSpan (MLNCoordinateSpan span);

	// @property (readonly) MLNCoordinateSpan MLNCoordinateSpanValue;
	[Export ("MLNCoordinateSpanValue")]
	MLNCoordinateSpan MLNCoordinateSpanValue { get; }

	// +(instancetype _Nonnull)valueWithMLNCoordinateBounds:(MLNCoordinateBounds)bounds;
	[Static]
	[Export ("valueWithMLNCoordinateBounds:")]
	NSValue ValueWithMLNCoordinateBounds (MLNCoordinateBounds bounds);

	// @property (readonly) MLNCoordinateBounds MLNCoordinateBoundsValue;
	[Export ("MLNCoordinateBoundsValue")]
	MLNCoordinateBounds MLNCoordinateBoundsValue { get; }

	// +(instancetype _Nonnull)valueWithMLNCoordinateQuad:(MLNCoordinateQuad)quad;
	[Static]
	[Export ("valueWithMLNCoordinateQuad:")]
	NSValue ValueWithMLNCoordinateQuad (MLNCoordinateQuad quad);

	// -(MLNCoordinateQuad)MLNCoordinateQuadValue;
	[Export ("MLNCoordinateQuadValue")]
	// [Verify (MethodToProperty)]
	MLNCoordinateQuad MLNCoordinateQuadValue { get; }

	// +(NSValue * _Nonnull)valueWithMLNOfflinePackProgress:(MLNOfflinePackProgress)progress;
	[Static]
	[Export ("valueWithMLNOfflinePackProgress:")]
	NSValue ValueWithMLNOfflinePackProgress (MLNOfflinePackProgress progress);

	// @property (readonly) MLNOfflinePackProgress MLNOfflinePackProgressValue;
	[Export ("MLNOfflinePackProgressValue")]
	MLNOfflinePackProgress MLNOfflinePackProgressValue { get; }

	// +(NSValue * _Nonnull)valueWithMLNTransition:(MLNTransition)transition;
	[Static]
	[Export ("valueWithMLNTransition:")]
	NSValue ValueWithMLNTransition (MLNTransition transition);

	// @property (readonly) MLNTransition MLNTransitionValue;
	[Export ("MLNTransitionValue")]
	MLNTransition MLNTransitionValue { get; }

	// +(instancetype _Nonnull)valueWithMLNSphericalPosition:(MLNSphericalPosition)lightPosition;
	[Static]
	[Export ("valueWithMLNSphericalPosition:")]
	NSValue ValueWithMLNSphericalPosition (MLNSphericalPosition lightPosition);

	// @property (readonly) MLNSphericalPosition MLNSphericalPositionValue;
	[Export ("MLNSphericalPositionValue")]
	MLNSphericalPosition MLNSphericalPositionValue { get; }

	// +(NSValue * _Nonnull)valueWithMLNLightAnchor:(MLNLightAnchor)lightAnchor;
	[Static]
	[Export ("valueWithMLNLightAnchor:")]
	NSValue ValueWithMLNLightAnchor (MLNLightAnchor lightAnchor);

	// @property (readonly) MLNLightAnchor MLNLightAnchorValue;
	[Export ("MLNLightAnchorValue")]
	MLNLightAnchor MLNLightAnchorValue { get; }
}
#endif

// @protocol MLNMapViewDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface MLNMapViewDelegate
{
	// @optional -(BOOL)mapView:(MLNMapView * _Nonnull)mapView shouldChangeFromCamera:(MLNMapCamera * _Nonnull)oldCamera toCamera:(MLNMapCamera * _Nonnull)newCamera;
	[Export ("mapView:shouldChangeFromCamera:toCamera:")]
	bool MapView (MLNMapView mapView, MLNMapCamera oldCamera, MLNMapCamera newCamera);

	// @optional -(BOOL)mapView:(MLNMapView * _Nonnull)mapView shouldChangeFromCamera:(MLNMapCamera * _Nonnull)oldCamera toCamera:(MLNMapCamera * _Nonnull)newCamera reason:(MLNCameraChangeReason)reason;
	[Export ("mapView:shouldChangeFromCamera:toCamera:reason:")]
	bool MapView (MLNMapView mapView, MLNMapCamera oldCamera, MLNMapCamera newCamera, MLNCameraChangeReason reason);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView regionWillChangeAnimated:(BOOL)animated;
	[Export ("mapView:regionWillChangeAnimated:")]
	void MapView (MLNMapView mapView, bool animated);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView regionWillChangeWithReason:(MLNCameraChangeReason)reason animated:(BOOL)animated;
	[Export ("mapView:regionWillChangeWithReason:animated:")]
	void MapView (MLNMapView mapView, MLNCameraChangeReason reason, bool animated);

	// @optional -(void)mapViewRegionIsChanging:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewRegionIsChanging:")]
	void MapViewRegionIsChanging (MLNMapView mapView);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView regionIsChangingWithReason:(MLNCameraChangeReason)reason;
	[Export ("mapView:regionIsChangingWithReason:")]
	void MapView (MLNMapView mapView, MLNCameraChangeReason reason);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView regionDidChangeAnimated:(BOOL)animated;
	[Export ("mapView:regionDidChangeAnimated:")]
	void MapViewRegionDidChangeAnimated (MLNMapView mapView, bool animated);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView regionDidChangeWithReason:(MLNCameraChangeReason)reason animated:(BOOL)animated;
	[Export ("mapView:regionDidChangeWithReason:animated:")]
	void MapViewRegionDidChangeWithReasonAnimated (MLNMapView mapView, MLNCameraChangeReason reason, bool animated);

	// @optional -(void)mapViewWillStartLoadingMap:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewWillStartLoadingMap:")]
	void MapViewWillStartLoadingMap (MLNMapView mapView);

	// @optional -(void)mapViewDidFinishLoadingMap:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewDidFinishLoadingMap:")]
	void MapViewDidFinishLoadingMap (MLNMapView mapView);

	// @optional -(void)mapViewDidFailLoadingMap:(MLNMapView * _Nonnull)mapView withError:(NSError * _Nonnull)error;
	[Export ("mapViewDidFailLoadingMap:withError:")]
	void MapViewDidFailLoadingMap (MLNMapView mapView, NSError error);

	// @optional -(void)mapViewWillStartRenderingMap:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewWillStartRenderingMap:")]
	void MapViewWillStartRenderingMap (MLNMapView mapView);

	// @optional -(void)mapViewDidFinishRenderingMap:(MLNMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered;
	[Export ("mapViewDidFinishRenderingMap:fullyRendered:")]
	void MapViewDidFinishRenderingMap (MLNMapView mapView, bool fullyRendered);

	// @optional -(void)mapViewWillStartRenderingFrame:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewWillStartRenderingFrame:")]
	void MapViewWillStartRenderingFrame (MLNMapView mapView);

	// @optional -(void)mapViewDidFinishRenderingFrame:(MLNMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered;
	[Export ("mapViewDidFinishRenderingFrame:fullyRendered:")]
	void MapViewDidFinishRenderingFrame (MLNMapView mapView, bool fullyRendered);

	// @optional -(void)mapViewDidFinishRenderingFrame:(MLNMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered frameEncodingTime:(double)frameEncodingTime frameRenderingTime:(double)frameRenderingTime;
	[Export ("mapViewDidFinishRenderingFrame:fullyRendered:frameEncodingTime:frameRenderingTime:")]
	void MapViewDidFinishRenderingFrame (MLNMapView mapView, bool fullyRendered, double frameEncodingTime, double frameRenderingTime);

	// @optional -(void)mapViewDidFinishRenderingFrame:(MLNMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered renderingStats:(MLNRenderingStats * _Nonnull)renderingStats;
	[Export ("mapViewDidFinishRenderingFrame:fullyRendered:renderingStats:")]
	void MapViewDidFinishRenderingFrame (MLNMapView mapView, bool fullyRendered, MLNRenderingStats renderingStats);

	// @optional -(void)mapViewDidBecomeIdle:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewDidBecomeIdle:")]
	void MapViewDidBecomeIdle (MLNMapView mapView);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didFinishLoadingStyle:(MLNStyle * _Nonnull)style;
	[Export ("mapView:didFinishLoadingStyle:")]
	void MapView (MLNMapView mapView, MLNStyle style);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView sourceDidChange:(MLNSource * _Nonnull)source;
	[Export ("mapView:sourceDidChange:")]
	void MapView (MLNMapView mapView, MLNSource source);

	// @optional -(UIImage * _Nullable)mapView:(MLNMapView * _Nonnull)mapView didFailToLoadImage:(NSString * _Nonnull)imageName;
	[Export ("mapView:didFailToLoadImage:")]
	[return: NullAllowed]
	UIImage MapView (MLNMapView mapView, string imageName);

	// @optional -(BOOL)mapView:(MLNMapView * _Nonnull)mapView shouldRemoveStyleImage:(NSString * _Nonnull)imageName;
	[Export ("mapView:shouldRemoveStyleImage:")]
	bool MapViewShouldRemoveStyleImage (MLNMapView mapView, string imageName);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView shaderWillCompile:(NSInteger)id backend:(NSInteger)backend defines:(NSString * _Nonnull)defines;
	[Export ("mapView:shaderWillCompile:backend:defines:")]
	void MapView (MLNMapView mapView, nint id, nint backend, string defines);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView shaderDidCompile:(NSInteger)id backend:(NSInteger)backend defines:(NSString * _Nonnull)defines;
	[Export ("mapView:shaderDidCompile:backend:defines:")]
	void MapViewShaderDidCompileBackendDefines (MLNMapView mapView, nint id, nint backend, string defines);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView shaderDidFailCompile:(NSInteger)id backend:(NSInteger)backend defines:(NSString * _Nonnull)defines;
	[Export ("mapView:shaderDidFailCompile:backend:defines:")]
	void MapViewShaderDidFailCompileBackendDefines (MLNMapView mapView, nint id, nint backend, string defines);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView glyphsWillLoad:(NSArray<NSString *> * _Nonnull)fontStack range:(NSRange)range;
	[Export ("mapView:glyphsWillLoad:range:")]
	void MapView (MLNMapView mapView, string[] fontStack, NSRange range);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView glyphsDidLoad:(NSArray<NSString *> * _Nonnull)fontStack range:(NSRange)range;
	[Export ("mapView:glyphsDidLoad:range:")]
	void MapViewGlyphsDidLoadRange (MLNMapView mapView, string[] fontStack, NSRange range);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView glyphsDidError:(NSArray<NSString *> * _Nonnull)fontStack range:(NSRange)range;
	[Export ("mapView:glyphsDidError:range:")]
	void MapViewGlyphsDidErrorRange (MLNMapView mapView, string[] fontStack, NSRange range);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView tileDidTriggerAction:(MLNTileOperation)operation x:(NSInteger)x y:(NSInteger)y z:(NSInteger)z wrap:(NSInteger)wrap overscaledZ:(NSInteger)overscaledZ sourceID:(NSString * _Nonnull)sourceID;
	[Export ("mapView:tileDidTriggerAction:x:y:z:wrap:overscaledZ:sourceID:")]
	void MapView (MLNMapView mapView, MLNTileOperation operation, nint x, nint y, nint z, nint wrap, nint overscaledZ, string sourceID);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView spriteWillLoad:(NSString * _Nonnull)id url:(NSString * _Nonnull)url;
	[Export ("mapView:spriteWillLoad:url:")]
	void MapView (MLNMapView mapView, string id, string url);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView spriteDidLoad:(NSString * _Nonnull)id url:(NSString * _Nonnull)url;
	[Export ("mapView:spriteDidLoad:url:")]
	void MapViewSpriteDidLoadUrl (MLNMapView mapView, string id, string url);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView spriteDidError:(NSString * _Nonnull)id url:(NSString * _Nonnull)url;
	[Export ("mapView:spriteDidError:url:")]
	void MapViewSpriteDidErrorUrl (MLNMapView mapView, string id, string url);

	// @optional -(void)mapViewWillStartLocatingUser:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewWillStartLocatingUser:")]
	void MapViewWillStartLocatingUser (MLNMapView mapView);

	// @optional -(void)mapViewDidStopLocatingUser:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewDidStopLocatingUser:")]
	void MapViewDidStopLocatingUser (MLNMapView mapView);

	// @optional -(MLNUserLocationAnnotationViewStyle * _Nonnull)mapViewStyleForDefaultUserLocationAnnotationView:(MLNMapView * _Nonnull)mapView __attribute__((swift_name("mapView(styleForDefaultUserLocationAnnotationView:)")));
	[Export ("mapViewStyleForDefaultUserLocationAnnotationView:")]
	MLNUserLocationAnnotationViewStyle MapViewStyleForDefaultUserLocationAnnotationView (MLNMapView mapView);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didUpdateUserLocation:(MLNUserLocation * _Nullable)userLocation;
	[Export ("mapView:didUpdateUserLocation:")]
	void MapView (MLNMapView mapView, [NullAllowed] MLNUserLocation userLocation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didFailToLocateUserWithError:(NSError * _Nonnull)error;
	[Export ("mapView:didFailToLocateUserWithError:")]
	void MapView (MLNMapView mapView, NSError error);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didChangeUserTrackingMode:(MLNUserTrackingMode)mode animated:(BOOL)animated;
	[Export ("mapView:didChangeUserTrackingMode:animated:")]
	void MapView (MLNMapView mapView, MLNUserTrackingMode mode, bool animated);

	// @optional -(CGPoint)mapViewUserLocationAnchorPoint:(MLNMapView * _Nonnull)mapView;
	[Export ("mapViewUserLocationAnchorPoint:")]
	CGPoint MapViewUserLocationAnchorPoint (MLNMapView mapView);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didChangeLocationManagerAuthorization:(id<MLNLocationManager> _Nonnull)manager __attribute__((availability(ios, introduced=14)));
	// [iOS (14,0)]
	[Export ("mapView:didChangeLocationManagerAuthorization:")]
	void MapView (MLNMapView mapView, MLNLocationManager manager);

	// @optional -(MLNAnnotationImage * _Nullable)mapView:(MLNMapView * _Nonnull)mapView imageForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:imageForAnnotation:")]
	[return: NullAllowed]
	MLNAnnotationImage MapView (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(CGFloat)mapView:(MLNMapView * _Nonnull)mapView alphaForShapeAnnotation:(MLNShape * _Nonnull)annotation;
	[Export ("mapView:alphaForShapeAnnotation:")]
	nfloat MapView (MLNMapView mapView, MLNShape annotation);

	// @optional -(UIColor * _Nonnull)mapView:(MLNMapView * _Nonnull)mapView strokeColorForShapeAnnotation:(MLNShape * _Nonnull)annotation;
	[Export ("mapView:strokeColorForShapeAnnotation:")]
	UIColor MapViewStrokeColorForShapeAnnotation (MLNMapView mapView, MLNShape annotation);

	// @optional -(UIColor * _Nonnull)mapView:(MLNMapView * _Nonnull)mapView fillColorForPolygonAnnotation:(MLNPolygon * _Nonnull)annotation;
	[Export ("mapView:fillColorForPolygonAnnotation:")]
	UIColor MapView (MLNMapView mapView, MLNPolygon annotation);

	// @optional -(CGFloat)mapView:(MLNMapView * _Nonnull)mapView lineWidthForPolylineAnnotation:(MLNPolyline * _Nonnull)annotation;
	[Export ("mapView:lineWidthForPolylineAnnotation:")]
	nfloat MapView (MLNMapView mapView, MLNPolyline annotation);

	// @optional -(MLNAnnotationView * _Nullable)mapView:(MLNMapView * _Nonnull)mapView viewForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:viewForAnnotation:")]
	[return: NullAllowed]
	MLNAnnotationView MapViewViewForAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didAddAnnotationViews:(NSArray<MLNAnnotationView *> * _Nonnull)annotationViews;
	[Export ("mapView:didAddAnnotationViews:")]
	void MapView (MLNMapView mapView, MLNAnnotationView[] annotationViews);

	// @optional -(BOOL)mapView:(MLNMapView * _Nonnull)mapView shapeAnnotationIsEnabled:(MLNShape * _Nonnull)annotation;
	[Export ("mapView:shapeAnnotationIsEnabled:")]
	bool MapViewShapeAnnotationIsEnabled (MLNMapView mapView, MLNShape annotation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didSelectAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:didSelectAnnotation:")]
	void MapViewDidSelectAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didDeselectAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:didDeselectAnnotation:")]
	void MapViewDidDeselectAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didSelectAnnotationView:(MLNAnnotationView * _Nonnull)annotationView;
	[Export ("mapView:didSelectAnnotationView:")]
	void MapView (MLNMapView mapView, MLNAnnotationView annotationView);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView didDeselectAnnotationView:(MLNAnnotationView * _Nonnull)annotationView;
	[Export ("mapView:didDeselectAnnotationView:")]
	void MapViewDidDeselectAnnotationView (MLNMapView mapView, MLNAnnotationView annotationView);

	// @optional -(BOOL)mapView:(MLNMapView * _Nonnull)mapView annotationCanShowCallout:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:annotationCanShowCallout:")]
	bool MapViewAnnotationCanShowCallout (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(id<MLNCalloutView> _Nullable)mapView:(MLNMapView * _Nonnull)mapView calloutViewForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:calloutViewForAnnotation:")]
	[return: NullAllowed]
	MLNCalloutView MapViewCalloutViewForAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(UIView * _Nullable)mapView:(MLNMapView * _Nonnull)mapView leftCalloutAccessoryViewForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:leftCalloutAccessoryViewForAnnotation:")]
	[return: NullAllowed]
	UIView MapViewLeftCalloutAccessoryViewForAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(UIView * _Nullable)mapView:(MLNMapView * _Nonnull)mapView rightCalloutAccessoryViewForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:rightCalloutAccessoryViewForAnnotation:")]
	[return: NullAllowed]
	UIView MapViewRightCalloutAccessoryViewForAnnotation (MLNMapView mapView, MLNAnnotation annotation);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView annotation:(id<MLNAnnotation> _Nonnull)annotation calloutAccessoryControlTapped:(UIControl * _Nonnull)control;
	[Export ("mapView:annotation:calloutAccessoryControlTapped:")]
	void MapView (MLNMapView mapView, MLNAnnotation annotation, UIControl control);

	// @optional -(void)mapView:(MLNMapView * _Nonnull)mapView tapOnCalloutForAnnotation:(id<MLNAnnotation> _Nonnull)annotation;
	[Export ("mapView:tapOnCalloutForAnnotation:")]
	void MapViewTapOnCalloutForAnnotation (MLNMapView mapView, MLNAnnotation annotation);
}

// @interface MLNPluginLayerProperty : NSObject
[BaseType (typeof(NSObject))]
interface MLNPluginLayerProperty
{
	// +(MLNPluginLayerProperty * _Nonnull)propertyWithName:(NSString * _Nonnull)propertyName propertyType:(MLNPluginLayerPropertyType)propertyType defaultValue:(id _Nonnull)defaultValue;
	[Static]
	[Export ("propertyWithName:propertyType:defaultValue:")]
	MLNPluginLayerProperty PropertyWithName (string propertyName, MLNPluginLayerPropertyType propertyType, NSObject defaultValue);

	// @property (copy) NSString * _Nonnull propertyName;
	[Export ("propertyName")]
	string PropertyName { get; set; }

	// @property MLNPluginLayerPropertyType propertyType;
	[Export ("propertyType", ArgumentSemantic.Assign)]
	MLNPluginLayerPropertyType PropertyType { get; set; }

	// @property float singleFloatDefaultValue;
	[Export ("singleFloatDefaultValue")]
	float SingleFloatDefaultValue { get; set; }

	// @property (copy) UIColor * _Nonnull colorDefaultValue;
	[Export ("colorDefaultValue", ArgumentSemantic.Copy)]
	UIColor ColorDefaultValue { get; set; }
}

// @interface MLNPluginLayerCapabilities : NSObject
[BaseType (typeof(NSObject))]
interface MLNPluginLayerCapabilities
{
	// @property (copy) NSString * _Nonnull layerID;
	[Export ("layerID")]
	string LayerID { get; set; }

	// @property BOOL requiresPass3D;
	[Export ("requiresPass3D")]
	bool RequiresPass3D { get; set; }

	// @property (copy) NSArray<MLNPluginLayerProperty *> * _Nonnull layerProperties;
	[Export ("layerProperties", ArgumentSemantic.Copy)]
	MLNPluginLayerProperty[] LayerProperties { get; set; }
}

// @interface MLNPluginLayer : NSObject
[BaseType (typeof(NSObject))]
interface MLNPluginLayer
{
	// +(MLNPluginLayerCapabilities * _Nonnull)layerCapabilities;
	[Static]
	[Export ("layerCapabilities")]
	// [Verify (MethodToProperty)]
	MLNPluginLayerCapabilities LayerCapabilities { get; }

	// -(void)onRenderLayer:(MLNMapView * _Nonnull)mapView renderEncoder:(id<MTLRenderCommandEncoder> _Nonnull)renderEncoder;
	[Export ("onRenderLayer:renderEncoder:")]
	void OnRenderLayer (MLNMapView mapView, IMTLRenderCommandEncoder renderEncoder);

	// -(void)onUpdateLayer:(MLNPluginLayerDrawingContext)drawingContext;
	[Export ("onUpdateLayer:")]
	void OnUpdateLayer (MLNPluginLayerDrawingContext drawingContext);

	// -(void)onUpdateLayerProperties:(NSDictionary * _Nonnull)layerProperties;
	[Export ("onUpdateLayerProperties:")]
	void OnUpdateLayerProperties (NSDictionary layerProperties);

	// -(void)didMoveToMapView:(MLNMapView * _Nonnull)mapView;
	[Export ("didMoveToMapView:")]
	void DidMoveToMapView (MLNMapView mapView);
}

// @interface MLNPluginStyleLayer : MLNStyleLayer
// [BaseType (typeof(MLNStyleLayer))]
// interface MLNPluginStyleLayer
// {
// 	[Export ("pluginLayer")]
// 	// [Verify (MethodToProperty)]
// 	MLNPluginLayer PluginLayer { get; }
// }

// @interface MLNScaleBar : UIView
// [BaseType (typeof(UIView))]
// interface MLNScaleBar
// {
// 	[Export ("metersPerPoint")]
// 	double MetersPerPoint { get; set; }
// 	[Export ("shouldShowDarkStyles")]
// 	bool ShouldShowDarkStyles { get; set; }
// 	[Export ("usesMetricSystem")]
// 	bool UsesMetricSystem { get; set; }
// 	[Export ("primaryColor", ArgumentSemantic.Assign)]
// 	UIColor PrimaryColor { get; set; }
// 	[Export ("secondaryColor", ArgumentSemantic.Assign)]
// 	UIColor SecondaryColor { get; set; }
// }

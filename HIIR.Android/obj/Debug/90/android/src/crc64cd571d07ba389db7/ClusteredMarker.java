package crc64cd571d07ba389db7;


public class ClusteredMarker
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterItem
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getPosition:()Lcom/google/android/gms/maps/model/LatLng;:GetGetPositionHandler:Com.Google.Maps.Android.Clustering.IClusterItemInvoker, GoogleMapsUtility\n" +
			"n_getSnippet:()Ljava/lang/String;:GetGetSnippetHandler:Com.Google.Maps.Android.Clustering.IClusterItemInvoker, GoogleMapsUtility\n" +
			"n_getTitle:()Ljava/lang/String;:GetGetTitleHandler:Com.Google.Maps.Android.Clustering.IClusterItemInvoker, GoogleMapsUtility\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.GoogleMaps.Clustering.Android.ClusteredMarker, Xamarin.Forms.GoogleMaps.Clustering.Android", ClusteredMarker.class, __md_methods);
	}


	public ClusteredMarker ()
	{
		super ();
		if (getClass () == ClusteredMarker.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.GoogleMaps.Clustering.Android.ClusteredMarker, Xamarin.Forms.GoogleMaps.Clustering.Android", "", this, new java.lang.Object[] {  });
	}


	public com.google.android.gms.maps.model.LatLng getPosition ()
	{
		return n_getPosition ();
	}

	private native com.google.android.gms.maps.model.LatLng n_getPosition ();


	public java.lang.String getSnippet ()
	{
		return n_getSnippet ();
	}

	private native java.lang.String n_getSnippet ();


	public java.lang.String getTitle ()
	{
		return n_getTitle ();
	}

	private native java.lang.String n_getTitle ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

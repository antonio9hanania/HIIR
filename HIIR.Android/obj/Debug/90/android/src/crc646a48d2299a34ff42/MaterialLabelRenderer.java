package crc646a48d2299a34ff42;


public class MaterialLabelRenderer
	extends crc643f46942d9dd1fff9.LabelRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Plugin.MaterialDesignControls.Android.MaterialLabelRenderer, Plugin.MaterialDesignControls.Android", MaterialLabelRenderer.class, __md_methods);
	}


	public MaterialLabelRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MaterialLabelRenderer.class)
			mono.android.TypeManager.Activate ("Plugin.MaterialDesignControls.Android.MaterialLabelRenderer, Plugin.MaterialDesignControls.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MaterialLabelRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MaterialLabelRenderer.class)
			mono.android.TypeManager.Activate ("Plugin.MaterialDesignControls.Android.MaterialLabelRenderer, Plugin.MaterialDesignControls.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MaterialLabelRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MaterialLabelRenderer.class)
			mono.android.TypeManager.Activate ("Plugin.MaterialDesignControls.Android.MaterialLabelRenderer, Plugin.MaterialDesignControls.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
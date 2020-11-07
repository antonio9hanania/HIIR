package crc64211a0aa9c31aade0;


public class MinutesSecondsWheelPicker
	extends android.widget.LinearLayout
	implements
		mono.android.IGCUserPeer,
		android.widget.NumberPicker.OnValueChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onValueChange:(Landroid/widget/NumberPicker;II)V:GetOnValueChange_Landroid_widget_NumberPicker_IIHandler:Android.Widget.NumberPicker/IOnValueChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("HIIR.Droid.MinutesSecondsWheelPicker, HIIR.Android", MinutesSecondsWheelPicker.class, __md_methods);
	}


	public MinutesSecondsWheelPicker (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MinutesSecondsWheelPicker.class)
			mono.android.TypeManager.Activate ("HIIR.Droid.MinutesSecondsWheelPicker, HIIR.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public MinutesSecondsWheelPicker (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MinutesSecondsWheelPicker.class)
			mono.android.TypeManager.Activate ("HIIR.Droid.MinutesSecondsWheelPicker, HIIR.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MinutesSecondsWheelPicker (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MinutesSecondsWheelPicker.class)
			mono.android.TypeManager.Activate ("HIIR.Droid.MinutesSecondsWheelPicker, HIIR.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MinutesSecondsWheelPicker (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == MinutesSecondsWheelPicker.class)
			mono.android.TypeManager.Activate ("HIIR.Droid.MinutesSecondsWheelPicker, HIIR.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void onValueChange (android.widget.NumberPicker p0, int p1, int p2)
	{
		n_onValueChange (p0, p1, p2);
	}

	private native void n_onValueChange (android.widget.NumberPicker p0, int p1, int p2);

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

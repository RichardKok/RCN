using UnityEditor;
using UnityEngine;

// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and prefab overrides.
[CustomEditor(typeof(MasterView))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor {
	const int margin = 1;
	const int marginSm = 0;
	SerializedProperty visualRangeProp;
	SerializedProperty attackProp;
	SerializedProperty decayProp;
	SerializedProperty sustainProp;
	SerializedProperty releaseProp;
	SerializedProperty speedSensitivityProp;
	SerializedProperty sustainIntensityPercentageProp;
	private float offIntensityRefValue;
	private float maxIntensityRefValue;
	string[] modeOptions;
	private int modeIndex = 0; //Temporary testing setting
	private float maxVisualRange;
	private int maxADSR;
	private float visualRangeStart;
	private int adsrStart;
	private float offIntensityLimit;
	private float maxIntensityLimit;
	private float sustainRefValue;
	
	void OnEnable () {
		// Setup the SerializedProperties.
		visualRangeProp = serializedObject.FindProperty ("visualRange");
		attackProp = serializedObject.FindProperty ("attack");
		decayProp = serializedObject.FindProperty ("decay");
		sustainProp = serializedObject.FindProperty ("sustain");
		releaseProp = serializedObject.FindProperty ("release");
		speedSensitivityProp = serializedObject.FindProperty("speedSensitivity");
		offIntensityLimit = serializedObject.FindProperty("minIntensity").floatValue;
		maxIntensityLimit = serializedObject.FindProperty("maxIntensity").floatValue;
		modeOptions = ((MasterView)target).modeOptions;
		maxVisualRange = serializedObject.FindProperty("maxVisRange").floatValue;
		visualRangeStart = serializedObject.FindProperty ("visRangeStart").floatValue;
		maxVisualRange = serializedObject.FindProperty("maxVisRange").floatValue;
		visualRangeStart = serializedObject.FindProperty ("visRangeStart").floatValue;
		maxADSR = serializedObject.FindProperty("maxADSR").intValue;
		adsrStart = serializedObject.FindProperty("adsrStart").intValue;
		sustainIntensityPercentageProp = serializedObject.FindProperty("sustainRatioOfMax");             
		offIntensityRefValue = 2.5f;
		maxIntensityRefValue = 7.5f;
	}

	public override void OnInspectorGUI() {
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		createSpeedSensitivityTools();
		if (GUILayout.Button("Apply sensitivity")) ((MasterView)target).ApplyTrackerChanges();
		addSpaces(margin);
		EditorGUILayout.LabelField("Mode");
		modeIndex = EditorGUILayout.Popup(modeIndex, modeOptions);
		addSpaces(marginSm);
		createVisualRangeTools();
		addSpaces(marginSm);
		createIntensityScalingTools();
		addSpaces(marginSm);
		createADSRGUITools();
		addSpaces(marginSm);
		if (GUILayout.Button("Apply settings")) ((MasterView)target).ApplyLightChanges(modeOptions[modeIndex], offIntensityRefValue, sustainRefValue, maxIntensityRefValue);
		serializedObject.ApplyModifiedProperties ();
	}

	public void addSpaces(int amount) {
		for (int i = 0; i < amount; i++) EditorGUILayout.Space ();
	}
	
	public void createIntensityScalingTools() {
		EditorGUILayout.LabelField("Light intensity scaling range [Dim - Bright]");
		EditorGUILayout.MinMaxSlider(ref offIntensityRefValue, ref maxIntensityRefValue, offIntensityLimit, maxIntensityLimit);
		EditorGUILayout.IntSlider(sustainIntensityPercentageProp, 0, 100);
		ProgressBar (offIntensityRefValue / maxIntensityLimit, "Dim Intensity: " + (int)offIntensityRefValue);
		sustainRefValue = (((float)sustainIntensityPercentageProp.intValue / 100.0f) * maxIntensityRefValue);
		ProgressBar (sustainRefValue / maxIntensityRefValue, "SustainIntensity: " + (int)sustainRefValue);
		ProgressBar (maxIntensityRefValue / maxIntensityLimit, "Maximum Intensity: " + (int)maxIntensityRefValue);
	} 
	
	public void createVisualRangeTools() {
		EditorGUILayout.Slider(visualRangeProp, 0, maxVisualRange, new GUIContent ("VisualRange scaling"));
		//ProgressBar (visualRangeProp.floatValue / (float)maxVisualRange, "Visual Range");
		if (GUILayout.Button("Reset range")) visualRangeProp.floatValue = visualRangeStart;	
	}
	
	public void createSpeedSensitivityTools() {
		EditorGUILayout.Slider(speedSensitivityProp, 0, 10, new GUIContent("SpeedSensitivity"));
		//ProgressBar(speedSensitivityProp.floatValue, "Speed Sensitivity");
		if (GUILayout.Button("Reset sensitivity")) speedSensitivityProp.floatValue = 1;
	}
	
	public void createADSRGUITools() {
		EditorGUILayout.IntSlider (attackProp, 0, maxADSR, new GUIContent ("Attack"));
		EditorGUILayout.IntSlider (decayProp, 0, maxADSR, new GUIContent ("Decay"));
		EditorGUILayout.IntSlider (sustainProp, 0, maxADSR, new GUIContent ("Sustain"));
		EditorGUILayout.IntSlider (releaseProp, 0, maxADSR, new GUIContent ("Release"));
		//ProgressBar (attackProp.intValue / (float)maxADSR, "Attack");
		//ProgressBar (decayProp.intValue / (float)maxADSR, "Decay");
		//ProgressBar (sustainProp.intValue / (float)maxADSR, "Sustain");
		//ProgressBar (releaseProp.intValue / (float)maxADSR, "Release");
		if (GUILayout.Button("Reset ASDR")) resetADSR();
	}
	
	public void resetADSR() {
		attackProp.intValue = adsrStart;
		sustainProp.intValue = adsrStart;
		decayProp.intValue = adsrStart;
		releaseProp.intValue = adsrStart;
	}
	
	// Custom GUILayout progress bar.
	void ProgressBar (float value, string label) {
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}

}
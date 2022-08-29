/* ***************************************************************************
 * Copyright © 2022 David Maisonave	(https://axter.com)	All rights reserved.
 * ***************************************************************************
 * The is free software. You can redistribute it and/or modify it under the 
 * terms of the MIT License. For more information, please see License file 
 * distributed with this package.
 * ***************************************************************************
*/
// MainWindow adapted from Steven M Cohn; ResxTranslator (https://github.com/stevencohn/ResxTranslator)
namespace ABetterTranslator
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Resources;
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using System.Xml.Linq;
    using System.ComponentModel;
    using BrightIdeasSoftware;
    using ABetterTranslator.Properties;
    using static TranslateMultiThread.MultiThreadTranslator;
    using File = System.IO.File;
	using MultiThreadTranslator_Options = TranslateMultiThread.MultiThreadTranslator.MultiThreadTranslator_Options;
    using static ABetterTranslator.MainWindow;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using GTranslate;
    using GTranslate.Translators;
    using Windows.Storage.Streams;
    using Windows.UI.ViewManagement;
    using static System.Windows.Forms.Design.AxImporter;
    using Microsoft.VisualBasic.Devices;
    using Windows.Globalization;
    using System.Runtime.Intrinsics.X86;
    using System.Reflection;

	public partial class MainWindow : Form
	{
        #region public variables
        // ToDo: Make MsgToDisableWarnPrompts and colors private if it does not get used out side of MainWindow.
        public static readonly string MsgToDisableWarnPrompts = "\n\n\nThis warning message can be disabled by option [Display Warnings] on the [Advance Options] tab.";
        public static readonly Color ErrFgColor = Color.Red;
        public static readonly Color ErrBgColor = Color.LightYellow;
        public static readonly Color StartAndEndFgColor = Color.DarkGreen;
        public static readonly Color StartAndEndBgColor = Color.Yellow;
        public static readonly Color ProgressInfoFgColor = Color.White;
        public static readonly Color ProgressInfoBgColor = Color.Blue;
        public static readonly Color PreStartInfoFgColor = Color.Blue;
        public static readonly Color PreStartInfoBgColor = Color.White;
        public static readonly Color BackUpInfoFgColor = Color.Teal;
        public static readonly Color BackUpInfoBgColor = Color.White;
        #endregion public variables
        #region delagates
        // Start of delagates /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public delegate void IntArgDelegateType(int Arg);
		public IntArgDelegateType _showProgressDelegate;
		public IntArgDelegateType _initializeProgressBarDelegate;
		public delegate void TaskCompleteDelegateType(int Arg, TimeSpan TotalRunTime);
		public TaskCompleteDelegateType _taskCompleteDelegate;
        public delegate void OutPutFromTranslatorDelegateType(string message, Color color);
        public OutPutFromTranslatorDelegateType _outPutFromTranslatorDelegate;
        public delegate void WriteLineFromTranslatorDelegateType(string message, LoggingColorSet loggingcolorset);
        public WriteLineFromTranslatorDelegateType _writeLineFromTranslatorDelegate;
        public delegate void LogLineFromTranslatorDelegateType(string message, OutPutLevel outputlevel, Color? color);
        public LogLineFromTranslatorDelegateType _logLineFromTranslatorDelegate;
        public delegate void LogToFileFromTranslatorDelegateType(string message, VerbosityLevels verbositylevels, bool DbgWriteLine = true);
        public LogToFileFromTranslatorDelegateType _logToFileFromTranslatorDelegate;
        #endregion delagates
        #region private/protected variables
        // Constant strings
        private const string _getLanguageCodePattern = @".+\.(?<code>.+)\.resx";
		private const string NL = "\n";
        private const string _logFileName = "LogFile.txt";
        private const string ErrMsgPrefix = "Error: - ";
        private const string WrnMsgPrefix = "Warn: - ";
        private const string InfMsgPrefix = "Info: - ";
        private const string VerboseMsgPrefix = "VERBOSE: - ";
        private const string DetailsMsgPrefix = "Details: - ";
        // ReadOnly variables
        private readonly char[] _numberCharsToTrim = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
		private readonly List<PreviousGroupLanguageSelections> _previousLanguageSelections = new();
		private readonly bool backendUpdate = false;
		private readonly List<int> _LogBoxUpdatePoints = new();
        private readonly object _lock = new object();
        private readonly IReadOnlyDictionary<string, GTranslate.Language> GTranslate_LangLookup;
        private readonly string ApplicationDefaultLanguage = null;
        // Other variables
		private CancellationTokenSource cancellation = new CancellationTokenSource();
		private string _lastValueOf_inputBox = "";
		private OutputBoxRelatedToInputBox _outputBoxRelationToInputBox = OutputBoxRelatedToInputBox.NoRelations;
		private ResxMultiThreadTranslator? _multiThreadTranslator = null;
		private List<TranslationRequestPacket>? _translationRequestPackets = null;
        private int SelectedLanguageSet = -1;
        private List<string> _previousLoggingErrors = new();
        private List<string[,]> LanguageSets = new();
        private List<string> _languagesInList = new();
        private LanguageServiceDetails languagedictionary = new ();
        private string _lastTextToTranslate_value = "";
        private bool _doSendErrMsgOnTranslateTextError = false;
        private bool _isFormReady = false;
        private StreamWriter _logFile  = null;
        private bool _SrcResxHasAppendedLanguageToBaseFileName = false;
        #endregion private/protected variables
        #region enums and sub classes
        public enum VerbosityLevels
        {
            Silent,
            Errors,
            Warn,
            Normal,
            Detail,
            LogToFileIfSilent,
            DebugWriteIfSilent,
            LogToFileIfNotDetail,
            DebugWriteIfNotDetail,
        }
        public enum LanguageSet
        {
            Windows10Plus_LanguagePacks,
            Windows10Plus_LanguageInterfacePacks,
            TranslatorSupportedLanguages,
            Iso639_1_Languages,
            Iso639_1_Plus,
        }

        public enum LoggingColorSet
        {
            NormalLogging,
            ErrorLogging,
            StartAndEndLogging,
            ProgressLogging,
            PreStartLogging,
            BackupLogging,
        }
        public enum OutputBoxRelatedToInputBox
        {
            NoRelations,
            OutPutBox_IsParent_OfInputBoxParent,
            OutPutBoxParent_IsParent_OfInputBoxParent,
            OutPutBoxParentParent_IsParent_OfInputBoxParent,
            OutPutBox_IsParent_OfInputBoxParentParent,
            OutPutBoxParent_IsParent_OfInputBoxParentParent,
            OutPutBoxParentParent_IsParent_OfInputBoxParentParent,
        }
        private class PreviousGroupLanguageSelections
		{
			public List<string> Selections { get; set; }	= new List<string>();
			public string Name { get; set; } = "";
        }
        public class ResxMultiThreadTranslator : TranslateMultiThread.MultiThreadTranslator
        {
            private readonly MainWindow? _mainWindow = null;
            private readonly Color DefaultColor = Color.Black;
            private List<string> _previousErrorMessages = new();
            private int _qtySkippedDisplayErrs = 0;
            public ResxMultiThreadTranslator(MainWindow mainWindow, MultiThreadTranslator_Options? options = null) : base(options)
            {
                _mainWindow = mainWindow;
            }

            protected override void ShowProgress(int TotalCount)
            {
                if (TotalCount < 1)
                    LogToFile($"ShowProgress received unexpected value of {TotalCount}", VerbosityLevels.Errors, true);
                _mainWindow.Invoke(_mainWindow._showProgressDelegate, TotalCount);
            }

            protected override void TaskComplete(int QtyItemsCompleted, TimeSpan TotalRunTime)
            {
                _previousErrorMessages.Clear();
                if ( _qtySkippedDisplayErrs > 0 )
                    LogToFile($"Skipped display {_qtySkippedDisplayErrs} duplicate errors.",VerbosityLevels.Warn);
                _mainWindow.Invoke(_mainWindow._taskCompleteDelegate, QtyItemsCompleted, TotalRunTime);
            }

            protected override void InitializeProgressBar(int QtyItemsToCheck)
            {
                _previousErrorMessages.Clear();
                _mainWindow.Invoke(_mainWindow._initializeProgressBarDelegate, QtyItemsToCheck);
            }
            protected override void LogToFile(string message, VerbosityLevels verbositylevels, bool DbgWriteLine = true)
            {
                    _ = _mainWindow.Invoke(_mainWindow._logToFileFromTranslatorDelegate, message, verbositylevels, DbgWriteLine);
            }
            protected override void LogLine(string message, OutPutLevel outputlevel = OutPutLevel.NormalLvl, Color? color = null)
            {
                if ( _options._silent )
                    return;
                if ( outputlevel == OutPutLevel.ShowInWindow )
                    _ = _mainWindow.Invoke(_mainWindow._logLineFromTranslatorDelegate, message, outputlevel, color ?? DefaultColor);
                else if (outputlevel == OutPutLevel.ErrorLvl)
                {
                    if (this._options._verbose ||
                        !_previousErrorMessages
                            .Contains(message)) // Avoid sending the same error message over and over again
                    {
                        Debug.WriteLine(message);
                        _previousErrorMessages.Add(message);
                    }
                    else
                        ++_qtySkippedDisplayErrs;
                }
                else if ( outputlevel == OutPutLevel.PreProgressBar || outputlevel == OutPutLevel.PostProgressBar )
                    Debug.WriteLine(InfMsgPrefix + message);
                else if ( _options._verbose )
                    Debug.WriteLine(VerboseMsgPrefix + message);
                else if ( outputlevel == OutPutLevel.VerboseIfNotSilent )
                    Debug.WriteLine(InfMsgPrefix + message);
            }

            protected override void WriteLine(string message, OutPutLevel outputlevel = OutPutLevel.NormalLvl)
            {
                if ( _options._silent )
                    return;
                LoggingColorSet logColorSet = LoggingColorSet.NormalLogging;
                switch (outputlevel)
                {
                    case OutPutLevel.VerboseIfNotSilent:
                        logColorSet = LoggingColorSet.ProgressLogging;
                        break;
                    default:
                    case OutPutLevel.ShowInWindow:
                    case OutPutLevel.NormalLvl:
                        logColorSet = LoggingColorSet.NormalLogging;
                        break;
                    case OutPutLevel.PreProgressBar:
                        logColorSet = LoggingColorSet.PreStartLogging;
                        break;
                    case OutPutLevel.PostProgressBar:
                        logColorSet = LoggingColorSet.StartAndEndLogging;
                        break;
                    case OutPutLevel.ErrorLvl:
                        logColorSet = LoggingColorSet.ErrorLogging;
                        break;
                }
                _ = _mainWindow.Invoke(_mainWindow._writeLineFromTranslatorDelegate, message, logColorSet);
            }
        }
        public class ObjListVwRowDetails : INotifyPropertyChanged
        {
            public bool IsActive = true;
            public ObjListVwRowDetails(string langTag)
            {
                _languageTag = langTag;
            }
            public ObjListVwRowDetails(string languagetag, string languagename, string languageadjective, string languagealiases, string translatorsupported, string iso639_3)
            {
                LanguageTag = languagetag;
                LanguageName = languagename;
                LanguageTranslatorTag = languageadjective;
                LanguageAliases = languagealiases;
                TranslatorSupported = translatorsupported;
                Iso639_3 = iso639_3;
            }
            public ObjListVwRowDetails(ObjListVwRowDetails other)
            {
                LanguageTag = other.LanguageTag;
                LanguageName = other.LanguageName;
                LanguageTranslatorTag = other.LanguageTranslatorTag;
                LanguageAliases = other.LanguageAliases;
                TranslatorSupported = other.TranslatorSupported;
                Iso639_3 = other.Iso639_3;
            }
            [OLVIgnore]
            public string ImageName {get{return "user";}}
            // Allows tests for properties.
            [OLVColumn(ImageAspectName = "ImageName")]
            public string LanguageTag
            {
                get { return _languageTag; }
                set
                {
                    if ( _languageTag == value ) return;
                    _languageTag = value;
                    OnPropertyChanged("LanguageTag");
                }
            }
            private string _languageTag;
            public string LanguageName
            {
                get { return _languageName; }
                set
                {
                    if ( _languageName == value ) return;
                    _languageName = value;
                    OnPropertyChanged("LanguageName");
                }
            }
            private string _languageName;
            public string LanguageTranslatorTag
            {
                get { return _languageTranslatorTag; }
                set
                {
                    if ( _languageTranslatorTag == value ) return;
                    _languageTranslatorTag = value;
                    OnPropertyChanged("LanguageTranslatorTag");
                }
            }
            private string _languageTranslatorTag;
            public string LanguageAliases
            {
                get { return _languageAliase; }
                set
                {
                    if ( _languageAliase == value ) return;
                    _languageAliase = value;
                    OnPropertyChanged("LanguageAliases");
                }
            }
            private string _languageAliase;
            public string TranslatorSupported
            {
                get { return _translatorSupported; }
                set
                {
                    if ( _translatorSupported == value ) return;
                    _translatorSupported = value;
                    OnPropertyChanged("TranslatorSupported");
                }
            }
            private string _translatorSupported;
            public string Iso639_3
            {
                get { return _iso639_3; }
                set
                {
                    if ( _iso639_3 == value ) return;
                    _iso639_3 = value;
                    OnPropertyChanged("Iso639_3");
                }
            }
            private string _iso639_3;
            #region Implementation of INotifyPropertyChanged
            public event PropertyChangedEventHandler? PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                if ( PropertyChanged != null )
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        #endregion enums and sub classes

        #region Class constructor and miscellaneous methods
        public MainWindow()
		{
            string[] args = Environment.GetCommandLineArgs();
            ApplicationDefaultLanguage = ParseArgs(args);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(ApplicationDefaultLanguage);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(ApplicationDefaultLanguage);
            GTranslate_LangLookup = languagedictionary.Languages;
            InitializeComponent();
            ValidateLanguageSets();
            languageList.ColumnsNotEditable = new List<int>();
            languageList.ColumnsNotEditable.Add(0);
            languageList.ColumnsNotEditable.Add(1);
            languageList.ColumnsNotEditable.Add(2);
            languageList.ColumnsNotEditable.Add(3);
            languageList.ColumnsNotEditable.Add(4);
            languageList.ColumnsNotEditable.Add(5);

            LanguageSets.Add(LanguageCodes.LanguageCodesAndAliases);
            LanguageSets.Add(LanguageCodes.Windows10Plus_LanguagePacks);
            LanguageSets.Add(LanguageCodes.Windows10Plus_LanguageInterfacePacks);

			_showProgressDelegate = new IntArgDelegateType(Translator_ShowProgress);
			_taskCompleteDelegate = new TaskCompleteDelegateType(Translator_TaskComplete);
			_initializeProgressBarDelegate = new IntArgDelegateType(Translator_InitializeProgressBar);
			_outPutFromTranslatorDelegate = new OutPutFromTranslatorDelegateType(Translator_OutPutFromTranslator);
            _writeLineFromTranslatorDelegate = new WriteLineFromTranslatorDelegateType(WriteLine);
            _logLineFromTranslatorDelegate = new LogLineFromTranslatorDelegateType(LogLine);
            _logToFileFromTranslatorDelegate = new LogToFileFromTranslatorDelegateType(LogToFile);

			Width = Math.Min(1700, Screen.FromControl(this).WorkingArea.Width - 600);
			Height = Math.Min(1000, Screen.FromControl(this).WorkingArea.Height - 300);
            PopulateLanguageSet((LanguageSet) comboBoxDefaultLanguageSet.SelectedIndex);


			LockControls(false);
			statusLabel.Visible = false;
            PopulateComboBoxLanguage();
        }
        [ConditionalAttribute("DEBUG")]
        private void ValidateLanguageSets()
        { // Only use this in debug mode for validating the data in LanguageCode.cs and in LanguageDictionary.cs
            List<string> DetectedErrors = new();
            List<string> DetectedMinorIssues = new();
            System.Collections.Generic.SortedList<string, GTranslate.Language> SortedLanguageNames = new();
            foreach ( var language in GTranslate_LangLookup )
                SortedLanguageNames.Add(language.Value.Name, language.Value);

            string ReadMeFileName = "E:\\Repos\\ABetterTranslator\\ABetterTranslator\\Docs\\SupportedLanguages\\README.md";
            using StreamWriter file = new(ReadMeFileName, append: false);
            file.WriteLine("# Supported Languages\r\n");
            foreach ( KeyValuePair<string, GTranslate.Language> language in SortedLanguageNames )
            {
                string code = language.Key;
                string name = language.Value.Name;
                string translator = language.Value.SupportedServices.ToString();
                string ISO6391 = language.Value.ISO6391;
                string ISO6393 = language.Value.ISO6393;
                string NativeName = language.Value.NativeName;
                if ( LanguageCodes.LanguagesTagsWithIssues.Contains(ISO6391) )
                    continue;
                file.WriteLine($"* **{name}** - {code}:{ISO6391}  [{translator}]");
            }
            file.Close();
            string TextFileName = "E:\\Repos\\ABetterTranslator\\ABetterTranslator\\Docs\\SupportedLanguages\\LanguagesNotReconginzedInResxFiles.md";
            using StreamWriter file2 = new(TextFileName, append: false);
            file2.WriteLine("# Languages With Issues in Resx File\r\n");
            foreach ( KeyValuePair<string, GTranslate.Language> language in SortedLanguageNames )
            {
                string code = language.Key;
                string name = language.Value.Name;
                string translator = language.Value.SupportedServices.ToString();
                string ISO6391 = language.Value.ISO6391;
                string ISO6393 = language.Value.ISO6393;
                string NativeName = language.Value.NativeName;
                if ( !LanguageCodes.LanguageTagsNotRecongnizedByCompiler.Contains(ISO6391) )
                    continue;
                file2.WriteLine($"* **{name}** - {code}:{ISO6391}  [{translator}]");
            }
            file2.Close();
            System.Collections.Generic.SortedList<string, GTranslate.Language> ISO6391SortedLanguages = new();
            Dictionary<string,bool> LanguagesThatNeedToBeChecked = new();
            foreach ( var language in GTranslate_LangLookup )
            {
                string code = language.Key;
                string name = language.Value.Name;
                LanguagesThatNeedToBeChecked[code] = false;
                // Verify that no two languages have the same ISO6391 value in GTranslate_LangLookup
                if ( ISO6391SortedLanguages.ContainsKey(language.Value.ISO6391) )
                {
                    GTranslate.Language PreviousLanguage = ISO6391SortedLanguages[language.Value.ISO6391];
                    string errMsg = $"{ErrMsgPrefix}Found duplicate ISO639-1 tags for tag {language.Value.ISO6391}. Previous language=[{PreviousLanguage.Name}]; Current language=[{name}]";
                    Debug.WriteLine(errMsg);
                    DetectedErrors.Add(errMsg);
                    Debug.Assert(false, errMsg);
                }
                else
                    ISO6391SortedLanguages.Add(language.Value.ISO6391, language.Value);
            }
            foreach ( var languageSet in LanguageSets )
            {
                for ( int i = 0 ; i < languageSet.GetLength(0) ; ++i )
                {
                    string code = languageSet[i, 0];
                    string name = languageSet[i, 1];
                    if ( !GTranslate_LangLookup.ContainsKey(code) )
                    {
                        string Issue = $"{WrnMsgPrefix}{MethodBase.GetCurrentMethod().Name}: Did not find tag ({code})/Lang({name}) in GTranslate Languages.";
                        Debug.WriteLine(Issue);
                        DetectedErrors.Add(Issue);
                    }
                    else
                        LanguagesThatNeedToBeChecked[code] = true;
                }
            }
            foreach ( var lang in LanguagesThatNeedToBeChecked )
            {
                if ( !lang.Value )
                {
                    string Issue = $"{WrnMsgPrefix}Did not find tag ({lang.Key}) in any of the LanguageCodes translators.";
                    // Debug.WriteLine(Issue + "...");
                    DetectedMinorIssues.Add(Issue);
                }
            }
            Debug.WriteLine($"Finish validation. Found {DetectedErrors.Count} errors and {DetectedMinorIssues.Count} minor issues.");
        }
        private string ParseArgs(string[] args)
        {
            if ( args != null )
            {
                for ( int i = 0 ; i < args.Length ; i++ )
                {
                    string arg = args[i].StartsWith("--") ? args[i].Substring(1).ToLower() : args[i].ToLower();
                    switch ( arg )
                    {
                        case "-l":
                        case "-language":
                            if ( (i + 1) < args.Length && !args[i + 1].StartsWith("-") )
                                return args[++i];
                            break;
                        case "-f":
                        case "-file":
                            if ( (i + 1) < args.Length && !args[i + 1].StartsWith("-") )
                                inputBox.Text = args[++i];
                            break;
                    }
                }
            }
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
        private void UpdateOutPutBoxIfNeeded()
        {
            string outputBoxLastPathName = "";
            string outputBox2ndToLastPathName = "";
            try
            {
                switch ( _outputBoxRelationToInputBox )
                {
                    default:
                    case OutputBoxRelatedToInputBox.NoRelations:
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParent:
                        outputBox.Text = Path.GetDirectoryName(inputBox.Text);
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBoxParent_IsParent_OfInputBoxParent:
                        outputBoxLastPathName = Path.GetFileName(outputBox.Text);
                        outputBox.Text = $"{Path.GetDirectoryName(inputBox.Text)}\\{outputBoxLastPathName}";
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBoxParentParent_IsParent_OfInputBoxParent:
                        outputBoxLastPathName = Path.GetFileName(outputBox.Text);
                        outputBox2ndToLastPathName = Path.GetFileName(Path.GetDirectoryName(outputBox.Text));
                        outputBox.Text = $"{Path.GetDirectoryName(inputBox.Text)}\\{outputBox2ndToLastPathName}\\{outputBoxLastPathName}";
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParentParent:
                        outputBox.Text = Path.GetDirectoryName(Path.GetDirectoryName(inputBox.Text));
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBoxParent_IsParent_OfInputBoxParentParent:
                        outputBoxLastPathName = Path.GetFileName(outputBox.Text);
                        outputBox.Text = $"{Path.GetDirectoryName(Path.GetDirectoryName(inputBox.Text))}\\{outputBoxLastPathName}";
                        break;
                    case OutputBoxRelatedToInputBox.OutPutBoxParentParent_IsParent_OfInputBoxParentParent:
                        outputBoxLastPathName = Path.GetFileName(outputBox.Text);
                        outputBox2ndToLastPathName = Path.GetFileName(Path.GetDirectoryName(outputBox.Text));
                        outputBox.Text = $"{Path.GetDirectoryName(Path.GetDirectoryName(inputBox.Text))}\\{outputBox2ndToLastPathName}\\{outputBoxLastPathName}";
                        break;
                }
            }
            catch(Exception e)
            {
                LogToFile(e.Message, VerbosityLevels.Warn, true);
                // If exception triggered, break the relationship
                _outputBoxRelationToInputBox = OutputBoxRelatedToInputBox.NoRelations;
            }
        }
        private void BrowseFiles(object sender, EventArgs e)
		{
			if ( openResxFileDialog.ShowDialog() == DialogResult.OK )
			{
                inputBox.Text = openResxFileDialog.FileName;
                _ = CheckIfSrcResxHasAppendedLanguageToBaseFileName();
                if (string.IsNullOrEmpty(outputBox.Text) )
                {
                    outputBox.Text = Path.GetDirectoryName(inputBox.Text);
                    _outputBoxRelationToInputBox = OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParent;
                }
                else
                    UpdateOutPutBoxIfNeeded();
            }
        }
		private void BrowseFolders(object sender, EventArgs e)
		{
            if ( folderBrowserDialog.ShowDialog() == DialogResult.OK )
            {
                outputBox.Text = folderBrowserDialog.SelectedPath;
                _outputBoxRelationToInputBox = WhatIsOutputBoxRelationToInputBox();
            }
		}
		private string GetDefaultPath(string DefaultLoadedValue, string SubPathInRoamingApp, string? ParentPath = null)
		{
			if ( string.IsNullOrEmpty(ParentPath) )
				ParentPath = Application.UserAppDataPath;
			//ParentPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
			// Environment.ExpandEnvironmentVariables("%AppData%\\Roaming\\ABetterTranslator");

			string PathToReturn =  string.IsNullOrEmpty(DefaultLoadedValue) ?  ParentPath + "\\" + SubPathInRoamingApp : DefaultLoadedValue;
			try
			{
				if ( !Directory.Exists(PathToReturn) )
				{   // Make sure both top path and parent path exist. If not exist, create
					string ParentPathToReturn = Directory.GetParent(PathToReturn).ToString();
					if ( !Directory.Exists(ParentPathToReturn) )
						_ = Directory.CreateDirectory(ParentPathToReturn);
					_ = Directory.CreateDirectory(PathToReturn);
				}
			}
			catch ( Exception e )
			{
                LogToFile("[GetDefaultPath] Could NOT create path [" + PathToReturn + "]. Exception thrown:" + e.Message, VerbosityLevels.Errors);
				return "";
			}
			return PathToReturn;
		}
        private bool CheckIfSrcResxHasAppendedLanguageToBaseFileName()
		{
			if ( !string.IsNullOrEmpty(inputBox.Text) )
			{
				Match match = Regex.Match( Path.GetFileName(inputBox.Text), _getLanguageCodePattern);
				if ( match.Success )
				{
					string code = match.Groups["code"].Value.ToLower();
					comboBoxFromLanguage.SelectedIndex = GetLanguageIndex(code);
                    _SrcResxHasAppendedLanguageToBaseFileName = true;
					return true;
				}
			}
			_SrcResxHasAppendedLanguageToBaseFileName = false;
			return false;
		}
        private void ChangedResxInput(object sender, EventArgs e)
        {
            bool fileOK = inputBox.Text.Length > 0 && File.Exists(inputBox.Text);

            if ( fileOK )
            {
                if ( !CheckIfSrcResxHasAppendedLanguageToBaseFileName() )
                    comboBoxFromLanguage.SelectedIndex = GetDefaultLanguageIndex();
            }
            toolStripSplitButtonTranslate.Enabled = fileOK;
        }
        private void CheckedLanguage(object sender, ItemCheckedEventArgs e)
		{
			if ( !backendUpdate )
				ChangedResxInput(sender, e);
		}
		private void LockControls(bool lockControls = true, bool LeaveSummary = false)
		{
            tabControlTranslateResxSubTab.SelectedIndex = lockControls ? 1 : 0;
            bool EnableControls = !lockControls;
			inputBox.Enabled = EnableControls;
			browseFileButton.Enabled = EnableControls;

            browseFolderButton.Enabled = EnableControls;
            toolStripSplitButtonTranslate.Enabled = EnableControls;
            toolStripSplitButtonSelectAllLanugages.Enabled = EnableControls;
            toolStripSplitButtonResetDisplayToSet.Enabled = EnableControls;
            outputBox.Enabled = EnableControls;
			comboBoxFromLanguage.Enabled = EnableControls;

            bool ShowControls = lockControls;
            progressBarWhileTranslatingResxFile.Enabled = progressBarWhileTranslatingResxFile.Visible = ShowControls;
			
            toolStripButtonStopTranslation.Enabled = toolStripButtonStopTranslation.Visible = ShowControls;
			if ( LeaveSummary )
                tabControlTranslateResxSubTab.SelectedIndex = 1;

            progressBarWhileTranslatingResxFile.Minimum = 0;
			progressBarWhileTranslatingResxFile.Value = 0;
		}
		private void Log(string message, Color? color = null, Color? bkcolor = null)
        {
            Monitor.Enter(_lock);
            try
            {
			if ( color == null || color.Equals(Color.Black) )
			{
                logBox.AppendText(message);
				return;
			}

            Color fore = logBox.SelectionColor;
            Color LastBkfore = logBox.SelectionBackColor;
                logBox.SelectionColor = (Color) color;
                if ( bkcolor != null )
                {
                    logBox.SelectionBackColor = (Color) bkcolor;
                }

                logBox.AppendText(message);
                logBox.SelectionColor = fore;
                logBox.SelectionBackColor = LastBkfore;
                logBox.ScrollToCaret();
                Monitor.Pulse(_lock);
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
        private void LogToFile(Exception e, string CallingFunctionName, string ExtraMsg = "")=>LogToFile($"{CallingFunctionName}: {ExtraMsg}{e.Message}", VerbosityLevels.Errors, true);
        private void LogToFile(string message, VerbosityLevels verbositylevels)=>LogToFile(message, verbositylevels, true);
        private string FormatMessage(string message, VerbosityLevels verbositylevels, bool AddNextLineChr = false)
        {
            if ( AddNextLineChr && !message.Contains(NL) )
                message += NL;
            if ( verbositylevels == VerbosityLevels.Errors && !message.StartsWith(ErrMsgPrefix) )
                message = ErrMsgPrefix + message;
            else if ( verbositylevels == VerbosityLevels.Warn && !message.StartsWith(WrnMsgPrefix) )
                message = WrnMsgPrefix + message;
            else if ( verbositylevels == VerbosityLevels.Normal && !message.StartsWith(InfMsgPrefix) )
                message = InfMsgPrefix + message;
            else if ( verbositylevels == VerbosityLevels.Detail && !message.StartsWith(DetailsMsgPrefix) )
                message = DetailsMsgPrefix + message;
            return message;
        }
        private void LogToFile(string message, VerbosityLevels verbositylevels, bool DbgWriteLine)
        {
            Debug.Assert(verbositylevels != VerbosityLevels.Silent);
            message = FormatMessage(message, verbositylevels);

            if ( comboBoxLogFileVerbosityLevel.SelectedIndex != 3 && verbositylevels == VerbosityLevels.Detail)
                return;
            if (comboBoxLogFileVerbosityLevel.SelectedIndex < 2 && verbositylevels == VerbosityLevels.Normal)
                return;
            if (comboBoxLogFileVerbosityLevel.SelectedIndex == 0)
            {
                // When user sets verbosity level to silent, do not log duplicate error messages.
                if (_previousLoggingErrors.Contains(message))
                    return;
                _previousLoggingErrors.Add(message);
            }

            if ( _logFile != null)
                _logFile.WriteLineAsync(message);
            if ( DbgWriteLine )
                Debug.WriteLine(message);
        }
        private void WriteLine(Exception e, string CallingFunctionName)=>WriteLine($"{CallingFunctionName}: {e.Message}", LoggingColorSet.ErrorLogging, VerbosityLevels.Errors);

        private void LogLine(string message, VerbosityLevels verbositylevels)
        {
            message = FormatMessage(message, verbositylevels, true);
            switch ( verbositylevels )
            {
                case VerbosityLevels.Errors: // Always log errors, even if user verbosity selection is set to silent
                    LogToFile(message, verbositylevels);
                    break;
                case VerbosityLevels.LogToFileIfSilent:
                case VerbosityLevels.Normal:
                    if ( comboBoxLogFileVerbosityLevel.SelectedIndex > 1 )
                        LogToFile(message, VerbosityLevels.Normal, false);
                    break;
                case VerbosityLevels.LogToFileIfNotDetail:
                case VerbosityLevels.Detail:
                    if ( comboBoxLogFileVerbosityLevel.SelectedIndex == 3 )
                        LogToFile(message, VerbosityLevels.Detail, false);
                    break;
                case VerbosityLevels.DebugWriteIfSilent:
                case VerbosityLevels.DebugWriteIfNotDetail:
                default:
                case VerbosityLevels.Silent:
                    Debug.Assert(false); // Code should never reach this point, because this function should not be called with this value
                    break;
            }
        }
        private void LogLine(string message, OutPutLevel outputlevel, Color? color)=>Log(message + NL, color);

        private void WriteLine(string message, LoggingColorSet logColorSet)=> WriteLine(message, logColorSet, VerbosityLevels.Normal);
        private void WriteLine(string message, LoggingColorSet logColorSet, VerbosityLevels verbositylevels = VerbosityLevels.Normal)
        {
            message = FormatMessage(message, logColorSet == LoggingColorSet.ErrorLogging ? VerbosityLevels.Errors : verbositylevels, true);
            if ( logColorSet != LoggingColorSet.ErrorLogging && logColorSet != LoggingColorSet.PreStartLogging && logColorSet != LoggingColorSet.StartAndEndLogging )
            {
                if ((verbositylevels == VerbosityLevels.Detail && comboBoxScreenVerbosityLevel.SelectedIndex < 3) ||
                   (verbositylevels == VerbosityLevels.Normal && comboBoxScreenVerbosityLevel.SelectedIndex < 2))
                {
                    if ( comboBoxLogFileVerbosityLevel.SelectedIndex == 3)
                        LogLine(message, verbositylevels);
                    return;
                }
                if ( comboBoxScreenVerbosityLevel.SelectedIndex == 0)
                {
                    if (verbositylevels == VerbosityLevels.DebugWriteIfSilent)
                    {
                        Debug.WriteLine(message);
                        return;
                    }
                    if (verbositylevels == VerbosityLevels.LogToFileIfSilent )
                    {
                        Debug.WriteLine(message);
                        LogLine(message, verbositylevels);
                        return;
                    }
                }
                if (comboBoxScreenVerbosityLevel.SelectedIndex != 3)
                {
                    if ( verbositylevels == VerbosityLevels.DebugWriteIfNotDetail )
                    {
                        Debug.WriteLine(message);
                        return;
                    }

                    if ( verbositylevels == VerbosityLevels.LogToFileIfNotDetail )
                    {
                        LogLine(message, verbositylevels);
                        return;
                    }
                }

                if (verbositylevels == VerbosityLevels.Errors)
                    logColorSet = LoggingColorSet.ErrorLogging;
            }
            
            if ( comboBoxLogFileVerbosityLevel.SelectedIndex == 3 )
                LogLine(message, verbositylevels);

            const string ErrLineWrap = "##################################################################################" + NL;
            Color FgColor = Color.Black;
            Color BgColor = Color.White;
            switch ( logColorSet )
            {
				case LoggingColorSet.NormalLogging:
                    FgColor = Color.Black;
                    BgColor = Color.White;
                    break;
                case  LoggingColorSet.ErrorLogging:
                    FgColor = ErrFgColor;
                    BgColor = ErrBgColor;
                    message = ErrLineWrap + message + ErrLineWrap; // Add extra line before and after error message
                    break;
                case LoggingColorSet.StartAndEndLogging:
                    FgColor = StartAndEndFgColor;
                    BgColor = StartAndEndBgColor;
                    break;
                case LoggingColorSet.ProgressLogging:
                    FgColor = ProgressInfoFgColor;
                    BgColor = ProgressInfoBgColor;
                    break;
                case LoggingColorSet.PreStartLogging:
                    FgColor = PreStartInfoFgColor;
                    BgColor = PreStartInfoBgColor;
                    break;
                case LoggingColorSet.BackupLogging:
                    FgColor = BackUpInfoFgColor;
                    BgColor = BackUpInfoBgColor;
                    break;
            }
            Log(message, FgColor, BgColor);
        }
        private void buttonOpenFolderInWindowsExplorer_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(outputBox.Text) )
                MessageBox.Show($"Please enter a valid folder in [Output Directory], before using this option.", "Empty Directory Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ( Directory.Exists(outputBox.Text) )
                Process.Start("explorer.exe", outputBox.Text);
            else
                MessageBox.Show($"Directory does NOT exist!\n[{outputBox.Text}]", "Directory does NOT exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void buttonInAdvOpt_OpenFolderInWindowsExplorer_Click(object sender, EventArgs e) => buttonOpenFolderInWindowsExplorer_Click(sender, e);
        private void buttonOpenBackupFolderInFileExplorer_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(textBoxBkUpDir.Text) )
                MessageBox.Show($"Please enter a valid folder in [Output Directory], before using this option.", "Empty Directory Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ( Directory.Exists(textBoxBkUpDir.Text) )
                Process.Start("explorer.exe", textBoxBkUpDir.Text);
            else
                MessageBox.Show($"Directory does NOT exist!\n[{textBoxBkUpDir.Text}]", "Directory does NOT exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void buttonOpenLoggingFolderInFileExplorer_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(textBoxLogFolderPath.Text) )
                MessageBox.Show($"Please enter a valid folder in [Output Directory], before using this option.", "Empty Directory Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ( Directory.Exists(textBoxLogFolderPath.Text) )
                Process.Start("explorer.exe", textBoxLogFolderPath.Text);
            else
                MessageBox.Show($"Directory does NOT exist!\n[{textBoxLogFolderPath.Text}]", "Directory does NOT exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void textBoxFilterText_TextChanged(object sender, EventArgs e)
        {
            TimedFilter(languageList, ((System.Windows.Forms.TextBox) sender).Text, 0);
        }
        public void TimedFilter(BrightIdeasSoftware.ObjectListView olv, string txt)
        {
            TimedFilter(olv, txt, 0);
        }
        public void TimedFilter(BrightIdeasSoftware.ObjectListView olv, string txt, int matchKind)
        {
            TextMatchFilter filter = null;
            if ( !String.IsNullOrEmpty(txt) )
            {
                switch ( matchKind )
                {
                    case 0:
                    default:
                        filter = TextMatchFilter.Contains(olv, txt);
                        break;
                    case 1:
                        filter = TextMatchFilter.Prefix(olv, txt);
                        break;
                    case 2:
                        filter = TextMatchFilter.Regex(olv, txt);
                        break;
                }
            }
            // Text highlighting requires at least a default renderer
            if ( olv.DefaultRenderer == null )
                olv.DefaultRenderer = new HighlightTextRenderer(filter);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            olv.AdditionalFilter = filter;
            stopWatch.Stop();
        }
        private void CreateLogFile()
        {
            if ( Directory.Exists(textBoxLogFolderPath.Text) )
            {
                string LogFileName = $"{textBoxLogFolderPath.Text}\\{_logFileName}";
                if ( _logFile != null )
                    _logFile.Close();
                _logFile = new(LogFileName, append: true);
            }
        }
        public bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach ( Screen screen in screens )
            {
                Point formTopLeft = new Point( form.Left, form.Top );

                if ( screen.WorkingArea.Contains(formTopLeft) )
                {
                    return true;
                }
            }

            return false;
        }
        private void textBoxLogFolderPath_TextChanged(object sender, EventArgs e)
        {
            CreateLogFile();
        }
        private OutputBoxRelatedToInputBox WhatIsOutputBoxRelationToInputBox()
        {//_outputBoxRelationToInputBox
            if ( string.IsNullOrEmpty(inputBox.Text) )
                return OutputBoxRelatedToInputBox.NoRelations;
            string inputBoxParent = Path.GetDirectoryName(inputBox.Text);
            if (string.IsNullOrEmpty(outputBox.Text) )
                outputBox.Text = inputBoxParent;
            if ( inputBoxParent.Equals(outputBox.Text) )
                return OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParent;
            string outputBoxParent = Path.GetDirectoryName(outputBox.Text);
            if ( inputBoxParent.Equals(outputBoxParent) )
                return OutputBoxRelatedToInputBox.OutPutBoxParent_IsParent_OfInputBoxParent;
            string inputBoxParentParent = Path.GetDirectoryName(inputBoxParent);
            if ( !string.IsNullOrEmpty(inputBoxParentParent) )
            {
                if (inputBoxParentParent.Equals(outputBox.Text))
                    return OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParentParent;
                if ( inputBoxParentParent.Equals(outputBoxParent) )
                    return OutputBoxRelatedToInputBox.OutPutBoxParent_IsParent_OfInputBoxParentParent;
            }
            string outputBoxParentParent = Path.GetDirectoryName(outputBoxParent);
            if ( !string.IsNullOrEmpty(outputBoxParentParent) )
            {
                if (outputBoxParentParent.Equals(inputBoxParent))
                    return OutputBoxRelatedToInputBox.OutPutBoxParentParent_IsParent_OfInputBoxParent;
                if (outputBoxParentParent.Equals(inputBoxParentParent))
                    return OutputBoxRelatedToInputBox.OutPutBoxParentParent_IsParent_OfInputBoxParentParent;
            }
            return OutputBoxRelatedToInputBox.NoRelations;
        }
        #endregion Class constructor and miscellaneous methods

        #region Load and Save method for persistent user inputs
        private void MainWindow_Load(object sender, EventArgs e)
		{
            if ( ModifierKeys != Keys.Shift )
            {
                try
                {
                    // Miscellaneous values
                    Location = Settings.Default.WindowLocation;
                    Size = Settings.Default.WindowSize;
                    SelectedLanguageSet = Settings.Default.SelectedLanguageSet;

                    // text fields
                    inputBox.Text = Settings.Default.inputBox;
                    MaxThread.Text = Settings.Default.MaxThread;
                    MaxTranslateLen.Text = Settings.Default.MaxTranslateLen;
                    outputBox.Text = Settings.Default.outputBox;
                    textBoxBkUpDir.Text = Settings.Default.textBoxBkUpDir;
                    textBoxLogFolderPath.Text = Settings.Default.textBoxLogFolderPath;
                    textBoxTextToTranslate.Text = Settings.Default.textBoxTextToTranslate;

                    // checkbox settings
                    checkBoxBackupFilesBeforeTranslation.Checked = Settings.Default.checkBoxBackupFilesBeforeTranslation;
                    checkBoxDeleteLangResxFilesBeforeTranslation.Checked = Settings.Default.checkBoxDeleteLangResxFilesBeforeTranslation;
                    checkBoxDispalyWarningPrompts.Checked = Settings.Default.checkBoxDispalyWarningPrompts;

                    // comboboxes
                    comboBoxAddOriginalSrcTextToComment.SelectedIndex = Settings.Default.comboBoxAddOriginalSrcTextToComment;
                    comboBoxDefaultLanguageSet.SelectedIndex = Settings.Default.comboBoxDefaultLanguageSet;
                    comboBoxFromLang.SelectedIndex = Settings.Default.comboBoxFromLang;
                    comboBoxFromLanguage.SelectedIndex = Settings.Default.comboBoxFromLanguage;
                    comboBoxItemsPerTransaltionRequest.SelectedIndex = Settings.Default.comboBoxItemsPerTransaltionRequest;
                    comboBoxLogFileVerbosityLevel.SelectedIndex = Settings.Default.comboBoxLogFileVerbosityLevel;
                    comboBoxScreenVerbosityLevel.SelectedIndex = Settings.Default.comboBoxScreenVerbosityLevel;
                    comboBoxToLang.SelectedIndex = Settings.Default.comboBoxToLang;
                    tabs.SelectedIndex = Settings.Default.tabs;
                    // Keep above code clean so it can be copied and pasted between Load and FormClosing, and a Regex replace can be performed
                    // Above code should only have plain assignments, and non-assignment code should be placed below
                    textBoxBkUpDir.Text = GetDefaultPath(textBoxBkUpDir.Text, "BackupResx");
                    textBoxLogFolderPath.Text = GetDefaultPath(Settings.Default.textBoxLogFolderPath, "Log", Path.GetTempPath() + "ABetterTranslator");
                    if ( Int32.Parse(MaxThread.Text) < 0 )
                        MaxThread.Text = Math.Max(Environment.ProcessorCount, 4).ToString();
                    if ( comboBoxFromLanguage.SelectedIndex == -1 )
                        comboBoxFromLanguage.SelectedIndex = comboBoxFromLang.SelectedIndex = comboBoxToLang.SelectedIndex = GetValid_codeBox_SelectIndex();
                    try
                    {
                        if ( SelectedLanguageSet > -1 )
                            PopulateLanguageSet((LanguageSet) SelectedLanguageSet);
                    }
                    catch ( Exception eee )
                    {
                        LogToFile(eee, MethodBase.GetCurrentMethod().Name + "-1");
                    }

                    // ToDo: Change how PopulateLanguageSet gets called.  It needs to come before setting the following three values, so they don't have to be reset again within this function.
                    comboBoxFromLang.SelectedIndex = Settings.Default.comboBoxFromLang;
                    comboBoxFromLanguage.SelectedIndex = Settings.Default.comboBoxFromLanguage;
                    comboBoxToLang.SelectedIndex = Settings.Default.comboBoxToLang;

                    if ( Settings.Default.languageList != null && Settings.Default.languageList.Count > 0 )
                    {
                        ArrayList l = new ArrayList();
                        for ( int count = 0 ; count < languageList.GetItemCount() ; count++ )
                        {
                            ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails) languageList.GetModelObject(count);
                            if ( languageList.IsDisabled(objlistvwrowdetails) )
                                continue;
                            if ( Settings.Default.languageList.Contains(objlistvwrowdetails.LanguageTag) )
                                l.Add(objlistvwrowdetails);
                        }
                        languageList.CheckObjects(l);
                    }

                    _lastValueOf_inputBox = inputBox.Text;
                    _outputBoxRelationToInputBox = WhatIsOutputBoxRelationToInputBox();
                    _ = CheckIfSrcResxHasAppendedLanguageToBaseFileName();
                    CreateLogFile();
                }
                catch ( Exception ee )
                {
                    LogToFile(ee, MethodBase.GetCurrentMethod().Name + "-2");
                }
            }

            try
            {
                if ( string.IsNullOrEmpty(comboBoxFromLanguage.Text) )
                    comboBoxFromLanguage.SelectedIndex = comboBoxFromLang.SelectedIndex = GetDefaultLanguageIndex();
            } catch(Exception ee)
            {
                LogToFile(ee, MethodBase.GetCurrentMethod().Name + "-3");
            }

            if ( !IsOnScreen(this) )
                Location = new Point(0, 0);
            _isFormReady = true;
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
            // Miscellaneous values
            Settings.Default.WindowLocation = Location;
            Settings.Default.WindowSize = Size;
            Settings.Default.SelectedLanguageSet = SelectedLanguageSet;

            // text fields
            Settings.Default.inputBox = inputBox.Text;
            Settings.Default.MaxThread = MaxThread.Text;
            Settings.Default.MaxTranslateLen = MaxTranslateLen.Text;
            Settings.Default.outputBox = outputBox.Text;
            Settings.Default.textBoxBkUpDir = textBoxBkUpDir.Text;
            Settings.Default.textBoxLogFolderPath = textBoxLogFolderPath.Text;
            Settings.Default.textBoxTextToTranslate = textBoxTextToTranslate.Text;

            // checkbox settings
            Settings.Default.checkBoxBackupFilesBeforeTranslation = checkBoxBackupFilesBeforeTranslation.Checked;
            Settings.Default.checkBoxDeleteLangResxFilesBeforeTranslation = checkBoxDeleteLangResxFilesBeforeTranslation.Checked;
            Settings.Default.checkBoxDispalyWarningPrompts = checkBoxDispalyWarningPrompts.Checked;

            // comboboxes
            Settings.Default.comboBoxAddOriginalSrcTextToComment = comboBoxAddOriginalSrcTextToComment.SelectedIndex;
            Settings.Default.comboBoxDefaultLanguageSet = comboBoxDefaultLanguageSet.SelectedIndex;
            Settings.Default.comboBoxFromLang = comboBoxFromLang.SelectedIndex;
            Settings.Default.comboBoxFromLanguage = comboBoxFromLanguage.SelectedIndex;
            Settings.Default.comboBoxItemsPerTransaltionRequest = comboBoxItemsPerTransaltionRequest.SelectedIndex;
            Settings.Default.comboBoxLogFileVerbosityLevel = comboBoxLogFileVerbosityLevel.SelectedIndex;
            Settings.Default.comboBoxScreenVerbosityLevel = comboBoxScreenVerbosityLevel.SelectedIndex;
            Settings.Default.comboBoxToLang = comboBoxToLang.SelectedIndex;
            Settings.Default.tabs = tabs.SelectedIndex;

            // Keep above code clean so it can be copied and pasted between Load and FormClosing, and a Regex replace can be performed
            // Above code should only have plain assignments, and non-assignment code should be placed below
            if ( WindowState != FormWindowState.Normal )
				Settings.Default.WindowSize = RestoreBounds.Size;

            if ( Settings.Default.languageList == null )
                Settings.Default.languageList = new System.Collections.Specialized.StringCollection();
            Settings.Default.languageList.Clear();
            foreach ( var obj in languageList.CheckedObjects )
                _ = Settings.Default.languageList.Add(((ObjListVwRowDetails) obj).LanguageTag);

            Settings.Default.Save();
		}
        #endregion Load and Save method for persistent user inputs

        #region Main MainWindow Translation Logic
        private void RunBackgroundTask(MultiThreadTranslator_Options options)
		{
			Debug.Assert(_translationRequestPackets != null);
			_multiThreadTranslator = new ResxMultiThreadTranslator(this, options);
			bool success = _multiThreadTranslator.TranslateData(_translationRequestPackets);
			if ( !success )
			{
                LogToFile("[RunBackgroundTask] TranslateData failed.", VerbosityLevels.Errors);
				throw new Exception("TranslateData failed.");
			}
		}
		private void TranslateFile(object sender, EventArgs e)=>TranslateFile();

        private string GetLanguageNameOfTagFrom_List(string Tag, string[,] LangList) //, bool ConvertAliasTagToStandardTag = false)
        {
            string tag = Tag.ToLower();
            for ( int i = 0 ; i < LangList.GetLength(0) ; ++i )
            {
                string Iso639_1_code = LangList[i, 0];
                string name = LangList[i, 1];
                if ( Iso639_1_code.ToLower().Equals(tag) )
                    return name;
            }
            return "";
        }

        private string GetLanguageNameOfTag(string Tag)
        {
            string tag = Tag.ToLower();
            if ( tag.Length == 3 )
            {
                for ( int i = 0 ; i < LanguageCodes.ISO639_1_To_ISO639_3.GetLength(0) ; ++i )
                {
                    string Iso639_1_code = LanguageCodes.ISO639_1_To_ISO639_3[i, 0];
                    string aliasCode = LanguageCodes.ISO639_1_To_ISO639_3[i, 1];
                    if ( aliasCode.ToLower().Equals(tag) )
                    {
                        tag = Iso639_1_code.ToLower();
                        break;
                    }
                }
            }

            foreach ( var set in LanguageSets )
            {
                string LangName = GetLanguageNameOfTagFrom_List(tag, set);
                if ( !string.IsNullOrEmpty(LangName) )
                    return LangName;
            }
            return Tag;
        }
        private int GetDefaultLanguageIndex()=>GetLanguageIndex();

        private int GetLanguageIndex(string Tag = "")
        {
            if ( !string.IsNullOrEmpty(Tag) && comboBoxFromLanguage.Items.Contains(GetLanguageNameOfTag(Tag)) )
                return comboBoxFromLanguage.Items.IndexOf(GetLanguageNameOfTag(Tag));
            if ( comboBoxFromLanguage.Items.Contains(GetLanguageNameOfTag(ApplicationDefaultLanguage)) )
                return comboBoxFromLanguage.Items.IndexOf(GetLanguageNameOfTag(ApplicationDefaultLanguage));
            if ( comboBoxFromLanguage.Items.Contains(GetLanguageNameOfTag("en")) )
                return comboBoxFromLanguage.Items.IndexOf(GetLanguageNameOfTag("en"));
            if ( comboBoxFromLanguage.Items.Contains(GetLanguageNameOfTag("en-US")) )
                return comboBoxFromLanguage.Items.IndexOf(GetLanguageNameOfTag("en-US"));
            if ( comboBoxFromLanguage.Items.Contains(GetLanguageNameOfTag("eng")) )
                return comboBoxFromLanguage.Items.IndexOf(GetLanguageNameOfTag("eng"));
            return 0;
        }
        private int GetValid_codeBox_SelectIndex()
        {
            if ( comboBoxFromLanguage.SelectedIndex < 0 )
            {
                comboBoxFromLanguage.SelectedIndex = GetDefaultLanguageIndex();
            }
            return comboBoxFromLanguage.SelectedIndex;
        }
        private string GetTag(string LangName)
        {
            if ( string.IsNullOrEmpty(LangName) )
                return "";
            string langName = LangName.ToLower();
            foreach ( var language in GTranslate_LangLookup )
                if ( language.Value.Name.ToLower().Equals(langName))
                    return language.Key;

            for ( int i = 0 ; i < LanguageCodes.LanguageCodesAndAliases.GetLength(0) ; ++i )
                if ( LanguageCodes.LanguageCodesAndAliases[i, 1].ToLower().Equals(langName) )
                    return LanguageCodes.LanguageCodesAndAliases[i, 0];
            return "";
        }
        private bool CheckIfValidValuesFor_InputboxFileAndOutPutDir()
        {
			if ( string.IsNullOrEmpty(inputBox.Text) )
                return false;
            if ( !File.Exists(inputBox.Text) )
                return false;
            if ( string.IsNullOrEmpty(outputBox.Text) )
            {
                outputBox.Text = Path.GetDirectoryName(inputBox.Text);
                if ( string.IsNullOrEmpty(outputBox.Text) )
                    return false;
                _outputBoxRelationToInputBox = OutputBoxRelatedToInputBox.OutPutBox_IsParent_OfInputBoxParent;
            }
            if ( !Directory.Exists(outputBox.Text) )
            {
                _ = Directory.CreateDirectory(outputBox.Text);
                if ( !Directory.Exists(outputBox.Text) )
                    return false;
            }
            return true;
        }
        private bool IsValid_InputboxFileAndOutPutDir(bool DisplayErrorMessageIfFail = true)
        {
            bool isvalid = CheckIfValidValuesFor_InputboxFileAndOutPutDir();
            if (!isvalid && DisplayErrorMessageIfFail )
            {
                MessageBox.Show("Invalid value(s) in Resx file field or in output directory.\nPlease enter proper values before using this option.", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isvalid;
        }
        private async void TranslateFile()
		{
            if ( !IsValid_InputboxFileAndOutPutDir() )
                return;
            string inputPath = Path.GetFullPath(inputBox.Text);
			string fromCode = GetTag(comboBoxFromLanguage.Text);
            if (!File.Exists(inputPath))
            {
                MessageBox.Show($"File [{inputPath}] does NOT exists!\nPlease select a valid Resx file before selecting this option.", "Resx NOT Exist!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
			LockControls();
            logBox.Clear();
            statusLabel.Visible = true;


			string? outputDir = outputBox.Text.Length > 0
				? Path.GetFullPath(outputBox.Text)
				: Path.GetDirectoryName(inputPath);
			if (!Directory.Exists(outputDir) )
				_ = Directory.CreateDirectory(outputDir);

            if ( checkBoxDeleteLangResxFilesBeforeTranslation.Checked )
			{
				bool doDeleteFiles = true;

				if ( checkBoxDispalyWarningPrompts.Checked )
				{
					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete all Resx files having a language code appended to the base file name?\n\nSelect [Yes] to delete the files.\nSelect [No] to continue translation without deletion.\nSelect [Cancel] to quit translation." + MsgToDisableWarnPrompts,
						  "Deleting Appended Language Resx Files", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if ( dialogResult == DialogResult.Cancel )
					{
						LockControls(false);
						return;
					}

					if ( dialogResult == DialogResult.No )
                        doDeleteFiles = false;
				}
				if ( doDeleteFiles )
				{
					List<string> filesToDelete = Directory.GetFiles(outputDir, "*.*.resx").ToList();
					foreach ( string file in filesToDelete )
					{
						if ( !file.ToLower().Equals(inputPath.ToLower()) )
							File.Delete(file);
					}
				}
			}
			cancellation = new CancellationTokenSource();
			_translationRequestPackets = new List<TranslationRequestPacket>();
			TranslationRequestPacket sourceResxData = new()
			{
				fromLang = fromCode,
                translateAsSingleDocument = true
            };
			ResXResourceSet resSet = new(inputPath);
            string ConsolidateLines = "";
            IDictionaryEnumerator dict = resSet.GetEnumerator();
			while ( dict.MoveNext() )
			{
				if ( dict.Value is string @string && !string.IsNullOrEmpty(@string))
				{
					string Key = dict.Key.ToString().TrimEnd(_numberCharsToTrim);
                    if (Key.EndsWith(".Text") || Key.EndsWith(".Items") || Key.EndsWith(".ToolTipText") || Key.EndsWith(".ToolTip") )
                    {
                        string Line = dict.Value.ToString();
                        sourceResxData.sourceText.Add(Line);
                        ConsolidateLines += Line + NL;
                        if (Line.Contains(NL))
                            sourceResxData.translateAsSingleDocument = false;
                    }
                }

            }
            resSet.Close();
            if (sourceResxData.translateAsSingleDocument && ConsolidateLines.Length < Int32.Parse(MaxTranslateLen.Text) && comboBoxItemsPerTransaltionRequest.SelectedIndex != 1)
            {
                sourceResxData.sourceText.Clear();
                sourceResxData.sourceText.Add(ConsolidateLines);
            }
            else
                sourceResxData.translateAsSingleDocument = false;

            string filepath = $"{outputBox.Text}\\{Path.GetFileNameWithoutExtension(inputPath)}";
            List<string> LangCodesIncluded = new();
            foreach ( var obj in languageList.CheckedObjects )
			{
                ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails)obj;
                if ( languageList.IsDisabled(objlistvwrowdetails) )
                    continue;
                string displayName = objlistvwrowdetails.LanguageName;
                string toCode = objlistvwrowdetails.LanguageTranslatorTag;
                
				if ( LangCodesIncluded.Contains(toCode) ) // Make sure we don't do multiple languages having the same code, but different aliases
					continue;
				LangCodesIncluded.Add(toCode);
				string appendedfilename = toCode;
				string outputFile = $"{filepath}.{appendedfilename}.resx";
                try
				{
					TranslationRequestPacket translationRequestPacket = new()
					{
						fromLang = fromCode,
						toLang = toCode,
						inputPath = inputPath,
						outputFile = outputFile,
                        translateAsSingleDocument = sourceResxData.translateAsSingleDocument,
                    };
					foreach ( string _item in sourceResxData.sourceText )
					{
						translationRequestPacket.sourceText.Add(_item);

					}
					_translationRequestPackets.Add(translationRequestPacket);
                    string msg = "Including ";
                    if ( sourceResxData.sourceText.Count > 1 )
                        msg = $"Adding {sourceResxData.sourceText.Count} items for ";
                   WriteLine($"{msg} language {displayName} [{toCode}]; Culture-Details:([{GetAppendedFileName(toCode)}], [{GetCultureName(toCode)}], [{GetCulture_2LetterISOName(toCode)}], [{GetCulture_3LetterISOName(toCode)}], [{GetCulture_Windows3LetterName(toCode)}], [{GetNativeName(toCode)}])", LoggingColorSet.PreStartLogging, VerbosityLevels.Normal);
                }
				catch ( Exception exc )
				{
					WriteLine(exc, MethodBase.GetCurrentMethod().Name);
				}
			}
			statusLabel.Text = $"Translating {LangCodesIncluded.Count} languages";
            MultiThreadTranslator_Options options = new();
            options._progressBarOutput = true;
            options._maxWorkerThreads = Int32.Parse(MaxThread.Text);
            options._maximumTranslateDataLength = Int32.Parse(MaxTranslateLen.Text);
            options._itemsPerTransReq = comboBoxItemsPerTransaltionRequest.SelectedIndex switch
            {
                0 => ItemsPerTransReq.Auto,
                1 => ItemsPerTransReq.OnePerItem,
                _ => ItemsPerTransReq.Many,
            };
            switch ( comboBoxScreenVerbosityLevel.SelectedIndex )
            {
                case (int) VerbosityLevels.Silent: // Silent
                    options._silent = true;
                    options._verbose = false;
                    break;
                case (int) VerbosityLevels.Errors: // Errors
                    options._silent = false;
                    options._verbose = false;
                    break;
                default:
                case (int) VerbosityLevels.Normal: // Normal
                    options._silent = false;
                    options._verbose = false;
                    break;
                case (int) VerbosityLevels.Detail: // Detail
                    options._silent = false;
                    options._verbose = true;
                    break;
            }
			await Task.Run(() => RunBackgroundTask(options));
		}
        private void SaveTranslations(TranslationRequestPacket item)
        {
            string inputPath = item.inputPath;
            string outputFile = item.outputFile;
            int idx = 0;
            try
            {
                if ( checkBoxBackupFilesBeforeTranslation.Checked )
                {
                    if ( Directory.Exists(textBoxBkUpDir.Text) )
                    {
                        string timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                        string NewSubPath = textBoxBkUpDir.Text + "\\" + timeStamp;
                        _ = Directory.CreateDirectory(NewSubPath);
                        System.IO.DirectoryInfo di = new(NewSubPath);
                        WriteLine($"Backing up Resx files to path {NewSubPath}", LoggingColorSet.BackupLogging, VerbosityLevels.LogToFileIfNotDetail);
                        foreach ( FileInfo file in di.GetFiles("*.resx",  SearchOption.TopDirectoryOnly) )
                        {
                            if ( Path.GetFileNameWithoutExtension(file.FullName).Contains('.') )
                            {
                                string fileName = System.IO.Path.GetFileName(file.FullName);
                                string destFile = System.IO.Path.Combine(NewSubPath, fileName);
                                WriteLine($"Backing up file {file.FullName} to {destFile}", LoggingColorSet.BackupLogging, VerbosityLevels.LogToFileIfNotDetail);
                                System.IO.File.Copy(file.FullName, destFile, true);
                            }
                        }

                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Backup folder \"" + textBoxBkUpDir.Text + "\" does not exists.\nResx files can not be backed up, before translation.\n\nDo you want to continue translation anyway?",
                           "Backup Folder does NOT Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if ( dialogResult == DialogResult.No )
                            return;
                    }
                }

                if (item.translateAsSingleDocument && item.sourceText.Count == 1)
                {
                    string SrcSingleDoc = item.sourceText[0];
                    string TranSingleDoc = item.translatedText[0];
                    item.sourceText.Clear();
                    item.translatedText.Clear();
                    string[] SrcLines = SrcSingleDoc.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string[] TranslatedLines = TranSingleDoc.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach ( string line in SrcLines )
                        item.sourceText.Add(line);
                    foreach ( string line in TranslatedLines )
                        item.translatedText.Add(line);
                }
                WriteLine($"Creating Resx file {outputFile}", LoggingColorSet.ProgressLogging, VerbosityLevels.Detail);
                ResXResourceWriter rw = new(outputFile);
                ResXResourceReader rr = new(inputPath);
                rr.UseResXDataNodes = true;
                foreach ( DictionaryEntry d in rr )
                {
                    ResXDataNode? node = d.Value as ResXDataNode;
                    object? value =  node.GetValue(null as System.ComponentModel.Design.ITypeResolutionService);
                    string comment = node.Comment;
                    if ( value is string )
                    {
                        string Key = d.Key.ToString().TrimEnd(_numberCharsToTrim);
                        if ( Key.EndsWith(".Text") || Key.EndsWith(".Items") || Key.EndsWith(".ToolTipText") || Key.EndsWith(".ToolTip") )
                        {
                            if ( value.Equals(item.sourceText[idx]) )
                            {
                                node = new ResXDataNode(Key, item.translatedText[idx++]);
                                // ToDo: Add GUI option to include original text in Comment field if it's not populated.
                                node.Comment = comment;
                            }
                            else
                                LogToFile($"[SaveTranslations] Source value did not match Resx file value for item [{Key}]. SrcVal=[{value}]; ResxVal=[{item.sourceText[idx]}]", VerbosityLevels.Errors );
                        }
                    }
                    rw.AddResource(node);
                }
                rr.Close();
                rw.Generate();
                rw.Close();
                WriteLine($"Resx file {outputFile} complete!", LoggingColorSet.ProgressLogging, VerbosityLevels.Detail);
            }
            catch ( Exception e )
            {
                string msg = $"[SaveTranslations]: Exception thrown for language {GetLanguageNameOfTag(item.toLang)} ({item.toLang});output={item.outputFile};source={item.inputPath}; Received Error Message: {e.Message}";
                LogToFile(e, MethodBase.GetCurrentMethod().Name);
                WriteLine(msg, LoggingColorSet.ErrorLogging, VerbosityLevels.Normal);
            }
        }
        private void toolStripButtonStopTranslation_Click(object sender, EventArgs e)
        {
            cancellation.Cancel();
            toolStripButtonStopTranslation.Enabled = false;
            statusLabel.Text = "Cancellation in Progress";
            if ( _multiThreadTranslator != null )
                _multiThreadTranslator._cancelTranslation = true;
        }
        #endregion Main MainWindow Translation Logic

        #region ResxMultiThreadTranslator Delegate Functions 
        public void Translator_TaskComplete(int QtyItemsCompleted, TimeSpan totalTranslationTime)
        {
            Debug.Assert(_multiThreadTranslator != null);
            Debug.Assert(_translationRequestPackets != null);
            Debug.Assert(_multiThreadTranslator._itemsToTranslate != null);
            if (!_multiThreadTranslator._options._progressBarOutput)
            {
                Translator_CompleteForTranslateText_Tab(QtyItemsCompleted);
                return;
            }
            Stopwatch sw = new();
            int qtyItemsThatNeededToBeTranslated = progressBarWhileTranslatingResxFile.Maximum;
            _translationRequestPackets = _multiThreadTranslator._itemsToTranslate;
            if ( QtyItemsCompleted < 1 || QtyItemsCompleted > progressBarWhileTranslatingResxFile.Maximum ) //TotalCount < 1 || TotalCount > progressBarWhileTranslatingResxFile.Maximum )
                WriteLine($"Received invalid value for ShowProgress. Received QtyItemsCompleted = ({QtyItemsCompleted}); Expected value between 1 and ({progressBarWhileTranslatingResxFile.Maximum})", LoggingColorSet.ErrorLogging);
            else
                progressBarWhileTranslatingResxFile.Value = QtyItemsCompleted;
            Dictionary<string, LanguageProcessStatus> LanguagesProcessedSuccessStatuses = _multiThreadTranslator.LanguagesProcessedSuccessStatuses;
            int QtySuccess = LanguagesProcessedSuccessStatuses.Count(x => x.Value == LanguageProcessStatus.Success);
            int QtyNotSuccess = LanguagesProcessedSuccessStatuses.Count(x => x.Value != LanguageProcessStatus.Success);
            int QtyFailed = LanguagesProcessedSuccessStatuses.Count(x => x.Value == LanguageProcessStatus.Failed);
            int QtyUnknown = LanguagesProcessedSuccessStatuses.Count(x => x.Value == LanguageProcessStatus.Unknown);
            string LanguagesWhichProducedErrors = "";
            foreach ( KeyValuePair<string, LanguageProcessStatus> langStatus in LanguagesProcessedSuccessStatuses )
                if ( langStatus.Value != LanguageProcessStatus.Success)
                    LanguagesWhichProducedErrors += $"{GetLanguageNameOfTag(langStatus.Key)} ({langStatus.Key})={langStatus.Value}; ";

            if ( !string.IsNullOrEmpty(LanguagesWhichProducedErrors) )
                WriteLine($"{QtyFailed} language(s) had failed status. {QtyUnknown} language(s) had unknown status. The following {QtyNotSuccess} language(s) failed to translate due to failed or unknown status;\n{LanguagesWhichProducedErrors}", LoggingColorSet.ErrorLogging);
            statusLabel.Text = $"Translations complete. Now writing to Resx file(s).";

            foreach ( TranslationRequestPacket item in _translationRequestPackets )
            {
                if ( cancellation.IsCancellationRequested )
                    break;
                if ( LanguagesProcessedSuccessStatuses[item.toLang] != LanguageProcessStatus.Success )
                    WriteLine($"Skipping language {GetLanguageNameOfTag(item.toLang)} - {item.toLang} due to Translator error or unknown status.", LoggingColorSet.ErrorLogging);
                else
                {
                    SaveTranslations(item);
                    WriteLine($"Saved language {GetLanguageNameOfTag(item.toLang)} to file [{item.outputFile}]{NL}", LoggingColorSet.BackupLogging, VerbosityLevels.Normal);
                }
            }

            if ( cancellation.IsCancellationRequested )
            {
                statusLabel.Text = "Translation Canceled";
                progressBarWhileTranslatingResxFile.Value = progressBarWhileTranslatingResxFile.Maximum;
            }
            else
                statusLabel.Text = "Done!";

            bool IsCancel = cancellation.IsCancellationRequested;
            cancellation.Dispose();
            LockControls(false, true);
            sw.Stop();
            if ( !IsCancel )
            {
                TimeSpan totalTime = totalTranslationTime + sw.Elapsed;
                if ( _translationRequestPackets.Count == qtyItemsThatNeededToBeTranslated )
                    WriteLine($"Translated {QtySuccess} languages out of {qtyItemsThatNeededToBeTranslated}.", LoggingColorSet.StartAndEndLogging);
                else
                    WriteLine($"Translated {QtyItemsCompleted} lines out of {qtyItemsThatNeededToBeTranslated}.", LoggingColorSet.StartAndEndLogging);
                if ( totalTime.Seconds < (5 * 60) ) // If less then 5 minutes, show milliseconds but don't show hours and don't show second digit for minutes.
                    WriteLine($"Translation time in seconds = {totalTime.Seconds} ({totalTime.ToString().Substring(4, 7)})", LoggingColorSet.StartAndEndLogging);
                else // If it takes more then 5 minutes, then don't show milliseconds
                    WriteLine($"Translation time in seconds = {totalTime.Seconds} ({totalTime.ToString().Substring(0, 8)})", LoggingColorSet.StartAndEndLogging);
                WriteLine("Translation completed..... " + DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss"), LoggingColorSet.StartAndEndLogging);
            }

            tabControlTranslateResxSubTab.SelectedIndex = 1;
            logBox.Select(logBox.Text.Length - 1, 0);
        }

        public void Translator_OutPutFromTranslator(string message, Color color)
		{
			Debug.Assert(_multiThreadTranslator != null);
			Debug.Assert(_translationRequestPackets != null);
			Log(message + NL, color);
		}
		public void Translator_ShowProgress(int TotalCount)
		{
			Debug.Assert(_multiThreadTranslator != null);
			Debug.Assert(_translationRequestPackets != null);
			if ( TotalCount < 1 || TotalCount > progressBarWhileTranslatingResxFile.Maximum )
                WriteLine($"Received invalid value for ShowProgress. Received TotalCount = ({TotalCount}); Expected value between 1 and ({progressBarWhileTranslatingResxFile.Maximum})", LoggingColorSet.ErrorLogging);
			progressBarWhileTranslatingResxFile.Value = TotalCount;
			//progressBarWhileTranslatingResxFile.Update();
			if ( _LogBoxUpdatePoints.Contains(TotalCount) )
				WriteLine($"Translated {TotalCount} of {progressBarWhileTranslatingResxFile.Maximum} items.", LoggingColorSet.ProgressLogging);
		}
        public void Translator_InitializeProgressBar(int QtyItemsToCheck)
		{
			Debug.Assert(_multiThreadTranslator != null);
			Debug.Assert(_translationRequestPackets != null);
			if ( QtyItemsToCheck < 1 )
                WriteLine("Received invalid value for InitializeProgressBar. (" + QtyItemsToCheck + ")", LoggingColorSet.ErrorLogging);
            _previousLoggingErrors.Clear();
            progressBarWhileTranslatingResxFile.Visible = true;
			progressBarWhileTranslatingResxFile.Minimum = 1;
			progressBarWhileTranslatingResxFile.Value = 1;
			progressBarWhileTranslatingResxFile.Step = 1;
			progressBarWhileTranslatingResxFile.Maximum = QtyItemsToCheck;
			_LogBoxUpdatePoints.Clear();
            const int QtyIncrements = 20;
			if ( QtyItemsToCheck > QtyIncrements )
				for ( int i = QtyItemsToCheck / QtyIncrements ; i < QtyItemsToCheck ; i += QtyItemsToCheck / QtyIncrements )
					_LogBoxUpdatePoints.Add(i);
            string msg = " Start time = " + DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss") + ".\nPlease standby while process loads...\n\n";
            if ( _multiThreadTranslator._itemsToTranslate.Count == QtyItemsToCheck )
                WriteLine($"Starting translation for {QtyItemsToCheck} languages.{msg}", LoggingColorSet.StartAndEndLogging);
            else
                WriteLine($"Starting translation for {QtyItemsToCheck} items.{msg}", LoggingColorSet.StartAndEndLogging);
        }
        #endregion ResxMultiThreadTranslator Delegate Functions

        #region Populate Language List View
        private void ClearDisplaySetChecks()
        {
            toolStripMenuItemWindowsLanguagePacks.Checked = toolStripMenuItemWindowsLanguagePackInterface.Checked = toolStripMenuItemAllTranslatorSupportedLanguages.Checked = toolStripMenuItemISO639_1UsingOfficialNames.Checked = toolStripMenuItemISO639_1PlusWinLangPack.Checked = false;
        }
		private void PopulateLanguageSet(string[,] languageSet, bool use2LettersOnly = false)
		{
			for ( int i = 0 ; i < languageSet.GetLength(0) ; ++i )
			{
				string code = languageSet[i, 0];
				string name = languageSet[i, 1];
                if ( use2LettersOnly && code.Length != 2 )
                    continue;
				AddLanguage(code, name);
			}

            languageList.Sort(1);
        }
		private void PopulateLanguageSet(LanguageSet index)
		{
            try
            {
                ClearLanguageSet();
                ClearDisplaySetChecks();
                switch ( index )
                {
                    case LanguageSet.Windows10Plus_LanguagePacks:
                        PopulateLanguageSet(LanguageCodes.Windows10Plus_LanguagePacks);
                        toolStripMenuItemWindowsLanguagePacks.Checked = true;
                        break;
                    case LanguageSet.Windows10Plus_LanguageInterfacePacks:
                        PopulateLanguageSet(LanguageCodes.Windows10Plus_LanguageInterfacePacks);
                        toolStripMenuItemWindowsLanguagePackInterface.Checked = true;
                        break;
                    case LanguageSet.TranslatorSupportedLanguages:
                        PopulateTranslatorSupportedLanguages();
                        toolStripMenuItemAllTranslatorSupportedLanguages.Checked = true;
                        break;
                    default:
                    case LanguageSet.Iso639_1_Languages:
                        PopulateLanguageSet(LanguageCodes.LanguageCodesAndAliases, true);
                        toolStripMenuItemISO639_1UsingOfficialNames.Checked = true;
                        break;
                    case LanguageSet.Iso639_1_Plus:
                        PopulateLanguageSet(LanguageCodes.LanguageCodesAndAliases);
                        toolStripMenuItemISO639_1PlusWinLangPack.Checked = true;
                        break;
                }
                SelectedLanguageSet = (int) index;
            }
            catch(Exception e)
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name);
            }
        }
        private void PopulateTranslatorSupportedLanguages()
        {
            foreach ( var language in GTranslate_LangLookup )
            {
                string code = language.Key;
                string name = language.Value.Name;
                string translator = language.Value.SupportedServices.ToString();
                AddLanguage(code, name, translator);
            }
        }
        private void ClearLanguageSet()
        {
            languageList.ClearObjects();
            _languagesInList.Clear();
        }
        private string GetIso639_3Tag(string Iso639_1_Tag)
        {
            try
            { 
            string tag = Iso639_1_Tag.ToLower();
            for ( int i = 0 ; i < LanguageCodes.ISO639_1_To_ISO639_3.GetLength(0) ; ++i )
            {
                string iso639_1 = LanguageCodes.ISO639_1_To_ISO639_3[i, 0];
                string iso639_3 = LanguageCodes.ISO639_1_To_ISO639_3[i, 1];
                if ( iso639_1 .ToLower().Equals(tag))
                    return iso639_3;
            }
            string Iso639_3_Name = GetCulture_3LetterISOName(Iso639_1_Tag);
            if (string.IsNullOrEmpty(Iso639_3_Name))
                return GetCulture_Windows3LetterName(Iso639_1_Tag);
            return Iso639_3_Name;
            }
            catch ( Exception e )
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name, $"Iso639_1_Tag={Iso639_1_Tag}: ");
            }
            return ""; 
        }
        private string GetAliases(string LangName, string Code)
        {
            try
            {
                const string Sep = ", ";
                string langName = LangName.ToLower();
                string code = Code.ToLower();
                HashSet<string> Aliases = new();
                Aliases.Add(GetCultureName(Code));
                Aliases.Add(GetNativeName(Code));
                Aliases.Add(GetCountry(Code));
                if ( GTranslate_LangLookup.ContainsKey(Code) )
                    Aliases.Add(GTranslate_LangLookup[Code].NativeName);
                else if ( GTranslate_LangLookup.ContainsKey(code) )
                    Aliases.Add(GTranslate_LangLookup[code].NativeName);



                for ( int i = 0 ; i < LanguageCodes.LanguageCodesAndAliases.GetLength(0) ; ++i )
                {
                    string tag = LanguageCodes.LanguageCodesAndAliases[i, 0];
                    string name = LanguageCodes.LanguageCodesAndAliases[i, 1];
                    string aliases = LanguageCodes.LanguageCodesAndAliases[i, 2];
                    if ( name.ToLower().Equals(langName) )
                    {
                        if ( !string.IsNullOrEmpty(aliases) && !aliases.ToLower().Equals(langName) )
                            Aliases.Add(aliases);
                        break;
                    }
                }

                Aliases.Remove(LangName);
                Aliases.Remove(Code);
                Aliases.Remove("Pilipinas");
                Aliases.Remove("");

                string AliasesStr = "";
                foreach ( var s in Aliases.Distinct() )
                    AliasesStr += s + Sep;
                return AliasesStr.TrimEnd(' ').TrimEnd(',').TrimEnd(';');
            }
            catch(Exception e)
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name, $"LangName={LangName} - {Code}: ");
            }
            return "";
        }
        private void PopulateComboBoxLanguage()
        {
            comboBoxFromLang.Items.Clear();
            comboBoxToLang.Items.Clear();
            comboBoxFromLanguage.Items.Clear();
            foreach ( var language in GTranslate_LangLookup )
            {
                string code = language.Key;
                string name = language.Value.Name;
                string translator = language.Value.SupportedServices.ToString();
                if ( LanguageCodes.LanguagesTagsWithIssues.Contains(code) )
                    continue;
                _ = comboBoxFromLang.Items.Add(name);
                _ = comboBoxToLang.Items.Add(name);
                _ = comboBoxFromLanguage.Items.Add(name);
            }
            int t1 = comboBoxFromLanguage.Items.Count;
            int t2 = comboBoxFromLang.Items.Count;
            if (t1 != t2 )
            {
                foreach(string s in comboBoxFromLang.Items)
                {
                    if ( !comboBoxFromLanguage.Items.Contains(s))
                    {
                        LogToFile($"Missing item {s} in comboBoxFromLanguage", VerbosityLevels.Errors);
                    }
                }
            }
        }
        private void AddLanguage(string code, string LangName, string? translatorSupported = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(LangName));
            try
            {
                if ( string.IsNullOrEmpty(code) || string.IsNullOrEmpty(LangName) )
                {
                    // Add logging here
                    return;
                }
                string langName = LangName.ToLower();
                if ( _languagesInList.Contains(langName) )
                    return;
                _languagesInList.Add(langName);
                const string NoTranslator = "No Translator";
                string languageTranslatorTag = "";
                if ( translatorSupported == null )
                {
                    translatorSupported = NoTranslator;
                    if ( GTranslate_LangLookup.ContainsKey(code) )
                    {
                        languageTranslatorTag = code;
                        translatorSupported = GTranslate_LangLookup[code].SupportedServices.ToString();
                    }
                    else
                    {
                        foreach ( var language in GTranslate_LangLookup )
                        {
                            string name = language.Value.Name;
                            if ( langName.Equals(name.ToLower()) )
                            {
                                languageTranslatorTag = language.Key;
                                translatorSupported = language.Value.SupportedServices.ToString();
                                break;
                            }
                        }
                    }
                }
                else
                    languageTranslatorTag = code;

                ArrayList l = new ArrayList();
                ObjListVwRowDetails objListVwRowDetails = new(code)
                {
                    LanguageTag = code,
                    LanguageName = LangName,
                    LanguageTranslatorTag = languageTranslatorTag,
                    LanguageAliases = GetAliases(LangName, code),
                    TranslatorSupported = translatorSupported,
                    Iso639_3 = GetIso639_3Tag(code),
                };
                l.Add(objListVwRowDetails);
                languageList.AddObjects(l);
                if ( translatorSupported.Equals(NoTranslator) || LanguageCodes.LanguagesTagsWithIssues.Contains(objListVwRowDetails.LanguageTranslatorTag) )
                    languageList.DisableObjects(l);
            }
            catch(Exception e)
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name, $"LangName={LangName} - {code}: ");
            }
        }
        #endregion Populate Language List View

        #region Language Selection Code
        private void SelectTopSpokenLanguages(int MaxLang)
		{
			/*	
             *	List of languages which don't seem to be included, but are part of the top 44 most used languages.
			 *	(#18)		Yue Chinese
			 *	(#19)		Wu Chinese
			 *	(#22)		Hausa
			 *	(#24)		Egyptian Spoken Arabic
			 *	(#25)		Swahili
             *	(#36)		Nigerian Pidgin
			 *	See following links:
			 *			44 Most Spoken Languages (2022)
			 *				https://www.edudwar.com/list-of-most-spoken-languages-in-the-world/
			 *			33 Most Spoken Languages (2021)
			 *				https://www.langoly.com/most-spoken-languages/
			 *			50 Most Spoken Languages (2020)
			 *				https://paladria.com/en/blog/what-are-the-50-most-spoken-languages-in-the-world/
			 *			50 Most Spoken Languages (1996)
			 *				https://photius.com/rankings/languages2.html
			 *			10 Most Spoken Languages (2022)
			 *				https://www.ethnologue.com/guides/ethnologue200
			 *	Some other sources with additional details:
			 *			Shows a chart with top most spoken languages. (2020)
			 *				https://www.visualcapitalist.com/100-most-spoken-languages/
			 *			Top 100 languages by population (1999)	
			 *				http://www2.harpercollege.edu/mhealy/g101ilec/intro/clt/cltclt/top100.html
			*/
			string[] Top31SpokenLanguages = {
					"Arabic",
                    "Bengali" /*Bengali*/,
                    "Chinese (Simplified)",
					"English",
					"French", /*Standard*/ 
					"German",/*Standard*/ 
					"Gujarati",
					"Hindi",
					"Indonesian",
					"Italian", /*Western*/ 
					"Japanese",
					"Javanese",
					"Kannada",
					"Korean",
					"Malayalam",
					"Marathi",
					"Persian",
					"Polish",
					"Portuguese",
					"Punjabi",
					"Russian",
					"Spanish",
					"Sundanese",
					"Tagalog" /*Filipino (Philippines)*/,
					"Tamil",
					"Telugu",
					"Thai",
					"Turkish",
					"Ukrainian",
					"Urdu",
					"Vietnamese",
			};
			SelectAGroupOfLanguages(Top31SpokenLanguages, $"Top {MaxLang}", MaxLang);
        }
        private void SelectAGroupOfLanguages(string[] LanguageNames, string UndoName = "Last Group", int MaxLang = -1)
        {
            ArrayList l = new ArrayList();
            int SelectedLangCount = 0;
            for ( int count = 0 ; count < languageList.GetItemCount() ; count++ )
            {
                ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails) languageList.GetModelObject(count);
                if ( languageList.IsDisabled(objlistvwrowdetails) )
                    continue;
                if ( LanguageNames.Contains(objlistvwrowdetails.LanguageName) )
                {
                    l.Add(objlistvwrowdetails);
                    ++SelectedLangCount;
                    if ( MaxLang != -1 && SelectedLangCount == MaxLang )
                        break;
                }
            }
            languageList.CheckObjects(l);
            toolStripMenuItemSelectUndo.Text = $"Deselect {UndoName} Selection";
        }
        private void SelectAGroupOfLanguages(string[,] LangList, string UndoName = "Last Group")
        {
            List<string> LanguageNames = new();
            for ( int i = 0 ; i < LangList.GetLength(0) ; ++i )
                LanguageNames.Add(LangList[i, 1]);
            LanguageNames.Sort();
            SelectAGroupOfLanguages(LanguageNames.ToArray(), UndoName, -1);
        }
        private void buttonSelectTopEuropean_Click()
		{
			string[] GroupTopSpokenLanguages = {
				"Bosnian",
				"Bulgarian",
				"Catalan",
				"Croatian",
				"Czech",
				"Danish",
				"Dutch",
				"English",
				"Estonian",
				"Finnish",
				"French",
				"Galician",
				"German",
				"Greek",
				"Hungarian",
				"Icelandic",
				"Irish",
				"Italian",
				"Latvian",
				"Lithuanian",
				"Norwegian",
				"Polish",
				"Portuguese",
				"Russian",
                "Serbian",
                "Serbian (Latin)",
                "Slovak",
				"Slovenian",
				"Spanish",
				"Swedish",
				"Turkish",
				"Ukrainian",
				"Welsh",
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "European");
		}
		private void buttonSelectTopLangInAfrica_Click()
		{
			string[] GroupTopSpokenLanguages = {
					"Amharic",
					"Arabic",
                    "Fulah",	// No translator that supports this language
					"Hausa",
					"Igbo",
					"Zulu",
					"Swahili",
					"Oromo",	// No translator that supports this language
					"Shona",
					"Yoruba",
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "Africa");
		}
		private void buttonSelectTopAsian_Click()
		{
			string[] GroupTopSpokenLanguages = {
					"Arabic",
					"Bengali",
					"Burmese",
                    "Cantonese (Traditional)",
                    "Chinese (Simplified)",
					"Hindi",
					"Japanese",
					"Khmer",
					"Korean",
					"Lao",
					"Malay",
					"Persian",
					"Tagalog",
					"Thai",
					"Tibetan",
					"Vietnamese"
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "Asian");
		}
		private void buttonSelectTopLangInTheMiddleEast_Click()
		{
			string[] GroupTopSpokenLanguages = {
					//"Amazigh",	// Not part of ISO 639-1 and there's no translator that supports this language
					"Amharic",
					"Arabic",
					//"Blooshi",		// This is not included ISO 639
					"Dari",			// No translator that supports this language
					"English",
                    "Persian", 
					"Greek",
					"Hebrew",
					"Kurdish",
					"Pashto",
					"Persian",
					"Somali",
					"Turkish",
					"Urdu"
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "Middle East");
		}
		private void buttonSelectTopLangInNorthAmerica_Click()
		{
			string[] GroupTopSpokenLanguages = {
					"English",
					"French",
					"Haitian",
					//"Mayan",		// Not part of ISO 639-1 and there's no translator that supports this language
					//"Nahuatl",	// Not part of ISO 639-1 and there's no translator that supports this language
					"Spanish",
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "North America");
		}
		private void buttonTopLangInSouthAmerica_Click()
		{
			string[] GroupTopSpokenLanguages = {
					"English",
					"Portuguese",
					"Spanish",
				};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "South America");
		}
		private void buttonSelectTopLangSpokenInBusinesses_Click()
		{
			string[] GroupTopSpokenLanguages = {
					"Arabic",
                    "Chinese (Simplified)",
					"Dutch (Flemish)",
					"Dutch",
					"English",
					"French",
					"German",
					"Hindi",
					"Indonesian",
					"Italian",
					"Japanese",
					"Swahili",
					"Korean",
					"Portuguese",
					"Russian",
					"Spanish",
			};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "Businesses Languages");
		}
        private void buttonSelectAllWindowsSupported_Click()
		{
			// ToDo: Finish updating this list and the languages display list
			string[] GroupTopSpokenLanguages = {
					"Arabic",
                    "Basque",
					"Bulgarian",
                    "Chinese (Traditional)",
                    "Chinese (Simplified)",
                    "Catalan",
                    "Croatian",
					"Czech",
					"Danish",
					"Dutch",
					"English",
                    "English (UK)",
                    "Estonian",
					"Finnish",
                    "French (Canada)",
                    "French",
                    "Galician",
                    "German",
					"Greek",
					"Hebrew",
					"Hungarian",
                    "Indonesian",
                    "Italian",
					"Japanese",
					"Korean",
					"Latvian",
					"Lithuanian",
					"Norwegian",
					"Polish",
                    "Portuguese (Brazil)",
					"Portuguese",
					"Romanian",
					"Russian",
                    "Serbian (Latin)",
					"Slovak",
					"Slovenian",
                    "Spanish (Mexico)",
					"Spanish",
					"Swedish",
					"Thai",
					"Turkish",
					"Ukrainian",
                    "Vietnamese",
					// "English International",	// ToDo: Add this to menu, because this includes other English words used in United Kingdom, Australia, etc.
				};
			SelectAGroupOfLanguages(GroupTopSpokenLanguages, "Windows Supported");
			//SelectAGroupOfLanguages(LanguageCodes.Windows10Plus_LanguagePacks, "Windows Supported");
		}
        private void CheckAllEnabledItems(ArrayList l = null)
        {
            if ( l == null )
            {
                l = new ArrayList();
                for ( int count = 0 ; count < languageList.GetItemCount() ; count++ )
                {
                    ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails) languageList.GetModelObject(count);
                    if ( !languageList.IsDisabled(objlistvwrowdetails) )
                        l.Add(objlistvwrowdetails);
                }
            }
            languageList.CheckObjects(l);
        }
        private void SetAll_LanguagesCkBx(bool CheckedValue)
		{
            PreviousGroupLanguageSelections previousGroupLanguageSelections = new();
            ArrayList l = new ArrayList();
            for ( int count = 0 ; count < languageList.GetItemCount() ; count++ )
			{
                ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails) languageList.GetModelObject(count);
                previousGroupLanguageSelections.Selections.Add(objlistvwrowdetails.LanguageName);
                if ( !languageList.IsDisabled(objlistvwrowdetails) && CheckedValue )
                    l.Add(objlistvwrowdetails); 
            }
            if ( CheckedValue )
            {
                //languageList.CheckAll(); // Don't use this function, because it checks disabled items.
                CheckAllEnabledItems(l);
            }
            else
                languageList.UncheckAll();

            if ( CheckedValue )
			{
                LockControls(false);
                toolStripMenuItemSelectUndo.Text = "Deselect All Selections";
                toolStripMenuItemSelectUndo.Enabled = toolStripMenuItemSelectUndo.Visible = true;
                previousGroupLanguageSelections.Name = toolStripMenuItemSelectUndo.Text;
                _previousLanguageSelections.Add(previousGroupLanguageSelections);
            }
            else 
				Set_toolStripMenuItemSelectUndo_Default();

        }
        private void SelectAndTranslateLanguages(object sender, EventArgs e)
        {
            if ( !IsValid_InputboxFileAndOutPutDir() )
                return;
            toolStripMenuItemAllTranslatorSupportedLanguages_Click(sender,e);
            // SetAll_LanguagesCkBx(false);
			if ( sender == toolStripMenuItemTranslateTop5 )
				SelectTopSpokenLanguages(5);
			else if ( sender == toolStripMenuItemTranslateTop10 )
				SelectTopSpokenLanguages(10);
			else if ( sender == toolStripMenuItemTranslateTop20 )
				SelectTopSpokenLanguages(20);
			else if ( sender == toolStripMenuItemTranslateTop30 )
				SelectTopSpokenLanguages(30);
            else if ( sender == toolStripMenuItemWindowsSupportedLanguages )
            {
                toolStripMenuItemWindowsLanguagePacks_Click(sender, e);
                buttonSelectAllWindowsSupported_Click();
            }
            else if ( sender == toolStripMenuItemWindowsLanguageInterfacePacks )
            {
                toolStripMenuItemWindowsLanguagePackInterface_Click(sender, e);
                buttonSelectAllWindowsSupported_Click(); // ToDo: Need to create a separate function to set the languages for the interface packs
            }
            else if ( sender == toolStripMenuItem_TranslateAfrica )
				buttonSelectTopLangInAfrica_Click();
			else if ( sender == toolStripMenuItem_TranslateAsia )
				buttonSelectTopAsian_Click();
			else if ( sender == toolStripMenuItem_TranslateEurope )
				buttonSelectTopEuropean_Click();
			else if ( sender == toolStripMenuItem_TranslateMiddleEast )
				buttonSelectTopLangInTheMiddleEast_Click();
			else if ( sender == toolStripMenuItem_TranslateNorthAmerica )
				buttonSelectTopLangInNorthAmerica_Click();
			else if ( sender == toolStripMenuItem_TranslateSouthAmerica )
				buttonTopLangInSouthAmerica_Click();
			else if ( sender == toolStripMenuItemTranslateBusinessLanguages )
				buttonSelectTopLangSpokenInBusinesses_Click();
			else
			{
				Debug.Assert(false); // The code should never reach this point
				return;
			}

			TranslateFile();
        }
        private void toolStripSplitButtonTranslate_ButtonClick(object sender, EventArgs e)
        {
            if ( !IsValid_InputboxFileAndOutPutDir() )
                return;
            if ( languageList.CheckedObjects.Count == 0 ) // If no language selected, then select all languages
                SetAll_LanguagesCkBx(true);
            TranslateFile();
        }
        private void toolStripMenuItemTranslateAllLanguages_Click(object sender, EventArgs e)
        {
            if ( !IsValid_InputboxFileAndOutPutDir() )
                return;
            PopulateLanguageSet(LanguageSet.TranslatorSupportedLanguages);
            SetAll_LanguagesCkBx(true);
            TranslateFile();
        }
        private void toolStripMenuItemSelectWindowsSupported_Click(object sender, EventArgs e) => buttonSelectAllWindowsSupported_Click();
        private void toolStripMenuItemSelectAfrica_Click(object sender, EventArgs e) => buttonSelectTopLangInAfrica_Click();
        private void toolStripMenuItemSelectAsia_Click(object sender, EventArgs e) => buttonSelectTopAsian_Click();
        private void toolStripMenuItemSelectEurope_Click(object sender, EventArgs e) => buttonSelectTopEuropean_Click();
        private void toolStripMenuItemSelectMiddleEast_Click(object sender, EventArgs e) => buttonSelectTopLangInTheMiddleEast_Click();
        private void toolStripMenuItemSelectNorthAmerica_Click(object sender, EventArgs e) => buttonSelectTopLangInNorthAmerica_Click();
        private void toolStripMenuItemSelectSouthAmerica_Click(object sender, EventArgs e) => buttonTopLangInSouthAmerica_Click();
        private void toolStripMenuItemSelectTop5_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(5);
        private void toolStripMenuItemSelectTop10_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(10);
        private void toolStripMenuItemSelectTop20_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(20);
        private void toolStripMenuItemSelectTop30_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(30);
        private void toolStripMenuItemSelectBusiness_Click(object sender, EventArgs e) => buttonSelectTopLangSpokenInBusinesses_Click();
        private void toolStripSplitButtonSelectAllLanugages_ButtonClick(object sender, EventArgs e) => SetAll_LanguagesCkBx(true);
        private void toolStripMenuItemSelectClear_Click(object sender, EventArgs e) => SetAll_LanguagesCkBx(false);
        private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e) => SetAll_LanguagesCkBx(true);
        private void buttonSelectTop5Languages_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(5);
        private void buttonSelectTop10Languages_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(10);
        private void buttonSelectTop20Languages_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(20);
        private void buttonSelectTop30Languages_Click(object sender, EventArgs e) => SelectTopSpokenLanguages(30);
        private void buttonSelectAllLanguages_Click(object sender, EventArgs e) => SetAll_LanguagesCkBx(true);
        private void buttonClearLanguageSelection_Click(object sender, EventArgs e) => SetAll_LanguagesCkBx(false);
		private void toolStripMenuItemSelectUndo_Click(object sender, EventArgs e) => buttonUndoLastLangSelection_Click(sender, e);
        private void toolStripMenuItemWindowsLanguagePacks_ShowOnly_Click(object sender, EventArgs e)=> toolStripMenuItemWindowsLanguagePacks_Click(sender, e);
        private void toolStripMenuItemWindowsLanguagePacks_Add_Click(object sender, EventArgs e)=> PopulateLanguageSet(LanguageSet.Windows10Plus_LanguagePacks);
        private void toolStripMenuItemWindowsLanguagePackInterface_ShowOnly_Click(object sender, EventArgs e) => toolStripMenuItemWindowsLanguagePackInterface_Click(sender, e);
        private void toolStripMenuItemWindowsLanguagePackInterface_Add_Click(object sender, EventArgs e)=>PopulateLanguageSet(LanguageSet.Windows10Plus_LanguageInterfacePacks);
       private void toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly_Click(object sender, EventArgs e) => toolStripMenuItemISO639_1UsingOfficialNames_Click(sender, e);
        private void toolStripMenuItemISO639_1UsingOfficialNames_Add_Click(object sender, EventArgs e)=> PopulateLanguageSet(LanguageSet.Iso639_1_Plus);
        private void toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly_Click(object sender, EventArgs e) => toolStripMenuItemAllTranslatorSupportedLanguages_Click(sender, e);
        private void toolStripMenuItemAllTranslatorSupportedLanguages_Add_Click(object sender, EventArgs e) => PopulateLanguageSet(LanguageSet.TranslatorSupportedLanguages);
        private void toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly_Click(object sender, EventArgs e) => toolStripMenuItemISO639_1PlusWinLangPack_Click(sender, e);
        private void toolStripMenuItemISO639_1PlusWinLangPack_Add_Click(object sender, EventArgs e) => PopulateLanguageSet(LanguageSet.Iso639_1_Plus);
        private void toolStripMenuItemWindowsLanguagePacks_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet( LanguageSet.Windows10Plus_LanguagePacks);
        }
        private void toolStripMenuItemWindowsLanguagePackInterface_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet( LanguageSet.Windows10Plus_LanguageInterfacePacks);
        }
        private void toolStripMenuItemISO639_1UsingOfficialNames_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet( LanguageSet.Iso639_1_Languages);
        }
		private void toolStripSplitButtonDisplayLanguageSet_ButtonClick(object sender, EventArgs e)
		{
            ClearLanguageSet();
            PopulateLanguageSet((LanguageSet) comboBoxDefaultLanguageSet.SelectedIndex);
        }
        private void toolStripMenuItemISO639_1PlusWinLangPack_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet(LanguageSet.Iso639_1_Plus);
        }
        private void toolStripMenuItemWindowsLanguagePacks_AddAndSelect_Click(object sender, EventArgs e)
        {
            PopulateLanguageSet(LanguageSet.Windows10Plus_LanguagePacks);
            SelectAGroupOfLanguages(LanguageCodes.Windows10Plus_LanguagePacks, "Win Lang Pack");
        }
        private void toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect_Click(object sender, EventArgs e)
        {
            PopulateLanguageSet(LanguageSet.Windows10Plus_LanguageInterfacePacks);
            SelectAGroupOfLanguages(LanguageCodes.Windows10Plus_LanguageInterfacePacks, "Win Lang Interface Pack");
        }
        private void toolStripMenuItemWindowsLanguagePackInterface_ShowWithCode_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet(LanguageSet.Windows10Plus_LanguageInterfacePacks);
        }
        private void toolStripMenuItemAllTranslatorSupportedLanguages_Click(object sender, EventArgs e)
        {
            ClearLanguageSet();
            PopulateLanguageSet(LanguageSet.TranslatorSupportedLanguages);
        }
        private void Set_toolStripMenuItemSelectUndo_Default()
        {
            toolStripMenuItemSelectUndo.Text = "Undo Last Group Selection";
            toolStripMenuItemSelectUndo.Enabled = toolStripMenuItemSelectUndo.Visible = false;

            _previousLanguageSelections.Clear();
        }
        private void buttonUndoLastLangSelection_Click(object sender, EventArgs e)
        {
            if ( _previousLanguageSelections.Count < 1 )
                return;
            int LastItemsIndex = _previousLanguageSelections.Count - 1;
            PreviousGroupLanguageSelections LanguagesPreviouslySelected = _previousLanguageSelections[LastItemsIndex];
            for ( int count = 0 ; count < languageList.GetItemCount() ; count++ )
            {
                ObjListVwRowDetails objlistvwrowdetails = (ObjListVwRowDetails) languageList.GetModelObject(count);
                if ( LanguagesPreviouslySelected.Selections.Contains(objlistvwrowdetails.LanguageName) )
                    languageList.CheckObject(count);
            }
            _previousLanguageSelections.RemoveAt(LastItemsIndex);
            if ( _previousLanguageSelections.Count < 1 )
                Set_toolStripMenuItemSelectUndo_Default();

            else
            {
                toolStripMenuItemSelectUndo.Text = _previousLanguageSelections[_previousLanguageSelections.Count - 1].Name;
                toolStripMenuItemSelectUndo.Enabled = toolStripMenuItemSelectUndo.Visible = true;
            }
            languageList.Update();
        }
        #endregion Language Selection Code

        #region Get Cultural details
        private string GetAppendedFileName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                return cultureinfo.TwoLetterISOLanguageName;
            }
            catch ( Exception e )
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name, $"LangTag={code}: ");
            }
            return code;
        }
        private string GetNativeName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                return cultureinfo.NativeName;
            }
            catch ( Exception e )
            {
                LogToFile($"{MethodBase.GetCurrentMethod().Name}: LangTag={code}: {e.Message}", VerbosityLevels.Detail);
            }
            return "";
        }
        private string GetCountry(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                RegionInfo ri = new RegionInfo(cultureinfo.LCID);
                return ri.DisplayName;
            }
            catch ( Exception e )
            {
                LogToFile(e.Message, VerbosityLevels.Detail);
            }
            return "";
        }
        private string GetCultureName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                return cultureinfo.DisplayName;
            }
            catch ( Exception e )
            {
                LogToFile($"{MethodBase.GetCurrentMethod().Name}: LangTag={code}: {e.Message}", VerbosityLevels.Detail);
            }
            return "";
        }
        private string GetCulture_2LetterISOName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                return cultureinfo.TwoLetterISOLanguageName;
            }
            catch ( Exception e )
            {
                LogToFile(e, MethodBase.GetCurrentMethod().Name);
            }
            return "";
        }
        private string GetCulture_3LetterISOName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                return cultureinfo.ThreeLetterISOLanguageName;
            }
            catch ( Exception e )
            {
                LogToFile($"{MethodBase.GetCurrentMethod().Name}: LangTag={code}: {e.Message}", VerbosityLevels.Detail);
            }
            return "";
        }
        private string GetCulture_Windows3LetterName(string code)
        {
            try
            {
                CultureInfo? cultureinfo = CultureInfo.GetCultureInfo(code);
                if ( cultureinfo == null )
                    return code;
                if ( cultureinfo.ThreeLetterWindowsLanguageName.Equals("ZZZ") )
                    return "";
                return cultureinfo.ThreeLetterWindowsLanguageName;
            }
            catch ( Exception e )
            {
                LogToFile($"{MethodBase.GetCurrentMethod().Name}: LangTag={code}: {e.Message}", VerbosityLevels.Detail);
            }
            return "";
        }

        #endregion Get Cultural details

        #region Tab (Advanced Options) functions
        private void buttonDeleteLangAppendedResxFiles_Click(object sender, EventArgs e)
        {
            string PathToDeleteFilesFrom = outputBox.Text;
            if ( Directory.Exists(PathToDeleteFilesFrom) )
            {
                bool doDeleteFiles = true;
                if (checkBoxDispalyWarningPrompts.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Are you sure you want to delete all the Resx files having file name with appended language code in folder \"" +
                        PathToDeleteFilesFrom + "\"?",
                        "Deleting Resx Language Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if ( dialogResult == DialogResult.No )
                        doDeleteFiles = false;
                }

                if (doDeleteFiles)
                {
                    System.IO.DirectoryInfo di = new(PathToDeleteFilesFrom);
                    foreach ( FileInfo file in di.GetFiles("*.resx", SearchOption.TopDirectoryOnly) )
                    {
                        if ( Path.GetFileNameWithoutExtension(file.FullName).Contains('.') )
                            file.Delete();
                    }
                }
            }
            else
                _ = MessageBox.Show("Warning: Folder \"" + PathToDeleteFilesFrom + "\" does not exist.",
                     "Path does NOT exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion Tab (Advanced Options) functions

        #region Help Links
        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if ( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if ( RuntimeInformation.IsOSPlatform(OSPlatform.Linux) )
                {
                    Process.Start("xdg-open", url);
                }
                else if ( RuntimeInformation.IsOSPlatform(OSPlatform.OSX) )
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        private void linkLabelFilterText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Filter");
        }

        private void linkLabelcheckBoxDispalyWarningPrompts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Dispaly-Warning-Prompts");
        }

        private void linkLabelDefaultLanguageSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Default-Language-Set");
        }

        private void linkLabelDeleteLangAppendedResxFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Delete-Langugae-Appended-Resx-Files");
        }

        private void linkLabelAddOriginalSrcTextToComment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Translated-Resx-Comments");
        }

        private void linkLabelDeleteLangResxFilesBeforeTranslation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Delete-Language-ResxFiles-Before-Translation");
        }

        private void linkLabelBackupFilesBeforeTranslation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Backup-Files-Before-Translation");
        }

        private void linkLabelBkUpDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Backup-Directory");
        }

        private void linkLabelMaxThreads_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Max-Threads");
        }

        private void linkLabelMaxTranslateLen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Max-Translation-Len");
        }

        private void linkLabelItemsPerTransaltionRequest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Strings-Per-Translation-Req");
        }

        private void linkLabelScreenVerbosityLevel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Screen-Verbosity-Level");
        }

        private void linkLabelLogFileVerbosityLevel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Log-File-Verbosity-Level");
        }

        private void linkLabelLogFolderPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/David-Maisonave/ABetterTranslator#Logging-Directory");
        }
        #endregion Help Links

    }
}

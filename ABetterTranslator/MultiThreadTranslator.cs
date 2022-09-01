/* ***************************************************************************
 * Copyright © 2022 David Maisonave	(https://axter.com)	All rights reserved.
 * ***************************************************************************
 * The is free software. You can redistribute it and/or modify it under the 
 * terms of the MIT License. For more information, please see License file 
 * distributed with this package.
 * ***************************************************************************
*/
using System.Diagnostics;
using System.Reflection;

using GTranslate.Translators;

using ABetterTranslator;

using static TranslateMultiThread.MultiThreadTranslator;
using static ABetterTranslator.MainWindow;

namespace TranslateMultiThread
{
    public class MultiThreadTranslator
    {
        #region public variables
        public Dictionary<string, LanguageProcessStatus> LanguagesProcessedSuccessStatuses
        {// Each language is set to true when processed successfully
            get { return _languagesProcessedSuccessStatuses; }
        }
        public List<string> LanguagesTagsWithErrors
        {
            get { return _languagesTagsWithErrors; }
        }
        public bool _cancelTranslation { get; set; } = false;
        public MultiThreadTranslator_Options _options { get; } = new MultiThreadTranslator_Options();
        public List<TranslationRequestPacket> _itemsToTranslate { get; set; } = new List<TranslationRequestPacket>();
        #endregion public variables
        #region private/protected variables
        protected string _undoListText = "";
        private const string _HEAD_AND_FOOT_WRAPPER = "\n//////////////////////////////////////////////////////////////////////////////////////\n";
        private int _workerThreadCount = 0;
        private readonly bool _initializeSuccess = false;
        private readonly bool _isConsoleMode = false;
        private List<string> _languagesTagsWithErrors = new();
        private Dictionary<string,LanguageProcessStatus> _languagesProcessedSuccessStatuses = new();
        #endregion private/protected variables
        #region enums and sub classes
        public enum TranslationPacketTypes
        {
            SingleDocument,
            MultiPacket,
            OnePacketPerString
        }
        public class TranslationRequestPacket
        {
            public string fromLang { get; set; } = "";
            public string toLang { get; set; } = "";
            public string inputPath { get; set; } = "";
            public string outputFile { get; set; } = "";
            public List<string> sourceText { get; set; } = new List<string>();
            public List<string> translatedText { get; set; } = new List<string>();
            public int qtyItemsTranslated { get; set; } = 0;
            public TranslationPacketTypes translationPacketType { get; set; } = TranslationPacketTypes.SingleDocument;
        }
        public class ItemDetails
        {
            public string OriginalText { get; set; } = "";
            public string Translation { get; set; } = "";
            public string SourceLang { get; set; } = "";
        }
        public class MultiThreadTranslator_Options
        {
            public Assembly _assembly { get; set; } = Assembly.GetExecutingAssembly();
            //public string _fromLang { get; set; } = null;
            //public string _targetLang { get; set; } = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            public bool _appendOrgName { get; set; } = false;
            public int _appendLangName { get; set; } = 0;
            public bool _dottedOutput { get; set; } = false;
            public bool _progressBarOutput { get; set; } = true;
            public ItemsPerTransReq _itemsPerTransReq { get; set; } = ItemsPerTransReq.Auto;
            public bool _waitKeyPress { get; set; } = false;
            public bool _createUndoList { get; set; } = false;
            public string _undoListFileName { get; set; } = "";
            public string _programCommandLineName { get; set; } = "";
            public string _programVersion { get; set; } = "";
            public string _programFileVersion { get; set; } = "";
            public string _programDescription { get; set; } = "";
            public StreamWriter? _writer { get; set; } = null;
            public int _postProgressBarPos { get; set; } = 8;
            public int _totalCount { get; set; } = 0;
            public int _translateCount { get; set; } = 0;
            public int _translationErrorCount { get; set; } = 0;
            public int _translationSubErrorCount { get; set; } = 0;
            public int _errorCount { get; set; } = 0;
            public int _maximumTranslateDataLength { get; set; } = 10000; // Fails at 10500
            public int _maxWorkerThreads { get; set; } = Math.Max(Environment.ProcessorCount, 4);
            public List<ItemDetails> _ItemsTranslated { get; set; } = new List<ItemDetails>();
        }
        public enum OutPutLevel
        {
            VerboseIfNotSilent,
            NormalLvl,
            PreProgressBar,
            PostProgressBar,
            ShowInWindow,
            ErrorLvl
        }

        public enum ItemsPerTransReq
        {
            Auto,
            OnePerItem,
            Many
        }
        public enum LanguageProcessStatus
        {
            Unknown,
            Success,
            Failed,
        }
        #endregion enums and sub classes

        #region Class constructor and initialization methods
        public MultiThreadTranslator(MultiThreadTranslator_Options? options = null)
        {
            options ??= new MultiThreadTranslator_Options();
            _options = options;
            _initializeSuccess = Initialize();
        }
        public bool Initialize()
        {
            if ( _isConsoleMode )
            {
                _options._programCommandLineName = _options._assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
                _options._programVersion = _options._assembly.GetName().Version.ToString();
                _options._programFileVersion = _options._assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version.ToString();
                AssemblyDescriptionAttribute? Description = _options._assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
                if ( Description != null )
                    _options._programDescription = Description.Description;
                else
                    _options._programDescription = new string("Language translator");
                Console.OutputEncoding = System.Text.Encoding.Unicode;
            }
            if ( _options._undoListFileName.Length == 0 )
                _options._undoListFileName = Path.Combine(Directory.GetCurrentDirectory(), "undo_translation.bat");
            if ( _options._createUndoList )
            {
                if ( File.Exists(_options._undoListFileName) )
                    File.Delete(_options._undoListFileName);
                try
                {
                    _options._writer = new StreamWriter(_options._undoListFileName);
                }
                catch ( Exception e )
                {
                    LogLine("Early exit because could not create undo item at path \"" + _options._undoListFileName + "\".\nErr Msg: \"" + e.Message + "\"", OutPutLevel.ErrorLvl);
                    ++_options._errorCount;
                    return false;
                }
            }
            return true;
        }
        #endregion Class constructor and initialization methods

        #region Class main functional logic
        // TranslateData is the entry point for external logic
        public bool TranslateData(List<TranslationRequestPacket> DataToTranslate)
        {
            if ( _initializeSuccess == false )
            {
                LogLine("Class MultiThreadTranslator failed initialization.", OutPutLevel.ErrorLvl);
                return false;
            }
            _itemsToTranslate = DataToTranslate;
            _languagesTagsWithErrors.Clear();

            Stopwatch sw = new();
            sw.Start();
            if ( _itemsToTranslate.Count == 0 )
            {
                LogLine("No items exist in list. Exiting.");
                LogLine("Early exit because no items exist.", OutPutLevel.ErrorLvl);
                return false;
            }

            int TotalItemsToTranslate = 0;
            foreach ( TranslationRequestPacket items in _itemsToTranslate )
            {
                foreach ( string item in items.sourceText )
                    ++TotalItemsToTranslate;
            }

            InitializeProgressBar(TotalItemsToTranslate);
            if ( MainWindow.VerbosityLevel.ScreenVerbosity == VerbosityLevels.Detail)
                WriteLine($"Starting translation with settings: [MaxThreads:{_options._maxWorkerThreads}]", OutPutLevel.NormalLvl);
            else
                LogLine($"Starting translation with settings: [MaxThreads:{_options._maxWorkerThreads}]", OutPutLevel.VerboseIfNotSilent);
            if ( TotalItemsToTranslate < ((_options._maxWorkerThreads * 4) + 11) && _options._itemsPerTransReq == ItemsPerTransReq.Auto )
                _options._itemsPerTransReq = ItemsPerTransReq.OnePerItem;
            List<Task> tasks = new();
            for ( int p = 0 ; p < _itemsToTranslate.Count ; ++p )
            {
                if ( _cancelTranslation )
                    break;
                for ( int i = 0 ; i < _itemsToTranslate[p].sourceText.Count ; ++i ) // string source in items.Source)
                {
                    if ( _cancelTranslation )
                        break;
                    if ( _itemsToTranslate[p].sourceText[i].Length == 0 )
                    {
                        LogLine("Failed to get item string.", OutPutLevel.ErrorLvl);
                        continue;
                    }
                    _itemsToTranslate[p].translatedText.Add(_itemsToTranslate[p].sourceText[i]);

                    while ( Volatile.Read(ref _workerThreadCount) > _options._maxWorkerThreads )
                        _ = Task.WaitAny(tasks.ToArray());
                    Task task = TranslateSourceText(p, i);
                    tasks.Add(task);
                }
            }

            Task.WaitAll(tasks.ToArray());
            sw.Stop();
            if ( _options._translationErrorCount > 0 )
                WriteLine($"{_options._translationErrorCount} Translation errors occurred and {_options._translationSubErrorCount} sub translation errors.", OutPutLevel.ErrorLvl);
            if ( _options._errorCount > 0 )
                WriteLine($"{_options._errorCount} unknown errors occurred.", OutPutLevel.ErrorLvl);
            TaskComplete(_options._translateCount, sw.Elapsed);
            if ( _options._waitKeyPress && _isConsoleMode )
            {
                Console.WriteLine("Press any key to exit.");
                _ = Console.ReadKey();
            }
            return true;
        }
        private async Task TranslateSourceText(int PacketIndex, int ItemIndex)
        {
            if ( _cancelTranslation )
                return;
            try
            {
                try
                {
                    _ = Interlocked.Increment(ref _workerThreadCount);
                    await GTranslate_TranslateSourceText(PacketIndex, ItemIndex);
                }
                catch ( Exception ee )
                {
                    // LogLine("TranslateSourceText[a]:" + ee.Message, OutPutLevel.ErrorLvl);
                    ++_options._errorCount;
                    throw new Exception($"TranslateSourceText[a]: {ee.Message}");
                }
                finally
                {
                    _ = Interlocked.Decrement(ref _workerThreadCount);
                    ++_options._totalCount;
                    if ( _options._progressBarOutput )
                        ShowProgress(_options._totalCount);
                    // LogLine("TotalCount=" + _options._totalCount, OutPutLevel.VerboseIfNotSilent);
                    if ( !_itemsToTranslate[PacketIndex].sourceText[ItemIndex].Equals(_itemsToTranslate[PacketIndex].translatedText[ItemIndex]) )
                    {
                        ++_options._translateCount;
                        ++_itemsToTranslate[PacketIndex].qtyItemsTranslated;
                    }
                }
            }
            catch ( Exception e )
            {
                LogLine("TranslateSourceText[b]:" + e.Message, OutPutLevel.ErrorLvl);
                ++_options._errorCount;
            }
        }
        private async Task GTranslate_TranslateSourceText(int PacketIndex, int ItemIndex)
        {
            if ( _cancelTranslation )
                return;
            if ( !_languagesProcessedSuccessStatuses.ContainsKey(_itemsToTranslate[PacketIndex].toLang))
                _languagesProcessedSuccessStatuses.Add(_itemsToTranslate[PacketIndex].toLang, LanguageProcessStatus.Unknown);
            string msgStart = "Start language translation: ";
            string msgNL = "";
            string msgTextWrapper = "\"";
            if ( _itemsToTranslate[PacketIndex].translationPacketType != TranslationPacketTypes.OnePacketPerString)
            {
                msgNL = "\r\n";
                msgTextWrapper = _HEAD_AND_FOOT_WRAPPER;
            }

            GTranslate.Translators.ITranslator translator = new AggregateTranslator();// GoogleTranslator();GoogleTranslator2();MicrosoftTranslator();YandexTranslator();BingTranslator();
            try
            {
                GTranslate.Results.ITranslationResult result = await translator.TranslateAsync(_itemsToTranslate[PacketIndex].sourceText[ItemIndex], _itemsToTranslate[PacketIndex].toLang, _itemsToTranslate[PacketIndex].fromLang);
                if ( result.Translation.Equals(_itemsToTranslate[PacketIndex].sourceText[ItemIndex]) && (_itemsToTranslate[PacketIndex].sourceText[ItemIndex].Length != 1 || !char.IsPunctuation(_itemsToTranslate[PacketIndex].sourceText[ItemIndex][0])) )
                    msgStart = "Translated text same as source: ";
                if ( MainWindow.VerbosityLevel.ScreenVerbosity == VerbosityLevels.Detail || MainWindow.VerbosityLevel.LogFileVerbosity == VerbosityLevels.Detail )
                {
                    string msg = $"{msgStart}{_itemsToTranslate[PacketIndex].toLang} using service: {result.Service}: {msgNL}Translated-Text({result.Translation.Length}): {msgTextWrapper}{result.Translation}{msgTextWrapper}{msgNL}{result.TargetLanguage}{msgNL}\n";
                    if ( MainWindow.VerbosityLevel.ScreenVerbosity == VerbosityLevels.Detail)
                        LogLine(msg, OutPutLevel.ShowInWindow, Color.YellowGreen);
                    if ( MainWindow.VerbosityLevel.LogFileVerbosity == VerbosityLevels.Detail && msgStart.Equals("Translated text same as source: ") )
                        LogToFile(msg, VerbosityLevels.Warn, _itemsToTranslate[PacketIndex].translationPacketType == TranslationPacketTypes.OnePacketPerString);
                }
                _itemsToTranslate[PacketIndex].translatedText[ItemIndex] = result.Translation;
                if ( _languagesProcessedSuccessStatuses[_itemsToTranslate[PacketIndex].toLang] == LanguageProcessStatus.Unknown )
                    _languagesProcessedSuccessStatuses[_itemsToTranslate[PacketIndex].toLang] = LanguageProcessStatus.Success;
            }
            catch ( Exception e )
            {
                _languagesProcessedSuccessStatuses[_itemsToTranslate[PacketIndex].toLang] = LanguageProcessStatus.Failed;
                if ( !_languagesTagsWithErrors.Contains(_itemsToTranslate[PacketIndex].toLang) )
                {   // Only log to screen the first time a language produces an error.
                    _languagesTagsWithErrors.Add(_itemsToTranslate[PacketIndex].toLang);
                    string Msg = $"GTranslate_..:toLang({_itemsToTranslate[PacketIndex].toLang}):fromLang({_itemsToTranslate[PacketIndex].fromLang}):Translator=({translator.GetType().Name}): {e.Message}";
                    LogLine(Msg, OutPutLevel.ShowInWindow,Color.DarkRed);
                    LogToFile($"{Msg}: {msgNL}Source-Text({_itemsToTranslate[PacketIndex].sourceText[ItemIndex].Length}):{msgNL}{msgTextWrapper}{_itemsToTranslate[PacketIndex].sourceText[ItemIndex]}{msgTextWrapper}{msgNL}{msgNL}", VerbosityLevels.Errors, true);
                }
                LogLine($"GTranslate_..:toLang({_itemsToTranslate[PacketIndex].toLang}):fromLang({_itemsToTranslate[PacketIndex].fromLang}):Translator=({translator.GetType().Name}): {e.Message}", OutPutLevel.ErrorLvl);
                throw new Exception($"GTranslate_..:toLang({_itemsToTranslate[PacketIndex].toLang}):fromLang({_itemsToTranslate[PacketIndex].fromLang}):Translator=({translator.GetType().Name}): {e.Message}");
            }
        }
        #endregion Class main functional logic

        #region Virtual functions for derive class
        /// <summary>
        /// Allows derived class to replace invalid characters with alternatives.
        /// </summary>
        /// <param name="itemDetailsList">Item detail to translate</param>
        protected virtual string ItemDetailsReplaceInvalidChar(ItemDetails itemDetails)
        {
            return itemDetails.Translation; // See example commented-out function at end of this file
        }
        /// <summary>
        /// Allows derived class to get details on how many items will be checked for language translation candidates
        /// </summary>
        /// <param name="QtyItemsToCheck">Quantity of items which have to be checked</param>
        protected virtual void InitializeProgressBar(int QtyItemsToCheck) { }
        /// <summary>
        /// Allows derived class to get details on how many items will be checked for language translation candidates
        /// </summary>
        /// <param name="QtyItemsTranslationCandidate">Quantity of items found that are translation candidates</param>
        protected virtual void TaskComplete(int QtyItemsTranslationCandidate, TimeSpan TotalRunTime) { }
        /// <summary>
        /// Allows derived class to get on going progress status
        /// </summary>
        /// <param name="TotalCount">Quantity of items that have been processed</param>
        protected virtual void ShowProgress(int TotalCount) { }
        /// <summary>
        /// Allows derived class to either write to log file, or to Debug.WriteLine
        /// </summary>
        /// <param name="message">String to print</param>
        /// <param name="OutPutLevel">Verbosity Level</param>
        /// <param name="Color">Foreground Color if sent to screen</param>
        protected virtual void LogLine(string message, OutPutLevel outputlevel = OutPutLevel.NormalLvl, Color? color = null) // ToDo: Separate ShowInWindow from OutPutLevel and take out Color parameter from this function
        {
            if ( MainWindow.VerbosityLevel.ScreenVerbosity == VerbosityLevels.Silent )
                return;
            Debug.WriteLine(message);
        }
        /// <summary>
        /// Allows derived class to write to screen/window
        /// </summary>
        /// <param name="message">String to print</param>
        /// <param name="OutPutLevel">Verbosity Level</param>
        /// <param name="Color">Foreground Color if sent to screen</param>
        protected virtual void WriteLine(string message, OutPutLevel outputlevel = OutPutLevel.NormalLvl) // ToDo: Add Color variable, and make this the expected write to window function
        {
            if ( MainWindow.VerbosityLevel.ScreenVerbosity == VerbosityLevels.Silent )
                return;
            Console.WriteLine(message);
        }
        /// <summary>
        /// Allows derived class to have method to display help
        /// </summary>
        /// <param name="AdvanceHelpPage">Set to true to display advance help</param>
        protected virtual void PrintHelp(bool AdvanceHelpPage) { }
        protected virtual void LogToFile(string message, VerbosityLevels verbositylevels, bool DbgWriteLine = true) { }
        #endregion Virtual functions for derive class
    }
}
#region comment out miscellaneous info // -- Consider deleting this section
//protected virtual string ItemDetailsReplaceInvalidChar(ItemDetails itemDetails)
//{
//    // Example usage for derived classes
//    try
//    {
//        // replaces illegal filename characters with alternatives
//        itemDetails.Translation = itemDetails.Translation.Replace('\\', '-');
//        itemDetails.Translation = itemDetails.Translation.Replace('/', '-');
//        itemDetails.Translation = itemDetails.Translation.Replace(':', '-');
//        itemDetails.Translation = itemDetails.Translation.Replace('*', ' ');
//        itemDetails.Translation = itemDetails.Translation.Replace('?', ' ');
//        itemDetails.Translation = itemDetails.Translation.Replace('\"', '\'');
//        itemDetails.Translation = itemDetails.Translation.Replace('<', '[');
//        itemDetails.Translation = itemDetails.Translation.Replace('>', ']');
//        itemDetails.Translation = itemDetails.Translation.Replace('|', '-');
//        itemDetails.Translation = itemDetails.Translation.Replace('\t', ' ');
//        itemDetails.Translation = itemDetails.Translation.Replace('\n', ' ');
//    }
//    catch ( Exception e )
//    {
//        LogLine(e.Message, OutPutLevel.ErrorLvl);
//        throw new Exception(e.Message);
//    }
//    return itemDetails.Translation;
//}
#endregion comment out miscellaneous info // -- Consider deleting this section

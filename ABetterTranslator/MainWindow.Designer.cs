/* ***************************************************************************
 * Copyright © 2022 David Maisonave	(https://axter.com)	All rights reserved.
 * ***************************************************************************
 * The is free software. You can redistribute it and/or modify it under the 
 * terms of the MIT License. For more information, please see License file 
 * distributed with this package.
 * ***************************************************************************
*/
namespace ABetterTranslator
{
    using BrightIdeasSoftware;

	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.openResxFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxDispalyWarningPrompts = new System.Windows.Forms.CheckBox();
            this.linkLabelcheckBoxDispalyWarningPrompts = new System.Windows.Forms.LinkLabel();
            this.browseLogFolderPath = new System.Windows.Forms.Button();
            this.linkLabelLogFolderPath = new System.Windows.Forms.LinkLabel();
            this.linkLabelLogFileVerbosityLevel = new System.Windows.Forms.LinkLabel();
            this.linkLabelScreenVerbosityLevel = new System.Windows.Forms.LinkLabel();
            this.buttonOpenLoggingFolderInFileExplorer = new System.Windows.Forms.Button();
            this.groupBoxItemsPerTransaltionRequest = new System.Windows.Forms.GroupBox();
            this.linkLabelItemsPerTransaltionRequest = new System.Windows.Forms.LinkLabel();
            this.comboBoxItemsPerTransaltionRequest = new System.Windows.Forms.ComboBox();
            this.linkLabelMaxTranslateLen = new System.Windows.Forms.LinkLabel();
            this.linkLabelMaxThreads = new System.Windows.Forms.LinkLabel();
            this.checkBoxDeleteLangResxFilesBeforeTranslation = new System.Windows.Forms.CheckBox();
            this.groupBoxAddOriginalSrcTextToComment = new System.Windows.Forms.GroupBox();
            this.linkLabelAddOriginalSrcTextToComment = new System.Windows.Forms.LinkLabel();
            this.comboBoxAddOriginalSrcTextToComment = new System.Windows.Forms.ComboBox();
            this.linkLabelDeleteLangResxFilesBeforeTranslation = new System.Windows.Forms.LinkLabel();
            this.linkLabelBackupFilesBeforeTranslation = new System.Windows.Forms.LinkLabel();
            this.browseBkUpDirFolderButton = new System.Windows.Forms.Button();
            this.linkLabelBkUpDir = new System.Windows.Forms.LinkLabel();
            this.groupBoxDefaultLanguageSet = new System.Windows.Forms.GroupBox();
            this.comboBoxDefaultLanguageSet = new System.Windows.Forms.ComboBox();
            this.linkLabelDefaultLanguageSet = new System.Windows.Forms.LinkLabel();
            this.buttonDeleteLangAppendedResxFiles = new System.Windows.Forms.Button();
            this.linkLabelDeleteLangAppendedResxFiles = new System.Windows.Forms.LinkLabel();
            this.buttonInAdvOpt_OpenFolderInWindowsExplorer = new System.Windows.Forms.Button();
            this.buttonOpenBackupFolderInFileExplorer = new System.Windows.Forms.Button();
            this.browseFileButton = new System.Windows.Forms.Button();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.progressBarWhileTranslatingResxFile = new System.Windows.Forms.ProgressBar();
            this.buttonOpenFolderInWindowsExplorer = new System.Windows.Forms.Button();
            this.buttonBrowseForTextFile = new System.Windows.Forms.Button();
            this.advanceOptionsTab = new System.Windows.Forms.TabPage();
            this.tabControlAdvanceOptionsSubTab = new System.Windows.Forms.TabControl();
            this.tabPageResxTranslationOptions = new System.Windows.Forms.TabPage();
            this.groupBoxBkUpDir = new System.Windows.Forms.GroupBox();
            this.textBoxBkUpDir = new System.Windows.Forms.TextBox();
            this.groupBoxDeleteLangAppendedResxFiles = new System.Windows.Forms.GroupBox();
            this.checkBoxBackupFilesBeforeTranslation = new System.Windows.Forms.CheckBox();
            this.tabPageTranslationOptions = new System.Windows.Forms.TabPage();
            this.labelMaxThread = new System.Windows.Forms.Label();
            this.MaxThread = new System.Windows.Forms.TextBox();
            this.MaxTranslateLen = new System.Windows.Forms.TextBox();
            this.labelMaxTranslateLen = new System.Windows.Forms.Label();
            this.tabPageLoggingOptions = new System.Windows.Forms.TabPage();
            this.groupBoxScreenVerbosityLevel = new System.Windows.Forms.GroupBox();
            this.comboBoxScreenVerbosityLevel = new System.Windows.Forms.ComboBox();
            this.groupBoxLogFileVerbosityLevel = new System.Windows.Forms.GroupBox();
            this.comboBoxLogFileVerbosityLevel = new System.Windows.Forms.ComboBox();
            this.textBoxLogFolderPath = new System.Windows.Forms.TextBox();
            this.labelLogFolderPath = new System.Windows.Forms.Label();
            this.textTab = new System.Windows.Forms.TabPage();
            this.groupBoxTextBoxTextToTranslate = new System.Windows.Forms.GroupBox();
            this.textBoxTextToTranslate = new System.Windows.Forms.TextBox();
            this.groupBoxTextBoxTranslation = new System.Windows.Forms.GroupBox();
            this.textBoxTranslation = new System.Windows.Forms.RichTextBox();
            this.textCmdPanel = new System.Windows.Forms.Panel();
            this.buttonTranslateTextInTextBox = new System.Windows.Forms.Button();
            this.toCodeLabel = new System.Windows.Forms.Label();
            this.comboBoxToLang = new System.Windows.Forms.ComboBox();
            this.comboBoxFromLang = new System.Windows.Forms.ComboBox();
            this.fromCodeLabel = new System.Windows.Forms.Label();
            this.resxTab = new System.Windows.Forms.TabPage();
            this.tabControlTranslateResxSubTab = new System.Windows.Forms.TabControl();
            this.tabPageLanguageList = new System.Windows.Forms.TabPage();
            this.groupBoxFilterText = new System.Windows.Forms.GroupBox();
            this.textBoxFilterText = new System.Windows.Forms.TextBox();
            this.linkLabelFilterText = new System.Windows.Forms.LinkLabel();
            this.languageList = new BrightIdeasSoftware.FastObjectListView();
            this.olv_LanguageTag = new BrightIdeasSoftware.OLVColumn();
            this.olv_LanguageName = new BrightIdeasSoftware.OLVColumn();
            this.olv_LanguageAliases = new BrightIdeasSoftware.OLVColumn();
            this.olv_TranslationSupported = new BrightIdeasSoftware.OLVColumn();
            this.olv_LanguageTranslatorTag = new BrightIdeasSoftware.OLVColumn();
            this.olv_Iso639_3 = new BrightIdeasSoftware.OLVColumn();
            this.tabPageTranslateResxLogging = new System.Windows.Forms.TabPage();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonTranslate = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemTranslateAllLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsSupportedLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguageInterfacePacks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTranslateBusinessLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_TranslateAfrica = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_TranslateAsia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_TranslateEurope = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_TranslateMiddleEast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_TranslateNorthAmerica = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_TranslateSouthAmerica = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTranslateTop5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTranslateTop10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTranslateTop20 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTranslateTop30 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButtonSelectAllLanugages = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSelectClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSelectUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButtonResetDisplayToSet = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemWindowsLanguagePacks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePacks_ShowOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePacks_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePackInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePackInterface_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemAllTranslatorSupportedLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAllTranslatorSupportedLanguages_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemISO639_1UsingOfficialNames = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemISO639_1UsingOfficialNames_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemISO639_1PlusWinLangPack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemISO639_1PlusWinLangPack_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSubLanguagesAndRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonStopTranslation = new System.Windows.Forms.ToolStripButton();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.comboBoxFromLanguage = new System.Windows.Forms.ComboBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.tabs = new System.Windows.Forms.TabControl();
            this.openFileDialogTextFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxItemsPerTransaltionRequest.SuspendLayout();
            this.groupBoxAddOriginalSrcTextToComment.SuspendLayout();
            this.groupBoxDefaultLanguageSet.SuspendLayout();
            this.advanceOptionsTab.SuspendLayout();
            this.tabControlAdvanceOptionsSubTab.SuspendLayout();
            this.tabPageResxTranslationOptions.SuspendLayout();
            this.groupBoxBkUpDir.SuspendLayout();
            this.groupBoxDeleteLangAppendedResxFiles.SuspendLayout();
            this.tabPageTranslationOptions.SuspendLayout();
            this.tabPageLoggingOptions.SuspendLayout();
            this.groupBoxScreenVerbosityLevel.SuspendLayout();
            this.groupBoxLogFileVerbosityLevel.SuspendLayout();
            this.textTab.SuspendLayout();
            this.groupBoxTextBoxTextToTranslate.SuspendLayout();
            this.groupBoxTextBoxTranslation.SuspendLayout();
            this.textCmdPanel.SuspendLayout();
            this.resxTab.SuspendLayout();
            this.tabControlTranslateResxSubTab.SuspendLayout();
            this.tabPageLanguageList.SuspendLayout();
            this.groupBoxFilterText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.languageList)).BeginInit();
            this.tabPageTranslateResxLogging.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // openResxFileDialog
            // 
            this.openResxFileDialog.DefaultExt = "resx";
            this.openResxFileDialog.FileName = "Form1.resx";
            resources.ApplyResources(this.openResxFileDialog, "openResxFileDialog");
            // 
            // folderBrowserDialog
            // 
            resources.ApplyResources(this.folderBrowserDialog, "folderBrowserDialog");
            // 
            // checkBoxDispalyWarningPrompts
            // 
            resources.ApplyResources(this.checkBoxDispalyWarningPrompts, "checkBoxDispalyWarningPrompts");
            this.checkBoxDispalyWarningPrompts.Checked = true;
            this.checkBoxDispalyWarningPrompts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDispalyWarningPrompts.Name = "checkBoxDispalyWarningPrompts";
            this.toolTip.SetToolTip(this.checkBoxDispalyWarningPrompts, resources.GetString("checkBoxDispalyWarningPrompts.ToolTip"));
            this.checkBoxDispalyWarningPrompts.UseVisualStyleBackColor = true;
            // 
            // linkLabelcheckBoxDispalyWarningPrompts
            // 
            resources.ApplyResources(this.linkLabelcheckBoxDispalyWarningPrompts, "linkLabelcheckBoxDispalyWarningPrompts");
            this.linkLabelcheckBoxDispalyWarningPrompts.Name = "linkLabelcheckBoxDispalyWarningPrompts";
            this.linkLabelcheckBoxDispalyWarningPrompts.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelcheckBoxDispalyWarningPrompts, resources.GetString("linkLabelcheckBoxDispalyWarningPrompts.ToolTip"));
            this.linkLabelcheckBoxDispalyWarningPrompts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelcheckBoxDispalyWarningPrompts_LinkClicked);
            // 
            // browseLogFolderPath
            // 
            resources.ApplyResources(this.browseLogFolderPath, "browseLogFolderPath");
            this.browseLogFolderPath.Image = global::ABetterTranslator.Properties.Resources.browseFileButton;
            this.browseLogFolderPath.Name = "browseLogFolderPath";
            this.toolTip.SetToolTip(this.browseLogFolderPath, resources.GetString("browseLogFolderPath.ToolTip"));
            this.browseLogFolderPath.UseVisualStyleBackColor = true;
            // 
            // linkLabelLogFolderPath
            // 
            resources.ApplyResources(this.linkLabelLogFolderPath, "linkLabelLogFolderPath");
            this.linkLabelLogFolderPath.Name = "linkLabelLogFolderPath";
            this.linkLabelLogFolderPath.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelLogFolderPath, resources.GetString("linkLabelLogFolderPath.ToolTip"));
            this.linkLabelLogFolderPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogFolderPath_LinkClicked);
            // 
            // linkLabelLogFileVerbosityLevel
            // 
            resources.ApplyResources(this.linkLabelLogFileVerbosityLevel, "linkLabelLogFileVerbosityLevel");
            this.linkLabelLogFileVerbosityLevel.Name = "linkLabelLogFileVerbosityLevel";
            this.linkLabelLogFileVerbosityLevel.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelLogFileVerbosityLevel, resources.GetString("linkLabelLogFileVerbosityLevel.ToolTip"));
            this.linkLabelLogFileVerbosityLevel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogFileVerbosityLevel_LinkClicked);
            // 
            // linkLabelScreenVerbosityLevel
            // 
            resources.ApplyResources(this.linkLabelScreenVerbosityLevel, "linkLabelScreenVerbosityLevel");
            this.linkLabelScreenVerbosityLevel.Name = "linkLabelScreenVerbosityLevel";
            this.linkLabelScreenVerbosityLevel.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelScreenVerbosityLevel, resources.GetString("linkLabelScreenVerbosityLevel.ToolTip"));
            this.linkLabelScreenVerbosityLevel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelScreenVerbosityLevel_LinkClicked);
            // 
            // buttonOpenLoggingFolderInFileExplorer
            // 
            resources.ApplyResources(this.buttonOpenLoggingFolderInFileExplorer, "buttonOpenLoggingFolderInFileExplorer");
            this.buttonOpenLoggingFolderInFileExplorer.Image = global::ABetterTranslator.Properties.Resources.FileExplorer_96__16_x_16_;
            this.buttonOpenLoggingFolderInFileExplorer.Name = "buttonOpenLoggingFolderInFileExplorer";
            this.toolTip.SetToolTip(this.buttonOpenLoggingFolderInFileExplorer, resources.GetString("buttonOpenLoggingFolderInFileExplorer.ToolTip"));
            this.buttonOpenLoggingFolderInFileExplorer.UseVisualStyleBackColor = true;
            this.buttonOpenLoggingFolderInFileExplorer.Click += new System.EventHandler(this.buttonOpenLoggingFolderInFileExplorer_Click);
            // 
            // groupBoxItemsPerTransaltionRequest
            // 
            this.groupBoxItemsPerTransaltionRequest.Controls.Add(this.linkLabelItemsPerTransaltionRequest);
            this.groupBoxItemsPerTransaltionRequest.Controls.Add(this.comboBoxItemsPerTransaltionRequest);
            resources.ApplyResources(this.groupBoxItemsPerTransaltionRequest, "groupBoxItemsPerTransaltionRequest");
            this.groupBoxItemsPerTransaltionRequest.Name = "groupBoxItemsPerTransaltionRequest";
            this.groupBoxItemsPerTransaltionRequest.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxItemsPerTransaltionRequest, resources.GetString("groupBoxItemsPerTransaltionRequest.ToolTip"));
            // 
            // linkLabelItemsPerTransaltionRequest
            // 
            resources.ApplyResources(this.linkLabelItemsPerTransaltionRequest, "linkLabelItemsPerTransaltionRequest");
            this.linkLabelItemsPerTransaltionRequest.Name = "linkLabelItemsPerTransaltionRequest";
            this.linkLabelItemsPerTransaltionRequest.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelItemsPerTransaltionRequest, resources.GetString("linkLabelItemsPerTransaltionRequest.ToolTip"));
            this.linkLabelItemsPerTransaltionRequest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelItemsPerTransaltionRequest_LinkClicked);
            // 
            // comboBoxItemsPerTransaltionRequest
            // 
            this.comboBoxItemsPerTransaltionRequest.FormattingEnabled = true;
            this.comboBoxItemsPerTransaltionRequest.Items.AddRange(new object[] {
            resources.GetString("comboBoxItemsPerTransaltionRequest.Items"),
            resources.GetString("comboBoxItemsPerTransaltionRequest.Items1"),
            resources.GetString("comboBoxItemsPerTransaltionRequest.Items2")});
            resources.ApplyResources(this.comboBoxItemsPerTransaltionRequest, "comboBoxItemsPerTransaltionRequest");
            this.comboBoxItemsPerTransaltionRequest.Name = "comboBoxItemsPerTransaltionRequest";
            // 
            // linkLabelMaxTranslateLen
            // 
            resources.ApplyResources(this.linkLabelMaxTranslateLen, "linkLabelMaxTranslateLen");
            this.linkLabelMaxTranslateLen.Name = "linkLabelMaxTranslateLen";
            this.linkLabelMaxTranslateLen.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelMaxTranslateLen, resources.GetString("linkLabelMaxTranslateLen.ToolTip"));
            this.linkLabelMaxTranslateLen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMaxTranslateLen_LinkClicked);
            // 
            // linkLabelMaxThreads
            // 
            resources.ApplyResources(this.linkLabelMaxThreads, "linkLabelMaxThreads");
            this.linkLabelMaxThreads.Name = "linkLabelMaxThreads";
            this.linkLabelMaxThreads.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelMaxThreads, resources.GetString("linkLabelMaxThreads.ToolTip"));
            this.linkLabelMaxThreads.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMaxThreads_LinkClicked);
            // 
            // checkBoxDeleteLangResxFilesBeforeTranslation
            // 
            resources.ApplyResources(this.checkBoxDeleteLangResxFilesBeforeTranslation, "checkBoxDeleteLangResxFilesBeforeTranslation");
            this.checkBoxDeleteLangResxFilesBeforeTranslation.Name = "checkBoxDeleteLangResxFilesBeforeTranslation";
            this.toolTip.SetToolTip(this.checkBoxDeleteLangResxFilesBeforeTranslation, resources.GetString("checkBoxDeleteLangResxFilesBeforeTranslation.ToolTip"));
            this.checkBoxDeleteLangResxFilesBeforeTranslation.UseVisualStyleBackColor = true;
            // 
            // groupBoxAddOriginalSrcTextToComment
            // 
            this.groupBoxAddOriginalSrcTextToComment.Controls.Add(this.linkLabelAddOriginalSrcTextToComment);
            this.groupBoxAddOriginalSrcTextToComment.Controls.Add(this.comboBoxAddOriginalSrcTextToComment);
            resources.ApplyResources(this.groupBoxAddOriginalSrcTextToComment, "groupBoxAddOriginalSrcTextToComment");
            this.groupBoxAddOriginalSrcTextToComment.Name = "groupBoxAddOriginalSrcTextToComment";
            this.groupBoxAddOriginalSrcTextToComment.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxAddOriginalSrcTextToComment, resources.GetString("groupBoxAddOriginalSrcTextToComment.ToolTip"));
            // 
            // linkLabelAddOriginalSrcTextToComment
            // 
            resources.ApplyResources(this.linkLabelAddOriginalSrcTextToComment, "linkLabelAddOriginalSrcTextToComment");
            this.linkLabelAddOriginalSrcTextToComment.Name = "linkLabelAddOriginalSrcTextToComment";
            this.linkLabelAddOriginalSrcTextToComment.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelAddOriginalSrcTextToComment, resources.GetString("linkLabelAddOriginalSrcTextToComment.ToolTip"));
            this.linkLabelAddOriginalSrcTextToComment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAddOriginalSrcTextToComment_LinkClicked);
            // 
            // comboBoxAddOriginalSrcTextToComment
            // 
            this.comboBoxAddOriginalSrcTextToComment.FormattingEnabled = true;
            this.comboBoxAddOriginalSrcTextToComment.Items.AddRange(new object[] {
            resources.GetString("comboBoxAddOriginalSrcTextToComment.Items"),
            resources.GetString("comboBoxAddOriginalSrcTextToComment.Items1"),
            resources.GetString("comboBoxAddOriginalSrcTextToComment.Items2"),
            resources.GetString("comboBoxAddOriginalSrcTextToComment.Items3")});
            resources.ApplyResources(this.comboBoxAddOriginalSrcTextToComment, "comboBoxAddOriginalSrcTextToComment");
            this.comboBoxAddOriginalSrcTextToComment.Name = "comboBoxAddOriginalSrcTextToComment";
            // 
            // linkLabelDeleteLangResxFilesBeforeTranslation
            // 
            resources.ApplyResources(this.linkLabelDeleteLangResxFilesBeforeTranslation, "linkLabelDeleteLangResxFilesBeforeTranslation");
            this.linkLabelDeleteLangResxFilesBeforeTranslation.Name = "linkLabelDeleteLangResxFilesBeforeTranslation";
            this.linkLabelDeleteLangResxFilesBeforeTranslation.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelDeleteLangResxFilesBeforeTranslation, resources.GetString("linkLabelDeleteLangResxFilesBeforeTranslation.ToolTip"));
            this.linkLabelDeleteLangResxFilesBeforeTranslation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeleteLangResxFilesBeforeTranslation_LinkClicked);
            // 
            // linkLabelBackupFilesBeforeTranslation
            // 
            resources.ApplyResources(this.linkLabelBackupFilesBeforeTranslation, "linkLabelBackupFilesBeforeTranslation");
            this.linkLabelBackupFilesBeforeTranslation.Name = "linkLabelBackupFilesBeforeTranslation";
            this.linkLabelBackupFilesBeforeTranslation.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelBackupFilesBeforeTranslation, resources.GetString("linkLabelBackupFilesBeforeTranslation.ToolTip"));
            this.linkLabelBackupFilesBeforeTranslation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBackupFilesBeforeTranslation_LinkClicked);
            // 
            // browseBkUpDirFolderButton
            // 
            resources.ApplyResources(this.browseBkUpDirFolderButton, "browseBkUpDirFolderButton");
            this.browseBkUpDirFolderButton.Image = global::ABetterTranslator.Properties.Resources.browseFileButton;
            this.browseBkUpDirFolderButton.Name = "browseBkUpDirFolderButton";
            this.toolTip.SetToolTip(this.browseBkUpDirFolderButton, resources.GetString("browseBkUpDirFolderButton.ToolTip"));
            this.browseBkUpDirFolderButton.UseVisualStyleBackColor = true;
            // 
            // linkLabelBkUpDir
            // 
            resources.ApplyResources(this.linkLabelBkUpDir, "linkLabelBkUpDir");
            this.linkLabelBkUpDir.Name = "linkLabelBkUpDir";
            this.linkLabelBkUpDir.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelBkUpDir, resources.GetString("linkLabelBkUpDir.ToolTip"));
            this.linkLabelBkUpDir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBkUpDir_LinkClicked);
            // 
            // groupBoxDefaultLanguageSet
            // 
            this.groupBoxDefaultLanguageSet.Controls.Add(this.comboBoxDefaultLanguageSet);
            this.groupBoxDefaultLanguageSet.Controls.Add(this.linkLabelDefaultLanguageSet);
            resources.ApplyResources(this.groupBoxDefaultLanguageSet, "groupBoxDefaultLanguageSet");
            this.groupBoxDefaultLanguageSet.Name = "groupBoxDefaultLanguageSet";
            this.groupBoxDefaultLanguageSet.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxDefaultLanguageSet, resources.GetString("groupBoxDefaultLanguageSet.ToolTip"));
            // 
            // comboBoxDefaultLanguageSet
            // 
            this.comboBoxDefaultLanguageSet.FormattingEnabled = true;
            this.comboBoxDefaultLanguageSet.Items.AddRange(new object[] {
            resources.GetString("comboBoxDefaultLanguageSet.Items"),
            resources.GetString("comboBoxDefaultLanguageSet.Items1"),
            resources.GetString("comboBoxDefaultLanguageSet.Items2"),
            resources.GetString("comboBoxDefaultLanguageSet.Items3"),
            resources.GetString("comboBoxDefaultLanguageSet.Items4")});
            resources.ApplyResources(this.comboBoxDefaultLanguageSet, "comboBoxDefaultLanguageSet");
            this.comboBoxDefaultLanguageSet.Name = "comboBoxDefaultLanguageSet";
            this.toolTip.SetToolTip(this.comboBoxDefaultLanguageSet, resources.GetString("comboBoxDefaultLanguageSet.ToolTip"));
            // 
            // linkLabelDefaultLanguageSet
            // 
            resources.ApplyResources(this.linkLabelDefaultLanguageSet, "linkLabelDefaultLanguageSet");
            this.linkLabelDefaultLanguageSet.Name = "linkLabelDefaultLanguageSet";
            this.linkLabelDefaultLanguageSet.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelDefaultLanguageSet, resources.GetString("linkLabelDefaultLanguageSet.ToolTip"));
            this.linkLabelDefaultLanguageSet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDefaultLanguageSet_LinkClicked);
            // 
            // buttonDeleteLangAppendedResxFiles
            // 
            resources.ApplyResources(this.buttonDeleteLangAppendedResxFiles, "buttonDeleteLangAppendedResxFiles");
            this.buttonDeleteLangAppendedResxFiles.Name = "buttonDeleteLangAppendedResxFiles";
            this.toolTip.SetToolTip(this.buttonDeleteLangAppendedResxFiles, resources.GetString("buttonDeleteLangAppendedResxFiles.ToolTip"));
            this.buttonDeleteLangAppendedResxFiles.UseVisualStyleBackColor = true;
            this.buttonDeleteLangAppendedResxFiles.Click += new System.EventHandler(this.buttonDeleteLangAppendedResxFiles_Click);
            // 
            // linkLabelDeleteLangAppendedResxFiles
            // 
            resources.ApplyResources(this.linkLabelDeleteLangAppendedResxFiles, "linkLabelDeleteLangAppendedResxFiles");
            this.linkLabelDeleteLangAppendedResxFiles.Name = "linkLabelDeleteLangAppendedResxFiles";
            this.linkLabelDeleteLangAppendedResxFiles.TabStop = true;
            this.toolTip.SetToolTip(this.linkLabelDeleteLangAppendedResxFiles, resources.GetString("linkLabelDeleteLangAppendedResxFiles.ToolTip"));
            this.linkLabelDeleteLangAppendedResxFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeleteLangAppendedResxFiles_LinkClicked);
            // 
            // buttonInAdvOpt_OpenFolderInWindowsExplorer
            // 
            resources.ApplyResources(this.buttonInAdvOpt_OpenFolderInWindowsExplorer, "buttonInAdvOpt_OpenFolderInWindowsExplorer");
            this.buttonInAdvOpt_OpenFolderInWindowsExplorer.Image = global::ABetterTranslator.Properties.Resources.FileExplorer_96__16_x_16_;
            this.buttonInAdvOpt_OpenFolderInWindowsExplorer.Name = "buttonInAdvOpt_OpenFolderInWindowsExplorer";
            this.toolTip.SetToolTip(this.buttonInAdvOpt_OpenFolderInWindowsExplorer, resources.GetString("buttonInAdvOpt_OpenFolderInWindowsExplorer.ToolTip"));
            this.buttonInAdvOpt_OpenFolderInWindowsExplorer.UseVisualStyleBackColor = true;
            this.buttonInAdvOpt_OpenFolderInWindowsExplorer.Click += new System.EventHandler(this.buttonInAdvOpt_OpenFolderInWindowsExplorer_Click);
            // 
            // buttonOpenBackupFolderInFileExplorer
            // 
            resources.ApplyResources(this.buttonOpenBackupFolderInFileExplorer, "buttonOpenBackupFolderInFileExplorer");
            this.buttonOpenBackupFolderInFileExplorer.Image = global::ABetterTranslator.Properties.Resources.FileExplorer_96__16_x_16_;
            this.buttonOpenBackupFolderInFileExplorer.Name = "buttonOpenBackupFolderInFileExplorer";
            this.toolTip.SetToolTip(this.buttonOpenBackupFolderInFileExplorer, resources.GetString("buttonOpenBackupFolderInFileExplorer.ToolTip"));
            this.buttonOpenBackupFolderInFileExplorer.UseVisualStyleBackColor = true;
            this.buttonOpenBackupFolderInFileExplorer.Click += new System.EventHandler(this.buttonOpenBackupFolderInFileExplorer_Click);
            // 
            // browseFileButton
            // 
            resources.ApplyResources(this.browseFileButton, "browseFileButton");
            this.browseFileButton.Image = global::ABetterTranslator.Properties.Resources.browseFileButton;
            this.browseFileButton.Name = "browseFileButton";
            this.toolTip.SetToolTip(this.browseFileButton, resources.GetString("browseFileButton.ToolTip"));
            this.browseFileButton.UseVisualStyleBackColor = true;
            this.browseFileButton.Click += new System.EventHandler(this.BrowseFiles);
            // 
            // browseFolderButton
            // 
            resources.ApplyResources(this.browseFolderButton, "browseFolderButton");
            this.browseFolderButton.Image = global::ABetterTranslator.Properties.Resources.browseFileButton;
            this.browseFolderButton.Name = "browseFolderButton";
            this.toolTip.SetToolTip(this.browseFolderButton, resources.GetString("browseFolderButton.ToolTip"));
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.BrowseFolders);
            // 
            // progressBarWhileTranslatingResxFile
            // 
            resources.ApplyResources(this.progressBarWhileTranslatingResxFile, "progressBarWhileTranslatingResxFile");
            this.progressBarWhileTranslatingResxFile.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.progressBarWhileTranslatingResxFile.Name = "progressBarWhileTranslatingResxFile";
            this.progressBarWhileTranslatingResxFile.Step = 1;
            this.toolTip.SetToolTip(this.progressBarWhileTranslatingResxFile, resources.GetString("progressBarWhileTranslatingResxFile.ToolTip"));
            // 
            // buttonOpenFolderInWindowsExplorer
            // 
            resources.ApplyResources(this.buttonOpenFolderInWindowsExplorer, "buttonOpenFolderInWindowsExplorer");
            this.buttonOpenFolderInWindowsExplorer.Image = global::ABetterTranslator.Properties.Resources.FileExplorer_96__16_x_16_;
            this.buttonOpenFolderInWindowsExplorer.Name = "buttonOpenFolderInWindowsExplorer";
            this.toolTip.SetToolTip(this.buttonOpenFolderInWindowsExplorer, resources.GetString("buttonOpenFolderInWindowsExplorer.ToolTip"));
            this.buttonOpenFolderInWindowsExplorer.UseVisualStyleBackColor = true;
            this.buttonOpenFolderInWindowsExplorer.Click += new System.EventHandler(this.buttonOpenFolderInWindowsExplorer_Click);
            // 
            // buttonBrowseForTextFile
            // 
            resources.ApplyResources(this.buttonBrowseForTextFile, "buttonBrowseForTextFile");
            this.buttonBrowseForTextFile.Image = global::ABetterTranslator.Properties.Resources.browseFileButton;
            this.buttonBrowseForTextFile.Name = "buttonBrowseForTextFile";
            this.toolTip.SetToolTip(this.buttonBrowseForTextFile, resources.GetString("buttonBrowseForTextFile.ToolTip"));
            this.buttonBrowseForTextFile.UseVisualStyleBackColor = true;
            this.buttonBrowseForTextFile.Click += new System.EventHandler(this.buttonBrowseForTextFile_Click);
            // 
            // advanceOptionsTab
            // 
            this.advanceOptionsTab.Controls.Add(this.tabControlAdvanceOptionsSubTab);
            this.advanceOptionsTab.Controls.Add(this.linkLabelcheckBoxDispalyWarningPrompts);
            this.advanceOptionsTab.Controls.Add(this.checkBoxDispalyWarningPrompts);
            resources.ApplyResources(this.advanceOptionsTab, "advanceOptionsTab");
            this.advanceOptionsTab.Name = "advanceOptionsTab";
            this.advanceOptionsTab.UseVisualStyleBackColor = true;
            // 
            // tabControlAdvanceOptionsSubTab
            // 
            resources.ApplyResources(this.tabControlAdvanceOptionsSubTab, "tabControlAdvanceOptionsSubTab");
            this.tabControlAdvanceOptionsSubTab.Controls.Add(this.tabPageResxTranslationOptions);
            this.tabControlAdvanceOptionsSubTab.Controls.Add(this.tabPageTranslationOptions);
            this.tabControlAdvanceOptionsSubTab.Controls.Add(this.tabPageLoggingOptions);
            this.tabControlAdvanceOptionsSubTab.Name = "tabControlAdvanceOptionsSubTab";
            this.tabControlAdvanceOptionsSubTab.SelectedIndex = 0;
            // 
            // tabPageResxTranslationOptions
            // 
            this.tabPageResxTranslationOptions.Controls.Add(this.groupBoxBkUpDir);
            this.tabPageResxTranslationOptions.Controls.Add(this.groupBoxDeleteLangAppendedResxFiles);
            this.tabPageResxTranslationOptions.Controls.Add(this.groupBoxDefaultLanguageSet);
            this.tabPageResxTranslationOptions.Controls.Add(this.checkBoxBackupFilesBeforeTranslation);
            this.tabPageResxTranslationOptions.Controls.Add(this.linkLabelBackupFilesBeforeTranslation);
            this.tabPageResxTranslationOptions.Controls.Add(this.linkLabelDeleteLangResxFilesBeforeTranslation);
            this.tabPageResxTranslationOptions.Controls.Add(this.groupBoxAddOriginalSrcTextToComment);
            this.tabPageResxTranslationOptions.Controls.Add(this.checkBoxDeleteLangResxFilesBeforeTranslation);
            resources.ApplyResources(this.tabPageResxTranslationOptions, "tabPageResxTranslationOptions");
            this.tabPageResxTranslationOptions.Name = "tabPageResxTranslationOptions";
            this.tabPageResxTranslationOptions.UseVisualStyleBackColor = true;
            // 
            // groupBoxBkUpDir
            // 
            resources.ApplyResources(this.groupBoxBkUpDir, "groupBoxBkUpDir");
            this.groupBoxBkUpDir.Controls.Add(this.textBoxBkUpDir);
            this.groupBoxBkUpDir.Controls.Add(this.buttonOpenBackupFolderInFileExplorer);
            this.groupBoxBkUpDir.Controls.Add(this.browseBkUpDirFolderButton);
            this.groupBoxBkUpDir.Controls.Add(this.linkLabelBkUpDir);
            this.groupBoxBkUpDir.Name = "groupBoxBkUpDir";
            this.groupBoxBkUpDir.TabStop = false;
            // 
            // textBoxBkUpDir
            // 
            resources.ApplyResources(this.textBoxBkUpDir, "textBoxBkUpDir");
            this.textBoxBkUpDir.Name = "textBoxBkUpDir";
            // 
            // groupBoxDeleteLangAppendedResxFiles
            // 
            this.groupBoxDeleteLangAppendedResxFiles.Controls.Add(this.buttonInAdvOpt_OpenFolderInWindowsExplorer);
            this.groupBoxDeleteLangAppendedResxFiles.Controls.Add(this.linkLabelDeleteLangAppendedResxFiles);
            this.groupBoxDeleteLangAppendedResxFiles.Controls.Add(this.buttonDeleteLangAppendedResxFiles);
            resources.ApplyResources(this.groupBoxDeleteLangAppendedResxFiles, "groupBoxDeleteLangAppendedResxFiles");
            this.groupBoxDeleteLangAppendedResxFiles.Name = "groupBoxDeleteLangAppendedResxFiles";
            this.groupBoxDeleteLangAppendedResxFiles.TabStop = false;
            // 
            // checkBoxBackupFilesBeforeTranslation
            // 
            resources.ApplyResources(this.checkBoxBackupFilesBeforeTranslation, "checkBoxBackupFilesBeforeTranslation");
            this.checkBoxBackupFilesBeforeTranslation.Checked = true;
            this.checkBoxBackupFilesBeforeTranslation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBackupFilesBeforeTranslation.Name = "checkBoxBackupFilesBeforeTranslation";
            this.checkBoxBackupFilesBeforeTranslation.UseVisualStyleBackColor = true;
            // 
            // tabPageTranslationOptions
            // 
            this.tabPageTranslationOptions.Controls.Add(this.labelMaxThread);
            this.tabPageTranslationOptions.Controls.Add(this.MaxThread);
            this.tabPageTranslationOptions.Controls.Add(this.MaxTranslateLen);
            this.tabPageTranslationOptions.Controls.Add(this.labelMaxTranslateLen);
            this.tabPageTranslationOptions.Controls.Add(this.linkLabelMaxThreads);
            this.tabPageTranslationOptions.Controls.Add(this.linkLabelMaxTranslateLen);
            this.tabPageTranslationOptions.Controls.Add(this.groupBoxItemsPerTransaltionRequest);
            resources.ApplyResources(this.tabPageTranslationOptions, "tabPageTranslationOptions");
            this.tabPageTranslationOptions.Name = "tabPageTranslationOptions";
            this.tabPageTranslationOptions.UseVisualStyleBackColor = true;
            // 
            // labelMaxThread
            // 
            resources.ApplyResources(this.labelMaxThread, "labelMaxThread");
            this.labelMaxThread.Name = "labelMaxThread";
            // 
            // MaxThread
            // 
            resources.ApplyResources(this.MaxThread, "MaxThread");
            this.MaxThread.Name = "MaxThread";
            // 
            // MaxTranslateLen
            // 
            resources.ApplyResources(this.MaxTranslateLen, "MaxTranslateLen");
            this.MaxTranslateLen.Name = "MaxTranslateLen";
            // 
            // labelMaxTranslateLen
            // 
            resources.ApplyResources(this.labelMaxTranslateLen, "labelMaxTranslateLen");
            this.labelMaxTranslateLen.Name = "labelMaxTranslateLen";
            // 
            // tabPageLoggingOptions
            // 
            this.tabPageLoggingOptions.Controls.Add(this.buttonOpenLoggingFolderInFileExplorer);
            this.tabPageLoggingOptions.Controls.Add(this.groupBoxScreenVerbosityLevel);
            this.tabPageLoggingOptions.Controls.Add(this.groupBoxLogFileVerbosityLevel);
            this.tabPageLoggingOptions.Controls.Add(this.textBoxLogFolderPath);
            this.tabPageLoggingOptions.Controls.Add(this.labelLogFolderPath);
            this.tabPageLoggingOptions.Controls.Add(this.linkLabelLogFolderPath);
            this.tabPageLoggingOptions.Controls.Add(this.browseLogFolderPath);
            resources.ApplyResources(this.tabPageLoggingOptions, "tabPageLoggingOptions");
            this.tabPageLoggingOptions.Name = "tabPageLoggingOptions";
            this.tabPageLoggingOptions.UseVisualStyleBackColor = true;
            // 
            // groupBoxScreenVerbosityLevel
            // 
            this.groupBoxScreenVerbosityLevel.Controls.Add(this.comboBoxScreenVerbosityLevel);
            this.groupBoxScreenVerbosityLevel.Controls.Add(this.linkLabelScreenVerbosityLevel);
            resources.ApplyResources(this.groupBoxScreenVerbosityLevel, "groupBoxScreenVerbosityLevel");
            this.groupBoxScreenVerbosityLevel.Name = "groupBoxScreenVerbosityLevel";
            this.groupBoxScreenVerbosityLevel.TabStop = false;
            // 
            // comboBoxScreenVerbosityLevel
            // 
            this.comboBoxScreenVerbosityLevel.FormattingEnabled = true;
            this.comboBoxScreenVerbosityLevel.Items.AddRange(new object[] {
            resources.GetString("comboBoxScreenVerbosityLevel.Items"),
            resources.GetString("comboBoxScreenVerbosityLevel.Items1"),
            resources.GetString("comboBoxScreenVerbosityLevel.Items2"),
            resources.GetString("comboBoxScreenVerbosityLevel.Items3")});
            resources.ApplyResources(this.comboBoxScreenVerbosityLevel, "comboBoxScreenVerbosityLevel");
            this.comboBoxScreenVerbosityLevel.Name = "comboBoxScreenVerbosityLevel";
            // 
            // groupBoxLogFileVerbosityLevel
            // 
            this.groupBoxLogFileVerbosityLevel.Controls.Add(this.comboBoxLogFileVerbosityLevel);
            this.groupBoxLogFileVerbosityLevel.Controls.Add(this.linkLabelLogFileVerbosityLevel);
            resources.ApplyResources(this.groupBoxLogFileVerbosityLevel, "groupBoxLogFileVerbosityLevel");
            this.groupBoxLogFileVerbosityLevel.Name = "groupBoxLogFileVerbosityLevel";
            this.groupBoxLogFileVerbosityLevel.TabStop = false;
            // 
            // comboBoxLogFileVerbosityLevel
            // 
            this.comboBoxLogFileVerbosityLevel.FormattingEnabled = true;
            this.comboBoxLogFileVerbosityLevel.Items.AddRange(new object[] {
            resources.GetString("comboBoxLogFileVerbosityLevel.Items"),
            resources.GetString("comboBoxLogFileVerbosityLevel.Items1"),
            resources.GetString("comboBoxLogFileVerbosityLevel.Items2"),
            resources.GetString("comboBoxLogFileVerbosityLevel.Items3")});
            resources.ApplyResources(this.comboBoxLogFileVerbosityLevel, "comboBoxLogFileVerbosityLevel");
            this.comboBoxLogFileVerbosityLevel.Name = "comboBoxLogFileVerbosityLevel";
            // 
            // textBoxLogFolderPath
            // 
            resources.ApplyResources(this.textBoxLogFolderPath, "textBoxLogFolderPath");
            this.textBoxLogFolderPath.Name = "textBoxLogFolderPath";
            this.textBoxLogFolderPath.TextChanged += new System.EventHandler(this.textBoxLogFolderPath_TextChanged);
            // 
            // labelLogFolderPath
            // 
            resources.ApplyResources(this.labelLogFolderPath, "labelLogFolderPath");
            this.labelLogFolderPath.Name = "labelLogFolderPath";
            // 
            // textTab
            // 
            this.textTab.Controls.Add(this.groupBoxTextBoxTextToTranslate);
            this.textTab.Controls.Add(this.groupBoxTextBoxTranslation);
            this.textTab.Controls.Add(this.textCmdPanel);
            resources.ApplyResources(this.textTab, "textTab");
            this.textTab.Name = "textTab";
            this.textTab.UseVisualStyleBackColor = true;
            // 
            // groupBoxTextBoxTextToTranslate
            // 
            resources.ApplyResources(this.groupBoxTextBoxTextToTranslate, "groupBoxTextBoxTextToTranslate");
            this.groupBoxTextBoxTextToTranslate.Controls.Add(this.textBoxTextToTranslate);
            this.groupBoxTextBoxTextToTranslate.Name = "groupBoxTextBoxTextToTranslate";
            this.groupBoxTextBoxTextToTranslate.TabStop = false;
            // 
            // textBoxTextToTranslate
            // 
            resources.ApplyResources(this.textBoxTextToTranslate, "textBoxTextToTranslate");
            this.textBoxTextToTranslate.Name = "textBoxTextToTranslate";
            this.textBoxTextToTranslate.TextChanged += new System.EventHandler(this.ChangeTransInput);
            // 
            // groupBoxTextBoxTranslation
            // 
            resources.ApplyResources(this.groupBoxTextBoxTranslation, "groupBoxTextBoxTranslation");
            this.groupBoxTextBoxTranslation.Controls.Add(this.textBoxTranslation);
            this.groupBoxTextBoxTranslation.Name = "groupBoxTextBoxTranslation";
            this.groupBoxTextBoxTranslation.TabStop = false;
            // 
            // textBoxTranslation
            // 
            resources.ApplyResources(this.textBoxTranslation, "textBoxTranslation");
            this.textBoxTranslation.Name = "textBoxTranslation";
            this.textBoxTranslation.ReadOnly = true;
            this.textBoxTranslation.TabStop = false;
            // 
            // textCmdPanel
            // 
            resources.ApplyResources(this.textCmdPanel, "textCmdPanel");
            this.textCmdPanel.Controls.Add(this.buttonBrowseForTextFile);
            this.textCmdPanel.Controls.Add(this.buttonTranslateTextInTextBox);
            this.textCmdPanel.Controls.Add(this.toCodeLabel);
            this.textCmdPanel.Controls.Add(this.comboBoxToLang);
            this.textCmdPanel.Controls.Add(this.comboBoxFromLang);
            this.textCmdPanel.Controls.Add(this.fromCodeLabel);
            this.textCmdPanel.Name = "textCmdPanel";
            // 
            // buttonTranslateTextInTextBox
            // 
            resources.ApplyResources(this.buttonTranslateTextInTextBox, "buttonTranslateTextInTextBox");
            this.buttonTranslateTextInTextBox.Image = global::ABetterTranslator.Properties.Resources.Translate;
            this.buttonTranslateTextInTextBox.Name = "buttonTranslateTextInTextBox";
            this.buttonTranslateTextInTextBox.UseVisualStyleBackColor = true;
            this.buttonTranslateTextInTextBox.Click += new System.EventHandler(this.buttonTranslateTextInTextBox_Click);
            // 
            // toCodeLabel
            // 
            resources.ApplyResources(this.toCodeLabel, "toCodeLabel");
            this.toCodeLabel.Name = "toCodeLabel";
            // 
            // comboBoxToLang
            // 
            this.comboBoxToLang.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxToLang, "comboBoxToLang");
            this.comboBoxToLang.Name = "comboBoxToLang";
            this.comboBoxToLang.Sorted = true;
            this.comboBoxToLang.SelectedIndexChanged += new System.EventHandler(this.ChangeTransInput);
            // 
            // comboBoxFromLang
            // 
            this.comboBoxFromLang.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxFromLang, "comboBoxFromLang");
            this.comboBoxFromLang.Name = "comboBoxFromLang";
            this.comboBoxFromLang.Sorted = true;
            this.comboBoxFromLang.SelectedIndexChanged += new System.EventHandler(this.ChangeTransInput);
            // 
            // fromCodeLabel
            // 
            resources.ApplyResources(this.fromCodeLabel, "fromCodeLabel");
            this.fromCodeLabel.Name = "fromCodeLabel";
            // 
            // resxTab
            // 
            this.resxTab.Controls.Add(this.buttonOpenFolderInWindowsExplorer);
            this.resxTab.Controls.Add(this.tabControlTranslateResxSubTab);
            this.resxTab.Controls.Add(this.toolStrip1);
            this.resxTab.Controls.Add(this.browseFolderButton);
            this.resxTab.Controls.Add(this.outputBox);
            this.resxTab.Controls.Add(this.inputBox);
            this.resxTab.Controls.Add(this.outputLabel);
            this.resxTab.Controls.Add(this.browseFileButton);
            this.resxTab.Controls.Add(this.comboBoxFromLanguage);
            this.resxTab.Controls.Add(this.inputLabel);
            resources.ApplyResources(this.resxTab, "resxTab");
            this.resxTab.Name = "resxTab";
            this.resxTab.UseVisualStyleBackColor = true;
            // 
            // tabControlTranslateResxSubTab
            // 
            resources.ApplyResources(this.tabControlTranslateResxSubTab, "tabControlTranslateResxSubTab");
            this.tabControlTranslateResxSubTab.Controls.Add(this.tabPageLanguageList);
            this.tabControlTranslateResxSubTab.Controls.Add(this.tabPageTranslateResxLogging);
            this.tabControlTranslateResxSubTab.Name = "tabControlTranslateResxSubTab";
            this.tabControlTranslateResxSubTab.SelectedIndex = 0;
            // 
            // tabPageLanguageList
            // 
            this.tabPageLanguageList.Controls.Add(this.groupBoxFilterText);
            this.tabPageLanguageList.Controls.Add(this.languageList);
            resources.ApplyResources(this.tabPageLanguageList, "tabPageLanguageList");
            this.tabPageLanguageList.Name = "tabPageLanguageList";
            this.tabPageLanguageList.UseVisualStyleBackColor = true;
            // 
            // groupBoxFilterText
            // 
            resources.ApplyResources(this.groupBoxFilterText, "groupBoxFilterText");
            this.groupBoxFilterText.Controls.Add(this.textBoxFilterText);
            this.groupBoxFilterText.Controls.Add(this.linkLabelFilterText);
            this.groupBoxFilterText.Name = "groupBoxFilterText";
            this.groupBoxFilterText.TabStop = false;
            // 
            // textBoxFilterText
            // 
            resources.ApplyResources(this.textBoxFilterText, "textBoxFilterText");
            this.textBoxFilterText.Name = "textBoxFilterText";
            this.textBoxFilterText.TextChanged += new System.EventHandler(this.textBoxFilterText_TextChanged);
            // 
            // linkLabelFilterText
            // 
            resources.ApplyResources(this.linkLabelFilterText, "linkLabelFilterText");
            this.linkLabelFilterText.Name = "linkLabelFilterText";
            this.linkLabelFilterText.TabStop = true;
            this.linkLabelFilterText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFilterText_LinkClicked);
            // 
            // languageList
            // 
            this.languageList.AllowColumnReorder = true;
            this.languageList.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.languageList, "languageList");
            this.languageList.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.languageList.CellEditEnterChangesRows = true;
            this.languageList.CellEditTabChangesRows = true;
            this.languageList.CheckBoxes = true;
            this.languageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olv_LanguageTag,
            this.olv_LanguageName,
            this.olv_LanguageAliases,
            this.olv_TranslationSupported,
            this.olv_LanguageTranslatorTag,
            this.olv_Iso639_3});
            this.languageList.ColumnsNotEditable = null;
            this.languageList.Name = "languageList";
            this.languageList.ShowCommandMenuOnRightClick = true;
            this.languageList.ShowGroups = false;
            this.languageList.ShowImagesOnSubItems = true;
            this.languageList.ShowItemToolTips = true;
            this.languageList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.languageList.TintSortColumn = true;
            this.languageList.UseAlternatingBackColors = true;
            this.languageList.UseCellFormatEvents = true;
            this.languageList.UseCompatibleStateImageBehavior = false;
            this.languageList.UseFilterIndicator = true;
            this.languageList.UseFiltering = true;
            this.languageList.View = System.Windows.Forms.View.Details;
            this.languageList.VirtualMode = true;
            // 
            // olv_LanguageTag
            // 
            this.olv_LanguageTag.AspectName = "LanguageTag";
            this.olv_LanguageTag.Hideable = false;
            this.olv_LanguageTag.IsEditable = false;
            resources.ApplyResources(this.olv_LanguageTag, "olv_LanguageTag");
            // 
            // olv_LanguageName
            // 
            this.olv_LanguageName.AspectName = "LanguageName";
            this.olv_LanguageName.Hideable = false;
            this.olv_LanguageName.IsEditable = false;
            resources.ApplyResources(this.olv_LanguageName, "olv_LanguageName");
            // 
            // olv_LanguageAliases
            // 
            this.olv_LanguageAliases.AspectName = "LanguageAliases";
            this.olv_LanguageAliases.IsEditable = false;
            resources.ApplyResources(this.olv_LanguageAliases, "olv_LanguageAliases");
            // 
            // olv_TranslationSupported
            // 
            this.olv_TranslationSupported.AspectName = "TranslatorSupported";
            this.olv_TranslationSupported.IsEditable = false;
            resources.ApplyResources(this.olv_TranslationSupported, "olv_TranslationSupported");
            // 
            // olv_LanguageTranslatorTag
            // 
            this.olv_LanguageTranslatorTag.AspectName = "LanguageTranslatorTag";
            this.olv_LanguageTranslatorTag.IsEditable = false;
            resources.ApplyResources(this.olv_LanguageTranslatorTag, "olv_LanguageTranslatorTag");
            // 
            // olv_Iso639_3
            // 
            this.olv_Iso639_3.AspectName = "Iso639_3";
            this.olv_Iso639_3.IsEditable = false;
            resources.ApplyResources(this.olv_Iso639_3, "olv_Iso639_3");
            // 
            // tabPageTranslateResxLogging
            // 
            this.tabPageTranslateResxLogging.Controls.Add(this.logBox);
            this.tabPageTranslateResxLogging.Controls.Add(this.statusLabel);
            this.tabPageTranslateResxLogging.Controls.Add(this.progressBarWhileTranslatingResxFile);
            resources.ApplyResources(this.tabPageTranslateResxLogging, "tabPageTranslateResxLogging");
            this.tabPageTranslateResxLogging.Name = "tabPageTranslateResxLogging";
            this.tabPageTranslateResxLogging.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            resources.ApplyResources(this.logBox, "logBox");
            this.logBox.Name = "logBox";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonTranslate,
            this.toolStripSplitButtonSelectAllLanugages,
            this.toolStripSplitButtonResetDisplayToSet,
            this.toolStripButtonStopTranslation});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripSplitButtonTranslate
            // 
            this.toolStripSplitButtonTranslate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTranslateAllLanguages,
            this.toolStripMenuItemWindowsSupportedLanguages,
            this.toolStripMenuItemWindowsLanguageInterfacePacks,
            this.toolStripMenuItemTranslateBusinessLanguages,
            this.toolStripSeparator4,
            this.toolStripMenuItem_TranslateAfrica,
            this.toolStripMenuItem_TranslateAsia,
            this.toolStripMenuItem_TranslateEurope,
            this.toolStripMenuItem_TranslateMiddleEast,
            this.toolStripMenuItem_TranslateNorthAmerica,
            this.toolStripMenuItem_TranslateSouthAmerica,
            this.toolStripSeparator5,
            this.toolStripMenuItemTranslateTop5,
            this.toolStripMenuItemTranslateTop10,
            this.toolStripMenuItemTranslateTop20,
            this.toolStripMenuItemTranslateTop30});
            this.toolStripSplitButtonTranslate.Image = global::ABetterTranslator.Properties.Resources.PlayButton__64_x_64_;
            resources.ApplyResources(this.toolStripSplitButtonTranslate, "toolStripSplitButtonTranslate");
            this.toolStripSplitButtonTranslate.Name = "toolStripSplitButtonTranslate";
            this.toolStripSplitButtonTranslate.ButtonClick += new System.EventHandler(this.toolStripSplitButtonTranslate_ButtonClick);
            // 
            // toolStripMenuItemTranslateAllLanguages
            // 
            this.toolStripMenuItemTranslateAllLanguages.Image = global::ABetterTranslator.Properties.Resources.TranslateWorld__64_x_64_;
            this.toolStripMenuItemTranslateAllLanguages.Name = "toolStripMenuItemTranslateAllLanguages";
            resources.ApplyResources(this.toolStripMenuItemTranslateAllLanguages, "toolStripMenuItemTranslateAllLanguages");
            this.toolStripMenuItemTranslateAllLanguages.Click += new System.EventHandler(this.toolStripMenuItemTranslateAllLanguages_Click);
            // 
            // toolStripMenuItemWindowsSupportedLanguages
            // 
            this.toolStripMenuItemWindowsSupportedLanguages.Image = global::ABetterTranslator.Properties.Resources.Windows_icon_64x64_;
            this.toolStripMenuItemWindowsSupportedLanguages.Name = "toolStripMenuItemWindowsSupportedLanguages";
            resources.ApplyResources(this.toolStripMenuItemWindowsSupportedLanguages, "toolStripMenuItemWindowsSupportedLanguages");
            this.toolStripMenuItemWindowsSupportedLanguages.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItemWindowsLanguageInterfacePacks
            // 
            this.toolStripMenuItemWindowsLanguageInterfacePacks.Image = global::ABetterTranslator.Properties.Resources.WindowsLogoInCircle__64_x_64_;
            this.toolStripMenuItemWindowsLanguageInterfacePacks.Name = "toolStripMenuItemWindowsLanguageInterfacePacks";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguageInterfacePacks, "toolStripMenuItemWindowsLanguageInterfacePacks");
            this.toolStripMenuItemWindowsLanguageInterfacePacks.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItemTranslateBusinessLanguages
            // 
            this.toolStripMenuItemTranslateBusinessLanguages.Image = global::ABetterTranslator.Properties.Resources.BusinessTranslation__64_x_64_;
            this.toolStripMenuItemTranslateBusinessLanguages.Name = "toolStripMenuItemTranslateBusinessLanguages";
            resources.ApplyResources(this.toolStripMenuItemTranslateBusinessLanguages, "toolStripMenuItemTranslateBusinessLanguages");
            this.toolStripMenuItemTranslateBusinessLanguages.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripMenuItem_TranslateAfrica
            // 
            this.toolStripMenuItem_TranslateAfrica.Image = global::ABetterTranslator.Properties.Resources.Africa_64;
            this.toolStripMenuItem_TranslateAfrica.Name = "toolStripMenuItem_TranslateAfrica";
            resources.ApplyResources(this.toolStripMenuItem_TranslateAfrica, "toolStripMenuItem_TranslateAfrica");
            this.toolStripMenuItem_TranslateAfrica.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItem_TranslateAsia
            // 
            this.toolStripMenuItem_TranslateAsia.Image = global::ABetterTranslator.Properties.Resources.Asia_64;
            this.toolStripMenuItem_TranslateAsia.Name = "toolStripMenuItem_TranslateAsia";
            resources.ApplyResources(this.toolStripMenuItem_TranslateAsia, "toolStripMenuItem_TranslateAsia");
            this.toolStripMenuItem_TranslateAsia.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItem_TranslateEurope
            // 
            this.toolStripMenuItem_TranslateEurope.Image = global::ABetterTranslator.Properties.Resources.EuropeBlue__64_x_64_;
            this.toolStripMenuItem_TranslateEurope.Name = "toolStripMenuItem_TranslateEurope";
            resources.ApplyResources(this.toolStripMenuItem_TranslateEurope, "toolStripMenuItem_TranslateEurope");
            this.toolStripMenuItem_TranslateEurope.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItem_TranslateMiddleEast
            // 
            this.toolStripMenuItem_TranslateMiddleEast.Image = global::ABetterTranslator.Properties.Resources.MiddleEast__64_x_64_;
            this.toolStripMenuItem_TranslateMiddleEast.Name = "toolStripMenuItem_TranslateMiddleEast";
            resources.ApplyResources(this.toolStripMenuItem_TranslateMiddleEast, "toolStripMenuItem_TranslateMiddleEast");
            this.toolStripMenuItem_TranslateMiddleEast.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItem_TranslateNorthAmerica
            // 
            this.toolStripMenuItem_TranslateNorthAmerica.Image = global::ABetterTranslator.Properties.Resources.NorthAmericaWithUS_HighLight__64_x_64_;
            this.toolStripMenuItem_TranslateNorthAmerica.Name = "toolStripMenuItem_TranslateNorthAmerica";
            resources.ApplyResources(this.toolStripMenuItem_TranslateNorthAmerica, "toolStripMenuItem_TranslateNorthAmerica");
            this.toolStripMenuItem_TranslateNorthAmerica.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItem_TranslateSouthAmerica
            // 
            this.toolStripMenuItem_TranslateSouthAmerica.Image = global::ABetterTranslator.Properties.Resources.SouthAmericaInColor__64_x_64_;
            this.toolStripMenuItem_TranslateSouthAmerica.Name = "toolStripMenuItem_TranslateSouthAmerica";
            resources.ApplyResources(this.toolStripMenuItem_TranslateSouthAmerica, "toolStripMenuItem_TranslateSouthAmerica");
            this.toolStripMenuItem_TranslateSouthAmerica.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripMenuItemTranslateTop5
            // 
            this.toolStripMenuItemTranslateTop5.Image = global::ABetterTranslator.Properties.Resources.Translation_Top5__64_x_64_;
            this.toolStripMenuItemTranslateTop5.Name = "toolStripMenuItemTranslateTop5";
            resources.ApplyResources(this.toolStripMenuItemTranslateTop5, "toolStripMenuItemTranslateTop5");
            this.toolStripMenuItemTranslateTop5.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItemTranslateTop10
            // 
            this.toolStripMenuItemTranslateTop10.Image = global::ABetterTranslator.Properties.Resources.Translation_Top10__64_x_64_;
            this.toolStripMenuItemTranslateTop10.Name = "toolStripMenuItemTranslateTop10";
            resources.ApplyResources(this.toolStripMenuItemTranslateTop10, "toolStripMenuItemTranslateTop10");
            this.toolStripMenuItemTranslateTop10.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItemTranslateTop20
            // 
            this.toolStripMenuItemTranslateTop20.Image = global::ABetterTranslator.Properties.Resources.Translation_Top20__64_x_64_;
            this.toolStripMenuItemTranslateTop20.Name = "toolStripMenuItemTranslateTop20";
            resources.ApplyResources(this.toolStripMenuItemTranslateTop20, "toolStripMenuItemTranslateTop20");
            this.toolStripMenuItemTranslateTop20.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripMenuItemTranslateTop30
            // 
            this.toolStripMenuItemTranslateTop30.Image = global::ABetterTranslator.Properties.Resources.Translation_Top30__64_x_64_;
            this.toolStripMenuItemTranslateTop30.Name = "toolStripMenuItemTranslateTop30";
            resources.ApplyResources(this.toolStripMenuItemTranslateTop30, "toolStripMenuItemTranslateTop30");
            this.toolStripMenuItemTranslateTop30.Click += new System.EventHandler(this.SelectAndTranslateLanguages);
            // 
            // toolStripSplitButtonSelectAllLanugages
            // 
            this.toolStripSplitButtonSelectAllLanugages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSelectAll,
            this.toolStripMenuItemSelectClear,
            this.toolStripMenuItemSelectUndo});
            this.toolStripSplitButtonSelectAllLanugages.Image = global::ABetterTranslator.Properties.Resources.Checkbox_3D__64_x_64_;
            resources.ApplyResources(this.toolStripSplitButtonSelectAllLanugages, "toolStripSplitButtonSelectAllLanugages");
            this.toolStripSplitButtonSelectAllLanugages.Name = "toolStripSplitButtonSelectAllLanugages";
            this.toolStripSplitButtonSelectAllLanugages.ButtonClick += new System.EventHandler(this.toolStripSplitButtonSelectAllLanugages_ButtonClick);
            // 
            // toolStripMenuItemSelectAll
            // 
            this.toolStripMenuItemSelectAll.Image = global::ABetterTranslator.Properties.Resources.TranslateWorld4__64_x_64_;
            this.toolStripMenuItemSelectAll.Name = "toolStripMenuItemSelectAll";
            resources.ApplyResources(this.toolStripMenuItemSelectAll, "toolStripMenuItemSelectAll");
            this.toolStripMenuItemSelectAll.Click += new System.EventHandler(this.toolStripMenuItemSelectAll_Click);
            // 
            // toolStripMenuItemSelectClear
            // 
            this.toolStripMenuItemSelectClear.Image = global::ABetterTranslator.Properties.Resources.erase;
            this.toolStripMenuItemSelectClear.Name = "toolStripMenuItemSelectClear";
            resources.ApplyResources(this.toolStripMenuItemSelectClear, "toolStripMenuItemSelectClear");
            this.toolStripMenuItemSelectClear.Click += new System.EventHandler(this.toolStripMenuItemSelectClear_Click);
            // 
            // toolStripMenuItemSelectUndo
            // 
            resources.ApplyResources(this.toolStripMenuItemSelectUndo, "toolStripMenuItemSelectUndo");
            this.toolStripMenuItemSelectUndo.Image = global::ABetterTranslator.Properties.Resources.ChecklistPaper__64_x_64_;
            this.toolStripMenuItemSelectUndo.Name = "toolStripMenuItemSelectUndo";
            this.toolStripMenuItemSelectUndo.Click += new System.EventHandler(this.toolStripMenuItemSelectUndo_Click);
            // 
            // toolStripSplitButtonResetDisplayToSet
            // 
            this.toolStripSplitButtonResetDisplayToSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWindowsLanguagePacks,
            this.toolStripMenuItemWindowsLanguagePackInterface,
            this.toolStripSeparator7,
            this.toolStripMenuItemAllTranslatorSupportedLanguages,
            this.toolStripMenuItemISO639_1UsingOfficialNames,
            this.toolStripMenuItemISO639_1PlusWinLangPack,
            this.toolStripMenuItemSubLanguagesAndRegions});
            this.toolStripSplitButtonResetDisplayToSet.Image = global::ABetterTranslator.Properties.Resources.ISO_InWorld__64_x_64_;
            resources.ApplyResources(this.toolStripSplitButtonResetDisplayToSet, "toolStripSplitButtonResetDisplayToSet");
            this.toolStripSplitButtonResetDisplayToSet.Name = "toolStripSplitButtonResetDisplayToSet";
            this.toolStripSplitButtonResetDisplayToSet.ButtonClick += new System.EventHandler(this.toolStripSplitButtonDisplayLanguageSet_ButtonClick);
            // 
            // toolStripMenuItemWindowsLanguagePacks
            // 
            this.toolStripMenuItemWindowsLanguagePacks.Checked = true;
            this.toolStripMenuItemWindowsLanguagePacks.CheckOnClick = true;
            this.toolStripMenuItemWindowsLanguagePacks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemWindowsLanguagePacks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWindowsLanguagePacks_ShowOnly,
            this.toolStripMenuItemWindowsLanguagePacks_Add,
            this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect});
            this.toolStripMenuItemWindowsLanguagePacks.Image = global::ABetterTranslator.Properties.Resources.Windows_icon_64x64_;
            this.toolStripMenuItemWindowsLanguagePacks.Name = "toolStripMenuItemWindowsLanguagePacks";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePacks, "toolStripMenuItemWindowsLanguagePacks");
            this.toolStripMenuItemWindowsLanguagePacks.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePacks_Click);
            // 
            // toolStripMenuItemWindowsLanguagePacks_ShowOnly
            // 
            this.toolStripMenuItemWindowsLanguagePacks_ShowOnly.Name = "toolStripMenuItemWindowsLanguagePacks_ShowOnly";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePacks_ShowOnly, "toolStripMenuItemWindowsLanguagePacks_ShowOnly");
            this.toolStripMenuItemWindowsLanguagePacks_ShowOnly.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePacks_ShowOnly_Click);
            // 
            // toolStripMenuItemWindowsLanguagePacks_Add
            // 
            this.toolStripMenuItemWindowsLanguagePacks_Add.Name = "toolStripMenuItemWindowsLanguagePacks_Add";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePacks_Add, "toolStripMenuItemWindowsLanguagePacks_Add");
            this.toolStripMenuItemWindowsLanguagePacks_Add.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePacks_Add_Click);
            // 
            // toolStripMenuItemWindowsLanguagePacks_AddAndSelect
            // 
            this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect.Name = "toolStripMenuItemWindowsLanguagePacks_AddAndSelect";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect, "toolStripMenuItemWindowsLanguagePacks_AddAndSelect");
            this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePacks_AddAndSelect_Click);
            // 
            // toolStripMenuItemWindowsLanguagePackInterface
            // 
            this.toolStripMenuItemWindowsLanguagePackInterface.Checked = true;
            this.toolStripMenuItemWindowsLanguagePackInterface.CheckOnClick = true;
            this.toolStripMenuItemWindowsLanguagePackInterface.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemWindowsLanguagePackInterface.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly,
            this.toolStripMenuItemWindowsLanguagePackInterface_Add,
            this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect});
            this.toolStripMenuItemWindowsLanguagePackInterface.Image = global::ABetterTranslator.Properties.Resources.WindowsLogoInCircle__64_x_64_;
            this.toolStripMenuItemWindowsLanguagePackInterface.Name = "toolStripMenuItemWindowsLanguagePackInterface";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePackInterface, "toolStripMenuItemWindowsLanguagePackInterface");
            this.toolStripMenuItemWindowsLanguagePackInterface.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePackInterface_Click);
            // 
            // toolStripMenuItemWindowsLanguagePackInterface_ShowOnly
            // 
            this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly.Name = "toolStripMenuItemWindowsLanguagePackInterface_ShowOnly";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly, "toolStripMenuItemWindowsLanguagePackInterface_ShowOnly");
            this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePackInterface_ShowOnly_Click);
            // 
            // toolStripMenuItemWindowsLanguagePackInterface_Add
            // 
            this.toolStripMenuItemWindowsLanguagePackInterface_Add.Name = "toolStripMenuItemWindowsLanguagePackInterface_Add";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePackInterface_Add, "toolStripMenuItemWindowsLanguagePackInterface_Add");
            this.toolStripMenuItemWindowsLanguagePackInterface_Add.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePackInterface_Add_Click);
            // 
            // toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect
            // 
            this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect.Name = "toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect";
            resources.ApplyResources(this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect, "toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect");
            this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect.Click += new System.EventHandler(this.toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // toolStripMenuItemAllTranslatorSupportedLanguages
            // 
            this.toolStripMenuItemAllTranslatorSupportedLanguages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly,
            this.toolStripMenuItemAllTranslatorSupportedLanguages_Add});
            this.toolStripMenuItemAllTranslatorSupportedLanguages.Image = global::ABetterTranslator.Properties.Resources.TranslateWhiteBlueAndRed__64_x_64_;
            this.toolStripMenuItemAllTranslatorSupportedLanguages.Name = "toolStripMenuItemAllTranslatorSupportedLanguages";
            resources.ApplyResources(this.toolStripMenuItemAllTranslatorSupportedLanguages, "toolStripMenuItemAllTranslatorSupportedLanguages");
            this.toolStripMenuItemAllTranslatorSupportedLanguages.Click += new System.EventHandler(this.toolStripMenuItemAllTranslatorSupportedLanguages_Click);
            // 
            // toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly
            // 
            this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly.Name = "toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly";
            resources.ApplyResources(this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly, "toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly");
            this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly.Click += new System.EventHandler(this.toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly_Click);
            // 
            // toolStripMenuItemAllTranslatorSupportedLanguages_Add
            // 
            this.toolStripMenuItemAllTranslatorSupportedLanguages_Add.Name = "toolStripMenuItemAllTranslatorSupportedLanguages_Add";
            resources.ApplyResources(this.toolStripMenuItemAllTranslatorSupportedLanguages_Add, "toolStripMenuItemAllTranslatorSupportedLanguages_Add");
            this.toolStripMenuItemAllTranslatorSupportedLanguages_Add.Click += new System.EventHandler(this.toolStripMenuItemAllTranslatorSupportedLanguages_Add_Click);
            // 
            // toolStripMenuItemISO639_1UsingOfficialNames
            // 
            this.toolStripMenuItemISO639_1UsingOfficialNames.Checked = true;
            this.toolStripMenuItemISO639_1UsingOfficialNames.CheckOnClick = true;
            this.toolStripMenuItemISO639_1UsingOfficialNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemISO639_1UsingOfficialNames.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly,
            this.toolStripMenuItemISO639_1UsingOfficialNames_Add});
            this.toolStripMenuItemISO639_1UsingOfficialNames.Image = global::ABetterTranslator.Properties.Resources.ISO_InWorld__64_x_64_;
            this.toolStripMenuItemISO639_1UsingOfficialNames.Name = "toolStripMenuItemISO639_1UsingOfficialNames";
            resources.ApplyResources(this.toolStripMenuItemISO639_1UsingOfficialNames, "toolStripMenuItemISO639_1UsingOfficialNames");
            this.toolStripMenuItemISO639_1UsingOfficialNames.Click += new System.EventHandler(this.toolStripMenuItemISO639_1UsingOfficialNames_Click);
            // 
            // toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly
            // 
            this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly.Name = "toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly";
            resources.ApplyResources(this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly, "toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly");
            this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly.Click += new System.EventHandler(this.toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly_Click);
            // 
            // toolStripMenuItemISO639_1UsingOfficialNames_Add
            // 
            this.toolStripMenuItemISO639_1UsingOfficialNames_Add.Name = "toolStripMenuItemISO639_1UsingOfficialNames_Add";
            resources.ApplyResources(this.toolStripMenuItemISO639_1UsingOfficialNames_Add, "toolStripMenuItemISO639_1UsingOfficialNames_Add");
            this.toolStripMenuItemISO639_1UsingOfficialNames_Add.Click += new System.EventHandler(this.toolStripMenuItemISO639_1UsingOfficialNames_Add_Click);
            // 
            // toolStripMenuItemISO639_1PlusWinLangPack
            // 
            this.toolStripMenuItemISO639_1PlusWinLangPack.Checked = true;
            this.toolStripMenuItemISO639_1PlusWinLangPack.CheckOnClick = true;
            this.toolStripMenuItemISO639_1PlusWinLangPack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemISO639_1PlusWinLangPack.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly,
            this.toolStripMenuItemISO639_1PlusWinLangPack_Add});
            resources.ApplyResources(this.toolStripMenuItemISO639_1PlusWinLangPack, "toolStripMenuItemISO639_1PlusWinLangPack");
            this.toolStripMenuItemISO639_1PlusWinLangPack.Image = global::ABetterTranslator.Properties.Resources.WindowsAnd_ISO_Languages__64_x_64_;
            this.toolStripMenuItemISO639_1PlusWinLangPack.Name = "toolStripMenuItemISO639_1PlusWinLangPack";
            this.toolStripMenuItemISO639_1PlusWinLangPack.Click += new System.EventHandler(this.toolStripMenuItemISO639_1PlusWinLangPack_Click);
            // 
            // toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly
            // 
            this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly.Name = "toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly";
            resources.ApplyResources(this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly, "toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly");
            this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly.Click += new System.EventHandler(this.toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly_Click);
            // 
            // toolStripMenuItemISO639_1PlusWinLangPack_Add
            // 
            this.toolStripMenuItemISO639_1PlusWinLangPack_Add.Name = "toolStripMenuItemISO639_1PlusWinLangPack_Add";
            resources.ApplyResources(this.toolStripMenuItemISO639_1PlusWinLangPack_Add, "toolStripMenuItemISO639_1PlusWinLangPack_Add");
            this.toolStripMenuItemISO639_1PlusWinLangPack_Add.Click += new System.EventHandler(this.toolStripMenuItemISO639_1PlusWinLangPack_Add_Click);
            // 
            // toolStripMenuItemSubLanguagesAndRegions
            // 
            resources.ApplyResources(this.toolStripMenuItemSubLanguagesAndRegions, "toolStripMenuItemSubLanguagesAndRegions");
            this.toolStripMenuItemSubLanguagesAndRegions.Image = global::ABetterTranslator.Properties.Resources.TranslateWorld3__64_x_64_;
            this.toolStripMenuItemSubLanguagesAndRegions.Name = "toolStripMenuItemSubLanguagesAndRegions";
            // 
            // toolStripButtonStopTranslation
            // 
            this.toolStripButtonStopTranslation.Image = global::ABetterTranslator.Properties.Resources.StopButton__64_x_64_;
            resources.ApplyResources(this.toolStripButtonStopTranslation, "toolStripButtonStopTranslation");
            this.toolStripButtonStopTranslation.Name = "toolStripButtonStopTranslation";
            this.toolStripButtonStopTranslation.Click += new System.EventHandler(this.toolStripButtonStopTranslation_Click);
            // 
            // outputBox
            // 
            resources.ApplyResources(this.outputBox, "outputBox");
            this.outputBox.Name = "outputBox";
            this.outputBox.TextChanged += new System.EventHandler(this.ChangedResxInput);
            // 
            // inputBox
            // 
            resources.ApplyResources(this.inputBox, "inputBox");
            this.inputBox.Name = "inputBox";
            this.inputBox.TextChanged += new System.EventHandler(this.ChangedResxInput);
            // 
            // outputLabel
            // 
            resources.ApplyResources(this.outputLabel, "outputLabel");
            this.outputLabel.Name = "outputLabel";
            // 
            // comboBoxFromLanguage
            // 
            resources.ApplyResources(this.comboBoxFromLanguage, "comboBoxFromLanguage");
            this.comboBoxFromLanguage.DropDownWidth = 230;
            this.comboBoxFromLanguage.FormattingEnabled = true;
            this.comboBoxFromLanguage.Name = "comboBoxFromLanguage";
            this.comboBoxFromLanguage.Sorted = true;
            // 
            // inputLabel
            // 
            resources.ApplyResources(this.inputLabel, "inputLabel");
            this.inputLabel.Name = "inputLabel";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this.miniToolStrip, "miniToolStrip");
            this.miniToolStrip.Name = "miniToolStrip";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.resxTab);
            this.tabs.Controls.Add(this.textTab);
            this.tabs.Controls.Add(this.advanceOptionsTab);
            resources.ApplyResources(this.tabs, "tabs");
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            // 
            // openFileDialogTextFile
            // 
            this.openFileDialogTextFile.DefaultExt = "txt";
            this.openFileDialogTextFile.FileName = "*.*";
            resources.ApplyResources(this.openFileDialogTextFile, "openFileDialogTextFile");
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabs);
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBoxItemsPerTransaltionRequest.ResumeLayout(false);
            this.groupBoxItemsPerTransaltionRequest.PerformLayout();
            this.groupBoxAddOriginalSrcTextToComment.ResumeLayout(false);
            this.groupBoxAddOriginalSrcTextToComment.PerformLayout();
            this.groupBoxDefaultLanguageSet.ResumeLayout(false);
            this.groupBoxDefaultLanguageSet.PerformLayout();
            this.advanceOptionsTab.ResumeLayout(false);
            this.advanceOptionsTab.PerformLayout();
            this.tabControlAdvanceOptionsSubTab.ResumeLayout(false);
            this.tabPageResxTranslationOptions.ResumeLayout(false);
            this.tabPageResxTranslationOptions.PerformLayout();
            this.groupBoxBkUpDir.ResumeLayout(false);
            this.groupBoxBkUpDir.PerformLayout();
            this.groupBoxDeleteLangAppendedResxFiles.ResumeLayout(false);
            this.groupBoxDeleteLangAppendedResxFiles.PerformLayout();
            this.tabPageTranslationOptions.ResumeLayout(false);
            this.tabPageTranslationOptions.PerformLayout();
            this.tabPageLoggingOptions.ResumeLayout(false);
            this.tabPageLoggingOptions.PerformLayout();
            this.groupBoxScreenVerbosityLevel.ResumeLayout(false);
            this.groupBoxScreenVerbosityLevel.PerformLayout();
            this.groupBoxLogFileVerbosityLevel.ResumeLayout(false);
            this.groupBoxLogFileVerbosityLevel.PerformLayout();
            this.textTab.ResumeLayout(false);
            this.groupBoxTextBoxTextToTranslate.ResumeLayout(false);
            this.groupBoxTextBoxTextToTranslate.PerformLayout();
            this.groupBoxTextBoxTranslation.ResumeLayout(false);
            this.textCmdPanel.ResumeLayout(false);
            this.textCmdPanel.PerformLayout();
            this.resxTab.ResumeLayout(false);
            this.resxTab.PerformLayout();
            this.tabControlTranslateResxSubTab.ResumeLayout(false);
            this.tabPageLanguageList.ResumeLayout(false);
            this.groupBoxFilterText.ResumeLayout(false);
            this.groupBoxFilterText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.languageList)).EndInit();
            this.tabPageTranslateResxLogging.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.OpenFileDialog openResxFileDialog;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ToolTip toolTip;
        private TabPage advanceOptionsTab;
        private TabControl tabControlAdvanceOptionsSubTab;
        private TabPage tabPageResxTranslationOptions;
        private Button buttonOpenBackupFolderInFileExplorer;
        private GroupBox groupBoxDeleteLangAppendedResxFiles;
        private Button buttonInAdvOpt_OpenFolderInWindowsExplorer;
        private LinkLabel linkLabelDeleteLangAppendedResxFiles;
        private Button buttonDeleteLangAppendedResxFiles;
        private GroupBox groupBoxDefaultLanguageSet;
        private ComboBox comboBoxDefaultLanguageSet;
        private LinkLabel linkLabelDefaultLanguageSet;
        private LinkLabel linkLabelBkUpDir;
        private Button browseBkUpDirFolderButton;
        private CheckBox checkBoxBackupFilesBeforeTranslation;
        private TextBox textBoxBkUpDir;
        private LinkLabel linkLabelBackupFilesBeforeTranslation;
        private LinkLabel linkLabelDeleteLangResxFilesBeforeTranslation;
        private GroupBox groupBoxAddOriginalSrcTextToComment;
        private LinkLabel linkLabelAddOriginalSrcTextToComment;
        private ComboBox comboBoxAddOriginalSrcTextToComment;
        private CheckBox checkBoxDeleteLangResxFilesBeforeTranslation;
        private TabPage tabPageTranslationOptions;
        private Label labelMaxThread;
        private TextBox MaxThread;
        private TextBox MaxTranslateLen;
        private Label labelMaxTranslateLen;
        private LinkLabel linkLabelMaxThreads;
        private LinkLabel linkLabelMaxTranslateLen;
        private GroupBox groupBoxItemsPerTransaltionRequest;
        private LinkLabel linkLabelItemsPerTransaltionRequest;
        private ComboBox comboBoxItemsPerTransaltionRequest;
        private TabPage tabPageLoggingOptions;
        private Button buttonOpenLoggingFolderInFileExplorer;
        private GroupBox groupBoxScreenVerbosityLevel;
        private ComboBox comboBoxScreenVerbosityLevel;
        private LinkLabel linkLabelScreenVerbosityLevel;
        private GroupBox groupBoxLogFileVerbosityLevel;
        private ComboBox comboBoxLogFileVerbosityLevel;
        private LinkLabel linkLabelLogFileVerbosityLevel;
        private TextBox textBoxLogFolderPath;
        private Label labelLogFolderPath;
        private LinkLabel linkLabelLogFolderPath;
        private Button browseLogFolderPath;
        private LinkLabel linkLabelcheckBoxDispalyWarningPrompts;
        private CheckBox checkBoxDispalyWarningPrompts;
        private TabPage textTab;
        private GroupBox groupBoxTextBoxTextToTranslate;
        private TextBox textBoxTextToTranslate;
        private GroupBox groupBoxTextBoxTranslation;
        private RichTextBox textBoxTranslation;
        private Panel textCmdPanel;
        private Button buttonTranslateTextInTextBox;
        private Label toCodeLabel;
        private ComboBox comboBoxToLang;
        private ComboBox comboBoxFromLang;
        private Label fromCodeLabel;
        private TabPage resxTab;
        private Button buttonOpenFolderInWindowsExplorer;
        private TabControl tabControlTranslateResxSubTab;
        private TabPage tabPageLanguageList;
        private LinkLabel linkLabelFilterText;
        private TextBox textBoxFilterText;
        private FastObjectListView languageList;
        private OLVColumn olv_LanguageTag;
        private OLVColumn olv_LanguageName;
        private OLVColumn olv_LanguageAliases;
        private OLVColumn olv_TranslationSupported;
        private OLVColumn olv_LanguageTranslatorTag;
        private OLVColumn olv_Iso639_3;
        private TabPage tabPageTranslateResxLogging;
        private RichTextBox logBox;
        private Label statusLabel;
        private ProgressBar progressBarWhileTranslatingResxFile;
        private ToolStrip toolStrip1;
        private ToolStripSplitButton toolStripSplitButtonTranslate;
        private ToolStripMenuItem toolStripMenuItemTranslateAllLanguages;
        private ToolStripMenuItem toolStripMenuItemWindowsSupportedLanguages;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguageInterfacePacks;
        private ToolStripMenuItem toolStripMenuItemTranslateBusinessLanguages;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolStripMenuItem_TranslateAfrica;
        private ToolStripMenuItem toolStripMenuItem_TranslateAsia;
        private ToolStripMenuItem toolStripMenuItem_TranslateEurope;
        private ToolStripMenuItem toolStripMenuItem_TranslateMiddleEast;
        private ToolStripMenuItem toolStripMenuItem_TranslateNorthAmerica;
        private ToolStripMenuItem toolStripMenuItem_TranslateSouthAmerica;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toolStripMenuItemTranslateTop5;
        private ToolStripMenuItem toolStripMenuItemTranslateTop10;
        private ToolStripMenuItem toolStripMenuItemTranslateTop20;
        private ToolStripMenuItem toolStripMenuItemTranslateTop30;
        private ToolStripSplitButton toolStripSplitButtonSelectAllLanugages;
        private ToolStripMenuItem toolStripMenuItemSelectAll;
        private ToolStripMenuItem toolStripMenuItemSelectClear;
        private ToolStripMenuItem toolStripMenuItemSelectUndo;
        private ToolStripSplitButton toolStripSplitButtonResetDisplayToSet;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePacks;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePacks_ShowOnly;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePacks_Add;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePacks_AddAndSelect;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePackInterface;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePackInterface_ShowOnly;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePackInterface_Add;
        private ToolStripMenuItem toolStripMenuItemWindowsLanguagePackInterface_AddAndSelect;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem toolStripMenuItemAllTranslatorSupportedLanguages;
        private ToolStripMenuItem toolStripMenuItemAllTranslatorSupportedLanguages_ShowOnly;
        private ToolStripMenuItem toolStripMenuItemAllTranslatorSupportedLanguages_Add;
        private ToolStripMenuItem toolStripMenuItemISO639_1UsingOfficialNames;
        private ToolStripMenuItem toolStripMenuItemISO639_1UsingOfficialNames_ShowOnly;
        private ToolStripMenuItem toolStripMenuItemISO639_1UsingOfficialNames_Add;
        private ToolStripMenuItem toolStripMenuItemISO639_1PlusWinLangPack;
        private ToolStripMenuItem toolStripMenuItem2toolStripMenuItemISO639_1PlusWinLangPack_ShowOnly;
        private ToolStripMenuItem toolStripMenuItemISO639_1PlusWinLangPack_Add;
        private ToolStripMenuItem toolStripMenuItemSubLanguagesAndRegions;
        private ToolStripButton toolStripButtonStopTranslation;
        private Button browseFolderButton;
        private TextBox outputBox;
        private TextBox inputBox;
        private Label outputLabel;
        private Button browseFileButton;
        private ComboBox comboBoxFromLanguage;
        private Label inputLabel;
        private ToolStrip miniToolStrip;
        private TabControl tabs;
        private OpenFileDialog openFileDialogTextFile;
        private Button buttonBrowseForTextFile;
        private GroupBox groupBoxBkUpDir;
        private GroupBox groupBoxFilterText;
    }
}


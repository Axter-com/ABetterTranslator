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
    using static System.Net.Mime.MediaTypeNames;

    public partial class MainWindow : Form
    {
        // Tab [Translate Text] implementation
        private async void TranslateTextInTextBox(object sender, EventArgs e)
        {
            textBoxTranslation.Clear();
            textBoxTranslation.ForeColor = Color.Black;
            string fromLangTag = GetTag(comboBoxFromLang.Text);
            string toLangTag = GetTag(comboBoxToLang.Text);
            _translationRequestPackets = new List<TranslationRequestPacket>();
            TranslationRequestPacket translationRequestPacket = new()
            {
                fromLang = fromLangTag,
                toLang = toLangTag,
                translateAsSingleDocument = true
            };
            translationRequestPacket.sourceText.Add(textBoxTextToTranslate.Text);
            _translationRequestPackets.Add(translationRequestPacket);

            MultiThreadTranslator_Options options = new();
            options._progressBarOutput = false;
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
                    options._silent = true;
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

        public void Translator_CompleteForTranslateText_Tab(int QtyItemsCompleted)
        {
            if ( _doSendErrMsgOnTranslateTextError )
            {
                _doSendErrMsgOnTranslateTextError = false;
                if ( QtyItemsCompleted < 1 || _multiThreadTranslator == null || _multiThreadTranslator._itemsToTranslate == null || _multiThreadTranslator._itemsToTranslate.Count < 1 || _multiThreadTranslator._itemsToTranslate[0].translatedText.Count < 1 )
                {
                    // ToDo: Add log file path to below message.
                    MessageBox.Show("Error: Failed to translate text.  For more information, view log files.", "Failed to Translate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            textBoxTranslation.Text = _multiThreadTranslator._itemsToTranslate[0].translatedText[0];
        }
        private void ChangeTransInput(object sender, EventArgs e)
        {
            buttonTranslateTextInTextBox.Enabled =
                comboBoxFromLang.SelectedIndex >= 0 &&
                comboBoxToLang.SelectedIndex >= 0 &&
                textBoxTextToTranslate.Text.Length > 0;
            if ( !_isFormReady )
                return;
            const int WordLen = 4;
            bool lastChrIsLetterOrDigit = textBoxTextToTranslate.Text.Length > 0 ? Char.IsLetterOrDigit(textBoxTextToTranslate.Text[textBoxTextToTranslate.Text.Length-1]):true;
            if ( buttonTranslateTextInTextBox.Enabled && textBoxTextToTranslate.Text.Length > WordLen && 
                (textBoxTextToTranslate.Text.Length > _lastTextToTranslate_value.Length + WordLen || _lastTextToTranslate_value.Length > textBoxTextToTranslate.Text.Length + WordLen ||
                (!textBoxTextToTranslate.Text.Equals(_lastTextToTranslate_value) && !lastChrIsLetterOrDigit))
                )
            {
                if ( !textBoxTextToTranslate.Text.Contains(' ') )
                    return;
                string lastWord = textBoxTextToTranslate.Text.Split(' ').Last();
                if ( lastChrIsLetterOrDigit && lastWord.Length < 4 )
                    return;
                _lastTextToTranslate_value = textBoxTextToTranslate.Text;
                _doSendErrMsgOnTranslateTextError = false;
                TranslateTextInTextBox(sender, e);
            }
        }
        private void buttonTranslateTextInTextBox_Click(object sender, EventArgs e)
        {
            _doSendErrMsgOnTranslateTextError = true;
            TranslateTextInTextBox(sender, e);
        }
        private void buttonBrowseForTextFile_Click(object sender, EventArgs e)
        {
            if ( openFileDialogTextFile.ShowDialog() == DialogResult.OK )
                textBoxTextToTranslate.Text = System.IO.File.ReadAllText(openFileDialogTextFile.FileName);
        }

    }
}
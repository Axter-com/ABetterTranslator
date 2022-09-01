![](ABetterTranslator/Images/BannerWithNameAndSubLabel.png)

# Translates Resx Resource Files
[![All Releases](https://img.shields.io/github/downloads/David-Maisonave/ABetterTranslator/total.svg)](https://github.com/David-Maisonave/ABetterTranslator/releases/latest)
[![GitHub issues](https://img.shields.io/github/issues/David-Maisonave/ABetterTranslator.svg)](https://GitHub.com/David-Maisonave/ABetterTranslator/issues/)
[![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)](https://github.com/Naereen/badges/)
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![csharp](https://img.shields.io/badge/--019733?logo=csharp)](https://www.vim.org/)
[![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
[![Visual Studio](https://badgen.net/badge/icon/visualstudio?icon=visualstudio&label)](https://visualstudio.microsoft.com)
[![Windows](https://badgen.net/badge/icon/windows?icon=windows&label)](https://microsoft.com/windows/)


Translates Resx files to over 130 different languages in less then 60 seconds.

## What does it do?
The GUI allows the user to select multiple languages from a list of over 130 different languages.
A file (Resx) is created for each language, with the associated language tag in the file name.
The code is optimized so that over 130 languages can be translated in less then 60 seconds. 
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot_(512_x_512).png)


# Content

[Features](README.md#Options)
-  [Multiple Translation Services](README.md#Multiple-Translation-Services)
-  [Supported Languages](README.md#Supported-Languages)
-  [Filter](README.md#Filter)
-  [Sortable Language List](README.md#Sortable-Language-List)
-  [Resize-able Program Window](README.md#Resize-able-Program-Window)
-  [Last Session Data Retention](README.md#Last-Session-Data-Retention)
-  [Screen Logging](README.md#Screen-Logging)

[Options](README.md#Options)
-  [Max Threads](README.md#Max-Threads)
-  [Max Translation Len](README.md#Max-Translation-Len)
-  [Strings-Per-Translation-Req](README.md#Strings-Per-Translation-Req)
-  [Display Warning Prompts](README.md#Display-Warning-Prompts)
-  [Default Language Set](README.md#Default-Language-Set)
-  [Delete Language Appended Resx Files](README.md#Delete-Language-Appended-Resx-Files)
-  [Translated Resx Comments](README.md#Translated-Resx-Comments)
-  [Delete Language Resx Files Before Translation](README.md#Delete-Language-ResxFiles-Before-Translation)
-  [Backup Files Before Translation](README.md#Backup-Files-Before-Translation)
-  [Backup Directory](README.md#Backup-Directory)
-  [Screen Verbosity Level](README.md#Screen-Verbosity-Level)
-  [Log File Verbosity Level](README.md#Log-File-Verbosity-Level)
-  [Logging Directory](README.md#Logging-Directory)

[Screenshots](README.md#Screenshots)
-  [Main Tab](README.md#Main-Tab)
-  [Translate Text Tab](README.md#Translate-Text-Tab)
-  [Advance Options Tab](README.md#Advance-Options-Tab)
-  [Group Language Selection](README.md#Group-Language-Selection)
-  [Language Display Set](README.md#Language-Display-Set)
-  [Language Filter](README.md#Language-Filter)
-  [Logging Tab](README.md#Logging-Tab)
-  [Resize-able Window](README.md#Resize-able-Window)


[Author](README.md#Author)

[License](README.md#License)


## Features

#### Multiple Translation Services

Translate text using multiple translation services.
* Google Translate
* Microsoft Azure Translator
* Bing Translator
* Yandex Translate

#### Supported Languages

Supports over 130 languages, with more to come.

For more details see [Supported Languages](ABetterTranslator/Docs/SupportedLanguages/README.md#Supported-Languages)


#### Filter

This option allows user to use keywords to find or filter the list to only items having the keyword(s).
For example usage, see [Language Filter](README.md#Language-Filter)

#### Sortable Language List

The list view has multiple columns, and the list can be sorted by any column by clicking on the column header.

#### Resize-able Program Window

The main program window is resize-able. See [Resize-able Window](README.md#Resize-able-Window)

#### Last Session Data Retension

* The program loads with the settings from the last session. 
  * Window size and position
  * User selected options in the Advanced Option tab
  * The last Language Set displayed.
  * Source Resx
  * Output Folder
  * Languages selected (checked)

#### Screen Logging

When a translation is executed, the Screen-Logging-Tab is displayed, which list the translation progress.
See [Logging Tab](README.md#Logging-Tab)


## Options

#### Max Threads

The number of threads to use when issuing translation request. The minimum (none negative) value is 4, and the maximum value is 400.
If this value is set to -1, then the program will use the ProcessorCount to determine the maximum threads to use.

Default: **-1**

#### Max Translation Len

The translation length is used when the program translates many strings in a single translation request.
The minimum value is 255, and the maximum value is 5,000.

Default: **5000**

#### Strings Per Translation Req

* Determines if one string is used per Resx translation request, or if many strings are used per Resx translation request. The following are the possible options to select.

  * [Auto]
    * This is the default option. It automatically sets the best method depending on the total number of strings and the maximum thread settings.

  * [One]
    * Only one string is sent per Resx translation request. Use this option if any of the strings contains the {next-line} character.

  * [Multiple]
    * Multiple strings are sent per Resx translation request. This is the preferred option if total translation is less then 10,000 characters.

* Default: **Auto**

#### Dispaly Warning Prompts

* Option to disable/enable warning prompts before files are deleted.

* Default: **Enabled**

#### Default Language Set

* Determines the default set of languages to display on the language list.

* Default: **Windows 10/11 Language Pack**

#### Delete Langugae Appended Resx Files

* Option to manually delete all Resx files which have an appended language tag in the name.

#### Translated Resx Comments

* Use this option to save the original language string inside the translated Resx 
  * **Do NOT change comments**
    * Comments in Resx files are not modified.  Translated Resx file has same comments as original source Resx file.
  * **If comment empty, set comment to original language text**
    * If the comment field is empty, set the comment field to the string Text value of the original language.
  * **Always set comments to original language text**
    * Always sets the comment field to the string Text value of the original language.
  * **Always append original language text to comments**
    * Always append the comment field with the string Text value of the original language.

* Default: **Do NOT change comments**

#### Delete Language ResxFiles Before Translation

* When enabled, deletes all Resx files having an appended language tag in the file name, before executing the translation.

* Default: **Disabled**

#### Backup Files Before Translation

* When enabled, backs up Resx files before executing the translation.

* Default: **Enabled**

#### Backup Directory

* The destination directory used to backup files.

* Default: **C:\Users\*[User-Name]*\AppData\Roaming\ABetterTranslator\ABetterTranslator\1.0.0\BackupResx**

#### Screen Verbosity Level

* The verbosity level for output data going to the Logging-Tab window.

* Default: **Normal**


#### Log File Verbosity Level

* The verbosity level for output data going to the log file.

* Default: **Normal**

#### Logging Directory

* The directory used for log files.

* Default: **C:\Users\*[User-Name]*\AppData\Local\Temp\ABetterTranslator\Log**


## Screenshots
### Main Tab
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot_(512_x_512).png)

### Translate Text Tab 
#### This tab translates text on the fly as the user is typing.
![](ABetterTranslator/Docs/screenshots/screenshot_translate_text_tab_(512_x_512).png)

### Advance Options Tab
![](ABetterTranslator/Docs/screenshots/screenshot_advance_options_tab_(512_x_512).png)

### Group Language Selection
#### The user can select groups of languages based on region, popularity, and business usage.
![](ABetterTranslator/Docs/screenshots/screenshot_group_languages_selections_(512_x_512).png)

#### The selections for top spoken languages, are based on 2022 statistics.

### Language Display Set
![](ABetterTranslator/Docs/screenshots/screenshot_LanguageDisplaySetSelection_(512_x_512).png)

### Language Filter
#### The language list can displays well over 130 different languages. To help quickly find a language, the GUI has a filter field.
![](ABetterTranslator/Docs/screenshots/screenshot_language_filter_usage_(512_x_512).png)

### Logging Tab
#### When a translation is executed, the screen logging tab is displayed, which shows translation progress and any translation errors.
![](ABetterTranslator/Docs/screenshots/LoggingTab_(512_x_512).png)

### Resize-able Window
#### The program window can be shrunk or expanded to user's preferred size.
![](ABetterTranslator/Docs/screenshots/screenshot_ABetterTranslator_long_window_(512_x_1600).png)


# Author

* **David Maisonave** - [David-Maisonave](https://github.com/David-Maisonave)


# License

-  This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

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
The code is optimized so that over 130 languages can be tranlsated in less then 60 seconds. 
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot_(512_x_512).png)


# Content

[Features](README.md#Features)
-  [Filter](README.md#Filter)
-  [Sortable Language List](README.md#Sortable-Language-List)
-  [Max Threads](README.md#Max-Threads)
-  [Max Translation Len](README.md#Max-Translation-Len)
-  [Strings-Per-Translation-Req](README.md#Strings-Per-Translation-Req)
-  [Dispaly Warning Prompts](README.md#Dispaly-Warning-Prompts)
-  [Default Language Set](README.md#Default-Language-Set)
-  [Delete Langugae Appended Resx Files](README.md#Delete-Langugae-Appended-Resx-Files)
-  [Translated Resx Comments](README.md#Translated-Resx-Comments)
-  [Delete Language Resx Files Before Translation](README.md#Delete-Language-ResxFiles-Before-Translation)
-  [Backup Files Before Translation](README.md#Backup-Files-Before-Translation)
-  [Backup Directory](README.md#Backup-Directory)
-  [Screen Verbosity Level](README.md#Screen-Verbosity-Level)
-  [Log File Verbosity Level](README.md#Log-File-Verbosity-Level)
-  [Logging Directory](README.md#Logging-Directory)




[Author](README.md#Author)

[License](README.md#License)


## Features

#### Filter

This option allows user to use keywords to find or filter the list to only items having the keyword(s).
For example usage, see [Language Filter](README.md#Language-Filter)

#### Sortable Language List

The list view has multiple columns, and the list can be sorted by any column by clicking on the column header.

#### Max Threads

The number of threads to use when issuing translation request. The minimum (none negative) value is 4, and the maximum value is 400.
If this value is set to -1, then the program will use the ProcessorCount to determine the maximum threads to use.

Default: **-1**

#### Max Translation Len

The translation length is used when the program translates many strings in a single translation request.
The minimum value is 255, and the maximum value is 10,000.

Default: **10000**

#### Strings-Per-Translation-Req

Determines if one string is used per Resx translation request, or if many strings are used per Resx translation request. The following are the possible options to select.

-  [Auto]

		This is the default option. It automatically sets the best method depending on the totoal number of strings and the maximum thread settings.

-  [One]

		Only one string is sent per Resx translation request. Use this option if any of the strings contains the {next-line} character.

-  [Multiple]

		Multiple strings are sent per Resx translation request. This is the perferred option if total translation is less then 10,000 characters.

Default: **Auto**


#### Dispaly-Warning-Prompts

Option to disable/enable warning prompts before files are deleted.

Default: **Enabled**

#### Default-Language-Set

Determines the default set of languages to display on the language list.

Default: **Windows 10/11 Language Pack**


#### Delete-Langugae-Appended-Resx-Files

Option to manually delete all Resx files which have an appended language tag in the name.


#### Translated-Resx-Comments

Default: **Do NOT change comments**


#### Delete-Language-ResxFiles-Before-Translation

Default: **todo_add_default_here**


#### Backup-Files-Before-Translation

Default: **todo_add_default_here**


#### Backup-Directory

Default: **todo_add_default_here**


#### Screen-Verbosity-Level

Default: **todo_add_default_here**


#### Log-File-Verbosity-Level

Default: **todo_add_default_here**


#### Logging-Directory

Default: **todo_add_default_here**




## Screenshots
### Main Tab
![](ABetterTranslator/Docs/screenshots/ABetterTranslatorScreenshot.png)

### Translate Text Tab 
#### This tab translates text on the fly as the user is typing.
![](ABetterTranslator/Docs/screenshots/screenshot_translate_text_tab.png)

### Advance Options Tab
![](ABetterTranslator/Docs/screenshots/screenshot_advance_options_tab.png)

### Group Language Selection
#### The user can select groups of languages based on region, popularity, and business usage.
![](ABetterTranslator/Docs/screenshots/screenshot_group_languages_selections.png)

#### The selections for top spoken languages, are based on 2022 statistics.

### Language Display Set
![](ABetterTranslator/Docs/screenshots/screenshot_LanguageDisplaySetSelection.png)

### Language Filter
#### The language list can displays well over 130 different languages. To help quickly find a language, the GUI has a filter field.
![](ABetterTranslator/Docs/screenshots/screenshot_language_filter_usage.png)

### Resizeable Window
#### The program window can be shrunk or expanded to user's preferred size.
![](ABetterTranslator/Docs/screenshots/screenshot_ABetterTranslator_long_window.png)


# Author

* **David Maisonave** - [David-Maisonave](https://github.com/David-Maisonave)


# License

-  This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

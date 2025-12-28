using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.CommandPlacements;

/// <summary>
/// Known VSCT placements based on VSGlobals.vsct from Community.VisualStudio.VSCT
/// https://github.com/VsixCommunity/Community.VisualStudio.VSCT
/// </summary>
public static class KnownPlacements
{
    #region VSEditor (9adf33d0-8aad-11d0-b606-00a0c922e851)

    private static readonly Guid VSEditor = new("{9adf33d0-8aad-11d0-b606-00a0c922e851}");

    public static CommandPlacement Edit => CommandPlacement.VsctParent(VSEditor, 0x3E8A);
    public static CommandPlacement Edit_FindGroup_Find => CommandPlacement.VsctParent(VSEditor, 0x3EA3);
    public static CommandPlacement Edit_GoToGroup_GoTo => CommandPlacement.VsctParent(VSEditor, 0x3EA5);
    public static CommandPlacement Edit_LanguageInfoGroup => CommandPlacement.VsctParent(VSEditor, 0x3E93);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced => CommandPlacement.VsctParent(VSEditor, 0x3EA0);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced_CommandsGroup => CommandPlacement.VsctParent(VSEditor, 0x3E8F);
    public static CommandPlacement Edit_LanguageInfoGroup_Bookmark => CommandPlacement.VsctParent(VSEditor, 0x3E9E);
    public static CommandPlacement Edit_LanguageInfoGroup_Bookmark_AllDocumentsGroup => CommandPlacement.VsctParent(VSEditor, 0x3EB2);
    public static CommandPlacement Edit_LanguageInfoGroup_Bookmark_DocumentGroup => CommandPlacement.VsctParent(VSEditor, 0x3EB1);
    public static CommandPlacement Edit_LanguageInfoGroup_Bookmark_FolderGroup => CommandPlacement.VsctParent(VSEditor, 0x3EB0);
    public static CommandPlacement Edit_LanguageInfoGroup_Bookmark_TaskListGroup => CommandPlacement.VsctParent(VSEditor, 0x3EB3);
    public static CommandPlacement Edit_LanguageInfoGroup_Intellisense => CommandPlacement.VsctParent(VSEditor, 0x3EA2);
    public static CommandPlacement Edit_LanguageInfoGroup_Intellisense_CommandsGroup => CommandPlacement.VsctParent(VSEditor, 0x3E94);
    public static CommandPlacement Edit_LanguageInfoGroup_Outlining => CommandPlacement.VsctParent(VSEditor, 0x3EA1);
    public static CommandPlacement Edit_LanguageInfoGroup_Outlining_CommandsGroup => CommandPlacement.VsctParent(VSEditor, 0x3E90);
    public static CommandPlacement Edit_PasteGroup_Special => CommandPlacement.VsctParent(VSEditor, 0x3EA4);

    #endregion

    #region VSEditor2 (160961B3-909D-4B28-9353-A1BEF587B4A6)

    private static readonly Guid VSEditor2 = new("{160961B3-909D-4B28-9353-A1BEF587B4A6}");

    public static CommandPlacement Edit_LanguageInfoGroup_MultiCarets => CommandPlacement.VsctParent(VSEditor2, 0x0002);
    public static CommandPlacement Edit_LanguageInfoGroup_MultiCarets_DefaultGroup => CommandPlacement.VsctParent(VSEditor2, 0x0003);
    public static CommandPlacement Edit_GoToGroup_GoTo_CommandsGroup => CommandPlacement.VsctParent(VSEditor2, 0x1000);
    public static CommandPlacement CodeWin_NavigateGroup => CommandPlacement.VsctParent(VSEditor2, 0x0009);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced_CommandsGroup_Newline => CommandPlacement.VsctParent(VSEditor2, 0x0032);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced_CommandsGroup_Newline_NormalizeGroup => CommandPlacement.VsctParent(VSEditor2, 0x0033);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced_CommandsGroup_Whitespace => CommandPlacement.VsctParent(VSEditor2, 0x0040);
    public static CommandPlacement Edit_LanguageInfoGroup_Advanced_CommandsGroup_Whitespace_NormalizeGroup => CommandPlacement.VsctParent(VSEditor2, 0x0041);
    public static CommandPlacement Edit_GoToGroup_GoTo_EditorGroup => CommandPlacement.VsctParent(VSEditor2, 0x1000);

    #endregion

    #region VSFeedback (769a4361-ad47-4ff4-86d8-d13bfdb7ed65)

    private static readonly Guid VSFeedback = new("{769a4361-ad47-4ff4-86d8-d13bfdb7ed65}");

    public static CommandPlacement Help_FeedbackGroup => CommandPlacement.VsctParent(VSFeedback, 0x0040);
    public static CommandPlacement Help_FeedbackGroup_SendFeedback => CommandPlacement.VsctParent(VSFeedback, 0x0700);
    public static CommandPlacement Help_FeedbackGroup_SendFeedback_ReportGroup => CommandPlacement.VsctParent(VSFeedback, 0x1020);
    public static CommandPlacement Help_FeedbackGroup_SendFeedback_SettingsGroup => CommandPlacement.VsctParent(VSFeedback, 0x1023);

    #endregion

    #region VSTerminal (2ECA946C-513F-4FDA-8D52-45F01346C2D4)

    private static readonly Guid VSTerminal = new("{2ECA946C-513F-4FDA-8D52-45F01346C2D4}");

    public static CommandPlacement Tools_OptionsGroup_CommandLine => CommandPlacement.VsctParent(VSTerminal, 0x0001);
    public static CommandPlacement Tools_OptionsGroup_CommandLine_TerminalsGroup => CommandPlacement.VsctParent(VSTerminal, 0x0002);

    #endregion

    #region VSNuGet (C0D88179-5D25-4982-BFE6-EC5FD59AC103)

    private static readonly Guid VSNuGet = new("{C0D88179-5D25-4982-BFE6-EC5FD59AC103}");

    public static CommandPlacement Tools_Other2Group_NuGet => CommandPlacement.VsctParent(VSNuGet, 0x100);
    public static CommandPlacement Tools_Other2Group_NuGet_ToolsGroup => CommandPlacement.VsctParent(VSNuGet, 0x200);

    #endregion

    #region VSMainMenu (d309f791-903f-11d0-9efc-00a0c911004f)

    private static readonly Guid VSMainMenu = new("{d309f791-903f-11d0-9efc-00a0c911004f}");

    public static CommandPlacement BookmarksToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0019);
    public static CommandPlacement Build => CommandPlacement.VsctParent(VSMainMenu, 0x008C);
    public static CommandPlacement Build_CancelGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0283);
    public static CommandPlacement Build_MiscGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0282);
    public static CommandPlacement Build_SelectionGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0281);
    public static CommandPlacement Build_SolutionGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0280);
    public static CommandPlacement BuildToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x000d);
    public static CommandPlacement BuildToolbar_BuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D2);
    public static CommandPlacement ClassViewToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0010);
    public static CommandPlacement CodeWindow => CommandPlacement.VsctParent(VSMainMenu, 0x0499);
    public static CommandPlacement CodeWindow_AdvancedGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D1);
    public static CommandPlacement CodeWindow_AnnotationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D7);
    public static CommandPlacement CodeWindow_AnnotationGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x02D6);
    public static CommandPlacement CodeWindow_AnnotationGroup_Submenu_AnnotationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D5);
    public static CommandPlacement CodeWindow_DebugStepGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C3);
    public static CommandPlacement CodeWindow_DebugWatchGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C2);
    public static CommandPlacement CodeWindow_IntellisenseGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B0);
    public static CommandPlacement CodeWindow_IntellitraceStepGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D8);
    public static CommandPlacement CodeWindow_LanguageGroupGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D0);
    public static CommandPlacement CodeWindow_MarkerGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C4);
    public static CommandPlacement CodeWindow_NavigateToFileGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B2);
    public static CommandPlacement CodeWindow_NavigationLocationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B1);
    public static CommandPlacement CodeWindow_OpenUrlGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C5);
    public static CommandPlacement CodeWindow_OutliningGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B3);
    public static CommandPlacement CodeWindow_OutliningGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x3EA1);
    public static CommandPlacement CodeWindow_OutliningGroup_Submenu_OutliningGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B4);
    public static CommandPlacement CodeWindow_RefactoringGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B5);
    public static CommandPlacement CodeWindow_ShortcutGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C6);
    public static CommandPlacement CodeWindow_SnippetsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D4);
    public static CommandPlacement CodeWindow_SnippetsGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x02D3);
    public static CommandPlacement CodeWindow_SnippetsGroup_Submenu_SnippetsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02D2);
    public static CommandPlacement CodeWindow_TextEditGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01C0);
    public static CommandPlacement DataToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0012);
    public static CommandPlacement Debug => CommandPlacement.VsctParent(VSMainMenu, 0x0089);
    public static CommandPlacement DebuggerToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x00006);
    public static CommandPlacement DebuggerToolbar_DebuggerGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0200);
    public static CommandPlacement Edit_MainMenu => CommandPlacement.VsctParent(VSMainMenu, 0x0081);
    public static CommandPlacement Edit_CutCopyGroup => CommandPlacement.VsctParent(VSMainMenu, 0x012A);
    public static CommandPlacement Edit_FindGroup => CommandPlacement.VsctParent(VSMainMenu, 0x012C);
    public static CommandPlacement Edit_GoToGroup => CommandPlacement.VsctParent(VSMainMenu, 0x012D);
    public static CommandPlacement Edit_ObjectsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0128);
    public static CommandPlacement Edit_PasteGroup => CommandPlacement.VsctParent(VSMainMenu, 0x012F);
    public static CommandPlacement Edit_SelectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x012B);
    public static CommandPlacement Edit_UndoRedoGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0129);
    public static CommandPlacement ErrorListToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x002c);
    public static CommandPlacement ErrorListToolbar_BuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01DC);
    public static CommandPlacement ErrorListToolbar_ClearFilterGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01DD);
    public static CommandPlacement ErrorListToolbar_ErrorGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D4);
    public static CommandPlacement ErrorListToolbar_FilterCategoryGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01DB);
    public static CommandPlacement ErrorListToolbar_FilterListGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01DA);
    public static CommandPlacement ErrorListToolbar_MessageGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D9);
    public static CommandPlacement ErrorListToolbar_WarningGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D8);
    public static CommandPlacement Extensions => CommandPlacement.VsctParent(VSMainMenu, 0x0091);
    public static CommandPlacement Extensions_RedirectionGroup => CommandPlacement.VsctParent(VSMainMenu, 0x010A);
    public static CommandPlacement Extensions_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x6000);
    public static CommandPlacement File => CommandPlacement.VsctParent(VSMainMenu, 0x0080);
    public static CommandPlacement File_AccountSettingsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0711);
    public static CommandPlacement File_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0111);
    public static CommandPlacement File_AddGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x030A);
    public static CommandPlacement File_AddGroup_Submenu_ExistingGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011D);
    public static CommandPlacement File_AddGroup_Submenu_NewGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011C);
    public static CommandPlacement File_BrowserGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0120);
    public static CommandPlacement File_DeleteGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0117);
    public static CommandPlacement File_ExitGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0116);
    public static CommandPlacement File_FileGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0110);
    public static CommandPlacement File_ItemGroup => CommandPlacement.VsctParent(VSMainMenu, 0x010F);
    public static CommandPlacement File_MiscGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0124);
    public static CommandPlacement File_MiscGroup_Submenu_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0125);
    public static CommandPlacement File_MoveGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0121);
    public static CommandPlacement File_MoveGroup_Project => CommandPlacement.VsctParent(VSMainMenu, 0x030F);
    public static CommandPlacement File_MoveGroup_Project_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0122);
    public static CommandPlacement File_NewGroup => CommandPlacement.VsctParent(VSMainMenu, 0x010E);
    public static CommandPlacement File_NewGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x0308);
    public static CommandPlacement File_NewGroup_Submenu_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0119);
    public static CommandPlacement File_OpenFileGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011B);
    public static CommandPlacement File_OpenProjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011A);
    public static CommandPlacement File_PrintGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0114);
    public static CommandPlacement File_RecentlyUsedv => CommandPlacement.VsctParent(VSMainMenu, 0x0115);
    public static CommandPlacement File_RecentlyUsedv_Files => CommandPlacement.VsctParent(VSMainMenu, 0x030C);
    public static CommandPlacement File_RecentlyUsedv_Files_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011E);
    public static CommandPlacement File_RecentlyUsedv_Projects => CommandPlacement.VsctParent(VSMainMenu, 0x030D);
    public static CommandPlacement File_RecentlyUsedv_Projects_DefaultGroup => CommandPlacement.VsctParent(VSMainMenu, 0x011F);
    public static CommandPlacement File_RenameGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0113);
    public static CommandPlacement File_SaveGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0112);
    public static CommandPlacement File_SolutionGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0118);
    public static CommandPlacement FindAllReferencesToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x000a);
    public static CommandPlacement FindAllReferencesToolbar_CommonGroup => CommandPlacement.VsctParent(VSMainMenu, 0x026c);
    public static CommandPlacement FindAllReferencesToolbar_LockWinGroup => CommandPlacement.VsctParent(VSMainMenu, 0x026e);
    public static CommandPlacement FindAllReferencesToolbar_PreservedGroup => CommandPlacement.VsctParent(VSMainMenu, 0x026f);
    public static CommandPlacement FindAllReferencesToolbar_PresetGroup => CommandPlacement.VsctParent(VSMainMenu, 0x026d);
    public static CommandPlacement FindResults1Toolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0015);
    public static CommandPlacement FindResults1Toolbar_GoTClearGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0299);
    public static CommandPlacement FindResults1Toolbar_GoToGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0297);
    public static CommandPlacement FindResults1Toolbar_NextPrevGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0298);
    public static CommandPlacement FindResults1Toolbar_StopFindGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B6);
    public static CommandPlacement FindResults2Toolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0016);
    public static CommandPlacement FindResults2Toolbar_GoTClearGroup => CommandPlacement.VsctParent(VSMainMenu, 0x029C);
    public static CommandPlacement FindResults2Toolbar_GoToGroup => CommandPlacement.VsctParent(VSMainMenu, 0x029A);
    public static CommandPlacement FindResults2Toolbar_NextPrevGroup => CommandPlacement.VsctParent(VSMainMenu, 0x029B);
    public static CommandPlacement FindResults2Toolbar_StopFindGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02B7);
    public static CommandPlacement FolderNode => CommandPlacement.VsctParent(VSMainMenu, 0x0431);
    public static CommandPlacement FolderNode_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A3);
    public static CommandPlacement FolderNode_ExploreGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0267);
    public static CommandPlacement Help => CommandPlacement.VsctParent(VSMainMenu, 0x0088);
    public static CommandPlacement Help_AboutGroup => CommandPlacement.VsctParent(VSMainMenu, 0x016B);
    public static CommandPlacement Help_AccessibilityGroup => CommandPlacement.VsctParent(VSMainMenu, 0x016C);
    public static CommandPlacement Help_SupportGroup => CommandPlacement.VsctParent(VSMainMenu, 0x016A);
    public static CommandPlacement ItemNode => CommandPlacement.VsctParent(VSMainMenu, 0x0430);
    public static CommandPlacement ItemNode_ExploreGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02E6);
    public static CommandPlacement ItemNode_OpenGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0209);
    public static CommandPlacement ItemNode_PropertiesGroup => CommandPlacement.VsctParent(VSMainMenu, 0x020E);
    public static CommandPlacement ItemNode_RenameGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0210);
    public static CommandPlacement ItemNode_TransferGroup => CommandPlacement.VsctParent(VSMainMenu, 0x020A);
    public static CommandPlacement ItemNode_ViewBrowserGroup => CommandPlacement.VsctParent(VSMainMenu, 0x020B);
    public static CommandPlacement ItemNode_ViewObjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0208);
    public static CommandPlacement MainMenuToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0000);
    public static CommandPlacement MainMenuToolbar_BuildDebugRunGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0103);
    public static CommandPlacement MainMenuToolbar_FileEditViewGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0101);
    public static CommandPlacement MainMenuToolbar_FullScreenBarGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0106);
    public static CommandPlacement MainMenuToolbar_ProjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0102);
    public static CommandPlacement MainMenuToolbar_ToolsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0109);
    public static CommandPlacement MainMenuToolbar_WindowHelpGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0105);
    public static CommandPlacement Node_IncludeExcludeGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A2);
    public static CommandPlacement Node_SourceControlGroup => CommandPlacement.VsctParent(VSMainMenu, 0x020F);
    public static CommandPlacement OutputWindowToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0014);
    public static CommandPlacement OutputWindowToolbar_ClearGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0296);
    public static CommandPlacement OutputWindowToolbar_GoToGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0294);
    public static CommandPlacement OutputWindowToolbar_NextPrevGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0295);
    public static CommandPlacement OutputWindowToolbar_SelectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0293);
    public static CommandPlacement OutputWindowToolbar_WordWrapGroup => CommandPlacement.VsctParent(VSMainMenu, 0x029F);
    public static CommandPlacement Project => CommandPlacement.VsctParent(VSMainMenu, 0x0083);
    public static CommandPlacement Project_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0140);
    public static CommandPlacement Project_AddCodeGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0145);
    public static CommandPlacement Project_AddMiscGroup => CommandPlacement.VsctParent(VSMainMenu, 0x014C);
    public static CommandPlacement Project_AddRemoveGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0147);
    public static CommandPlacement Project_AdminGroup => CommandPlacement.VsctParent(VSMainMenu, 0x014E);
    public static CommandPlacement Project_FolderGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0143);
    public static CommandPlacement Project_OptionsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0141);
    public static CommandPlacement Project_ProjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0146);
    public static CommandPlacement Project_ReferenceGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0142);
    public static CommandPlacement Project_SettingsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x014D);
    public static CommandPlacement Project_Toolbar1Group => CommandPlacement.VsctParent(VSMainMenu, 0x014A);
    public static CommandPlacement Project_Toolbar2Group => CommandPlacement.VsctParent(VSMainMenu, 0x014B);
    public static CommandPlacement Project_UnloadReloadGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0144);
    public static CommandPlacement Project_Web1Group => CommandPlacement.VsctParent(VSMainMenu, 0x0148);
    public static CommandPlacement Project_Web2Group => CommandPlacement.VsctParent(VSMainMenu, 0x0149);
    public static CommandPlacement ProjectNode => CommandPlacement.VsctParent(VSMainMenu, 0x0402);
    public static CommandPlacement ProjectNode_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0202);
    public static CommandPlacement ProjectNode_AddGroup_Submenu => CommandPlacement.VsctParent(VSMainMenu, 0x0202);
    public static CommandPlacement ProjectNode_AddGroup_Submenu_FormsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A0);
    public static CommandPlacement ProjectNode_AddGroup_Submenu_ItemsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0203);
    public static CommandPlacement ProjectNode_AddGroup_Submenu_MiscGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A1);
    public static CommandPlacement ProjectNode_BuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0206);
    public static CommandPlacement ProjectNode_BuildDependenciesGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02E0);
    public static CommandPlacement ProjectNode_DebugGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0204);
    public static CommandPlacement ProjectNode_EditGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0237);
    public static CommandPlacement ProjectNode_ExploreGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0266);
    public static CommandPlacement ProjectNode_MultiProjectBuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0201);
    public static CommandPlacement ProjectNode_PropertiesGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0214);
    public static CommandPlacement ProjectNode_RenameGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0211);
    public static CommandPlacement ProjectNode_SourceControlGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0216);
    public static CommandPlacement ProjectNode_StartGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0205);
    public static CommandPlacement ProjectNode_TransferGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0207);
    public static CommandPlacement ProjectNode_UnloadReloadGroup => CommandPlacement.VsctParent(VSMainMenu, 0x023A);
    public static CommandPlacement ReferencesNode => CommandPlacement.VsctParent(VSMainMenu, 0x0450);
    public static CommandPlacement ReferencesNode_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A4);
    public static CommandPlacement ReferencesNode_TransferGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02A5);
    public static CommandPlacement SnippetsToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x002D);
    public static CommandPlacement SnippetsToolbar_PropertyGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02C8);
    public static CommandPlacement SnippetsToolbar_ReferenceGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02C9);
    public static CommandPlacement SnippetsToolbar_ReplaceGroup => CommandPlacement.VsctParent(VSMainMenu, 0x02CA);
    public static CommandPlacement SolutionExplorerToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x0003);
    public static CommandPlacement SolutionExplorerToolbar_NavigationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0730);
    public static CommandPlacement SolutionExplorerToolbar_FiltersGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0738);
    public static CommandPlacement SolutionFolder => CommandPlacement.VsctParent(VSMainMenu, 0x0414);
    public static CommandPlacement SolutionFolder_AddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0264);
    public static CommandPlacement SolutionFolder_AddGroup_AddMenu => CommandPlacement.VsctParent(VSMainMenu, 0x0357);
    public static CommandPlacement SolutionFolder_AddGroup_AddMenu_AddItemGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0262);
    public static CommandPlacement SolutionFolder_AddGroup_AddMenu_AddProjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0261);
    public static CommandPlacement SolutionFolder_UnloadReloadGroup => CommandPlacement.VsctParent(VSMainMenu, 0x023A);
    public static CommandPlacement SolutionFolder_BuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0263);
    public static CommandPlacement SolutionNode => CommandPlacement.VsctParent(VSMainMenu, 0x0413);
    public static CommandPlacement SolutionNode_AddGroup_AddMenu => CommandPlacement.VsctParent(VSMainMenu, 0x0350);
    public static CommandPlacement SolutionNode_AddGroup_AddMenu_AddItemGroup => CommandPlacement.VsctParent(VSMainMenu, 0x021E);
    public static CommandPlacement SolutionNode_AddGroup_AddMenu_AddProjectGroup => CommandPlacement.VsctParent(VSMainMenu, 0x021D);
    public static CommandPlacement SolutionNode_BuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0219);
    public static CommandPlacement SolutionNode_DebugGroup => CommandPlacement.VsctParent(VSMainMenu, 0x021F);
    public static CommandPlacement SolutionNode_DebugGroup_DebugMenu => CommandPlacement.VsctParent(VSMainMenu, 0x0351);
    public static CommandPlacement SolutionNode_ExploreGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0265);
    public static CommandPlacement SolutionNode_PropertiesGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0215);
    public static CommandPlacement SolutionNode_RenameGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0212);
    public static CommandPlacement SolutionNode_SourceControlGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0217);
    public static CommandPlacement SolutionNode_StartGroup => CommandPlacement.VsctParent(VSMainMenu, 0x021B);
    public static CommandPlacement SolutionNode_TransferGroup => CommandPlacement.VsctParent(VSMainMenu, 0x021C);
    public static CommandPlacement SolutionNode_UnhideGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0236);
    public static CommandPlacement StandardToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x00001);
    public static CommandPlacement StandardToolbar_CutCopyGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0172);
    public static CommandPlacement StandardToolbar_FindGroup => CommandPlacement.VsctParent(VSMainMenu, 0x017D);
    public static CommandPlacement StandardToolbar_GaugeGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0176);
    public static CommandPlacement StandardToolbar_NavigateGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0179);
    public static CommandPlacement StandardToolbar_NewAddGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0170);
    public static CommandPlacement StandardToolbar_NewWindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0178);
    public static CommandPlacement StandardToolbar_ReplaceGroup => CommandPlacement.VsctParent(VSMainMenu, 0x017E);
    public static CommandPlacement StandardToolbar_RunBuildGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0174);
    public static CommandPlacement StandardToolbar_SaveOpenGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0171);
    public static CommandPlacement StandardToolbar_SearchGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0177);
    public static CommandPlacement StandardToolbar_UndoRedoGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0173);
    public static CommandPlacement StandardToolbar_WindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0175);
    public static CommandPlacement TaskListToolbart => CommandPlacement.VsctParent(VSMainMenu, 0x002a);
    public static CommandPlacement TaskListToolbart_ProviderListGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D1);
    public static CommandPlacement TextEditorToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x000e);
    public static CommandPlacement TextEditorToolbar_CommentGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0552);
    public static CommandPlacement TextEditorToolbar_CompletionGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0550);
    public static CommandPlacement TextEditorToolbar_IndentGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0551);
    public static CommandPlacement TextEditorToolbar_TempBookmarksGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0553);
    public static CommandPlacement Tools => CommandPlacement.VsctParent(VSMainMenu, 0x0085);
    public static CommandPlacement Tools_ExtensibilityGroup => CommandPlacement.VsctParent(VSMainMenu, 0x015F);
    public static CommandPlacement Tools_ExternalCustomGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0158);
    public static CommandPlacement Tools_ExternalToolsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0159);
    public static CommandPlacement Tools_ObjectSubsetGroup => CommandPlacement.VsctParent(VSMainMenu, 0x015C);
    public static CommandPlacement Tools_OptionsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x015A);
    public static CommandPlacement Tools_Other2Group => CommandPlacement.VsctParent(VSMainMenu, 0x015B);
    public static CommandPlacement Tools_SnippetsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x3E95);
    public static CommandPlacement UserTasksToolbar => CommandPlacement.VsctParent(VSMainMenu, 0x002b);
    public static CommandPlacement UserTasksToolbar_EditGroup => CommandPlacement.VsctParent(VSMainMenu, 0x01D3);
    public static CommandPlacement View => CommandPlacement.VsctParent(VSMainMenu, 0x0082);
    public static CommandPlacement View_ArcitectureWindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0720);
    public static CommandPlacement View_BrowserGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0130);
    public static CommandPlacement View_CodeGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0133);
    public static CommandPlacement View_DefineViewsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0134);
    public static CommandPlacement View_DevWindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0723);
    public static CommandPlacement View_DevWindowsGroup_FindResults_Group => CommandPlacement.VsctParent(VSMainMenu, 0x0724);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group0 => CommandPlacement.VsctParent(VSMainMenu, 0x019E);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group1 => CommandPlacement.VsctParent(VSMainMenu, 0x019F);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group2 => CommandPlacement.VsctParent(VSMainMenu, 0x01A0);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group3 => CommandPlacement.VsctParent(VSMainMenu, 0x01A1);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group4 => CommandPlacement.VsctParent(VSMainMenu, 0x01A2);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group5 => CommandPlacement.VsctParent(VSMainMenu, 0x01A3);
    public static CommandPlacement View_DevWindowsGroup_OtherWindows_Group6 => CommandPlacement.VsctParent(VSMainMenu, 0x01A7);
    public static CommandPlacement View_ExplorerWindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0721);
    public static CommandPlacement View_LinksGroup => CommandPlacement.VsctParent(VSMainMenu, 0x013B);
    public static CommandPlacement View_NavigateGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0137);
    public static CommandPlacement View_NavigationWindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0722);
    public static CommandPlacement View_ObjectBrowserGroup => CommandPlacement.VsctParent(VSMainMenu, 0x013A);
    public static CommandPlacement View_PropertyPagesGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0131);
    public static CommandPlacement View_RefreshGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0136);
    public static CommandPlacement View_SmallNavigateGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0139);
    public static CommandPlacement View_SymbolNavigateGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0138);
    public static CommandPlacement View_ToolbarsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0132);
    public static CommandPlacement View_WindowsGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0135);
    public static CommandPlacement View_WindowsGroup_InteractiveWindows => CommandPlacement.VsctParent(VSMainMenu, 0x01A7);
    public static CommandPlacement Window => CommandPlacement.VsctParent(VSMainMenu, 0x0086);
    public static CommandPlacement Window_ArrangeGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0161);
    public static CommandPlacement Window_LayoutGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0164);
    public static CommandPlacement Window_LayoutListGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0165);
    public static CommandPlacement Window_ListGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0162);
    public static CommandPlacement Window_NavigationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0163);
    public static CommandPlacement Window_NewGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0160);
    public static CommandPlacement Window_TabOrientationGroup => CommandPlacement.VsctParent(VSMainMenu, 0x0166);

    #endregion

    #region Legacy Aliases (Preserved for backward compatibility)

    [Obsolete("Use ItemNode_OpenGroup instead")]
    public static CommandPlacement FileInProjectContextMenu
        => CommandPlacement.VsctParent(VSMainMenu, 0x0209, 0x0801);

    [Obsolete("Use ProjectNode_BuildGroup instead")]
    public static CommandPlacement ProjectContextMenu
        => CommandPlacement.VsctParent(VSMainMenu, 0x0206, 0x0801);

    [Obsolete("Use SolutionNode_BuildGroup instead")]
    public static CommandPlacement SolutionContextMenu
        => CommandPlacement.VsctParent(VSMainMenu, 0x0219, 0x0801);

    #endregion
}
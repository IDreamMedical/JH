using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;

namespace UniGuy.Windows.Definitions
{
    public static class ResourceDefinitions
    {
        // Styles
        public static string ButtonStyleKey = "ButtonStyle";
        public static string EmbeddedButtonStyleKey = "EmbeddedButtonStyle";
        public static string CheckBoxStyleKey = "CheckBoxStyle";
        public static string RadioButtonStyleKey = "RadioButtonStyle";
        public static string DropDownButtonStyleKey = "DropDownButtonStyle";
        public static string RepeatButtonStyleKey = "RepeatButtonStyle";
        public static string ToggleButtonStyleKey = "ToggleButtonStyle";
        public static string ExpanderToggleButtonStyleKey = "ExpanderToggleButtonStyle";
        public static string MiddleCapsuleExpanderToggleButtonStyleKey = "MiddleCapsuleExpanderToggleButtonStyle";
        public static string ScrollBarStyleKey = "ScrollBarStyle";
        public static string ListBoxStyleKey = "ListBoxStyle";
        public static string ListBoxItemStyleKey = "ListBoxItemStyle";
        public static string ExpanderStyleKey = "ExpanderStyle";
        public static string ComboBoxStyleKey = "ComboBoxStyle";
        public static string ComboBoxItemStyleKey = "ComboBoxItemStyle";
        public static string ProgressBarStyleKey = "ProgressBarStyle";
        public static string ColorPickerStyleKey = "ColorPickerStyle";
        public static string TextBoxStyleKey = "TextBoxStyle";
        public static string OverlayWidgetStyleKey = "OverlayWidgetStyle";
        public static string PasswordBoxStyleKey = "PasswordBoxStyle";
        public static string RichTextBoxStyleKey = "RichTextBoxStyle";
        public static string LabelStyleKey = "LabelStyle";
        public static string MenuStyleKey = "MenuStyle";
        public static string ContextMenuStyleKey = "ContextMenuStyle";
        public static string MenuItemStyleKey = "MenuItemStyle";
        public static string SeparatorStyleKey = "SeparatorStyle";
        public static string TabControlStyleKey = "TabControlStyle";
        public static string OverflowTabControlStyleKey = "OverflowTabControlStyle";
        public static string TabItemStyleKey = "TabItemStyle";
        public static string OverflowTabItemStyleKey = "OverflowTabItemStyle";
        public static string SliderStyleKey = "SliderStyle";
        public static string TreeViewStyleKey = "TreeViewStyle";
        public static string TreeViewItemStyleKey = "TreeViewItemStyle";
        public static string GroupBoxStyleKey = "GroupBoxStyle";
        public static string ListViewStyleKey = "ListViewStyle";
        public static string ListViewItemStyleKey = "ListViewItemStyle";
        public static string AssetListViewItemStyleKey = "AssetListViewItemStyle";
        public static string TextModeAssetListViewItemStyleKey = "TextModeAssetListViewItemStyle";
        public static string ToolBarStyleKey = "ToolBarStyle";
        public static string GridSplitterStyleKey = "GridSplitterStyle";
        public static string ScrollViewerStyleKey = "ScrollViewerStyle";
        public static string ToolTipStyleKey = "ToolTipStyle";
        public static string PopupStyleKey = "PopupStyle";
        public static string TextBoxAndButtonStyleKey = "TextBoxAndButtonStyle";
        public static string TokenizedTextBoxStyleKey = "TokenizedTextBoxStyle";
        public static string TokenizedRendererStyleKey = "TokenizedRendererStyle";

        public static string CapsuleButtonLeftStyleKey = "CapsuleButtonLeftStyle";
        public static string CapsuleButtonRightStyleKey = "CapsuleButtonRightStyle";
        public static string CapsuleButtonMiddleStyleKey = "CapsuleButtonMiddleStyle";
        public static string ToolbarCommandButtonStyleKey = "ToolbarCommandButtonStyle";
        public static string CommonCloseButtonStyleKey = "CommonCloseButtonStyle";

        public static string MinimizeButtonStyleKey = "MinimizeButtonStyle";
        public static string MaximizeButtonStyleKey = "MaximizeButtonStyle";
        public static string RestoreButtonStyleKey = "RestoreButtonStyleKey";
        public static string CloseButtonStyleKey = "CloseButtonStyle";
        public static string CaptionButtonsContainerStyleKey = "CaptionButtonsContainerStyle";
        public static string MinimizeMenuItemStyleKey = "MinimizeMenuItemStyle";
        public static string MaximizeMenuItemStyleKey = "MaximizeMenuItemStyle";
        public static string RestoreMenuItemStyleKey = "RestoreMenuItemStyle";
        public static string CloseMenuItemStyleKey = "CloseMenuItemStyle";

        public static string NewClipTransportButtonStyleKey = "NewClipTransportButtonStyle";
        public static string CueStartTransportButtonStyleKey = "CueStartTransportButtonStyle";
        public static string EjectTransportButtonStyleKey = "EjectTransportButtonStyle";
        public static string LoopTransportToggleButtonStyleKey = "LoopTransportButtonStyle";

        public static string PlayPauseButtonStyleKey = "PlayPauseButtonStyle";
        public static string RecordStopButtonStyleKey = "RecordStopButtonStyle";

        public static string IndependentLoopToggleButtonStyleKey = "IndependentLoopToggleButtonStyle";
        public static string IndependentExpanderToggleButtonStyleKey = "IndependentExpanderToggleButtonStyle";

        public static string HotKeyTextBoxStyleKey = "HotKeyTextBoxStyle";

        public static string LinkButtonStyleKey = "LinkButtonStyleKey";
        public static string SalvoButtonStyleKey = "SalvoButtonStyle";

        public static string StatusBarStyleKey = "StatusBarStyleKey";

        public static string HyperLinkStyleKey = "HyperLinkStyle";
        public static string TimecodeTextBoxStyleKey = "TimecodeTextBoxStyle";
        public static string DarkerTextBoxStyleKey = "DarkerTextBoxStyle";

        // Base Controls
        public static string AudioBarStyleKey = "AudioBarTemplate";
        public static string AdjustableAudioBarStyleKey = "AdjustableAudioBarStyle";
        public static string AudioButtonMuteStyleKey = "MuteToggleTemplate";
        public static string AudioButtonSoloStyleKey = "SoloToggleTemplate";
        public static string AudioDotStyleKey = "AudioDotTemplate";
        public static string AudioLevelLineKey = "AudioLevelLineTemplate";

        public static string ApplicationWindowStyleKey = "ApplicationWindowStyle";

        // Brushes
        public static string WindowBackgroundBrushKey = "WindowBackgroundBrush";
        public static string WindowForegroundBrushKey = "WindowForegroundBrush";
        public static string RedButtonBackgroundBrushKey = "RedButtonBackgroundBrush";
        public static string RedButtonMouseOverBackgroundBrushKey = "RedButtonMouseOverBackgroundBrush";
        public static string ListRow0BackgroundBrushKey = "ListRow0BackgroundBrush";
        public static string ListRow1BackgroundBrushKey = "ListRow1BackgroundBrush";
        public static string ExtendedSearchBackgroundBrushKey = "ExtendedSearchBackgroundBrush";

        // Borders
        public static string MinimizeIconBorderKey = "MinimizeIconBorder";
        public static string MinimizeDisabledIconBorderKey = "MinimizeDisabledIconBorder";
        public static string MaximizeIconBorderKey = "MaximizeIconBorder";
        public static string MaximizeDisabledIconBorderKey = "MaximizeDisabledIconBorder";
        public static string ChannelViewBorderStyleKey = "ChannelViewBorder";

        // Icons
        public static string CloseIconKey = "CloseIcon";

        public static string VideoIconKey = "VideoIcon";
        public static string AudioIconKey = "AudioIcon";
        public static string TimecodeIconKey = "TimecodeIcon";
        public static string DataIconKey = "DataIcon";

        public static string DetailsViewIconKey = "DetailsViewIcon";
        public static string TilesViewIconKey = "TilesViewIcon";
        public static string SmallThumbnailsViewIconKey = "SmallThumbnailsViewIcon";
        public static string MediumThumbnailsViewIconKey = "MediumThumbnailsViewIcon";
        public static string LargeThumbnailsViewIconKey = "LargeThumbnailsViewIcon";

        public static string RefreshIconKey = "RefreshIcon";

        // Canvases
        public static string RestoreIconCanvasKey = "RestoreIconCanvas";
        public static string RestoreDisabledIconCanvasKey = "RestoreDisabledIconCanvas";

        // Control Templates
        public static string PropertyErrorTemplateKey = "PropertyErrorTemplate";
        public static string AssetListItemTemplateKey = "AssetListItemTemplate";

        // Data Templates
        public static string PauseImageDataTemplateKey = "PauseImageDataTemplate";
        public static string StopImageDataTemplateKey = "StopImageDataTemplate";
        public static string NewClipImageDataTemplateKey = "NewClipImageDataTemplate";
        public static string CueStartImageDataTemplateKey = "CueStartImageDataTemplate";
        public static string PlayImageDataTemplateKey = "PlayImageDataTemplate";
        public static string RecordImageDataTemplateKey = "RecordImageDataTemplate";
        public static string EjectImageDataTemplateKey = "EjectImageDataTemplate";
        public static string LoopImageDataTemplateKey = "LoopImageDataTemplate";
        public static string IndependentLoopImageDataTemplateKey = "IndependentLoopImageDataTemplate";

        // Drag/Drop Highlight Adorner
        public static string DragHighlightAdornerBrushKey = "DragHighlightAdornerBrush";
        public static string DragHighlightAdornerCornerRadiusKey = "DragHighlightAdornerCornerRadius";

        public static string ListViewHeaderTemplateArrowUpKey = "ListViewHeaderTemplateArrowUp";
        public static string ListViewHeaderTemplateArrowDownKey = "ListViewHeaderTemplateArrowDown";
        public static string ListViewHeaderTemplateArrowNeutralKey = "ListViewHeaderTemplateArrowNeutral";

        // Docking
        public static string ToolWindowDataTemplateKey = "ToolWindowDataTemplate";


        // Appended...
        public static string ToolBarButtonStyleKey = "ToolBarButtonStyleKey";
        public static string NotSelectableListViewItemStyleKey = "NotSelectableListViewItemStyleKey";
        public static string ToolBarSeparatorStyleKey = "ToolBarSeparatorStyleKey";

        //  TODO
    }
}
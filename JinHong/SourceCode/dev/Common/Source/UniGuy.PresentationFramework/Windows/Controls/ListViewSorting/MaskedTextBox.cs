using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace UniGuy.Windows.Controls
{
    /// <summary>
    /// Masked textbox control is a text box that can have a mask for the text.
    /// </summary>
    public class MaskedTextBox : TextBox
    {
        #region DependencyProperties
        /// <summary>
        /// Dependency property to store the mask to apply to the textbox.
        /// </summary>
        public static readonly DependencyProperty MaskProperty
            = DependencyProperty.Register("Mask", typeof(string), typeof(MaskedTextBox), new UIPropertyMetadata(null, AsMaskChanged));
        /// <summary>
        /// Callback for when the Mask property is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void AsMaskChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.RefreshText(textBox.MaskProvider, 0);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the MaskTextProvider for the specified Mask.
        /// </summary>
        public MaskedTextProvider MaskProvider
        {// TODO if creating a MaskedTextProvider each time is necessary?
            get
            {
                if (Mask != null)
                {
                    MaskedTextProvider maskProvider = new MaskedTextProvider(Mask);
                    maskProvider.Set(Text);
                    return maskProvider;
                }
                return null;
            }
        }
        /// <summary>
        /// Gets or sets the mask to apply to the textbox.
        /// </summary>
        public string Mask
        {
            //	Make sure to update the text if the mask changes. I think this is omittable if MaskProperty is set tobe AffectRender.
            set { SetValue(MaskProperty, value); }
            get { return (string)GetValue(MaskProperty); }
        }
        #endregion

        /// <summary>
        /// Static Constructor
        /// </summary>
        static MaskedTextBox()
        {
            //	Override the meta data for the Text Property of the textbox.
            FrameworkPropertyMetadata metaData = new FrameworkPropertyMetadata();
            metaData.CoerceValueCallback = AsCoerceValue;
            TextProperty.OverrideMetadata(typeof(MaskedTextBox), metaData);
        }

        /// <summary>
        /// Force the text of the control to use the mask.
        /// </summary>
        private static object AsCoerceValue(DependencyObject sender, object value)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            if (textBox.Mask != null)
            {
                MaskedTextProvider provider = new MaskedTextProvider(textBox.Mask);
                provider.Set((string)value);
                return provider.ToDisplayString();
            }
            return value;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MaskedTextBox()
        {
            //	Cancel the paste and cut command.
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, null, CancelCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, CancelCommand));
        }

        //	Cancel the command.
        private static void CancelCommand(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = false;
            args.Handled = true;
        }

        #region Overrides
        /// <summary>
        /// Override this method to replace the characters entered with the mask
        /// </summary>
        protected override void OnPreviewTextInput(TextCompositionEventArgs args)
        {
            //	If the text is readonly do not add the text.
            if (IsReadOnly)
            {
                args.Handled = true;
                return;
            }
            int position = SelectionStart;
            Text = Text.Remove(this.SelectionStart, this.SelectionLength);
            MaskedTextProvider provider = MaskProvider;
            if (position < Text.Length)
            {
                position = GetNextCharacterPosition(position);
                if (Keyboard.IsKeyToggled(Key.Insert))
                {
                    if (provider.Replace(args.Text, position))
                        position++;
                }
                else
                {
                    if (provider.InsertAt(args.Text, position))
                        position++;
                }
                position = GetNextCharacterPosition(position);
            }

            RefreshText(provider, position);
            args.Handled = true;

            base.OnPreviewTextInput(args);
        }
        /// <summary>
        /// Override the key down to handle delete of a character
        /// </summary>
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);
            MaskedTextProvider provider = MaskProvider;
            int position = SelectionStart;
            if (args.Key == Key.Delete && position < Text.Length)//handle the delete key
            {
                if (provider.RemoveAt(position))
                    RefreshText(provider, position);

                args.Handled = true;
            }

            else if (args.Key == Key.Space)
            {
                if (provider.InsertAt(" ", position))
                    RefreshText(provider, position);
                args.Handled = true;
            }

            else if (args.Key == Key.Back)//handle the back space
            {
                if (position > 0)
                {
                    position--;
                    if (provider.RemoveAt(position))
                        RefreshText(provider, position);
                }
                args.Handled = true;
            }
        }
        #endregion

        #region Helper Methods
        //	Refresh the text of the textbox.
        private void RefreshText(MaskedTextProvider provider, int position)
        {
            Text = provider.ToDisplayString();
            SelectionStart = position;
        }
        //	Gets the next position in the textbox to move
        private int GetNextCharacterPosition(int startPosition)
        {
            int position = MaskProvider.FindEditPositionFrom(startPosition, true);
            if (position == -1)
                return startPosition;
            else
                return position;
        }
        #endregion
    }
}
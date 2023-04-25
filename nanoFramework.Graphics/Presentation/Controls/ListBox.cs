//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;
using nanoFramework.UI.Input;

namespace nanoFramework.Presentation.Controls
{
    /// <summary>
    /// Represents a control that displays a list of items, where an item in the list can be selected.
    /// </summary>
    public class ListBox : ContentControl
    {
        internal ScrollViewer _scrollViewer;
        internal StackPanel _panel;
        private int _selectedIndex = -1;
        private SelectionChangedEventHandler _selectionChanged;

        private ListBoxItemCollection _items;

        /// <summary>
        /// Initializes a new instance of the ListBox class.
        /// </summary>
        public ListBox()
        {
            _panel = new StackPanel();
            _scrollViewer = new ScrollViewer();
            _scrollViewer.Child = _panel;
            this.LogicalChildren.Add(_scrollViewer);
        }

        /// <summary>
        /// Gets the collection of items in the ListBox.
        /// </summary>
        public ListBoxItemCollection Items
        {
            get
            {
                VerifyAccess();

                if (_items == null)
                {
                    _items = new ListBoxItemCollection(this, _panel.Children);
                }

                return _items;
            }
        }

        /// <summary>
        /// Occurs when the selection of a ListBox item changes.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add
            {
                VerifyAccess();
                _selectionChanged += value;
            }

            remove
            {
                VerifyAccess();
                _selectionChanged -= value;
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected item in a ListBox.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                VerifyAccess();

                if (_selectedIndex != value)
                {
                    if (value < -1)
                    {
                        throw new ArgumentOutOfRangeException("SelectedIndex");
                    }

                    ListBoxItem item = (_items != null && value >= 0 && value < _items.Count) ? _items[value] : null;

                    if (item != null && !item.IsSelectable)
                    {
                        throw new InvalidOperationException("Item is not selectable");
                    }

                    ListBoxItem previousItem = SelectedItem;
                    if (previousItem != null)
                    {
                        previousItem.OnIsSelectedChanged(false);
                    }

                    SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selectedIndex, value);
                    _selectedIndex = value;

                    if (item != null)
                    {
                        item.OnIsSelectedChanged(true);
                    }

                    if (_selectionChanged != null)
                    {
                        _selectionChanged(this, args);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the currently selected item in a ListBox.
        /// </summary>
        public ListBoxItem SelectedItem
        {
            get
            {
                if (_items != null && _selectedIndex >= 0 && _selectedIndex < _items.Count)
                {
                    return _items[_selectedIndex];
                }

                return null;
            }

            set
            {
                VerifyAccess();

                int index = Items.IndexOf(value);
                if (index != -1)
                {
                    SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// Scrolls the ListBox to bring the specified ListBoxItem into view.
        /// </summary>
        /// <param name="item">The ListBoxItem to bring into view.</param>
        public void ScrollIntoView(ListBoxItem item)
        {
            VerifyAccess();

            if (!Items.Contains(item)) return;

            int panelX, panelY;
            _panel.GetLayoutOffset(out panelX, out panelY);

            int x, y;
            item.GetLayoutOffset(out x, out y);

            int top = y + panelY;
            int bottom = top + item._renderHeight;

            // Make sure bottom of item is in view
            //
            if (bottom > _scrollViewer._renderHeight)
            {
                _scrollViewer.VerticalOffset -= (_scrollViewer._renderHeight - bottom);
            }

            // Make sure top of item is in view
            //
            if (top < 0)
            {
                _scrollViewer.VerticalOffset += top;
            }
        }

        /// <summary>
        /// Called when a button is pressed down. If the button is VK_DOWN and the currently selected item
        /// is not the last item, the selection is moved down to the next selectable item. If the button is
        /// VK_UP and the currently selected item is not the first item, the selection is moved up to the
        /// previous selectable item.
        /// </summary>
        /// <param name="e">The ButtonEventArgs containing information about the button press.</param>
        protected override void OnButtonDown(ButtonEventArgs e)
        {
            if (e.Button == Button.VK_DOWN && _selectedIndex < Items.Count - 1)
            {
                int newIndex = _selectedIndex + 1;
                while (newIndex < Items.Count && !Items[newIndex].IsSelectable) newIndex++;

                if (newIndex < Items.Count)
                {
                    SelectedIndex = newIndex;
                    ScrollIntoView(SelectedItem);
                    e.Handled = true;
                }
            }
            else if (e.Button == Button.VK_UP && _selectedIndex > 0)
            {
                int newIndex = _selectedIndex - 1;
                while (newIndex >= 0 && !Items[newIndex].IsSelectable) newIndex--;

                if (newIndex >= 0)
                {
                    SelectedIndex = newIndex;
                    ScrollIntoView(SelectedItem);
                    e.Handled = true;
                }
            }
        }

        //
        // Scrolling events re-exposed from the ScrollViewer
        //

        /// <summary>
        /// Occurs when the scroll position changes.
        /// </summary>
        public event ScrollChangedEventHandler ScrollChanged
        {
            add { _scrollViewer.ScrollChanged += value; }
            remove { _scrollViewer.ScrollChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the horizontal scroll offset.
        /// </summary>
        public int HorizontalOffset
        {
            get
            {
                return _scrollViewer.HorizontalOffset;
            }

            set
            {
                _scrollViewer.HorizontalOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the vertical scroll offset.
        /// </summary>
        public int VerticalOffset
        {
            get
            {
                return _scrollViewer.VerticalOffset;
            }

            set
            {
                _scrollViewer.VerticalOffset = value;
            }
        }

        /// <summary>
        /// Gets the extent height of the scrollable content.
        /// </summary>
        public int ExtentHeight
        {
            get
            {
                return _scrollViewer.ExtentHeight;
            }
        }

        /// <summary>
        /// Gets the extent width of the scrollable content.
        /// </summary>
        public int ExtentWidth
        {
            get
            {
                return _scrollViewer.ExtentWidth;
            }
        }

        /// <summary>
        /// Gets or sets the scrolling behavior.
        /// </summary>
        public ScrollingStyle ScrollingStyle
        {
            get
            {
                return _scrollViewer.ScrollingStyle;
            }

            set
            {
                _scrollViewer.ScrollingStyle = value;
            }
        }
    }
}

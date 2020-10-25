using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static TreeViewItem currentItem = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Global values
        // create a string variable holding all Alphabetical characters to use to assing name to tree view items
        string itemNameChars = "abcdefghijklmnopqrstuvwxyz";

        // add a counter to tell you how many itemNameChars have been used
        int NumberOfItems = 0;

        // get the nane of the parent of a new node
        string  parentName = "";

        // store the current rootName
        int  currentRootNum = 0;
        #endregion


        /// <summary>
        /// Add a new root node whose header is the text in the itemName textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRoot_Click(object sender, RoutedEventArgs e)
        {
            // Create a new tree view item which will become a new root item
            TreeViewItem root = new TreeViewItem();

            // Assign the text in the itemName text box to its header property
            root.Header = itemName.Text;

            char nameChar = itemNameChars[currentRootNum];

            string nameString = nameChar.ToString();
            currentRootNum++;


            root.Name = nameString;
            root.Tag = false;
            ItemsTree.Items.Add(root);
            itemName.Text = "";
        }

        /// <summary>
        /// returns a 2 character alphabetic string
        /// </summary>
        /// <returns></returns>
        private string getItemName(int numberOfParentItems)
        {
            // initialize the name string to return
            string itemName = "";

            // Create dummy chars to hold the first and second char
            var firstChar = '+';
            var secondChar = '-';

            //determine the character position of the first and sedcond char
            int firstCharNumber = numberOfParentItems / 26;
            int secondCharNumber = numberOfParentItems % 26;

            firstChar = itemNameChars[firstCharNumber];
            secondChar = itemNameChars[secondCharNumber];
            char[] chars = { firstChar, secondChar };
            
            itemName = new string(chars);

            return itemName;
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            if(itemName.Text == null || itemName.Text == "")
            {
                MessageBox.Show("To add an item, first enter a name in the textbox then double click the desired parent");
            }
           
        }

        private void ItemsTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //make a treeviewitme from the node double clicked
            TreeViewItem parent = (TreeViewItem)ItemsTree.SelectedItem;

            parentName = parent.Name;
            int numberOfChildren = 0;
            //int numChildren = parent
            string parentHeader = parent.Header.ToString();
            TreeViewItem child = new TreeViewItem();
            child.Header = itemName.Text;
            var parentsNumberOfChildItems = parent.Items.Count;

            string childName =  getItemName(parentsNumberOfChildItems);
            child.Name = childName;
            parent.Items.Add(child);
            MessageBox.Show($"You created a child item whose header is {child.Header} and whose name is {child.Name}");

            itemName.Text = "";

        }

        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /

            //TEST
             TreeViewItem tvi1= (TreeViewItem)ItemsTree.ItemContainerGenerator.ContainerFromItem(​SelectedItem) ;
            //endTest

            TreeViewItem tvi = (TreeViewItem)e.NewValue;
            string thisNode = (string)tvi.Header;

           
           currentItem = tvi;
            MessageBox.Show(((TreeViewItem)e.NewValue).Header.ToString());
        }
    }
}

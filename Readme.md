<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [CarInfo.cs](./CS/Model/CarInfo.cs) (VB: [CarInfo.vb](./VB/Model/CarInfo.vb))
* [ObservableObject.cs](./CS/ObservableObject.cs) (VB: [ObservableObject.vb](./VB/ObservableObject.vb))
* [EditView.xaml](./CS/View/EditView.xaml) (VB: [EditView.xaml](./VB/View/EditView.xaml))
* [EditView.xaml.cs](./CS/View/EditView.xaml.cs) (VB: [EditView.xaml.vb](./VB/View/EditView.xaml.vb))
* [CarInfoViewModel.cs](./CS/ViewModel/CarInfoViewModel.cs) (VB: [CarInfoViewModel.vb](./VB/ViewModel/CarInfoViewModel.vb))
<!-- default file list end -->
# DXRichEdit for WPF: Implementing MVVM


<p>This example illustrates how to implement a MVVM pattern (see <a href="http://en.wikipedia.org/wiki/Model_View_ViewModel"><u>Model View ViewModel</u></a>) in an application for RTF text editing. Model is represented by the <strong>CarInfo</strong> class with the <strong>Name</strong> and <strong>Description</strong> properties. The <strong>CarInfoViewModel</strong> class represents a view model. It wraps the model object and exposes its properties to the view which is represented by the <strong>EditView</strong> class. The view model implements the <a href="http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged.aspx"><u>INotifyPropertyChanged Interface</u></a> for the properties it exposes so that the view can easily data bind to them. The actual data binding is defined in the EditView.xaml file via a XAML binding of the <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfRichEditRichEditControl_Contenttopic"><u>RichEditControl.Content Property</u></a> to the <strong>Description</strong> property of a view model, which in turn, is defined in the UserControl's data context:</p>

```xml
<UserControl x:Class="RichEditMVVMScenarioWpf.View.EditView" ...><newline/>
   <UserControl.DataContext><newline/>
       <vm:CarInfoViewModel /><newline/>
   </UserControl.DataContext><newline/>
   <UserControl.Resources><newline/>
       <dxre:RtfToContentConverter x:Key="rtfToContentConverter" /><newline/>
   </UserControl.Resources><newline/>
       ...<newline/>
       <dxre:RichEditControl Name="reDescription" Content="{Binding Description, Converter={StaticResource rtfToContentConverter}, Mode=TwoWay}" /><newline/>
</UserControl>
```

<p> </p><p>Note that the <a href="http://documentation.devexpress.com/#WPF/clsDevExpressXpfRichEditRtfToContentConvertertopic"><u>RtfToContentConverter</u></a> is used for binding. See the <a href="https://www.devexpress.com/Support/Center/p/E3490">DXRichEdit for WPF: How to use RichEdit converters to bind a RichEditControl to a particular entity and vice versa</a> example to learn more on this concept.</p><p>The view model also contains <strong>Load </strong>and <strong>Save</strong> commands that are used to persist model property values in the database. This logic is implemented via regular <a href="http://msdn.microsoft.com/en-us/library/h43ks021(v=vs.100).aspx"><u>ADO.NET</u></a> methods.</p><p>Prior to running this example, it is required to register a "CarsDB" sample database on your local SQL Server instance. You can download the corresponding SQL scripts from the <a href="https://www.devexpress.com/Support/Center/p/E3480">How to use a RichEditControl in bound mode</a> example.</p><p>The picture below illustrates the sample in action.</p><p><img src="https://raw.githubusercontent.com/DevExpress-Examples/dxrichedit-for-wpf-implementing-mvvm-e3497/13.2.5+/media/89b5f043-aad4-4b34-9815-1f1f932e22a1.png"></p><p><strong>See also:</strong><br />
<a href="http://msdn.microsoft.com/en-us/magazine/dd419663.aspx"><u>WPF Apps With The Model-View-ViewModel Design Pattern</u></a><br />
<a href="http://www.codeproject.com/Articles/165368/WPF-MVVM-Quick-Start-Tutorial"><u>WPF/MVVM Quick Start Tutorial</u></a></p>

<br/>



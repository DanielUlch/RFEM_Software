   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbo<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  ]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure E    ?RFEMSoftware.Simulation.Desktop.CustomControls.SoilDistribution   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Nlocal<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     Dform3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     -   $��
DistParent��  � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn �� Property �� Value �� ElementName �� Path  RegularDistributionEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type9  3RFEMSoftware.Simulation.Infrastructure.Distribution  FrictionAngleEnum:  4RFEMSoftware.Simulation.Infrastructure.FrictionAngle;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverterC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter��%D  '��       7   '�� S     7   &	 �     &	 �     &	 �     &	 �     �� " ��8   .���  �@�Tag �� $ 
DistParent��$ DistributionTag��� " ��8   .���  �@�Tag �� $ 
DistParent��$ DistributionTag�S� $ 	GetValues��"  	 M� "
  S� $ 	GetValues��"  	 M� "
      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� -   $��lbDist������ $ 
DistParent��$ DistributionLabel��� -   $��cboDist������  �� Source# [  �� SelectedValue �� $ 
DistParent��$# Distribution.DistributionType� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� .R��� ����  �� 	Converter# [ �� $ 
Horizontal=����� " ��.���  � y�$ 	Collapsed=� �� Triggers ��  �� Binding �� $ 
DistParent��$ IsFrictionAngle� �� Value True.a���  � y�$ Visible=�.+��� -   $��lbFrictionAngleType��$$��Selected distribution governs:���� -   $��cboFrictionAngleType������ # [  �� $ 
DistParent��$ FrictionAngleType�$ PropertyChanged=�$ TwoWay=����� .R��� ���� # [ �� $ Vertical=�$��30 0 0 0q�.+��� $ 
Horizontal=�.+��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��    Bounded# Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=�.+��� -   $��lbMean��$��Mean:���� -   $��txtMean������ $ 
DistParent��$ Distribution.Mean�$ PropertyChanged=� �� ValidatesOnDataErrors . # [ 
 � Tag �� $ 
DistParent��$ DistributionTag��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��    Deterministic# Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=��� # Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=�.+��� -   $��lbStdDev��$��Standard deviation:���� -	   $��	txtStdDev������ $ 
DistParent��$$ Distribution.StandardDeviation�$ PropertyChanged=� . # [  �� $ 
DistParent��$ DistributionTag��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� �� # Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=���    Normal# Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=���    	Lognormal# Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=�.+��� -
   $��lbLowerBound��$��Lower bound:���� -   $��txtLowerBound������ $ 
DistParent��$ Distribution.LowerBound�$ PropertyChanged=� . # [  �� $ 
DistParent��$ DistributionTag��� -   $��lbUpperBound��$��Upper bound:���� -   $��txtUpperBound������ $ 
DistParent��$ Distribution.UpperBound�$ PropertyChanged=� . # [  �� $ 
DistParent��$ DistributionTag��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� �� # Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=��� # Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=��� # Z  �� $ cboDist��$ SelectedValue�.a���  � y�$ 	Collapsed=�.+��� -   $��
lbLocation��$��	Location:���� -   $��txtLocation������ $ 
DistParent��$ Distribution.Location�$ PropertyChanged=� . # [  �� $ 
DistParent��$ DistributionTag��� -   $��lbScale��$��Scale:���� -   $��txtScale������ $ 
DistParent��$ Distribution.Scale�$ PropertyChanged=� . # [  �� $ 
DistParent��$ DistributionTag�
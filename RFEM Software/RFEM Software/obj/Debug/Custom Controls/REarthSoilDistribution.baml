   M S B A M L   `   `   `  ����X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbo<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  5      ]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure 5	      K    ERFEMSoftware.Simulation.Desktop.CustomControls.REarthSoilDistribution   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     5      Dform3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     5      6sys&clr-namespace:System;assembly=mscorlib     5      ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     5	      -   5        $��
DistParent��5        � 	Resources  5       �� 
TargetType   HelpEnabled �� BasedOn �� Property �� Value �� ElementName �� Path  REarthDistributionEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type@  :RFEMSoftware.Simulation.Infrastructure.REarthDistributions  PhiFuncEnum?  9RFEMSoftware.Simulation.Infrastructure.REarthPhiFunctions;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverterC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter��%  '��       7   '�� x     7   &	 �     &	 K    &	 �    &	 �    �� 5   
   " ��6   8   .�5      ��  �@�Tag 6$   �� $ 
DistParent��$ DistributionTag�5      �� 5   
   " ��6   8   .�5      ��  �@�Tag 6$   �� $ 
DistParent��$ DistributionTag�5      S� 5   
   $ 	GetValues��6<   "  6S   	 5      M� 5      "
  6   5      S� 5   
   $ 	GetValues��61   "  6H   	 5      M� 5      "
  6   5         5"   
   6     5#   
   6'   5%      .��5(      � .+�5)   
   ��  �� Orientation$ Vertical=�6   .+�5+      �� $ 
Horizontal=�6   .+�5-      �� -   5        $��lbDist��5-      ��5.      �� $ 
DistParent��$ DistributionLabel�50      �� 51      -   5        $��cboDist��51      ��52      ��  �� Source# [  �� SelectedValue 53      �� $ 
DistParent��$# Distribution.DistributionType� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���55      �� 56      .R�57      �� ��6(   ��  �� 	Converter# [ 58      5:      5<      �� 5>      $ Vertical=�6   $��30 0 0 0q�60   .+�5@      �� $ 
Horizontal=�6   .+�5B      �� $ 
Horizontal=�6!   ��5C      �� 5D      " ��6$   .�5E   "   ��  � y�$ Visible=�6?    �� Triggers 5F   "   �� 5G   &      Bounded �� Value# Z 5H   .    �� Binding 5G   2   �� $ cboDist��$ SelectedValue�.a�5I   *   ��  � y�$ 	Collapsed=�6G   5J   '   �� 5K   &      fphi# Z 5L   .    5K   2   �� $ cboDist��$ SelectedValue�.a�5M   *   ��  � y�$ 	Collapsed=�6G   5N   '   5P      .+�5S      �� -   5        $��lbMean��5S       $��Mean:��5T       �� 5U      -   5        $��txtMean��5U   "   ��5V   "   �� $ 
DistParent��$ Distribution.Mean�$ PropertyChanged=� �� ValidatesOnDataErrors . # [ 
 � Tag 5[   "   �� $ 
DistParent��$ DistributionTag�5\      �� 5^      $ 
Horizontal=�6!   ��5_      �� 5`      " ��6$   .�5a   "   ��  � y�$ Visible=�6?    5b   "   �� 5c   &      Deterministic# Z 5d   .    5c   2   �� $ cboDist��$ SelectedValue�.a�5e   *   ��  � y�$ 	Collapsed=�6G   5f   '   �� 5g   &   # Z 5h   .    5g   2   �� $ cboDist��$ SelectedValue�.a�5i   *   ��  � y�$ 	Collapsed=�6G   5j   '   �� 5k   &   # Z 5l   .    5k   2   �� $ cboDist��$ SelectedValue�.a�5m   *   ��  � y�$ 	Collapsed=�6G   5n   '   5p      .+�5s      �� -   5        $��lbStdDev��5s       $��Standard deviation:��5t       �� 5u      -   5        $��	txtStdDev��5u   "   ��5v   "   �� $ 
DistParent��$$ Distribution.StandardDeviation�$ PropertyChanged=� . # [  5{   "   �� $ 
DistParent��$ DistributionTag�5}      5      �� 5�      $ Vertical=�6   ��5�      �� 5�      " ��6    .�5�      ��  � y�$ Visible=�6;    5�      �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "      Normal# Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "      	Lognormal# Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 5�   .    5�   .   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   5�      .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� -   5        $��lbLowerBound��5�       $��Lower bound:��5�      �� 5�      -	   5        $��txtLowerBound��5�   "   ��5�      �� $ 
DistParent��$ Distribution.LowerBound�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag��� 5�      -
   5        $��lbUpperBound��5�       $��Upper bound:��5�      �� 5�      -   5        $��txtUpperBound��5�   "   ��5�      �� $ 
DistParent��$ Distribution.UpperBound�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag�5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� -   5        $��
lbLocation��5�       $��	Location:��5�      �� 5�      -   5        $��txtLocation��5�   "   ��5�      �� $ 
DistParent��$ Distribution.Location�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag��� 5�      -   5        $��lbScale��5�       $��Scale:��5�      �� 5�      -   5        $��txtScale��5�   "   ��5�      �� $ 
DistParent��$ Distribution.Scale�$ PropertyChanged=� . # [  5�      �� $ 
DistParent��$ DistributionTag�5�      5�      �� 5�      $ Vertical=�6   ��5�      �� 5�      " ��6    .�5�      ��  � y�$ Visible=�6;    5�      �� 5�   "   # Z 6j    6.   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 6j    6.   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 6j    6.   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   �� 5�   "   # Z 6j    6.   �� $ cboDist��$ SelectedValue�.a�5�   &   ��  � y�$ 	Collapsed=�6C   5�   #   5�      .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� -   5        $��lbIntercept��5�       $��Intercept a:��5�       �� 5�      -   5        $��txtIntercept��5�   "   ��5�   "   �� $ 
DistParent��$ Distribution.Intercept�$ PropertyChanged=� . # [  5�   !   �� $ 
DistParent��$ DistributionTag��� 5�      -   5        $��lbSlope��5�       $��Slope b:��5�       �� 5�      -   5        $��txtSlope��5�   "   ��5�   "   �� $ 
DistParent��$ Distribution.Slope�$ PropertyChanged=� . # [  5�   !   �� $ 
DistParent��$ DistributionTag�5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� -   5        $��	lbPhiFunc��5�       $��f(phi):��5�       �� 5      -   5        $��
cboPhiFunc��5   #   ��5  #   �� # [  5  #   �� $ 
DistParent��$ Distribution.PhiFunc�$ PropertyChanged=�$ TwoWay=���5     �� 5  "   .R�5  &   �� ��60   �� # [ 5  #   5     5
     5     5     5     5     5     
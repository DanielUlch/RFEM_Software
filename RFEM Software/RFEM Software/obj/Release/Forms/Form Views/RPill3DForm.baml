   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  7    1RFEMSoftware.Simulation.Desktop.Forms.RPill3DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  SoilPropertyEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeA  ;RFEMSoftware.Simulation.Infrastructure.REarthSoilProperties  AxisEnum1  +RFEMSoftware.Simulation.Infrastructure.Axis  ElementTypeEnum?  9RFEMSoftware.Simulation.Infrastructure.RPill3DElementType  covFunction3DEnumA  ;RFEMSoftware.Simulation.Infrastructure.CovarianceFunction3DC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%w  '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     &	 �     &	 �     &	 �     &	 �     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� -   $��
lbJobTitle��$��
Job Title:��
 � Tag$ JobTitle���� -   $��txtJobTitle��$ JobTitle��$	��200������ 	 �� Path$	 JobTitle�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� ValidatesOnDataErrors . �� $ 
Horizontal=�.+��� -   $��
lbBaseName��$ BaseName��$��
Base Name:���� -   $��txtBaseName��$ BaseName��$	��200������ $	 BaseName�$
 PropertyChanged=� . �� -   $��chkEchoInput��$ EchoInputToOutputFile��$+��%Echo input data to output *.stt file?�� d� 	IsChecked �� $	 EchoInputDataToOutputFile�$
 PropertyChanged=��� -   $��chkOutputDebugData��$ OutputDebugInformation��$0��*Write debug data to the output *.stt file?�� �� $	 OutputDebugInfo�$
 PropertyChanged=��� -   $��chkPlotFirstRF��$ RPill3DPlotFirstRF��$��Plot first random field?�� �� $	 PlotFirstRF�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$ chkPlotFirstRF��$	 	IsChecked� �� Value	 False.a���  � y�$ 	Collapsed=�.+��� -   $��lbFirstRFPropertyToPlot��$ RPill3DPlotFirstRF��$��Property to plot:���� -	   $��cboFirstRFPropertyToPlot��$ RPill3DPlotFirstRF������  �� Source# [  �� SelectedValue �� $	 FirstRFPropertyToPlot�$
 PropertyChanged=� �� Mode$ TwoWay=����� .R��� ����  �� 	Converter# [ �� -
   $��lbFirstRFNodeIndexToPlot��$ RPill3DPlotFirstRF��$��Plot node index number:���� -   $��txtFirstRFNodeIndexToPlot��$ RPill3DPlotFirstRF������ $	 FirstRFNodeIndexToPlot�$
 PropertyChanged=� . �� -   $&�� lbFirstRFPerpindicularToThisAxis��$ RPill3DPlotFirstRF��$��Perpendicular to:���� -   $'��!cboFirstRFPerpindicularToThisAxis��$ RPill3DPlotFirstRF������ # [  �� $$	 FirstRFPerpindicularToThisAxis�$
 PropertyChanged=�$ TwoWay=����� .R��� ���� # [ �� -   $'��!chkNormalizePillarCapacitySamples��$+ %RPill3DNormalizePillarCapacitySamples��$(��"Normalize pillar capacity samples?�� �� $$	 NormalizePillarCapacitySamples�$
 PropertyChanged=��� -   $��chkOutputCapacitySamples��$" RPill3DOutputCapacitySamples��$%��Output pillar capacity samples?�� �� $	 OutputCapacitySamples�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��lbNumberOfElements��$ RPill3DNumberOfElements��$��Number of elements:���� -   $��txtNElementsInXDir��$ RPill3DNumberOfElements������ $	 NElementsInXDir�$
 PropertyChanged=� . �� -   $��lbNElementsInXDir��$ RPill3DNumberOfElements��$��in x-direction���� -   $��txtNElementsInYDir��$ RPill3DNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -   $��lbNElementsInYDir��$ RPill3DNumberOfElements��$��in y-direction���� -   $��txtNElementsInZDir��$ RPill3DNumberOfElements������ $	 NElementsInZDir�$
 PropertyChanged=� . �� -   $��lbNElementsInZDir��$ RPill3DNumberOfElements��$��in z-direction���� $ 
Horizontal=�.+��� -   $��lbElementSize��$ RPill3DElementSize��$��Element size:���� -   $��txtElementSizeInXDir��$ RPill3DElementSize������ $	 ElementSizeInXDir�$
 PropertyChanged=� . # [ �� -   $��lbElementSizeInXDir��$ RPill3DElementSize��$��in x-direction���� -   $��txtElementSizeInYDir��$ RPill3DElementSize������ $	 ElementSizeInYDir�$
 PropertyChanged=� . # [ �� -   $��lbElementSizeInYDir��$ RPill3DElementSize��$��in y-direction���� -   $��txtElementSizeInZDir��$ RPill3DElementSize������ $	 ElementSizeInZDir�$
 PropertyChanged=� . # [ �� -   $��lbElementSizeInZDir��$ RPill3DElementSize��$��in z-direction���� $ 
Horizontal=�.+��� -   $��lbElementType��$ RPill3DElementType��$��Element type:���� -   $��cboElementType��$ RPill3DElementType������ # [  �� $	 ElementType�$
 PropertyChanged=�$ TwoWay=����� .R��� ���� # [ �� $ 
Horizontal=�.+��� -    $��lbDisplacementInc��$ RPill3DDisplacementInc��$��Displacement inc:���� -!   $��txtDisplacementInc��$ RPill3DDisplacementInc������ $	 DisplacementInc�$
 PropertyChanged=� . # [ �� -"   $��lbPlasticTol��$ RPill3DDisplacementInc��$��Plastic tol:���� -#   $��txtPlasticTol��$ RPill3DDisplacementInc������ $	 
PlasticTol�$
 PropertyChanged=� . # [ �� -$   $��lbBearingTol��$ RPill3DDisplacementInc��$��Bearing tol:���� -%   $��txtBearingTol��$ RPill3DDisplacementInc������ $	 
BearingTol�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -&   $��lbMaxNumSteps��$ RPill3DMaxNumSteps��$��Max number of steps:���� -'   $��txtMaxNumSteps��$ RPill3DMaxNumSteps������ $	 MaxNumSteps�$
 PropertyChanged=� . �� -(   $��lbMaxNumIterations��$ RPill3DMaxNumSteps��$��Max number of iterations:���� -)   $��txtMaxNumIterations��$ RPill3DMaxNumSteps������ $	 MaxNumIterations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -*   $��lbNumberOfRealizations��$! RPill3DNumberOfRealizations��$��Number of simulations:���� -+   $��txtNumberOfRealizations��$! RPill3DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -,   $��lbGeneratorSeed��$ RPill3DGeneratorSeed��$��Generator seed:���� --   $��txtGeneratorSeed��$ RPill3DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -.   $��lbCorrelationLengths��$ RPill3DCorrelationLengths��$��Correlation lengths:���� -/   $ ��txtCorrelationLengthInXDir��$ RPill3DCorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -0   $��lbCorrelationLengthInXDir��$ RPill3DCorrelationLengths��$��in x-direction���� -1   $ ��txtCorrelationLengthInYDir��$ RPill3DCorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -2   $��lbCorrelationLengthInYDir��$ RPill3DCorrelationLengths��$��in y-direction���� -3   $ ��txtCorrelationLengthInZDir��$ RPill3DCorrelationLengths������ $	 CorrelationLengthInZDir�$
 PropertyChanged=� . �� -4   $��lbCorrelationLengthInZDir��$ RPill3DCorrelationLengths��$��in z-direction���� $ 
Horizontal=�.+��� -5   $��	lbCovFunc��$ RPill3DCovFunc��$��Covariance function:���� -6   $��
cboCovFunc��$ RPill3DCovFunc������ # [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=�E   ?RFEMSoftware.Simulation.Desktop.CustomControls.SoilDistribution     DistributionLabel$ Cohesion distribution:��   DistributionTag$ RPill3DCohesionDist��   Distribution �� $	 Cohesion�$
 PropertyChanged=�  $" Friction angle distribution:��$ RPill3DFrictionAngleDist��   IsFrictionAngle .  �� $	 FrictionAngle�$
 PropertyChanged=�   FrictionAngleType �� $	 FrictionAngleType�$
 PropertyChanged=�$ TwoWay=�  $" Dilation angle distribution:��$ RPill3DDilationAngleDist�� �� $	 DilationAngle�$
 PropertyChanged=�  $# Elastic modulus distribution:��$ RPill3DElasticModulusDist�� �� $	 ElasticModulus�$
 PropertyChanged=�  $# Poisson's ratio distribution:��$ RPill3DPoissonsRatioDist�� �� $	 PoissonsRatio�$
 PropertyChanged=��� -7   $��lbCorrelationMatrix��$ RPill3DCorrelationMatrix��$0��*Soil property correlation matrix elements:���  � ColumnDefinitions �� $
��auto ��� $��* �.+�&	   System.Windows.Controls.DataGrid	  -8   $��dgCorrelationMatrix��$ RPill3DCorrelationMatrix�� 	  AutoGenerateColumns . $��0�� 	  CanUserAddRows .   	  CanUserDeleteRows .    	  CanUserReorderColumns  .  ! 	  CanUserSortColumns! .  $
��auto��
   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  7    1RFEMSoftware.Simulation.Desktop.Forms.RPill2DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  SoilPropertyEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeA  ;RFEMSoftware.Simulation.Infrastructure.REarthSoilProperties  covFunctionEnum?  9RFEMSoftware.Simulation.Infrastructure.CovarianceFunctionC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%  '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     &	 �     &	 �     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��chkPlotFirstRF��$ RPill2DPlotFirstRF��$��Plot first random field?�� �� $	 PlotFirstRF�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$ chkPlotFirstRF��$	 	IsChecked� �� Value	 False.a���  � y�$ 	Collapsed=�.+��� -   $��lbFirstRFPropertyToPlot��$ RPill2DPlotFirstRF��$��Property to plot:���� -	   $��cboFirstRFPropertyToPlot��$ RPill2DPlotFirstRF������  �� Source# [  �� SelectedValue �� $	 FirstRFPropertyToPlot�$
 PropertyChanged=� �� Mode$ TwoWay=����� .R��� ����  �� 	Converter# [ �� -
   $*��$chkProducePSPlotOfFirstDisplacedMesh��$. (RPill2DProducePSPlotOfFirstDisplacedMesh��$N��HProduce a PostScript format file of first displaced finite element mesh?�� �� $'	 !ProducePSPlotOfFirstDisplacedMesh�$
 PropertyChanged=��� $ Vertical=�$��30 0 0 0q����� " ��.���  � y�$ Visible=� ��  �� $* $chkProducePSPlotOfFirstDisplacedMesh��$	 	IsChecked�	 False.a���  � y�$ 	Collapsed=�.+��� -   $$��chkShowMeshOnDisplacedMeshPlot��$( "RPill2DShowMeshOnDisplacedMeshPlot��$'��!Show mesh on displaced mesh plot?�� �� $!	 ShowMeshOnDisplacedMeshPlot�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $"��chkShowRFOnDisplacedMeshPlot��$&  RPill2DShowRFOnDisplacedMeshPlot��$ ��Show random field on plot?�� �� $	 ShowRFOnDisplacedMeshPlot�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $" chkShowRFOnDisplacedMeshPlot��$	 	IsChecked�	 False.a���  � y�$ 	Collapsed=�.+��� -   $��chkShowLogRF��$&  RPill2DShowRFOnDisplacedMeshPlot��$��Show log-random field?�� �� $	 	ShowLogRF�$
 PropertyChanged=��� -   $#��lbDisplacedMeshPropertyToPlot��$&  RPill2DShowRFOnDisplacedMeshPlot��$��Property to plot:���� -   $$��cboDisplacedMeshPropertyToPlot��$&  RPill2DShowRFOnDisplacedMeshPlot������ # [  �� $!	 DisplacedMeshPropertyToPlot�$
 PropertyChanged=�$ TwoWay=����� .R��� ���� # [ �� $ 
Horizontal=�.+��� -   $��lbDisplacedMeshPlotWidth��$# RPill2DDisplacedMeshPlotWidth��$ ��Displaced mesh plot width:���� -   $��txtDisplacedMeshPlotWidth��$# RPill2DDisplacedMeshPlotWidth������ $	 DisplacedMeshPlotWidth�$
 PropertyChanged=� . # [ �� -   $#��lbDisplacedMeshPlotWidthUnits��$# RPill2DDisplacedMeshPlotWidth��$��inches���� -   $'��!chkNormalizePillarCapacitySamples��$+ %RPill2DNormalizePillarCapacitySamples��$(��"Normalize pillar capacity samples?�� �� $$	 NormalizePillarCapacitySamples�$
 PropertyChanged=��� -   $$��chkOutputPillarCapacitySamples��$( "RPill2DOutputPillarCapacitySamples��$%��Output pillar capacity samples?�� �� $!	 OutputPillarCapacitySamples�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��lbNumberOfElements��$ RPill2DNumberOfElements��$��Number of elements:���� -   $��txtNElementsInXDir��$ RPill2DNumberOfElements������ $	 NElementsInXDir�$
 PropertyChanged=� . �� -   $��lbNElementsInXDir��$ RPill2DNumberOfElements��$��in x-direction���� -   $��txtNElementsInYDir��$ RPill2DNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -   $��lbNElementsInYDir��$ RPill2DNumberOfElements��$��in y-direction���� $ 
Horizontal=�.+��� -   $��lbElementSize��$ RPill2DElementSize��$��Number of elements:���� -   $��txtElementSizeInXDir��$ RPill2DElementSize������ $	 ElementSizeInXDir�$
 PropertyChanged=� . # [ �� -   $��lbElementSizeInXDir��$ RPill2DElementSize��$��in x-direction���� -   $��txtElementSizeInYDir��$ RPill2DElementSize������ $	 ElementSizeInYDir�$
 PropertyChanged=� . # [ �� -   $��lbElementSizeInYDir��$ RPill2DElementSize��$��in y-direction���� -   $��chkRoughLoadingCondition��$" RPill2DRoughLoadingCondition��$��Rough loading condiction?�� �� $	 RoughLoadingCondition�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -    $��lbDisplacementInc��$ RPill2DDisplacementInc��$��Displacement inc:���� -!   $��txtDisplacementInc��$ RPill2DDisplacementInc������ $	 DisplacementInc�$
 PropertyChanged=� . # [ �� -"   $��lbPlasticTol��$ RPill2DDisplacementInc��$��Plastic tol:���� -#   $��txtPlasticTol��$ RPill2DDisplacementInc������ $	 
PlasticTol�$
 PropertyChanged=� . # [ �� -$   $��lbBearingTol��$ RPill2DDisplacementInc��$��Bearing tol:���� -%   $��txtBearingTol��$ RPill2DDisplacementInc������ $	 
BearingTol�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -&   $��lbMaxNumSteps��$ RPill2DMaxNumSteps��$��Max number of steps:���� -'   $��txtMaxNumSteps��$ RPill2DMaxNumSteps������ $	 MaxNumSteps�$
 PropertyChanged=� . �� -(   $��lbMaxNumIterations��$ RPill2DMaxNumSteps��$��Max number of iterations:���� -)   $��txtMaxNumIterations��$ RPill2DMaxNumSteps������ $	 MaxNumIterations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -*   $��lbNumberOfRealizations��$! RPill2DNumberOfRealizations��$��Number of realizations:���� -+   $��txtNumberOfRealizations��$! RPill2DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -,   $��lbGeneratorSeed��$ RPill2DGeneratorSeed��$��Generator seed:���� --   $��txtGeneratorSeed��$ RPill2DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -.   $��lbCorrelationLengths��$ RPill2DCorrelationLengths��$��Correlation lengths:���� -/   $ ��txtCorrelationLengthInXDir��$ RPill2DCorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -0   $��lbCorrelationLengthInXDir��$ RPill2DCorrelationLengths��$��in x-direction���� -1   $ ��txtCorrelationLengthInYDir��$ RPill2DCorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -2   $��lbCorrelationLengthInYDir��$ RPill2DCorrelationLengths��$��in y-direction���� $ 
Horizontal=�.+��� -3   $��	lbCovFunc��$ RPill2DCovFunc��$��Covariance function:���� -4   $��
cboCovFunc��$ RPill2DCovFunc������ # [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=�E   ?RFEMSoftware.Simulation.Desktop.CustomControls.SoilDistribution     DistributionLabel$ Cohesion distribution:��   DistributionTag$ RPill2DCohesionDist��   Distribution �� $	 Cohesion�$
 PropertyChanged=�  $" Friction angle distribution:��$ RPill2DFrictionAngleDist��   IsFrictionAngle .  �� $	 FrictionAngle�$
 PropertyChanged=�   FrictionAngleType �� $	 FrictionAngleType�$
 PropertyChanged=�$ TwoWay=�  $" Dilation angle distribution:��$ RPill2DDilationAngleDist�� �� $	 DilationAngle�$
 PropertyChanged=�  $# Elastic modulus distribution:��$ RPill2DElasticModulusDist�� �� $	 ElasticModulus�$
 PropertyChanged=�  $# Poisson's ratio distribution:��$ RPill2DPoissonsRatioDist�� �� $	 PoissonsRatio�$
 PropertyChanged=��� -5   $��lbCorrelationMatrix��$ RPill2DCorrelationMatrix��$0��*Soil property correlation matrix elements:���  � ColumnDefinitions �� $
��auto ��� $��* �.+�&   System.Windows.Controls.DataGrid  -6   $��dgCorrelationMatrix��$ RPill2DCorrelationMatrix��   AutoGenerateColumns . $��0��   CanUserAddRows .     CanUserDeleteRows .      CanUserReorderColumns  .  !   CanUserSortColumns! .  $
��auto��
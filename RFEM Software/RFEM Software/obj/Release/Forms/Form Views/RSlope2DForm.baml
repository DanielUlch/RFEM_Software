   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  8    2RFEMSoftware.Simulation.Desktop.Forms.RSlope2DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  SoilPropertyEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type?  9RFEMSoftware.Simulation.Infrastructure.RSlopeSoilProperty  covFunctionEnum?  9RFEMSoftware.Simulation.Infrastructure.CovarianceFunctionC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%  '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     &	 �     &	 �     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� -   $��chkPlotARandomField��$ RSlope2DPlotARandomField��$��Plot a random field?�� �� $	 PlotARandomField�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$ chkPlotARandomField��$	 	IsChecked� �� Value	 False.a���  � y�$ 	Collapsed=�.+��� -   $��lbRFPropertyToPlot��$ RSlope2DPlotARandomField��$��Property to plot:���� -	   $��cboRFPropertyToPlot��$ RSlope2DPlotARandomField������  �� Source# [  �� SelectedValue �� $	 RFPropertyToPlot�$
 PropertyChanged=� �� Mode$ TwoWay=����� .R��� ����  �� 	Converter# [ �� -
   $%��chkProducePSPlotOfDisplacedMesh��$* $RSlope2DProducePSPlotOfDisplacedMesh��$G��AProduce a PostScript format file of displaced finite element mesh�� �� $"	 ProducePSPlotOfDisplacedMesh�$
 PropertyChanged=��� $ Vertical=�$��30 0 0 0q����� " ��.���  � y�$ Visible=� ��  �� $% chkProducePSPlotOfDisplacedMesh��$	 	IsChecked�	 False.a���  � y�$ 	Collapsed=�.+��� -   $ ��chkShowMeshOnDisplacedMesh��$% RSlope2DShowMeshOnDisplacedMesh��$'��!Show mesh on displaced mesh plot?�� �� $	 ShowMeshOnDisplacedMesh�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��chkShowRFOnDisplacedMesh��$# RSlope2DShowRFOnDisplacedMesh��$ ��Show random field on plot?�� �� $	 ShowRFOnDisplacedMesh�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ chkShowRFOnDisplacedMesh��$	 	IsChecked�	 False.a���  � y�$ 	Collapsed=�.+��� -   $!��chkShowLogRFOnDisplacedMesh��$# RSlope2DShowRFOnDisplacedMesh��$��Show log-random field?�� �� $	 ShowLogRFOnDisplacedMesh�$
 PropertyChanged=��� -   $#��lbDisplacedMeshPropertyToPlot��$# RSlope2DShowRFOnDisplacedMesh��$��Property to plot:���� -   $$��cboDisplacedMeshPropertyToPlot��$# RSlope2DShowRFOnDisplacedMesh������ # [  �� $!	 DisplacedMeshPropertyToPlot�$
 PropertyChanged=�$ TwoWay=����� .R��� ���� # [ �� $ 
Horizontal=�.+��� -   $��lbDisplacedMeshPlotWidth��$$ RSlope2DDisplacedMeshPlotWidth��$ ��Displaced mesh plot width:���� -   $��txtDisplacedMeshPlotWidth��$$ RSlope2DDisplacedMeshPlotWidth������ $	 DisplacedMeshPlotWidth�$
 PropertyChanged=� . # [ �� -   $#��lbDisplacedMeshPlotWidthUnits��$$ RSlope2DDisplacedMeshPlotWidth��$��inches���� $ 
Horizontal=�.+��� -   $��lbNumberOfElements��$ RSlope2DNumberOfElements��$��Number of elements���� $ Vertical=�.+��� $ 
Horizontal=�$��Right=�.+��� -   $��lbNElementsLeftOfEmbark��$ RSlope2DNumberOfElements��$��left of embankment:���� -   $��txtNElementsLeftOfEmbark��$ RSlope2DNumberOfElements������ $	 NElementsLeftOfEmbark�$
 PropertyChanged=� . �� $ 
Horizontal=�$��Right=�.+��� -   $��lbNElementsInEmbark��$ RSlope2DNumberOfElements��$��in embankment:���� -   $��txtNElementsInEmbark��$ RSlope2DNumberOfElements������ $	 NElementsInEmbark�$
 PropertyChanged=� . �� $ Vertical=�.+��� $ 
Horizontal=�$��Right=�.+��� -   $��lbNElementsRightOfEmbark��$ RSlope2DNumberOfElements��$��right of embankment:���� -   $��txtNElementsRightOfEmbark��$ RSlope2DNumberOfElements������ $	 NElementsRightOfEmbark�$
 PropertyChanged=� . �� $ 
Horizontal=�$��Right=�.+��� -   $��lbNElementsInFoundations��$ RSlope2DNumberOfElements��$��in foundation���� -   $��txtNElementsInFoundations��$ RSlope2DNumberOfElements������ $	 NElementsInFoundations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -   $��lbSlopeGradient��$ RSlope2DSlopeGradient��$ ��Slope gradient (run/rise):���� -   $��txtSlopeGradient��$ RSlope2DSlopeGradient������ $	 SlopeGradient�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -   $��lbElementSize��$ RSlope2DElementSize��$��Element size:���� -   $��txtElementSizeInXDir��$ RSlope2DElementSize������ $	 ElementSizeInXDir�$
 PropertyChanged=� . # [ �� -    $��lbElementSizeInXDir��$ RSlope2DElementSize��$��in x-direction���� -!   $��txtElementSizeInYDir��$ RSlope2DElementSize������ $	 ElementSizeInYDir�$
 PropertyChanged=� . # [ �� -"   $��lbElementSizeInYDir��$ RSlope2DElementSize��$��in y-direction���� $ 
Horizontal=�.+��� -#   $��lbNumberOfRealizations��$" RSlope2DNumberOfRealizations��$��Number of simulations:���� -$   $��txtNumberOfRealizations��$! RSlope2NumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -%   $��lbGeneratorSeed��$ RSlope2GeneratorSeed��$��Generator seed:���� -&   $��txtGeneratorSeed��$ RSlope2DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -'   $��lbCorrelationLengths��$ RSlope2CorrelationLengths��$��Correlation lengths:���� -(   $ ��txtCorrelationLengthInXDir��$ RSlope2CorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -)   $��lbCorrelationLengthInXDir��$ RSlope2CorrelationLengths��$��in x-direction���� -*   $ ��txtCorrelationLengthInYDir��$ RSlope2CorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -+   $��lbCorrelationLengthInYDir��$ RSlope2CorrelationLengths��$��in y-direction���� $ 
Horizontal=�.+��� -,   $��	lbCovFunc��$ RSlope2CovFunc��$��Covariance function:���� --   $��
cboCovFunc��$ RSlope2CovFunc������ # [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=��� $ 
Horizontal=�.+��� -.   $��lbConvergenceTolerance��$" RSlope2DConvergenceTolerance��$��Convergence tolerance:���� -/   $��txtConvergenceTolerance��$" RSlope2DConvergenceTolerance������ $	 ConvergenceTolerance�$
 PropertyChanged=� . # [ �� -0   $��lbMaxNumIterations��$" RSlope2DConvergenceTolerance��$#��Maximum number of iterations:���� -1   $��txtMaxNumIterations��$" RSlope2DConvergenceTolerance������ $	 MaxNumIterations�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -2   $ ��lbStrengthReductionFactors��$&  RSlope2DStrengthReductionFactors��$!��Strength reduction factors:��&   System.Windows.Controls.DataGrid  -3   $ ��dgStrengthReductionFactors��$&  RSlope2DStrengthReductionFactors������ $	 StrengthReductionFactors�$
 PropertyChanged=�$ TwoWay=�E   ?RFEMSoftware.Simulation.Desktop.CustomControls.SoilDistribution     DistributionLabel$ Cohesion distribution:��   DistributionTag$ RSlope2DCohesionDist��   Distribution �� $	 Cohesion�$
 PropertyChanged=�  $" Friction angle distribution:��$ RSlope2DFrictionAngleDist��   IsFrictionAngle .  �� $	 FrictionAngle�$
 PropertyChanged=�   FrictionAngleType �� $	 FrictionAngleType�$
 PropertyChanged=�$ TwoWay=�  $" Dilation angle distribution:��$ RSlope2DDilationAngleDist�� �� $	 DilationAngle�$
 PropertyChanged=�  $ Unit weight distribution:��$ RSlope2DUnitWeightDist�� �� $	 
UnitWeight�$
 PropertyChanged=�  $# Elastic modulus distribution:��$  RSlope2DElasticModulusDist�� �� $	 ElasticModulus�$
 PropertyChanged=�  $# Poisson's ratio distribution:��$ RSlope2DPoissonsRatioDist�� �� $	 PoissonsRatio�$
 PropertyChanged=��� -4   $��lbCorrelationMatrix��$ RSlope2DCorrelationMatrix��$0��*Soil property correlation matrix elements:���  � ColumnDefinitions �� $
��auto ��� $��* �.+�  -5   $��dgCorrelationMatrix��$ RPill2DCorrelationMatrix��   AutoGenerateColumns . $��0��   CanUserAddRows .     CanUserDeleteRows .      CanUserReorderColumns  .  !   CanUserSortColumns! .  $
��auto��
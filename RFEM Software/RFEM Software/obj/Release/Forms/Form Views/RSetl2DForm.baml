   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure o<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls  7    1RFEMSoftware.Simulation.Desktop.Forms.RSetl2DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  covFunctionEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type?  9RFEMSoftware.Simulation.Infrastructure.CovarianceFunctionC   =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%�   '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     &	 x     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� -   $4��.chkProduceDisplayOfFirstLogElasticModulusField��$8 2RSetl2DProduceDisplayOfFirstLogElasticModulusField��$G��AProduce a DISPLAY format file of first log-elastic modulus field?�� �� $1	 +ProduceDisplayOfFirstLogElasticModulusField�$
 PropertyChanged=��� -   $*��$chkProducePSPlotOfFirstDisplacedMesh��$. (RSetl2DProducePSPlotOfFirstDisplacedMesh��$H��BProduce a PostScript format file of displaced finite element mesh?�� �� $'	 !ProducePSPlotOfFirstDisplacedMesh�$
 PropertyChanged=��� $ Vertical=�$��30 0 0 0q����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$* $chkProducePSPlotOfFirstDisplacedMesh��$	 	IsChecked� �� Value	 False.a���  � y�$ 	Collapsed=�.+��� -	   $5��/chkOverlayLogElasticModulusFieldOnDisplacedMesh��$9 3RSetl2DOverlayLogElasticModulusFieldOnDisplacedMesh��$(��"Overlay log-elastic modulus field?�� �� $2	 ,OverlayLogElasticModulusFieldOnDisplacedMesh�$
 PropertyChanged=��� -
   $-��'chkShowProblemDimensionsOnDisplacedMesh��$1 +RSetl2DShowProblemDimensionsOnDisplacedMesh��$&�� Show problem dimensions on plot?�� �� $*	 $ShowProblemDimensionsOnDisplacedMesh�$
 PropertyChanged=��� -   $"��chkShowTitlesOnDisplacedMesh��$&  RSetl2DShowTitlesOnDisplacedMesh��$��Show titles above plot?�� �� $	 ShowTitlesOnDisplacedMesh�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $!��lbDisplacementMagnification��$&  RSetl2DDisplacementMagnification��$!��Displacement magnification:���� -   $��txtDisplacementMagInXDir��$&  RSetl2DDisplacementMagnification������ $	 DisplacementMagInXDir�$
 PropertyChanged=� .  �� 	Converter# [ �� -   $��lbDisplacementMagInXDir��$&  RSetl2DDisplacementMagnification��$��in x-direction���� -   $��txtDisplacementMagInYDir��$&  RSetl2DDisplacementMagnification������ $	 DisplacementMagInYDir�$
 PropertyChanged=� . # [ �� -   $��lbDisplacementMagInYDir��$&  RSetl2DDisplacementMagnification��$��in x-direction���� $ 
Horizontal=�.+��� -   $!��lbDisplacementMeshPlotWdith��$&  RSetl2DDisplacementMeshPlotWidth��$ ��Displaced mesh plot width:���� -   $��txtDisplacedMeshWidth��$&  RSetl2DDisplacementMeshPlotWidth������ $	 DisplacedMeshWidth�$
 PropertyChanged=� . # [ �� -   $��lbDisplacedMeshWidth��$&  RSetl2DDisplacementMeshPlotWidth��$��inches���� -   $)��#chkUseKGSelectiveReducedIntegration��$- 'RSetl2DUseKGSelectiveReducedIntegration��$8��2Use K-G settlement samples and summary statistics?�� �� $&	  UseKGSelectiveReducedIntegration�$
 PropertyChanged=��� -   $/��)chkOutputSettlementSamplesAndSummaryStats��$1 +RSetlOutputSettlementSamplesAndSummaryStats��$7��1Output settlement samples and summary statistics?�� �� $,	 &OutputSettlementSamplesAndSummaryStats�$
 PropertyChanged=��� -   $1��+chkOutputBlockModulusSamplesAndSummaryStats��$5 /RSetl2DOutputBlockModulusSamplesAndSummaryStats��$:��4Output block modulus samples and summary statistics?�� �� $.	 (OutputBlockModulusSamplesAndSummaryStats�$
 PropertyChanged=��� -   $9��3chkOutputFieldAveragedModulusSamplesAndSummaryStats��$= 7RSetl2DOutputFieldAveragedModulusSamplesAndSummaryStats��$C��=Output field averaged modulus samples and summary statistics?�� �� $6	 0OutputFieldAveragedModulusSamplesAndSummaryStats�$
 PropertyChanged=��� -   $$��chkGenerateUniformRandomFields��$( "RSetl2DGenerateUniformRandomFields��$%��Generate uniform random fields?�� �� $!	 GenerateUniformRandomFields�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��lbNumberOfElements��$ RSetl2DNumberOfElements��$��Number of elements:���� -   $��txtNElementsInXDir��$ RSetl2DNumberOfElements������ $	 NElementsInXDir�$
 PropertyChanged=� . �� -   $��lbNElementsInXDir��$ RSetl2DNumberOfElements��$��in x-direction���� -   $��txtNElementsInYDir��$ RSetl2DNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -   $��lbNElementsInYDir��$ RSetl2DNumberOfElements��$��in y-direction���� $ 
Horizontal=�.+��� -   $��lbElementSizes��$ RSetl2DElementSizes��$��Element sizes:���� -   $��txtElementSizeInXDir��$ RSetl2DElementSizes������ $	 ElementSizeInXDir�$
 PropertyChanged=� . # [ �� -    $��lbElementSizeInXDir��$ RSetl2DElementSizes��$��in x-direction���� -!   $��txtElementSizeInYDir��$ RSetl2DElementSizes������ $	 ElementSizeInYDir�$
 PropertyChanged=� . # [ �� -"   $��lbElementSizeInYDir��$ RSetl2DElementSizes��$��in y-direction���� $ 
Horizontal=�.+��� -#   $��lbNumberOfRealizations��$! RSetl2DNumberOfRealizations��$��Number of simulations:���� -$   $��txtNumberOfRealizations��$! RSetl2DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -%   $��lbGeneratorSeed��$ RSetl2DGeneratorSeed��$��Generator seed:���� -&   $��txtGeneratorSeed��$ RSetl2DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -'   $��lbPoissonsRatio��$ RSetl2DPoissonsRatio��$��Poisson's ratio:���� -(   $��txtPoissonsRatio��$ RSetl2DPoissonsRatio������ $	 PoissonsRatio�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -)   $��lbElasticModulus��$ RSetl2DElasticModulus��$��Elastic modulus:���� -*   $��txtElasticModulusMean��$ RSetl2DElasticModulus������ $	 ElasticModulusMean�$
 PropertyChanged=� . # [ �� -+   $��lbElasticModulusMean��$ RSetl2DElasticModulus��$
��mean���� -,   $��txtElasticModulusStdDev��$ RSetl2DElasticModulus������ $	 ElasticModulusStdDev�$
 PropertyChanged=� . # [ �� --   $��lbElasticModulusStdDev��$ RSetl2DElasticModulus��$��standard deviation���� $ 
Horizontal=�.+��� -.   $��lbCorrelationLengths��$ RSetl2DCorrelationLengths��$��Correlation lengths:���� -/   $ ��txtCorrelationLengthInXDir��$ RSetl2DCorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -0   $��lbCorrelationLengthInXDir��$ RSetl2DCorrelationLengths��$��in x-direction���� -1   $ ��txtCorrelationLengthInYDir��$ RSetl2DCorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -2   $��lbCorrelationLengthInYDir��$ RSetl2DCorrelationLengths��$��in y-direction���� $ 
Horizontal=�.+��� -3   $��	lbCovFunc��$ RSetl2DCovFunc��$��Covariance function:���� -4   $��
cboCovFunc��$ RSetl2DCovFunc������  �� Source# [  �� SelectedValue �� $	 CovFunc�$
 PropertyChanged=� �� Mode$ TwoWay=��� $ 
Horizontal=�.+��� -5   $��lbSettlementNodeList��$ RSetl2DSettlementNodeList��$��Settlement node list:��&   System.Windows.Controls.DataGrid  -6   $��dgSettlementNodeList��$ RSetl2DSettlementNodeList��$
��Auto������ $	 SettlementNodeList�$
 PropertyChanged=�$ TwoWay=��� $ 
Horizontal=�.+��� -7   $ ��lbDifferentialNodePairList��$% RSetl2DDifferentialNodePairList��$-��'Differential settlement node pair list:��  -8   $ ��dgDifferentialNodePairList��$% RSetl2DDifferentialNodePairList��$
��Auto������ $	 DifferentialNodePairList�$
 PropertyChanged=�$ TwoWay=��� -9   $��lbRigidFootingLoads��$ RSetl2DRigidFootingLoads��$9��3Rigid footing loads: Location, Value, and Roughness��  -:   $��gbRigidFootingLoads��$ RSetl2DRigidFootingLoads��$
��Auto������ $	 RigidFootingLoads�$
 PropertyChanged=�$ TwoWay=��� -;   $��lbUniformLoads��$ RSetl2DUniformLoads��$5��/Uniformly distributed loads: Location and Value��  -<   $��dgUniformLoads��$ RSetl2DUniformLoads��$
��Auto������ $	 UniformLoads�$
 PropertyChanged=�$ TwoWay=��� -=   $��lbLineLoads��$ RSetl2DLineLoads��$��Number of line loads:��  ->   $��dgLineLoads��$ RSetl2DLineLoads��$
��Auto������ $	 	LineLoads�$
 PropertyChanged=�$ TwoWay=�
   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure X TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbo<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls.RFEMSoftware.Simulation.Desktop.CustomControls ]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms 7   1RFEMSoftware.Simulation.Desktop.Forms.RSetl3DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     Kcc<clr-namespace:RFEMSoftware.Simulation.Desktop.CustomControls     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  AxisEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters M� Type1  +RFEMSoftware.Simulation.Infrastructure.Axis  covFunction3DEnumA  ;RFEMSoftware.Simulation.Infrastructure.CovarianceFunction3DC  =RFEMSoftware.Simulation.Desktop.Forms.EnumToUIStringConverter  UIStringConverter;  5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%  '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     &	 �     &	 �     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "  S� $ 	GetValues��"   M� "      .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� -   $4��.chkProduceDisplayOfFirstLogElasticModulusField��$8 2RSetl3DProduceDisplayOfFirstLogElasticModulusField��$E��?Produce DISPLAY format file of first log-elastic modulus field?�� �� $1	 +ProduceDisplayOfFirstLogElasticModulusField�$
 PropertyChanged=��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$4 .chkProduceDisplayOfFirstLogElasticModulusField��$	 	IsChecked� �� Value	 False.a���  � y�$ 	Collapsed=�.+��� -   $'��!lbLogElasticModulusFieldNodeIndex��$, &RSetl3DLogElasticModulusFieldNodeIndex��$��Plot node index number:���� -	   $(��"txtLogElasticModulusFieldNodeIndex��$, &RSetl3DLogElasticModulusFieldNodeIndex������ $%	 LogElasticModulusFieldNodeIndex�$
 PropertyChanged=� . �� -
   $5��/lbLogElasticModulusFieldPerpindicularToThisAxis��$, &RSetl3DLogElasticModulusFieldNodeIndex��$��Perpendicular to:���� -   $6��0cboLogElasticModulusFieldPerpindicularToThisAxis��$, &RSetl3DLogElasticModulusFieldNodeIndex������  �� Source# [  �� SelectedValue �� $3	 -LogElasticModulusFieldPerpindicularToThisAxis�$
 PropertyChanged=� �� Mode$ TwoWay=����� .R��� ����  �� 	Converter# [ �� -   $%��chkProducePSPlotOfDisplacedMesh��$) #RSetl3DProducePSPlotOfDisplacedMesh��$H��BProduce a PostScript format file of displaced finite element mesh?�� �� $"	 ProducePSPlotOfDisplacedMesh�$
 PropertyChanged=��� $ Vertical=�$��30 0 0 0q����� " ��.���  � y�$ Visible=� ��  �� $% chkProducePSPlotOfDisplacedMesh��$	 	IsChecked�	 False.a���  � y�$ 	Collapsed=�.+��� $ 
Horizontal=�.+��� -   $��lbDisplacedMeshNodeIndex��$) #RSetl3DProducePSPlotOfDisplacedMesh��$��Plot node index number:���� -   $��txtDisplacedMeshNodeIndex��$) #RSetl3DProducePSPlotOfDisplacedMesh������ $	 DisplacedMeshNodeIndex�$
 PropertyChanged=� . �� -   $0��*lbDisplacedMeshPlotPerpindicularToThisAxis��$) #RSetl3DProducePSPlotOfDisplacedMesh��$��Perpindicular to:���� -   $1��+cboDisplacedMeshPlotPerpindicularToThisAxis��$) #RSetl3DProducePSPlotOfDisplacedMesh������ # [  �� $.	 (DisplacedMeshPlotPerpindicularToThisAxis�$
 PropertyChanged=�$ TwoWay=��� -   $0��*chkOverlayLogElasticModulusOnDisplacedMesh��$4 .RSetl3DOverlayLogElasticModulusOnDisplacedMesh��$(��"Overlay log-elastic modulus field?�� �� $-	 'OverlayLogElasticModulusOnDisplacedMesh�$
 PropertyChanged=��� -   $-��'chkShowProblemDimensionsOnDisplacedMesh��$1 +RSetl3DShowProblemDimensionsOnDisplacedMesh��$&�� Show problem dimensions on plot?�� �� $*	 $ShowProblemDimensionsOnDisplacedMesh�$
 PropertyChanged=��� -   $"��chkShowTitlesOnDisplacedMesh��$&  RSetl3DShowTitlesOnDisplacedMesh��$��Show titles above plot?�� �� $	 ShowTitlesOnDisplacedMesh�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $!��lbDisplacementMagnification��$&  RSetl3DDisplacementMagnification��$!��Displacement magnification:���� -   $��txtDisplacementMagInXDir��$&  RSetl3DDisplacementMagnification������ $	 DisplacementMagInXDir�$
 PropertyChanged=� . # [ �� -   $��lbDisplacementMagInXDir��$&  RSetl3DDisplacementMagnification��$��in x-direction���� -   $��txtDisplacementMagInYDir��$&  RSetl3DDisplacementMagnification������ $	 DisplacementMagInYDir�$
 PropertyChanged=� . # [ �� -   $��lbDisplacementMagInYDir��$&  RSetl3DDisplacementMagnification��$��in y-direction���� $ 
Horizontal=�.+��� -   $��lbDisplacedMeshPlotWidth��$# RSetl3DDisplacedMeshPlotWidth��$ ��Displaced mesh plot width:���� -   $��txtDisplacedMeshPlotWidth��$# RSetl3DDisplacedMeshPlotWidth������ $	 DisplacedMeshPlotWidth�$
 PropertyChanged=� . # [ �� -   $#��lbDisplacedMeshPlotWidthUnits��$# RSetl3DDisplacedMeshPlotWidth��$��inches���� -   $/��)chkOutputSettlementSamplesAndSummaryStats��$3 -RSetl3DOutputSettlementSamplesAndSummaryStats��$7��1Output settlement samples and summary statistics?�� �� $,	 &OutputSettlementSamplesAndSummaryStats�$
 PropertyChanged=��� -   $1��+chkOutputBlockModulusSamplesAndSummaryStats��$5 /RSetl3DOutputBlockModulusSamplesAndSummaryStats��$:��4Output block modulus samples and summary statistics?�� �� $.	 (OutputBlockModulusSamplesAndSummaryStats�$
 PropertyChanged=��� -   $9��3chkOutputFieldAveragedModulusSamplesAndSummaryStats��$= 7RSetl3DOutputFieldAveragedModulusSamplesAndSummaryStats��$C��=Output field averaged modulus samples and summary statistics?�� �� $6	 0OutputFieldAveragedModulusSamplesAndSummaryStats�$
 PropertyChanged=��� -   $$��chkGenerateUniformRandomFields��$( "RSetl3DGenerateUniformRandomFields��$%��Generate uniform random fields?�� �� $!	 GenerateUniformRandomFields�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -    $��lbNumberOfElements��$ RSetl3DNumberOfElements��$��Number of elements:���� -!   $��txtNElementsInXidr��$ RSetl3DNumberOfElements������ $	 NElementsInXidr�$
 PropertyChanged=� . �� -"   $��lbNElementsInXidr��$ RSetl3DNumberOfElements��$��in x-direction���� -#   $��txtNElementsInYDir��$ RSetl3DNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -$   $��lbNElementsInYDir��$ RSetl3DNumberOfElements��$��in y-direction���� -%   $��txtNElementsInZDir��$ RSetl3DNumberOfElements������ $	 NElementsInZDir�$
 PropertyChanged=� . �� -&   $��lbNElementsInZDir��$ RSetl3DNumberOfElements��$��in z-direction���� $ 
Horizontal=�.+��� -'   $��lbElementSize��$ RSetl3DElementSize��$��Element size:���� -(   $��txtElementSizeInXDir��$ RSetl3DElementSize������ $	 ElementSizeInXDir�$
 PropertyChanged=� . # [ �� -)   $��lbElementSizeInXDir��$ RSetl3DElementSize��$��in x-direction���� -*   $��txtElementSizeInYDir��$ RSetl3DElementSize������ $	 ElementSizeInYDir�$
 PropertyChanged=� . # [ �� -+   $��lbElementSizeInYDir��$ RSetl3DElementSize��$��in y-direction���� -,   $��txtElementSizeInZDir��$ RSetl3DElementSize������ $	 ElementSizeInZDir�$
 PropertyChanged=� . # [ �� --   $��lbElementSizeInZDir��$ RSetl3DElementSize��$��in z-direction���� $ 
Horizontal=�.+��� -.   $��lbMaxNumIterations��$ RSetl3DMaxNumIterations��$#��Maximum number of iterations:���� -/   $��txtMaxNumIterations��$ RSetl3DMaxNumIterations������ $	 MaxNumIterations�$
 PropertyChanged=� . �� -0   $��lbConvergenceTolerance��$ RSetl3DMaxNumIterations��$��Convergence tolerance:���� -1   $��txtConvergenceTolerance��$ RSetl3DMaxNumIterations������ $	 ConvergenceTolerance�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -2   $��lbNumberOfRealizations��$! RSetl3DNumberOfRealizations��$��Number of simulations:���� -3   $��txtNumberOfRealizations��$! RSetl3DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -4   $��lbGeneratorSeed��$ RSetl3DGeneratorSeed��$��Generator seed:���� -5   $��txtGeneratorSeed��$ RSetl3DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -6   $��lbPoissonsRatio��$ RSetl3DPoissonsRatio��$��Poisson's ratio:���� -7   $��txtPoissonsRatio��$ RSetl3DPoissonsRatio������ $	 PoissonsRatio�$
 PropertyChanged=� . # [ �� $ 
Horizontal=�.+��� -8   $��lbElasticModulus��$ RSetl3DElasticModulus��$��Elastic modulus:���� -9   $��txtElasticModulusMean��$ RSetl3DElasticModulus������ $	 ElasticModulusMean�$
 PropertyChanged=� . # [ �� -:   $��lbElasticModulusMean��$ RSetl3DElasticModulus��$
��mean���� -;   $��txtElasticModulusStdDev��$ RSetl3DElasticModulus������ $	 ElasticModulusStdDev�$
 PropertyChanged=� . # [ �� -<   $��lbElasticModulusStdDev��$ RSetl3DElasticModulus��$��standard deviation���� $ 
Horizontal=�.+��� -=   $��lbCorrelationLengths��$ RSetl3DCorrelationLengths��$��Correlation lengths:���� ->   $ ��txtCorrelationLengthInXDir��$ RSetl3DCorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -?   $��lbCorrelationLengthInXDir��$ RSetl3DCorrelationLengths��$��in x-direction���� -@   $ ��txtCorrelationLengthInYDir��$ RSetl3DCorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -A   $��lbCorrelationLengthInYDir��$ RSetl3DCorrelationLengths��$��in y-direction���� -B   $ ��txtCorrelationLengthInZDir��$ RSetl3DCorrelationLengths������ $	 CorrelationLengthInZDir�$
 PropertyChanged=� . �� -C   $��lbCorrelationLengthInZDir��$ RSetl3DCorrelationLengths��$��in z-direction���� $ 
Horizontal=�.+��� -D   $��	lbCovFunc��$ RSetl3DCovFunc��$��Covariance function:���� -E   $��
cboCovFunc��$ RSetl3DCovFunc������ # [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=��� -F   $��lbRigidFootingLoads��$ RSetl3DRigidFootingLoads��$-��'Rigid footing loads: Location and Loads��&   System.Windows.Controls.DataGrid  -G   $��gbRigidFootingLoads��$ RSetl3DRigidFootingLoads������ $	 RigidFootingLoads�$
 PropertyChanged=�$ TwoWay=�
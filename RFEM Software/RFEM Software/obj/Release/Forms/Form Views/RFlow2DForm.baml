   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 7    1RFEMSoftware.Simulation.Desktop.Forms.RFlow2DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib     6sys&clr-namespace:System;assembly=mscorlib       � 	Resources  ;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter   DoubleConverter �� 
TargetType  HelpEnabled �� BasedOn  covFunctionEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type?  9RFEMSoftware.Simulation.Infrastructure.CovarianceFunction��%�   &	        '��      7  '��      7  '�� #     7  '	� 2     7  '�� A     7  &	 P       �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "  .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� -   $��chkDisplayLogCondField��$  RFlow2DDisplayLogCondField��$D��>Produce a DISPLAY format file of first log-conductivity field?�� �� $/	 )ProduceDisplayOfFirstLogConductivityField�$
 PropertyChanged=��� -   $$��chkProducePSPlotOfFirstFlownet��$( "RFlow2DProducePSPlotOfFirstFlownet��$1��+Produce a PostScript plot of first flownet?�� �� $!	 ProducePSPlotOfFirstFlownet�$
 PropertyChanged=��� -	   $/��)chkProduceDisplayOfTotalHeadMeanAndStdDev��$3 -RFlow2DProduceDisplayOfTotalHeadMeanAndStdDev��$N��HProduce a DISPLAY format file of total head mean and standard deviation?�� �� $,	 &ProduceDisplayOfTotalHeadMeanAndStdDev�$
 PropertyChanged=��� -
   $.��(chkOutputFlowRateExitGradientUpliftForce��$2 ,RFlow2DOutputFlowRateExitGradientUpliftForce��$S��MOutput flow rates, exit gradients (if walls), and uplift forces (if 2 walls)?�� �� $+	 %OutputFlowRateExitGradientUpliftForce�$
 PropertyChanged=��� -   $'��!chkOutputDetailedExitGradientInfo��$+ %RFlow2DOutputDetailedExitGradientInfo��$C��=Output detailed exit gradient information (if walls present)?�� �� $$	 OutputDetailedExitGradientInfo�$
 PropertyChanged=��� -   $+��%chkOutputBlockHydraulicConductivities��$/ )RFlow2DOutputBlockHydraulicConductivities��$8��2Output block (effective) hydraulic conductivities?�� �� $(	 "OutputBlockHydraulicConductivities�$
 PropertyChanged=��� -   $C��=chkOutputArithmeticGeometricHarmonicHydraulicConductivityAvgs��$G ARFlow2DOutputArithmeticGeometricHarmonicHydraulicConductivityAvgs��$W��QOutput arithmetic, geometric, and harmonic hydraulic conductivity field averages?�� �� $@	 :OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs�$
 PropertyChanged=��� -   $)��#chkGenerateUniformConductivityField��$- 'RFlow2DGenerateUniformConductivityField��$*��$Generate uniform conductivity field?�� �� $&	  GenerateUniformConductivityField�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��lbNumberOfWalls��$ RFlow2DNumberOfWalls��$��Number of walls:���� -   $��cboNumberOfWalls��$ RFlow2DNumberOfWalls�� �� SelectedValue �� $	 NumberOfWalls�$
 PropertyChanged=� �� Mode$ TwoWay=�.@���0����1����2���� -   $��gbNumberOfElements������ -   $��lbNumberOfElements��$ RFlowNumberOfElements��$��Number of Elements��.���� $ Vertical=�.+��� $ 
Horizontal=�.+��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$ cboNumberOfWalls��$	 SelectedValue� �� Value 1.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -   $��txtNElementsInXDir��$ RFlowNumberOfElements������ $	 NElementsInXDir�$
 PropertyChanged=� . �� -   $��lbNElementsInXDir��$ RFlowNumberOfElements��$��in x-direction���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -   $��txtNElementsLeftOfWall��$ RFlowNumberOfElements������ $	 NElementsLeftOfWall�$
 PropertyChanged=� . �� -   $��lbNElementsLeftOfWall��$ RFlowNumberOfElements��$��to left of wall���� -   $��txtNElementsRightOfWall��$ RFlowNumberOfElements������ $	 NElementsRightOfWall�$
 PropertyChanged=� . �� -   $��lbNElementsRightOfWall��$ RFlowNumberOfElements��$��to right of wall���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 1.a���  � y�$ 	Collapsed=�.+��� -   $ ��txtNElementsLeftOfLeftWall��$ RFlowNumberOfElements������ $	 NElementsLeftOfLeftWall�$
 PropertyChanged=� . �� -   $��lbNElementsLeftOfLeftWall��$ RFlowNumberOfElements��$��to left of left wall���� -   $��txtNElementsBetweenWall��$ RFlowNumberOfElements������ $	 NElementsBetweenWall�$
 PropertyChanged=� . �� -   $��lbNElementsBetweenWall��$ RFlowNumberOfElements��$��between walls���� -   $'��!txtNElementsToTheRightOfRightWall��$ RFlowNumberOfElements������ $$	 NElementsToTheRightOfRightWall�$
 PropertyChanged=� . �� -   $&�� lbNElementsToTheRightOfRightWall��$ RFlowNumberOfElements��$��to right of right wall���� $ 
Horizontal=�.+��� -   $��txtNElementsInYDir��$ RFlowNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -    $��lbNElementsInYDir��$ RFlowNumberOfElements��$��in y-direction���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -!   $��txtDepthOfWall��$ RFlowNumberOfElements������ $	 DepthOfWall�$
 PropertyChanged=� . �� -"   $��lbDepthOfWall��$ RFlowNumberOfElements��$��depth of wall���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 1.a���  � y�$ 	Collapsed=�.+��� -#   $��txtDepthOfLeftWall��$ RFlowNumberOfElements������ $	 DepthOfLeftWall�$
 PropertyChanged=� . �� -$   $��lbDepthOfLeftWall��$ RFlowNumberOfElements��$��depth of left wall���� -%   $��txtDepthOfRightWall��$ RFlowNumberOfElements������ $	 DepthOfRightWall�$
 PropertyChanged=� . �� -&   $��lbDepthOfRightWall��$ RFlowNumberOfElements��$��depth of right wall���� $ 
Horizontal=�.+��� -'   $��lbElementDimensions��$ RFlow2DElementDimensions��$��Element dimensions:���� -(   $$��txtElementDimensionHorrizontal��$ RFlow2DElementDimensions������ $!	 ElementDimensionHorrizontal�$
 PropertyChanged=� .  �� 	Converter# [  �� -)   $#��lbElementDimensionHorrizontal��$��horizontally��$ RFlow2DElementDimensions���� -*   $!��txtElementDimensionVertical��$ RFlow2DElementDimensions������ $	 ElementDimensionVertical�$
 PropertyChanged=� . # [  �� -+   $ ��lbElementDimensionVertical��$��
vertically��$ RFlow2DElementDimensions���� $ 
Horizontal=�.+��� -,   $��lbNumberOfRealizations��$! RFlow2DNumberOfRealizations��$��Number of realizations:���� --   $��txtNumberOfRealizations��$! RFlow2DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -.   $��lbGeneratorSeed��$ RFlow2DGeneratorSeed��$��Generator seed:���� -/   $��txtGeneratorSeed��$ RFlow2DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -0   $��lbCorrelationLengths��$ RFlow2DCorrelationLengths��$��Correlation lengths:���� -1   $ ��txtCorrelationLengthInXDir��$ RFlow2DCorrelationLengths������ $	 CorrelationLengthInXDir�$
 PropertyChanged=� . �� -2   $��lbCorrelationLengthInXDir��$ RFlow2DCorrelationLengths��$��x-direction���� -3   $ ��txtCorrelationLengthInYDir��$ RFlow2DCorrelationLengths������ $	 CorrelationLengthInYDir�$
 PropertyChanged=� . �� -4   $��lbCorrelationLengthInYDir��$ RFlow2DCorrelationLengths��$��y-direction���� $ 
Horizontal=�.+��� -5   $��lbHydraulicConductivity��$" RFlow2DHydraulicConductivity��$��Hydraulic conductivity:���� -6   $"��txtHydraulicConductivityMean��$" RFlow2DHydraulicConductivity������ $	 HydraulicConductivityMean�$
 PropertyChanged=� . �� -7   $!��lbHydraulicConductivityMean��$" RFlow2DHydraulicConductivity��$
��mean���� -8   $$��txtHydraulicConductivityStdDev��$" RFlow2DHydraulicConductivity������ $!	 HydraulicConductivityStdDev�$
 PropertyChanged=� . �� -9   $#��lbHydraulicConductivityStdDev��$" RFlow2DHydraulicConductivity��$��standard deviation���� $ 
Horizontal=�.+��� -:   $��lbCovFunction��$ RFlow2DCovFunc��$��Covariance function:���� -;   $��cboCovFunction��$ RFlow2DCovFunc������  �� Source# [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=��� -<   $��lbNote��$  ��$q��kNote: the following are plot options for total head and flownet plots. The defaults are usually reasonable.���� $ 
Horizontal=�.+��� -=   $��lbNContoursForTotalHead��$" RFlow2DNContoursForTotalHead��$5��/Number of contours to use for total head plots:���� ->   $"��txtNContoursForMeanTotalHead��$" RFlow2DNContoursForTotalHead������ $	 NContoursForMeanTotalHead�$
 PropertyChanged=� . �� -?   $!��lbNContoursForMeanTotalHead��$" RFlow2DNContoursForTotalHead��$��for mean���� -@   $$��txtNContoursForStdDevTotalHead��$" RFlow2DNContoursForTotalHead������ $!	 NContoursForStdDevTotalHead�$
 PropertyChanged=� . �� -A   $#��lbNContoursForStdDevTotalHead��$" RFlow2DNContoursForTotalHead��$��for standard deviation���� $ 
Horizontal=�.+��� -B   $��lbNEquipotentialDrops��$  RFlow2DNEquipotentialDrops��$6��0Number of equipotential drops to use in flownet:���� -C   $��txtNEquipotentialDrops��$  RFlow2DNEquipotentialDrops������ $	 NEquipotentialDrops�$
 PropertyChanged=� . �� -D   $*��$chkShowLogConductivityFieldOnFlownet��$. (RFlow2DShowLogConductivityFieldOnFlownet��$1��+Show log-conductivity field on the flownet?�� �� $'	 !ShowLogConductivityFieldOnFlownet�$
 PropertyChanged=��� -E   $(��"chkShowSoilMassDimensionsOnFlownet��$, &RFlow2DShowSoilMassDimensionsOnFlownet��$/��)Show soil mass dimensions on the flownet?�� �� $%	 ShowSoilMassDimensionsOnFlownet�$
 PropertyChanged=��� -F   $��chkShowTitlesOnFlownet��$  RFlow2DShowTitlesOnFlownet��$+��%Show titles and subtitles on flownet?�� �� $	 ShowTitlesOnFlownet�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -G   $��lbFlownetWidth��$ RFlow2DFlownetWidth��$��Flownet width in inches:���� -H   $��txtFlownetWidth��$ RFlow2DFlownetWidth������ $	 FlownetWidth�$
 PropertyChanged=�
   M S B A M L   `   `   `  ���� X  TRFEMDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193db]3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms%RFEMSoftware.Simulation.Desktop.Forms  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem X TInfrastructureLib, Version=0.1.0.0, Culture=neutral, PublicKeyToken=ae0f515758d193dbzOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib&RFEMSoftware.Simulation.Infrastructure 7    1RFEMSoftware.Simulation.Desktop.Forms.RFlow3DForm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:RFEMSoftware.Simulation.Desktop.Forms     6sys&clr-namespace:System;assembly=mscorlib     ainfraOclr-namespace:RFEMSoftware.Simulation.Infrastructure;assembly=InfrastructureLib       � 	Resources   �� 
TargetType   HelpEnabled �� BasedOn  covFunction3DEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeA  ;RFEMSoftware.Simulation.Infrastructure.CovarianceFunction3D;   5RFEMSoftware.Simulation.Desktop.Forms.DoubleConverter  DoubleConverter��%�   '��       7   '��      7   '��      7   '	� -     7   '�� <     7   &	 K     &	 s     �� " ��8   �� " ��8   �� " ��8   �� " 	�8   �� " ��8   S� $ 	GetValues��"   M� "    .��� .+���  �� Orientation$ Vertical=�.+��� $ 
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
 PropertyChanged=��� -   $.��(chkOutputFlowRateExitGradientUpliftForce��$2 ,RFlow3DOutputFlowRateExitGradientUpliftForce��$S��MOutput flow rates, exit gradients (if walls), and uplift forces (if 2 walls)?�� �� $+	 %OutputFlowRateExitGradientUpliftForce�$
 PropertyChanged=��� -   $+��%chkOutputBlockHydraulicConductivities��$/ )RFlow3DOutputBlockHydraulicConductivities��$8��2Output block (effective) hydraulic conductivities?�� �� $(	 "OutputBlockHydraulicConductivities�$
 PropertyChanged=��� -	   $C��=chkOutputArithmeticGeometricHarmonicHydraulicConductivityAvgs��$G ARFlow3DOutputArithmeticGeometricHarmonicHydraulicConductivityAvgs��$W��QOutput arithmetic, geometric, and harmonic hydraulic conductivity field averages?�� �� $@	 :OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs�$
 PropertyChanged=��� -
   $)��#chkGenerateUniformConductivityField��$- 'RFlow3DGenerateUniformConductivityField��$*��$Generate uniform conductivity field?�� �� $&	  GenerateUniformConductivityField�$
 PropertyChanged=��� $ 
Horizontal=�.+��� -   $��lbNumberOfWalls��$ RFlow3DNumberOfWalls��$��Number of walls:���� -   $��cboNumberOfWalls��$ RFlow3DNumberOfWalls�� �� SelectedValue �� $	 NumberOfWalls�$
 PropertyChanged=� �� Mode$ TwoWay=�.@���0����1����2���� -   $��gbNumberOfElements��$ RFlow3DNumberOfElements������ -   $��lbNumberOfElements��$ RFlow3DNumberOfElements��$��Number of Elements��.���� $ Vertical=�.+��� $ 
Horizontal=�.+��� $ 
Horizontal=����� " ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding ��  �� ElementName$ cboNumberOfWalls��$	 SelectedValue� �� Value 1.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -   $��txtNElementsInXDir��$ RFlow3DNumberOfElements������ $	 NElementsInXDir�$
 PropertyChanged=� . �� -   $��lbNElementsInXDir��$ RFlow3DNumberOfElements��$��in x-direction���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -   $��txtNElementsLeftOfWall��$ RFlow3DNumberOfElements������ $	 NElementsLeftOfWall�$
 PropertyChanged=� . �� -   $��lbNElementsLeftOfWall��$ RFlow3DNumberOfElements��$��to left of wall���� -   $��txtNElementsRightOfWall��$ RFlow3DNumberOfElements������ $	 NElementsRightOfWall�$
 PropertyChanged=� . �� -   $��lbNElementsRightOfWall��$ RFlow3DNumberOfElements��$��to right of wall���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 1.a���  � y�$ 	Collapsed=�.+��� -   $ ��txtNElementsLeftOfLeftWall��$ RFlow3DNumberOfElements������ $	 NElementsLeftOfLeftWall�$
 PropertyChanged=� . �� -   $��lbNElementsLeftOfLeftWall��$ RFlow3DNumberOfElements��$��to left of left wall���� -   $��txtNElementsBetweenWalls��$ RFlow3DNumberOfElements������ $	 NElementsBetweenWalls�$
 PropertyChanged=� . �� -   $��lbNElementsBetweenWalls��$ RFlow3DNumberOfElements��$��between walls���� -   $"��txtNElementsRightOfRightWall��$ RFlow3DNumberOfElements������ $	 NElementsRightOfRightWall�$
 PropertyChanged=� . �� -   $!��lbNElementsRightOfRightWall��$ RFlow3DNumberOfElements��$��to right of right wall���� $ 
Horizontal=�.+��� -   $��txtNElementsInYDir��$ RFlow3DNumberOfElements������ $	 NElementsInYDir�$
 PropertyChanged=� . �� -   $��lbNElementsInYDir��$ RFlow3DNumberOfElements��$��in y-direction���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 2.a���  � y�$ 	Collapsed=�.+��� -   $��txtDepthOfWall��$ RFlow3DNumberOfElements������ $	 DepthOfWall�$
 PropertyChanged=� . �� -   $��lbDepthOfWall��$ RFlow3DNumberOfElements��$��depth of wall���� $ 
Horizontal=����� " ��.���  � y�$ Visible=� ��  �� $ cboNumberOfWalls��$	 SelectedValue� 0.a���  � y�$ 	Collapsed=���  �� $ cboNumberOfWalls��$	 SelectedValue� 1.a���  � y�$ 	Collapsed=�.+��� -   $��txtDepthOfLeftWall��$ RFlow3DNumberOfElements������ $	 DepthOfLeftWall�$
 PropertyChanged=� . �� -    $��lbDepthOfLeftWall��$ RFlow3DNumberOfElements��$��depth of left wall���� -!   $��txtDepthOfRightWall��$ RFlow3DNumberOfElements������ $	 DepthOfRightWall�$
 PropertyChanged=� . �� -"   $��lbDepthOfRightWall��$ RFlow3DNumberOfElements��$��depth of right wall���� $ 
Horizontal=�.+��� -#   $��txtNElementsInZDir��$ RFlow3DNumberOfElements������ $	 NElementsInZDir�$
 PropertyChanged=� . �� -$   $��lbNElementsInZDir��$ RFlow3DNumberOfElements��$��in z-direction���� $ 
Horizontal=�.+��� -%   $��lbElementDimensions��$ RFlow3DElementDimensions��$��Element dimensions:���� -&   $��txtElementDimensionXDir��$ RFlow3DElementDimensions������ $	 ElementDimensionXDir�$
 PropertyChanged=� .  �� 	Converter# [ �� -'   $��lbElementDimensionXDir��$ RFlow3DElementDimensions��$��in x-direction���� -(   $��txtElementDimensionYDir��$ RFlow3DElementDimensions������ $	 ElementDimensionYDir�$
 PropertyChanged=� . # [ �� -)   $��lbElementDimensionYDir��$ RFlow3DElementDimensions��$��in y-direction���� -*   $��txtElementDimensionZDir��$ RFlow3DElementDimensions������ $	 ElementDimensionZDir�$
 PropertyChanged=� . # [ �� -+   $��lbElementDimensionZDir��$ RFlow3DElementDimensions��$��in z-direction���� $ 
Horizontal=�.+��� -,   $��lbNumberOfRealizations��$! RFlow3DNumberOfRealizations��$��Number of realizations:���� --   $��txtNumberOfRealizations��$! RFlow3DNumberOfRealizations������ $	 NumberOfRealizations�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -.   $��lbGeneratorSeed��$ RFlow3DGeneratorSeed��$��Generator seed:���� -/   $��txtGeneratorSeed��$ RFlow3DGeneratorSeed������ $	 GeneratorSeed�$
 PropertyChanged=� . �� $ 
Horizontal=�.+��� -0   $��lbCorrelationLengths��$ RFlow3DCorrelationLengths��$��Correlation lengths:���� -1   $��txtCorrelationLengthXDir��$ RFlow3DCorrelationLengths������ $	 CorrelationLengthXDir�$
 PropertyChanged=� . �� -2   $��lbCorrelationLengthXDir��$ RFlow3DCorrelationLengths��$��x-direction���� -3   $��txtCorrelationLengthYDir��$ RFlow3DCorrelationLengths������ $	 CorrelationLengthYDir�$
 PropertyChanged=� . �� -4   $��lbCorrelationLengthYDir��$ RFlow3DCorrelationLengths��$��y-direction���� -5   $��txtCorrelationLenghtZDir��$ RFlow3DCorrelationLengths������ $	 CorrelationLenghtZDir�$
 PropertyChanged=� . �� -6   $��lbCorrelationLenghtZDir��$ RFlow3DCorrelationLengths��$��z-direction���� $ 
Horizontal=�.+��� -7   $��lbHydraulicConductivity��$" RFlow3DHydraulicConductivity��$��Hydraulic conductivity:���� -8   $"��txtHydraulicConductivityMean��$" RFlow3DHydraulicConductivity������ $	 HydraulicConductivityMean�$
 PropertyChanged=� . �� -9   $!��lbHydraulicConductivityMean��$" RFlow3DHydraulicConductivity��$
��mean���� -:   $$��txtHydraulicConductivityStdDev��$" RFlow3DHydraulicConductivity������ $!	 HydraulicConductivityStdDev�$
 PropertyChanged=� . �� -;   $#��lbHydraulicConductivityStdDev��$" RFlow3DHydraulicConductivity��$��standard deviation���� $ 
Horizontal=�.+��� -<   $��	lbCovFunc��$ RFlow3DCovFunc��$��Covariance function:���� -=   $��
cboCovFunc��$ RFlow3DCovFunc������  �� Source# [  �� $	 CovFunc�$
 PropertyChanged=�$ TwoWay=�
   M S B A M L   `   `   `  ����H  DTestingMasuka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TestingMasukaTestingMasuka  5   	   S OManufaktura.Controls.WPF, Version=1.4.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Manufaktura.Controls.WPF;assembly=Manufaktura.Controls.WPFManufaktura.Controls.WPF 5   	   W SWPFSoundVisualizationLibNet5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulliLclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLibNet5WPFSoundVisualizationLib 5   	   U QScottPlot.WPF, Version=5.0.40.0, Culture=neutral, PublicKeyToken=86698dc10387c39eD2clr-namespace:ScottPlot.WPF;assembly=ScottPlot.WPFScottPlot.WPF 5	   	       TestingMasuka.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TestingMasuka     5   	   hManufakturaControlsHclr-namespace:Manufaktura.Controls.WPF;assembly=Manufaktura.Controls.WPF     5   	   ^musicLclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLibNet5     5   	   H	scottPlot2clr-namespace:ScottPlot.WPF;assembly=ScottPlot.WPF     5	   	     � Title$  
MainWindow��5   	   $
��1000��6    � WindowStartupLocation$ CenterScreen=�6*    � WindowState$ 	Maximized=�6O   $
��1258��6g   ���	#FFAEC4B5.��5      �  � ColumnDefinitions 5   
   �� 5      $	��91* �6   �� 5      $
��428* �6   �� 5      $
��601* �6   �� 5      $	��35* �6   �� 5      $
��104* �6   .+�5   
   �� $��4��6   $��1��6*   .+�5      o� $
��1304��6   .@�5      m� -   5         m�Name$ 	menu_file��5      $
��File��6.   .@�5      m� -   5        $
��Open��5      6-   m� 5      -   5        $
��Save��5      6-   �� 5      5      m� 5      -   5        $
��Edit��5      .@�5      m� -   5        $
��Undo��5      6-   m� 5      -   5        $
��Redo��5      6-   5      m� 5      -   5        $��Export to PDF��5      62   m� 5       -   5        $
��Exit��5       6)   m� 5!      -	   5        $ btnHelp��5!      $
��Help��6,   .@�5"      m� $��About��6   5#      5$      5'      �� 5*   
   $��	0,272,0,0q�6   $��5��6(    J� RenderTransformOrigin$ 0.494,0.775&�6<   .+�5+      )  #Manufaktura.Controls.WPF.NoteViewer  -
   5          Name$ NoteViewer1��5+   -   $ 0.5,0.5&�6`   $	��708��6�   $
��1254��6�      ScoreSource 6B   �� *Data+z�5,      V� 5-      .~�5.      ��  �� ScaleY$ 1Q�6)   �� 5/      �� 50      T� 51      52      54        ScottPlot.WPF.WpfPlot  56      -   5        	  Name$	 wpfPlot��56       $	��100��61   58      � 59   
   -   5        
 �Name$
 LoadingProgressBar��59       � Minimum$ 0Q�5:       � Maximum$	 100Q�5;      $��20��5<      $	��300��5=      $��1��6   $��393,145,335,819q�6)   $��2��6B   �� 5?   
   -   5         ��Name$ btnStopPlayingAudio��5?      $��2��6.   $��Stop Playing Audio��6>   $
��Left=�6[   $��195,198,0,0q�6v   $	��Top=�6�   6�   �� 5@   
   -   5        $ 	btnRecord��5@      $��Record��6$   $	��166��6M   $��36��6Y   $��2��6e   $��32,85,402,863q�6u   �� 5A   
   -   5        $ btnLoadFile��5A      $��1��6&   $��	Load File��66   $
��Left=�6J   $��376,198,0,0q�6e   $	��Top=�6z   $��2��6�   �� 5B   
   -   5         ��Name$ txtFileSelected��5B      $��2��6+   $
��Left=�6;   $��
50,204,0,0q�6V    �� TextWrapping$
 Wrap=�6j   $	��Top=�6~   $	��120��6�   �� 5D   
   -   5         ��Name$ 	imgBanner��5D      $����C:\Users\treya\OneDrive\Documents\GitHub\CM465Capstone\Trey\TestingMasuka (2)\TestingMasuka\TestingMasuka\TestingMasuka\BannerCapstone.jpg��6#   $��0��5E      $	��Top=�5F      $��UniformToFill=�5G      $��38��5H      $��	0,19,-4,0q�5I      $��5��6#   �� 5J   
   -   5        $ imgLogo��5J      $��2��6!   $
��Left=�61   $	��269��6L   $��
416,58,0,0q�6Y   $	��Top=�6m   $	��219��6�   $����C:\Users\treya\OneDrive\Documents\GitHub\CM465Capstone\Trey\TestingMasuka (2)\TestingMasuka\TestingMasuka\TestingMasuka\pngAudioTranscriber.png��6�   $��2��6*  5M      5N      
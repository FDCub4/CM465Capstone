   M S B A M L   `   `   `  ����H  DTestingMasuka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TestingMasukaTestingMasuka  5   	   S OManufaktura.Controls.WPF, Version=1.4.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Manufaktura.Controls.WPF;assembly=Manufaktura.Controls.WPFManufaktura.Controls.WPF 5   	   W SWPFSoundVisualizationLibNet5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulliLclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLibNet5WPFSoundVisualizationLib 5   	       TestingMasuka.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TestingMasuka     5   	   hManufakturaControlsHclr-namespace:Manufaktura.Controls.WPF;assembly=Manufaktura.Controls.WPF     5   	   ^musicLclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLibNet5     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��58* �6   �� 5      $
��743* �6   .+�5   
   �� $��
10,0,0,372q�6   $��2��6)   .+�5      o� $	��Top=�6   .@�5      m� -   5         m�Name$ 	menu_file��5      $
��File��6.   .@�5      m� -   5        $
��Open��5      6-   m� 5      -   5        $
��Save��5      6-   �� 5      5      m� 5      -   5        $
��Edit��5      .@�5      m� -   5        $
��Undo��5      6-   m� 5      -   5        $
��Redo��5      6-   5      m� 5      -   5        $��Export to PDF��5      62   m� 5      -   5        $
��Exit��5      6)   m� 5      -	   5        $ btnHelp��5      $
��Help��6,   .@�5      m� $��About��6   5      5       �� 5!      -
   5         ��Name$ 	btnRecord��5!      $��Record��6(   $	��166��6Q   $��36��6]   5"      �� 5%   
   $��	0,272,0,0q�6   $��2��6(   .+�5'      )  #Manufaktura.Controls.WPF.NoteViewer  -   5          Name$ noteViewer1��5'   -   $��Stretch=�6B   $��	10,10,0,0q�6`   $	��Top=�6s      ScoreSource 6�   �� *Data+5)      �� 5*   
   -   5        $ btnLoadFile��5*      $��1��6&   $��	Load File��66   $
��Left=�6J   $��145,136,0,0q�6e   $	��Top=�6z   6�   �� 5+   
   -   5         ��Name$ txtFileSelected��5+      $��1��6+   $
��Left=�6;   $��287,136,0,0q�6V    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $	��120��6�   6�   5,      5-      
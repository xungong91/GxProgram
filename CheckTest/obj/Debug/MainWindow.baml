   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   
    	CheckTest   CheckTest.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   
     � Title$  
MainWindow��5   	   $	��600��6   $
��1000��6)    � WindowStartupLocation$ CenterScreen=�66    � 	Resources 5       �� MergedDictionaries �� Source   DescriptionHeaderStyle �� 
TargetType �� Property �� Value�� 5   
    5      �� 5	      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5
      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatSlider.xaml?�6%   %z   &	        �� 5      " ��63   .�5      ��  � ��$ 22(�6-   �� 5       � ��$ Center=�68   5      5      (   "MahApps.Metro.Controls.MetroWindow   	GlowBrush 5      �� 5   
     AccentColor	 �� Color#	 �  6   .��5      � 
 � RowDefinitions
 5   
   �� 5      $	��16* �6   �� 5      $
��269* �6   .+�5    
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5!      �� $	��400��6d   ��6   �� *TxtCondition+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6d   �� 5"       �� 	IsDefault . $��提交��6&   $	��100��63   ��6?   �� *CommandSubmit+�� 5#      $��审核公式��6   $	��100��6$   ��60   �� *CommandFormula+�� 5$      $��计算公式��6   $	��100��6$   ��60   �� *CommandCalculate+5%      =   7MahApps.Metro.Controls.MetroAnimatedSingleRowTabControl  5&   
   $��1��64   .@�5'      �� $
��表1��6   .��5(      �� .��5)      �� $��Center=�6)   ��6F   �� *Table1+5*      5+      �� 5,      $
��表2��6   5-      5.      5/      50      
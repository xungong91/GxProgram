   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   
    RCCSever   RCCSever.WindVideo   5      HControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   
   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Icon$  mahapps.metro.logo2.ico��5   	   (   "MahApps.Metro.Controls.MetroWindow   ShowIconOnTitleBar .    ShowTitleBar .  � WindowStartupLocation$ CenterScreen=�5       � Title$ 视频��6B   $	��500��6M   $	��500��6Z    � 	Resources 5       �� MergedDictionaries �� Source   DescriptionHeaderStyle �� 
TargetType	 �� Property
 �� Value�� 5	   
    5
      �� 5      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatSlider.xaml?�6%   %z   &	        �� 5      " ��63   .�5      �� 	 � ��$
 22(�6-   �� 5      	 � ��$
 Center=�68   5      5         	GlowBrush 5      �� 5   
     AccentColor �� Color# �  6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5       $��* �6   .+�5"   
   �� .+�5#      �� -   5        $��Fromimg��5#      $	��200��6#   �� 5$       �� Orientation$ 
Horizontal=�6   .+�5%      �� -   5        $��iptxt��5%   /   $��192.168.1.104��6   6/   �� 5&      -   5        $��porttxt��5&   &   $
��8080��6   6&   �� 5'      -   5        $��连接��5'      6&   �� 5(      -   5        $��视频��5(      6&   �� 5)      -   5        $��语音��5)      6&   5*      �� 5+      -   5        $��Toimg��5+      $	��200��6!   5,      �� 5-   
   $��1��6   .+�5.      �� -	   5        $��
txtReceive��5.      $
��Wrap=�6*   $	��200��6>   �� 5/      -
   5        $��发送��5/      6"   �� 50      -   5        $��txtsend��50       �� TextWrapping$
 Wrap=�6%   $	��200��69   51      52      53      
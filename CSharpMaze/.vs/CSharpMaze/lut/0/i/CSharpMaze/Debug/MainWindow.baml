   M S B A M L   `   `   `  ����E  ACSharpMaze, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CSharpMaze
CSharpMaze  5   	       CSharpMaze.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   J FWpfAnimatedGif, Version=1.4.15.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5      $localclr-namespace:CSharpMaze  5   	   -   5        
���   �"  WpfAnimatedGif.ImageBehavior    AnimateInDesignMode  .  � 
ResizeMode$ NoResize=�5   	    � Title$  ��5   	   $	��447��6   $	��510��6    � WindowStartupLocation$ CenterScreen=�6S    J� InputBindings 5&      �� 5'   
      CSharpMaze.Commands   Save �� Command# Z 6    �� 	Modifiers$ Controlf�6>   
 �� Key$ S��6R   �� 5(   
   	   Game# Z	 6   $ Controlf�6>   $ G��6R   �� 5)   
   
   Load# Z
 6   $ Controlf�6>   $ L��6R   �� 5*   
      Exit# Z 6   $ Controlf�6>   $ X��6R   �� 5+   
      About# Z 6   $ Controlf�6?   $ A��6S   �� 5,   
   
   How# Z 6   $ Controlf�6=   $ H��6Q   �� 5-   
      Settings# Z 6   $ Controlf�6B   $ T��6V    J� CommandBindings 5/      )  #System.Windows.Input.CommandBinding  50   
   -   5           Command# Z 50      6_     51   
   -   5        # Z	 51      6_     52   
   -   5        # Z
 52      6_     53   
   -   5        # Z 53      6]     54   
   -   5        # Z 54      6_     55   
   -   5        # Z 55      6a     56   
   -   5        # Z 56      6g   .��58      �� $��Stretch=�6   $��0,0,0,14q�6-   .+�59   
   o� .@�5:      m� $��_File��6   $��30��6&   .@�5;      m� -	   5        $��mitmFile_Game��5;   .   $��	_New Game��6    m� InputGestureText$ Ctrl+G��6C    m� Command# Z	 6]   6�   m� 5<      -
   5        $��mitmFile_Save��5<   /   $��
_Save Game��6   $ Ctrl+S��6D   # Z 6^   6�   m� 5=      -   5        $��mitmFile_Load��5=   /   $��
_Load Game��6   $ Ctrl+L��6D   # Z
 6^   6�   m� 5>      -   5        $��mitmFile_Exit��5>   *   $��_Exit��6   $ Ctrl+X��6?   # Z 6Y   6�   5?      m� 5@      $��_Help��6   $��35��6&   .@�5A      m� -   5        $��mitmFile_Settings��5A   .   $��	_Settings��6   $ Ctrl+T��6G   # Z 6a   6�   m� 5B      -   5        $��mitmFile_About��5B   +   $��_About��6   $ Ctrl+A��6A   # Z 6[   6�   m� 5C      -   5        $��mitmFile_Instructions��5C   <   $��_Game Play Instructions��6   $ Ctrl+G��6Y   # Z 6s   6�   5D      5E      � 5F   
   $	��250��6    � ColumnDefinitions 5G      �� 5H      $��* �6#   �� 5I      $��* �6#   .+�5K      �� -   5        $��
PlayerRoom��5K   .   $	��250��6   $	��250��6"   $��0��6@   $��	0,0,0,119q�6P   ��5L      �� 5M       �� ImageSource$ dungeonFloor.jpg��6!   .+�5O      �� -   5         ��Name$ Player��5O   K      AnimatedSource$ export_move.gif��6   $��71��6[    �� Left$ 83��6g   
 �� Top$ 80��6x   $��80��6�   }�.  J� RenderTransformOrigin$ 0.5,0.5&�6�      	AutoStart .  6�   5P      � 5Q      -   5        $��MiniMap��5Q   #   $��1��6   $��20 20 20 20q�62    5R      �� 5S      $��* �6'   �� 5T      $��* �6'   �� 5U      $��* �6'   �� 5V      $��* �6'   �� 5W      $��* �6'    � RowDefinitions 5Y      �� 5Z      $��* �6$   �� 5[      $��* �6$   �� 5\      $��* �6$   �� 5]      $��* �6$   �� 5^      $��* �6$   .+�5a         CSharpMaze.MiniMapRoom  $��0��6$   $��0��64      Door1$ Hidden=�6A      Door2$ Hidden=�6P     5b      $��1��6$   $��0��64   $ Hidden=�6A     5c      $��2��6$   $��0��64   $ Hidden=�6A     5d      $��3��6$   $��0��64   $ Hidden=�6A     5e      $��4��6$   $��0��64   $ Hidden=�6A      Door4$ Hidden=�6P     5h      $��0��6$   $��1��64   $ Hidden=�6A     5i      $��1��6$   $��1��64     5j      $��2��6$   $��1��64     5k      $��3��6$   $��1��64     5l      $��4��6$   $��1��64   $ Hidden=�6A     5o      $��0��6$   $��2��64   $ Hidden=�6A     5p      $��1��6$   $��2��64     5q      $��2��6$   $��2��64     5r      $��3��6$   $��2��64     5s      $��4��6$   $��2��64   $ Hidden=�6A     5v      $��0��6$   $��3��64   $ Hidden=�6A     5w      $��1��6$   $��3��64     5x      $��2��6$   $��3��64     5y      $��3��6$   $��3��64     5z      $��4��6$   $��3��64   $ Hidden=�6A     5}      $��0��6$   $��4��64   $ Hidden=�6A      Door3$ Hidden=�6P     5~      $��1��6$   $��4��64   $ Hidden=�6A     5      $��2��6$   $��4��64   $ Hidden=�6A     5�      $��3��6$   $��4��64   $ Hidden=�6A     5�      $��4��6$   $��4��64   $ Hidden=�6A   $ Hidden=�6P   �� 5�      $��4��6   $��4��6(   $��coin.png��65   $��25��6G   � 5�      -   5        $
��star��5�   �   
��� ���
���   �$��1��66     �� StrokeLineJoin$  Round=�6J   $��25��6a   $��25��6l   ! �� Stretch$
! Fill=�6x   " � Pointsi" �
   ��� �� ��� ��j @f��j  � ��� ��� ��� @T�  �  	= ��� ��[ ��� ��j ��j ��j $��0��6�   $��0��6�   5�      5�      � 5�   
   $	��147��6   # � 	Resources# 5�      $ �� 
TargetType% �� Property& �� Value��%  '��       '�� �     '�� '    '�� ;    '	�     �� 5�      "$ ��6   .�5�      �� % � ��
& �  ��63   �� 5�      % � ��$& 13(�61   �� 5�      % � ��$& Arial)�63   5�      �� 5�      "$ ��6   .�5�      �� % � ��
& ��  �63   �� 5�      % � ��$	& 700��6.   �� 5�      % � ��$& 0q�68   5�      �� 5�      "$ ��6   .�5�      �� % � ��$
& Wrap=�65   �� 5�      % � ��
& �����63   �� 5�      % � ��$	& 400��6.   �� 5�      % � ��$& Arial)�63   �� 5�      % � ��$& 0, -12, -259, 47q�6/   �� 5�      % � ��$
& Bold"�63   5�      �� 5�      "$ ��6   .�5�      �� % � ��$& 70��6.   �� 5�      % � ��$& 22��6/   �� 5�      % � ��
& � ���63   �� 5�      % � ��
& �����63   �� 5�      % � ��$
& Bold"�63   5�      �� 5�      "$ 	�6   .�5�      �� % � ��$& 70��6.   �� 5�      % � ��$& 22��6/   �� 5�      % � ��$& Arial)�63   �� 5�      % � ��
& �����63   5�      5�      .+�5�      � $	��147��6   $��0,0,-1,0q�6!   $	��505��63   .+�5�      �� $��Stretch=�6   $��	0,12,0,-1q�69   ��5�      �� 5�      $ QuesAnsBackground12.jpg��6%   5�      �� 5�      -   5        ' ��Name$' gbMCQues��5�      $
��Left=�6-   $	��123��6H   $��10,4,0,0q�6U   $	��Top=�6g   $	��440��6   $y�Hidden=�6�   .��5�      � -   5        ( �Name$( griMC��5�      $��0,0,-39,-22q�6*   .+�5�      �� -   5        ) ��Name$) 	txbMCQues��5�   $   $��
0,20,10,89q�67   $
��Wrap=�6K   $
��Auto��6_   
Question6v   	� 5�      -   5        * 	�Name$* rdMCAns1��5�   &   + 	� 	GroupName$+ MC��68   $
��Ans1��6G   $��21,56,268,61q�6V   $
��Auto��6l   	� 5�      -   5        $* rdMCAns2��5�   &   $+ MC��68   $
��Ans2��6G   $��209,55,0,62q�6V   $
��Left=�6k   $	��200��6�   	� 5�      -   5        $* rdMCAns3��5�   &   $+ MC��68   $
��Ans3��6G   $��21,82,268,35q�6V   $
��Auto��6l   	� 5�      -   5        $* rdMCAns4��5�   &   $+ MC��68   $
��Ans4��6G   $��209,80,58,37q�6V   $
��Auto��6l   �� 5�      -   5        , ��Name$, btnMC_OK��5�   [   $��376,44,16,73q�6!   $ 1.787,1.952&�67   $��OK��6m   $
��Auto��6�   $
��Auto��6�   5�      5�      �� 5�      -   5        $' gbTFQues��5�      $
��Left=�6-   $	��108��6H   $��10,4,0,0q�6U   $	��Top=�6g   $	��408��6   $y�Hidden=�6�   .��5�      � -   5        $( griTF��5�      $��	0,0,0,-12q�6*   .+�5�      �� -   5        $) 	txbTFQues��5�   $   $��
0,13,56,56q�68   $
��Auto��6L   
Question6c   	� 5�      -   5        $* rdTFAns1��5�   &   $+ TF��68   $
��Ans1��6G   $��50,62,276,20q�6V   	� 5�      -    5        $* rdTFAns2��5�   &   $+ TF��68   $
��Ans2��6G   $��155,62,0,20q�6V   $
��Left=�6k   $	��109��6�   �� 5�      -!   5        $, btnTF_OK��5�   [   $��276,63,56,29q�6!   $ 1.787,1.952&�67   $��OK��6m   $
��Auto��6�   $
��Auto��6�   6�   5�      5�      �� 5�      -"   5        $' gbSAQues��5�      $
��Left=�6-   $��	10,12,0,0q�6H   $	��420��6[   $	��123��6g   $	��Top=�6t   $y�Hidden=�6�   .��5�      � -#   5        $( griSA��5�      $��
12,32,53,9q�6*    5�      �� 5�      $	��14* �6/   �� 5�      $
��Auto �6/   �� 5�      $	��18* �6/   �� 5�      $��5* �6/   �� 5�      $
��126* �6/   .+�5�      �� -$   5        $) 	txbSAQues��5�   $   $��0,-12,-119,47q�67   $��5��6N   $
��Auto��6b   
Question6y   �� 5�      -%   5        - ��Name$- txtSA��5�   "   $��	3,26,0,30q�61   $
��Left=�6D   $	��150��6_   $
��Auto��6k   $��5��6y   6�   �� 5�      -&   5        $, btnSA_OK��5�   !   $��OK��63   $
��Left=�6@   $��	90,0,0,30q�6[   $��Bottom=�6n   $��65��6�   $��20��6�   $��4��6�   $ 0.419,-1&�6�   6	  5�      5�      �� 5�      $��Bottom=�6   $��
0,112,0,14q�64   
������ .@�5�      �� $	��500��6$   $��0,-4,0,4q�60   .��5�      �� -'   5        $��lblCursorPosition��5�   $   $��21��6=   $��0q�6I   $�� ��6T   $	��490��6\   5�      5�      5�      5�      5�      5�      